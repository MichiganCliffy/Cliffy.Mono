using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI.HtmlControls;
using FlickrNet;

/// <summary>
/// TODO: Put Description here.
/// </summary>
public class FlickrImage : FlickrBase, ICloneable
{
    protected string mIdentifier;
    protected string mSecretID;
    protected string mServerID;
    protected string mTitle;
    protected string mDescription;
    protected string mOwner;
    protected NameValueCollection mUrls = new NameValueCollection();
    protected ArrayList mTags = new ArrayList();
    protected DateTime mDateAdded = DateTime.MinValue;
    protected DateTime mDateTaken = DateTime.MinValue;

    public string Identifier
    {
        get { return this.mIdentifier; }
        set { this.mIdentifier = value; }
    }

    public string SecretID
    {
        get { return this.mSecretID; }
        set { this.mSecretID = value; }
    }

    public string ServerID
    {
        get { return this.mServerID; }
        set { this.mServerID = value; }
    }

    public string Title
    {
        get { return this.mTitle; }
        set { this.mTitle = value; }
    }

    public string Description
    {
        get { return this.mDescription; }
        set { this.mDescription = value; }
    }

    public NameValueCollection Urls
    {
        get { return this.mUrls; }
    }

    public string[] Tags
    {
        get { return (string[])this.mTags.ToArray(typeof(string)); }
    }

    public DateTime DateAdded
    {
        get { return this.mDateAdded; }
        set { this.mDateAdded = value; }
    }

    public DateTime DateTaken
    {
        get { return this.mDateTaken; }
        set { this.mDateTaken = value; }
    }

    public DateTime DateSort
    {
        get
        {
            if (this.mDateTaken > DateTime.MinValue)
                return this.mDateTaken;
            else
                return this.mDateAdded;
        }
    }

    public string Owner
    {
        get { return this.mOwner; }
        set { this.mOwner = value; }
    }

    public FlickrImage()
    {
    }

    public FlickrImage(string identifier, string secretID, string serverID)
        : this()
    {
        this.mIdentifier = identifier;
        this.mSecretID = secretID;
        this.mServerID = serverID;
    }

    public object Clone()
    {
        return new FlickrImage(this.mIdentifier, this.mSecretID, this.mServerID);
    }

    public static FlickrImage Parse(FlickrNet.Photo photo)
    {
        FlickrImage image = new FlickrImage(photo.PhotoId, photo.Secret, photo.Server);
        image.Title = photo.Title;
        image.Urls.Add("Thumbnail", photo.SquareThumbnailUrl);
        image.Urls.Add("Small", photo.SmallUrl);
        image.Urls.Add("Medium", photo.MediumUrl);
        image.Urls.Add("Large", photo.LargeUrl);
        image.Urls.Add("Original", photo.OriginalUrl);
        image.DateAdded = photo.DateUploaded;
        image.DateTaken = photo.DateTaken;

        string[] tags = photo.CleanTags.Split(' ');
        foreach (string tag in tags)
        {
            image.AddTag(tag);
        }

        image.Owner = image.Owner;

        return image;
    }

    public void Load()
    {
        Flickr flickr = new Flickr(this.mCredentials.ApiKey);
        PhotoInfo photo = flickr.PhotosGetInfo(this.mIdentifier, this.mSecretID);
        this.mServerID = photo.Server.ToString();
        this.mTitle = photo.Title;
        this.mDescription = photo.Description.Replace("\n", "<br />");

        this.mUrls.Add("Thumbnail", photo.SquareThumbnailUrl);
        this.mUrls.Add("Small", photo.SmallUrl);
        this.mUrls.Add("Medium", photo.MediumUrl);
        this.mUrls.Add("Large", photo.LargeUrl);
        this.mUrls.Add("Original", photo.WebUrl);

        this.mDateAdded = photo.DateUploaded;
        this.mDateTaken = photo.Dates.TakenDate;

        this.mOwner = photo.Owner.RealName;

        PhotoInfoTag[] tags = photo.Tags.TagCollection;
        if (tags != null)
        {
            foreach (PhotoInfoTag tag in tags)
            {
                this.AddTag(tag.TagText);
            }
        }
    }

    public void Load(string identifier, string secretId)
    {
        this.mIdentifier = identifier;
        this.mSecretID = secretId;
        this.Load();
    }

    public void AddTag(string tag)
    {
        if (!this.mTags.Contains(tag))
            this.mTags.Add(tag);
    }

    public bool HasTag(string tag)
    {
        return this.mTags.Contains(tag);
    }
}

public class FlickrImageDateSort : IComparer, IComparer<FlickrImage>
{
    private int mSortDirection = 1;

    public FlickrImageDateSort() : base() { }

    public FlickrImageDateSort(SortDirection direction)
    {
        this.mSortDirection = (int)direction;
    }

    int IComparer.Compare(object x, object y)
    {
        FlickrImage imageX = (FlickrImage)x;
        FlickrImage imageY = (FlickrImage)y;

        if ((imageX == null) && (imageY == null))
        {
            return 0;
        }
        else if ((imageX == null) && (imageY != null))
        {
            return (this.mSortDirection == 0) ? -1 : 1;
        }
        else if ((imageX != null) && (imageY == null))
        {
            return (this.mSortDirection == 0) ? 1 : -1;
        }
        else
        {
            return (this.mSortDirection == 0) ?
                imageX.DateSort.CompareTo(imageY.DateSort) :
                imageY.DateSort.CompareTo(imageX.DateSort);
        }
    }

    #region IComparer<FlickrImage> Members

    public int Compare(FlickrImage x, FlickrImage y)
    {
        return (this.mSortDirection == 0) ?
                x.DateSort.CompareTo(y.DateSort) :
                y.DateSort.CompareTo(x.DateSort);
    }

    #endregion
}
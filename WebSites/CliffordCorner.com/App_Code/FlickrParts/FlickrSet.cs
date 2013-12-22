using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FlickrNet;

/// <summary>
/// Methods for Returning all images associated with a PhotoSet.
/// </summary>
public class FlickrSet : FlickrBase
{
    protected string mPhotoSetId = string.Empty;
    protected List<FlickrImage> mImages = new List<FlickrImage>();
    protected long mNumOfImages = long.MinValue;
    protected List<string> mTags = new List<string>();
    protected string mDescription = string.Empty;
    protected string mTitle = string.Empty;
    protected FlickrImage mDefaultImage = null;
    protected int mPhotoCount = int.MinValue;

    public string[] Tags
    {
        get { return this.mTags.ToArray(); }
    }

    public long NumberOfImages
    {
        get { return this.mImages.Count; }
    }

    public FlickrImage[] Images
    {
        get { return this.mImages.ToArray(); }
    }

    public virtual string Title
    {
        get { return this.mTitle; }
        set { this.mTitle = value; }
    }

    public virtual string Description
    {
        get { return this.mDescription; }
        set { this.mDescription = value; }
    }

    public virtual FlickrImage DefaultImage
    {
        get { return this.mDefaultImage; }
        set { this.mDefaultImage = value; }
    }

    public virtual int PhotoCount
    {
        get { return this.mPhotoCount; }
        set { this.mPhotoCount = value; }
    }

    public virtual string PhotoSetId
    {
        get { return this.mPhotoSetId; }
        set { this.Load(value); }
    }

    public void Load(string photosetId)
    {
        this.Load(photosetId, string.Empty);
    }

    public void Load(string photosetId, string setTag)
    {
        Debug.WriteLine("Flickr: Load Started");

        this.mPhotoSetId = photosetId;
        this.ClearImages();

        Debug.WriteLine("Flickr: Instantiation Object");
        Flickr conn = new Flickr(this.mCredentials.ApiKey);

        Debug.WriteLine("Flickr: Getting Set Information");
        Photoset setInfo = conn.PhotosetsGetInfo(photosetId);

        this.mTitle = setInfo.Title;
        this.mDescription = setInfo.Description.Replace("\n", "<br />");

        this.mDefaultImage = new FlickrImage();
        this.mDefaultImage.Load(setInfo.PrimaryPhotoId, setInfo.Secret);

        if (setTag.Length > 0)
            this.LoadPhotos(photosetId, setTag);
    }

    public void LoadPhotos(string photosetId, string setTag)
    {
        Debug.WriteLine("Flickr: Start LoadPhotos");
        Flickr conn = new Flickr(this.mCredentials.ApiKey);

        Debug.WriteLine("Flickr: Get Photos");
        Photoset photos = conn.PhotosetsGetPhotos(photosetId, PhotoSearchExtras.All, PrivacyFilter.None);

        int counter = 0;
        int endCount = photos.NumberOfPhotos;
        if (this.mPhotoCount != int.MinValue)
            endCount = this.mPhotoCount;

        foreach (FlickrNet.Photo photo in photos.PhotoCollection)
        {

            string[] tags = photo.CleanTags.Split(' ');
            foreach (string tag in tags)
            {
                if (!this.mTags.Contains(tag))
                    this.mTags.Add(tag);
            }

            Debug.WriteLine("Flickr: Parsing Photos");
            if (setTag.Length > 0)
            {
                //					foreach(FlickrNet.PhotoInfoTag tag in tags) 
                foreach (string tag in tags)
                {
                    //						if (string.Compare(setTag, tag.TagText, true) == 0) 
                    if (string.Compare(setTag, tag, true) == 0)
                    {
                        this.mImages.Add(FlickrImage.Parse(photo));
                        counter++;
                        break;
                    }
                }
            }
            else
            {
                this.mImages.Add(FlickrImage.Parse(photo));
                counter++;
            }
            if (counter >= endCount)
                break;
        }
    }

    public FlickrImage Get(string identifier)
    {
        foreach (FlickrImage image in this.mImages)
        {
            if (string.Compare(image.Identifier, identifier, true) == 0)
            {
                return image;
            }
        }

        return null;
    }

    private void ClearImages()
    {
        while (this.mImages.Count > 0)
        {
            FlickrImage image = this.mImages[0];
            image = null;
            this.mImages.RemoveAt(0);
        }

        this.mImages.Clear();
    }
}
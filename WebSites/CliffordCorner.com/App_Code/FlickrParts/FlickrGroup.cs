using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FlickrNet;

/// <summary>
/// TODO: Need description here.
/// </summary>
public class FlickrGroup : FlickrBase
{
    protected List<FlickrImage> mImages = new List<FlickrImage>();
    protected int mCurrentPage = 1;
    protected long mNumOfPages = long.MinValue;
    protected long mNumOfImages = long.MinValue;
    protected List<string> mTags = new List<string>();

    public int CurrentPage
    {
        get { return this.mCurrentPage; }
        set { this.mCurrentPage = value; }
    }

    public string[] Tags
    {
        get { return this.mTags.ToArray(); }
    }

    public long NumberOfPages
    {
        get { return this.mNumOfPages; }
    }

    public long NumberOfImages
    {
        get { return this.mNumOfImages; }
    }

    public FlickrImage[] Images
    {
        get { return this.mImages.ToArray(); }
    }

    public void Load(string groupID)
    {
        this.Load(groupID, new string[] {});
    }

    public void Load(string groupID, string groupTag)
    {
        if (!string.IsNullOrWhiteSpace(groupTag))
        {
            this.Load(groupID, new string[] { groupTag });
        }
        else
        {
            this.Load(groupID, new string[] { });
        }
    }

    public void Load(string groupID, string[] groupTags)
    {
        Debug.WriteLine("Flickr: Loading Flickr Group");
        this.ClearImages();

        StringBuilder tagString = new StringBuilder();
        foreach (string groupTag in groupTags)
        {
            if (tagString.Length > 0) tagString.Append(" ");
            tagString.Append(groupTag);
        }

        Debug.WriteLine("Flickr: Instantiating Flickr API");
        FlickrNet.Flickr conn = new Flickr(this.mCredentials.ApiKey);

        Debug.WriteLine("Flickr: Getting Group Photographs");
        FlickrNet.Photos photos = conn.GroupPoolGetPhotos(groupID, tagString.ToString(), this.mNumPerPage, this.mCurrentPage);
        //FlickrNet.Photos photos = conn.GroupPoolGetPhotos(groupID, tagString.ToString(), this.UserID, PhotoSearchExtras.All, this.mNumPerPage, this.mCurrentPage);

        Debug.WriteLine("Flickr: Parsing Tags from Group Photographs");
        if (photos != null)
        {
            foreach (FlickrNet.Photo photo in photos.PhotoCollection)
            {
                Debug.WriteLine("Flickr: Getting Photograph");

                string[] tags = photo.CleanTags.Split(' ');
                foreach (string tag in tags)
                {
                    if (!this.mTags.Contains(tag))
                        this.mTags.Add(tag);
                }

                this.mImages.Add(FlickrImage.Parse(photo));
            }

            this.mNumOfPages = photos.TotalPages;
            this.mNumOfImages = photos.TotalPhotos;

            this.mImages.Sort(new FlickrImageDateSort());
        }

        Debug.WriteLine("Flickr: Finished Loading Flickr API");
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

    public FlickrImage Next(string identifier)
    {
        for (int count = 0; count < this.mImages.Count; count++)
        {
            FlickrImage image = this.mImages[count];
            if (string.Compare(image.Identifier, identifier, true) == 0)
            {
                if (count + 1 < this.mImages.Count)
                {
                    return this.mImages[count + 1];
                }
                return null;
            }
        }

        return null;
    }

    public FlickrImage Prev(string identifier)
    {
        for (int count = 0; count < this.mImages.Count; count++)
        {
            FlickrImage image = this.mImages[count];
            if (string.Compare(image.Identifier, identifier, true) == 0)
            {
                if (count - 1 > 0)
                {
                    return this.mImages[count - 1];
                }
                return null;
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
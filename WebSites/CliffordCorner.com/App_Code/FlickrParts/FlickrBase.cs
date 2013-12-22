using System;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using FlickrNet;

public abstract class FlickrBase : CliffordCornerBase
{
    protected FlickrCredentials mCredentials = new FlickrCredentials();
    protected string mUserID = string.Empty;
    protected int mNumPerPage = 25;

    public virtual FlickrCredentials Credentials
    {
        get { return this.mCredentials; }
    }

    public virtual string UserID
    {
        get { return this.mUserID; }
        set { this.mUserID = value; }
    }

    public virtual int ImagesToShow
    {
        get { return this.mNumPerPage; }
        set { this.mNumPerPage = value; }
    }

    public FlickrBase()
    {
        this.mCredentials.ApiKey = ConfigurationManager.AppSettings["apiKey"];
        this.mCredentials.Email = ConfigurationManager.AppSettings["email"];
        this.mCredentials.Password = ConfigurationManager.AppSettings["password"];
        this.mCredentials.Secret = ConfigurationManager.AppSettings["secret"];
        this.mUserID = ConfigurationManager.AppSettings["userID"];
        this.mNumPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["imageCount"]);
    }

    public FlickrBase(string apiKey, string secret, string email, string password)
    {
        this.mCredentials.ApiKey = apiKey;
        this.mCredentials.Secret = secret;
        this.mCredentials.Email = email;
        this.mCredentials.Password = password;
    }

    public FlickrBase(string apiKey, string secret, string email, string password, string userID)
        : this(apiKey, secret, email, password)
    {
        this.mUserID = userID;
    }
}
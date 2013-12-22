using System;

public class FlickrCredentials : System.ICloneable
{
    protected string mApiKey;
    protected string mEmail;
    protected string mPassword;
    protected string mSecret;

    public string ApiKey
    {
        get { return this.mApiKey; }
        set { this.mApiKey = value; }
    }

    public string Email
    {
        get { return this.mEmail; }
        set { this.mEmail = value; }
    }

    public string Password
    {
        get { return this.mPassword; }
        set { this.mPassword = value; }
    }

    public string Secret
    {
        get { return this.mSecret; }
        set { this.mSecret = value; }
    }

    public FlickrCredentials()
    {
    }

    public FlickrCredentials(string apiKey, string secret, string email, string password)
    {
        this.mApiKey = apiKey;
        this.mSecret = secret;
        this.mEmail = email;
        this.mPassword = password;
    }

    public object Clone()
    {
        return new FlickrCredentials(this.mApiKey, this.mSecret, this.mEmail, this.mPassword);
    }
}
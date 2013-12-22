using System;

namespace Cliffy.Data.Flickr
{
    public class FlickrCredentials : ICloneable
    {
        private string mApiKey = string.Empty;
        private string mEmail = string.Empty;
        private string mPassword = string.Empty;
        private string mSecret = string.Empty;
        private string mUserID = string.Empty;

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

        public string UserID
        {
            get { return this.mUserID; }
            set { this.mUserID = value; }
        }

        public FlickrCredentials() { }

        public FlickrCredentials(string apiKey, string secret, string email, string password, string userID) : this()
        {
            this.mApiKey = apiKey;
            this.mSecret = secret;
            this.mEmail = email;
            this.mPassword = password;
            this.mUserID = userID;
        }

        public object Clone()
        {
            return new FlickrCredentials(this.mApiKey, this.mSecret, this.mEmail, this.mPassword, this.mUserID);
        }
    }
}
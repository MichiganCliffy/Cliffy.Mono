using System;

/// <summary>
/// Exception Handler for the Site.
/// </summary>
[Serializable]
public class SiteException : Exception
{
    protected SiteExceptionLocation mLocation = SiteExceptionLocation.SiteCode;

    public SiteExceptionLocation Location
    {
        get { return this.mLocation; }
        set { this.mLocation = value; }
    }

    public SiteException(string message)
        : base(message)
    {
    }

    public SiteException(SiteExceptionLocation location, string message)
        : base(message)
    {
        this.mLocation = location;
    }

    public SiteException(SiteExceptionLocation location, string message, Exception exception)
        : base(message, exception)
    {
        this.mLocation = location;
    }
}
using System;
using System.Collections.Generic;

/// <summary>
/// The originating location of a Site Exception.
/// </summary>
public enum SiteExceptionLocation
{
    SiteCode = 0,
    FlickrApi = 1,
    BloggerApi = 2,
    GoogleApi = 3
}

public interface ICliffordCornerPost : IDisposable
{
    string Id { get; }
    string Title { get; set; }
    string Content { get; set; }
    string Uri { get; set; }
    string Link { get; set; }
    DateTime DatePosted { get; set; }

    void Load(string uri);
}

public interface ICliffordCornerBlog : IDisposable
{
    ICliffordCornerPost[] Posts { get; }

    void Load();
}

public enum SortDirection
{
    Up = 1,
    Down = -1
}
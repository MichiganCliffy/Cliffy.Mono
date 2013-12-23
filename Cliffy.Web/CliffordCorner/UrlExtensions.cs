using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cliffy.Common;

namespace Cliffy.Web.CliffordCorner
{
    public static class UrlExtensions
    {
        private static string[] PartsToIgnore
        {
            get
            {
                return new string[] { "photograph", "posts", "notes" };
            }
        }

        public static string Content(this UrlHelper source, IPagePhotographs model, Photograph photograph)
        {
            StringBuilder output = new StringBuilder("~");
            string[] parts = model.Id.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length > 0)
            {
                if (string.Compare(parts[0], "photographs", true) == 0)
                {
                    output.Append(source.Content(photograph));
                }
                else
                {
                    if (model.IsTravelLog)
                    {
                        output.Append("/travellog");
                    }

                    output.AppendFormat("/{0}", parts[0]);

                    if (parts.Length > 1)
                    {
                        output.AppendFormat("/{0}", parts[1]);
                    }

                    if (string.Compare(photograph.Media, "video", true) == 0)
                    {
                        output.AppendFormat("/video/{0}", photograph.UniqueId);
                    }
                    else
                    {
                        output.AppendFormat("/photograph/{0}", photograph.UniqueId);
                    }
                }
            }

            return source.Content(output.ToString());
        }

        public static string ThumbnailLink(this UrlHelper source, Photograph item, string baseUrl)
        {
            StringBuilder output = new StringBuilder("/");

            if (!string.IsNullOrWhiteSpace(baseUrl))
            {
                output.Append(baseUrl);
            }

            if (string.Compare(item.Media, "video", true) == 0)
            {
                output.Append("video/");
            }
            else
            {
                output.Append("photograph/");
            }

            output.Append(item.UniqueId);

            return source.Content(output.ToString());
        }

        public static string Content(this UrlHelper source, Photograph photograph)
        {
            StringBuilder output = new StringBuilder("~");

            if (string.Compare(photograph.Media, "video", true) == 0)
            {
                output.AppendFormat("/video/{0}", photograph.UniqueId);
            }
            else
            {
                output.AppendFormat("/photograph/{0}", photograph.UniqueId);
            }

            return source.Content(output.ToString());
        }

        public static string Content(this UrlHelper source, PageLink link)
        {
            StringBuilder output = new StringBuilder("~");

            if (string.Compare(link.Id, "home", true) == 0)
            {
                output.Append("/");
            }
            else 
            {
                if (link.IsTravelLog)
                {
                    output.Append("/travellog");
                }

                string[] parts = link.Id.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 0)
                {
                    output.AppendFormat("/{0}", parts[0]);

                    if (parts.Length > 1)
                    {
                        switch (link.PageType)
                        {
                            case PageType.FlickrSetTag:
                                output.Append("/photographs");
                                break;

                            case PageType.HtmlFragment:
                                output.Append("/notes");
                                break;

                            case PageType.BlogPosts:
                                output.Append("/posts");
                                break;
                        }

                        for (int i = 1; i < parts.Length; i++)
                        {
                            string part = parts[i];
                            if (!UrlExtensions.PartsToIgnore.Any(x => string.Compare(x, part, true) == 0))
                            {
                                output.AppendFormat("/{0}", part);
                            }
                        }
                    }
                }
            }

            return source.Content(output.ToString());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Cliffy.Common;

namespace Cliffy.Web.CliffordCorner
{
    public static class HtmlExtensions
    {
        private static bool IsMatchOrChild(this HtmlHelper source, IPage model, PageLink link)
        {
            bool output = false;

            if (!string.IsNullOrWhiteSpace(model.Id))
            {
                if (link.Id == model.Id)
                {
                    output = true;
                }
                else if (model.Id.StartsWith(string.Concat(link.Id, "|")))
                {
                    output = true;
                }
            }

            return output;
        }

        private static bool IsSelected(this HtmlHelper source, IPage model, PageLink link)
        {
            bool output = false;

            if (model.Id == link.Id)
            {
                output = true;
            }

            return output;
        }

        public static IHtmlString GlobalNav(this HtmlHelper source, IPage model)
        {
            StringBuilder output = new StringBuilder();

            var links = model.Navigation[NavigationType.Global];
            if (links.Count > 0)
            {
                output.Append("<ul>");

                UrlHelper url = new UrlHelper(source.ViewContext.RequestContext);
                for (int i = 0; i < links.Count; i++)
                {
                    PageLink link = links[i];
                    output.AppendFormat("<li title=\"{0}\"", link.MetaDescription);

                    if (source.IsMatchOrChild(model, link))
                    {
                        output.Append(" class=\"selected\"");
                    }

                    output.Append(">");
                    output.AppendFormat("<a href=\"{0}\" title=\"{1}\">{2}</a>", url.Content(link), link.MetaDescription, link.Title);
                    output.Append("</li>");
                }

                output.Append("</ul>");
            }

            return source.Raw(output.ToString());
        }

        public static IHtmlString Paging(this HtmlHelper source, IPage model, PageLinks pages)
        {
            StringBuilder output = new StringBuilder();

            if ((pages != null) && (pages.Count > 0))
            {
                int current = 0;
                int startAt = 0;
                int endAt = 0;
                int counter = 0;
                foreach (PageLink page in pages)
                {
                    if (source.IsSelected(model, page))
                    {
                        current = counter;
                        startAt = counter - 2;
                        endAt = counter + 3;
                        break;
                    }

                    counter++;
                }

                if (startAt < 0)
                {
                    startAt = 0;
                    endAt = 5;
                }
                
                if (endAt > pages.Count)
                {
                    endAt = pages.Count;

                    if ((startAt > endAt - 5) && (startAt > 5))
                    {
                        startAt = endAt - 5;
                    }
                }

                output.Append("<ul>");
                output.AppendFormat("<li>{0}:</li>", pages.Title);

                UrlHelper url = new UrlHelper(source.ViewContext.RequestContext);

                if (current > 0)
                {
                    PageLink previous = pages[current - 1];
                    output.Append("<li title=\"Previous Page\">");
                    output.AppendFormat("<a href=\"{0}\"><</a>", url.Content(previous));
                    output.Append("</li>");
                }

                for (int i = startAt; i < endAt; i++)
                {
                    PageLink page = pages[i];
                    output.Append("<li ");
                    output.AppendFormat("title=\"{0}\"", page.Title);

                    if (source.IsSelected(model, page))
                    {
                        current = counter;
                        output.Append(" class=\"selected\"");
                    }

                    output.Append(">");

                    output.AppendFormat("<a href=\"{0}\">{1}</a>", url.Content(page), page.Title);

                    output.Append("</li>");
                }

                if ((current < pages.Count) && (current != pages.Count - 1))
                {
                    PageLink next = pages[current + 1];
                    output.Append("<li title=\"Next Page\">");
                    output.AppendFormat("<a href=\"{0}\">></a>", url.Content(next));
                    output.Append("</li>");
                }

                output.Append("</ul>");
            }

            return source.Raw(output.ToString());
        }

        public static IHtmlString SlideShow(this HtmlHelper source, List<Photograph> photographs, int fontSize = 14, string backgroundColor = "#467aa7", int borderWidth = 2)
        {
            StringBuilder output = new StringBuilder("\n<script language=\"javascript\" type=\"text/javascript\" src=\"http://slideshow.triptracker.net/slide.js\"></script>");
            output.Append("\n<script language=\"javascript\" type=\"text/javascript\">");
            output.Append("\nvar viewer = new PhotoViewer();");
            output.AppendFormat("\nviewer.setFontSize({0});", fontSize);
            output.AppendFormat("\nviewer.setBackgroundColor(\"{0}\");", backgroundColor);
            output.AppendFormat("\nviewer.setBorderWidth({0});", borderWidth);

            foreach(Photograph photograph in photographs)
            {
                output.Append(source.SlideShowItem(photograph));
            }

            output.Append("\n</script>\n");

            return source.Raw(output.ToString());
        }

        public static IHtmlString SlideShowItem(this HtmlHelper source, Photograph photograph)
        {
            return source.Raw(string.Format("\nviewer.add('{0}', '{1}');", photograph.UriSizes.First(x => x.Size == ImageSize.Medium).Uri, photograph.Title.Replace("'", "\\'")));
        }

        public static IHtmlString Thumbnails(this HtmlHelper source, IPagePhotographs model, string listCss = "thumbnails", string itemCss = "thumbnail")
        {
            StringBuilder output = new StringBuilder();

            if (model.Photographs.Count > 0)
            {
                output.AppendFormat("<ul class=\"{0}\">", listCss);

                UrlHelper url = new UrlHelper(source.ViewContext.RequestContext);
                foreach (Photograph photograph in model.Photographs)
                {
                    output.AppendFormat("<li class=\"{0}\" title=\"{1}\">", itemCss, photograph.Title);

                    output.AppendFormat("<a href=\"{0}\">", url.Content(model, photograph));
                    output.AppendFormat("<img src=\"{0}\" alt=\"{1}\" />", photograph.UriSizes.First(x => x.Size == ImageSize.Thumbnail).Uri, photograph.Title);
                    output.Append("</a>");

                    output.Append("</li>");
                }

                output.Append("</ul>");
            }

            return source.Raw(output.ToString());
        }

        public static IHtmlString FlashVars(this HtmlHelper source, Photograph video)
        {
            StringBuilder output = new StringBuilder();

            if (string.Compare(video.Media, "video", true) == 0)
            {
                string[] parts = video.UniqueId.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 2)
                {
                    output.Append("intl_lang=en-us&amp;photo_secret=");
                    output.Append(parts[1]);
                    output.Append("&amp;photo_id=");
                    output.Append(parts[0]);
                }
            }

            return source.Raw(output.ToString());
        }

        public static IHtmlString BlogLinks(this HtmlHelper source, IPage model, PageLinks links, string wrapperCss = "box", string listCss = "menublock")
        {
            StringBuilder output = new StringBuilder();

            if (links.Count > 0)
            {
                output.AppendFormat("<div class=\"{0}\">", wrapperCss);
                output.AppendFormat("<h2>{0}</h2>", links.Title);

                output.AppendFormat("<ul class=\"{0}\">", listCss);

                foreach(PageLink link in links)
                {
                    output.AppendFormat("<li title=\"{0}\">", link.MetaDescription);
                    output.AppendFormat("<a href=\"{0}\" title=\"{1}\">{2}</a>", link.Id, link.MetaDescription, link.Title);
                    output.Append("</li>");
                }

                output.Append("</ul>");
                output.Append("</div>");
            }

            return source.Raw(output.ToString());
        }

        public static IHtmlString MenuBlock(this HtmlHelper source, IPage model, PageLinks links, string wrapperCss = "box", string listCss = "menublock", string title = "", bool slideShow = false)
        {
            StringBuilder output = new StringBuilder();

            if (links.Count > 0)
            {
                output.AppendFormat("<div class=\"{0}\">", wrapperCss);

                if (!string.IsNullOrWhiteSpace(links.Title))
                {
                    output.AppendFormat("<h2>{0}</h2>", links.Title);
                }
                else if (!string.IsNullOrWhiteSpace(title))
                {
                    output.AppendFormat("<h2>{0}</h2>", title);
                }

                output.AppendFormat("<ul class=\"{0}\">", listCss);

                UrlHelper url = new UrlHelper(source.ViewContext.RequestContext);

                var sorted = links.OrderBy(x => x.SortOrder).ToList();
                foreach (var link in sorted)
                {
                    output.AppendFormat("<li title=\"{0}\">", link.MetaDescription);

                    if (source.IsSelected(model, link))
                    {
                        output.Append(link.Title);
                    }
                    else
                    {
                        output.AppendFormat("<a href=\"{0}\" title=\"{1}\">{2}</a>", url.Content(link), link.MetaDescription, link.Title);
                    }

                    output.Append("</li>");
                }

                if (slideShow)
                {
                    output.Append("<li title=\"view a slide show\"><a href=\"javascript: void(viewer.show(0))\">Slide Show</a></li>");
                }

                output.Append("</ul>");
                output.Append("</div>");
            }

            return source.Raw(output.ToString());
        }

        public static IHtmlString TagLine(this HtmlHelper source)
        {
            Random radomizer = new Random();
            int imageChoice = Convert.ToInt32(4 * radomizer.NextDouble());
            switch (imageChoice)
            {
                case 4:
                    return source.Raw("What's new with the family");

                case 3:
                    return source.Raw("And now for something completely different...");

                case 2:
                    return source.Raw("photos by the family, for the family, about the family");
            }

            return source.Raw("meaningless stories & mindless ramblings regarding our family");
        }
    }
}
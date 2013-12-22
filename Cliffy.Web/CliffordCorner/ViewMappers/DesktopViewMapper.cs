using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Cliffy.Web.CliffordCorner.ViewMappers
{
    public class DesktopViewMapper : IViewMapper
    {
        public event RedirectEventHandler Redirect;

        public string MapView(IPage model)
        {
            if (string.Compare(model.Id, "home", true) == 0)
            {
                return "Index";
            }
            else if (model is PagePhotoSet)
            {
                    return "PhotoSetIntro";
            }
            else if (model is PagePhotoSetTag)
            {
                return "PhotoSetTag";
            }
            else if (model is PagePhotoSetPhoto)
            {
                PagePhotoSetPhoto photoSetPhoto = (PagePhotoSetPhoto)model;
                if (string.Compare(photoSetPhoto.Photograph.Media, "video", true) == 0)
                {
                    return "PhotoSetVideo";
                }
                else
                {
                    return "PhotoSetPhoto";
                }
            }
            else if (model is PageBlogPosts)
            {
                return "BlogPosts";
            }
            else if (model is PageHtmlFragment)
            {
                return "HtmlFragment";
            }
            else if (model is PageRedirect)
            {
                if (Redirect != null)
                {
                    PageRedirect redirect = (PageRedirect)model;
                    Redirect(this, redirect.RedirectTo);
                }
            }
            else if (model is PagePhotoGroupPhoto)
            {
                PagePhotoGroupPhoto photoGroupPhoto = (PagePhotoGroupPhoto)model;
                if (photoGroupPhoto.Photograph != null)
                {
                    if (string.Compare(photoGroupPhoto.Photograph.Media, "video", true) == 0)
                    {
                        return "PhotoGroupVideo";
                    }
                    else
                    {
                        return "PhotoGroupPhoto";
                    }
                }
                else
                {
                    return "PhotoGroupPhoto";
                }
            }
            else if (model is PagePhotoGroupTag)
            {
                return "PhotoGroupTag";
            }
            else if (model is PagePhotoGroup)
            {
                return "PhotoGroup";
            }

            return string.Empty;
        }
   }
}
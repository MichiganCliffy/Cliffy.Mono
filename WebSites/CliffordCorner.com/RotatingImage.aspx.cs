using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;

/// <summary>
/// Summary description for Thumbnail.
/// </summary>
public partial class RotatingImage : System.Web.UI.Page
{
    protected List<string> mImages = new List<string>();
    protected System.Drawing.Image mImage;
    protected int mMaxDim = 0;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (Request.QueryString["i"] != null)
        {
            if (File.Exists(Server.MapPath(this.ResolveUrl("~/rotating.xml"))))
            {
                XmlDocument definition = new XmlDocument();
                definition.Load(Server.MapPath(this.ResolveUrl("~/rotating.xml")));

                XmlNodeList images = definition.SelectNodes("//image");
                foreach (XmlNode image in images)
                {
                    if (string.Compare(Request.QueryString["i"], image.Attributes.GetNamedItem("id").Value, true) == 0)
                    {
                        XmlNodeList options = image.SelectNodes("option");
                        foreach (XmlNode option in options)
                        {
                            if (option.InnerText.Length > 0)
                            {
                                this.mImages.Add(option.InnerText);
                            }
                        }
                    }
                }
            }
        }

        /* Get Image */
        System.Random radomizer = new System.Random();
        int imageChoice = Convert.ToInt32((this.mImages.Count - 1) * radomizer.NextDouble() + 1);
        string imagePath = this.mImages[imageChoice];
        this.mImage = System.Drawing.Image.FromFile(Server.MapPath(imagePath));

        /* Render the Image */
        this.Render();
    }

    protected void Render()
    {
        /* do some math to resize the image note: i know very little about image resizing,
         * but this just seemed to work under v1.1. I think under v2.0 the images look
         * incorrect. *note to self* fix this for v2.0 */

        int h = this.mImage.Height;
        int w = this.mImage.Width;
        int b = h > w ? h : w;

        /* create the thumbnail image */
        using (System.Drawing.Image img2 =
                   this.mImage.GetThumbnailImage(w, h,
                   new System.Drawing.Image.GetThumbnailImageAbort(this.Abort),
                   IntPtr.Zero))
        {
            /* emit it to the response stream */
            img2.Save(this.Context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }

    private bool Abort()
    {
        return false;
    }
}
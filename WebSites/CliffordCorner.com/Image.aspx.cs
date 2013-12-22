using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

	/// <summary>
	/// Summary description for Thumbnail.
	/// </summary>
public partial class Image : System.Web.UI.Page
{
    protected int mMaxDim = 0;
    protected System.Drawing.Image mImage;

    private void Page_Load(object sender, System.EventArgs e)
    {
        /* Get Image */
        string imagePath = Server.UrlDecode(Request.QueryString.Get("img"));
        this.mImage = System.Drawing.Image.FromFile(Server.MapPath(imagePath));

        /* Get Dimension */
        if (Request.QueryString.Get("maxdim") != null)
            this.mMaxDim = Convert.ToInt32(Request.QueryString.Get("maxdim"));

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
        if (this.mMaxDim > 0)
        {
            double per = (b > this.mMaxDim) ? (this.mMaxDim * 1.0) / b : 1.0;
            h = (int)(h * per);
            w = (int)(w * per);
        }

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
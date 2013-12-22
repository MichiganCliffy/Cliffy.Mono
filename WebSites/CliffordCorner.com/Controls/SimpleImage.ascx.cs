using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Controls_SimpleImage : System.Web.UI.UserControl
{
    private string mSrc = string.Empty;
    public string src
    {
        get { return this.mSrc; }
        set { this.mSrc = value; }
    }

    public int width
    {
        get
        {
            if (this.imgTheImage.Attributes["width"].Length > 0)
            {
                return Convert.ToInt32(this.imgTheImage.Attributes["width"]);
            }

            return 0;
        }
        set { this.imgTheImage.Attributes["width"] = value.ToString(); }
    }

    public int height
    {
        get
        {
            if (this.imgTheImage.Attributes["height"].Length > 0)
            {
                return Convert.ToInt32(this.imgTheImage.Attributes["height"]);
            }

            return 0;
        }
        set { this.imgTheImage.Attributes["height"] = value.ToString(); }
    }

    public string alt
    {
        get { return this.imgTheImage.Attributes["alt"]; }
        set { this.imgTheImage.Attributes["alt"] = value; }
    }

    public int border
    {
        get
        {
            if (this.imgTheImage.Attributes["border"].Length > 0)
            {
                return Convert.ToInt32(this.imgTheImage.Attributes["border"]);
            }

            return 0;
        }
        set
        {
            this.imgTheImage.Attributes["border"] = value.ToString();

            if (border > 0)
            {
                this.imgTheImage.BorderStyle = BorderStyle.Solid;
                this.imgTheImage.BorderWidth = new Unit(value);
            }
            else
            {
                this.imgTheImage.BorderStyle = BorderStyle.None;
            }
        }
    }

    public ImageAlign align
    {
        get { return this.imgTheImage.ImageAlign; }
        set { this.imgTheImage.ImageAlign = value; }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.Visible = false;
        if (this.mSrc.Length > 0)
        {
            this.Visible = true;
            this.imgTheImage.ImageUrl = this.ResolveUrl(this.mSrc);
        }
    }
}
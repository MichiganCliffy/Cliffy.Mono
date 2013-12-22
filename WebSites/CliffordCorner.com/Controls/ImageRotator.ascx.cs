using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;

/// <summary>
///	Simple Image Rotator.
/// </summary>
public partial class Controls_ImageRotator : System.Web.UI.UserControl
{
    protected ArrayList mDataSource = new ArrayList();
    protected string mImageId = string.Empty;

    public string ImageId
    {
        get { return this.mImageId; }
        set { this.mImageId = value; }
    }

    public int Height
    {
        get { return this.mRotator.Height; }
        set { this.mRotator.Height = value; }
    }

    public int Width
    {
        get { return this.mRotator.Width; }
        set { this.mRotator.Width = value; }
    }

    public string Alt
    {
        get { return this.mRotator.Alt; }
        set { this.mRotator.Alt = value; }
    }

    public string Class
    {
        get { return this.mRotator.Attributes["class"]; }
        set { this.mRotator.Attributes["class"] = value; }
    }

    public int Border
    {
        get { return this.mRotator.Border; }
        set { this.mRotator.Border = value; }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        // Put user code to initialize the page here
    }

    protected void Page_PreRender(object sender, System.EventArgs e)
    {
        this.LoadImages();
        this.LoadImage();

        if (this.mRotator.Src.Length == 0)
            this.Visible = false;
    }

    protected void LoadImages()
    {
        if (this.mImageId.Length > 0)
        {
            if (File.Exists(Server.MapPath(this.ResolveUrl("~/rotating.xml"))))
            {
                XmlDocument definition = new XmlDocument();
                definition.Load(Server.MapPath(this.ResolveUrl("~/rotating.xml")));

                XmlNodeList images = definition.SelectNodes("//image");
                foreach (XmlNode image in images)
                {
                    if (string.Compare(this.mImageId, image.Attributes.GetNamedItem("id").Value, true) == 0)
                    {
                        XmlNodeList options = image.SelectNodes("option");
                        foreach (XmlNode option in options)
                        {
                            if (option.InnerText.Length > 0)
                                this.mDataSource.Add(option.InnerText);
                        }
                    }
                }
            }
        }
    }

    protected void LoadImage()
    {
        if (this.mDataSource.Count > 0)
        {
            System.Random radomizer = new System.Random();
            int imageChoice = Convert.ToInt32((this.mDataSource.Count - 1) * radomizer.NextDouble());
            this.mRotator.Src = (string)this.mDataSource[imageChoice];
        }
    }
}
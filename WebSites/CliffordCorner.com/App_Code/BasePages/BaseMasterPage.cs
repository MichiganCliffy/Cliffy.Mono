using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class BaseMasterPage : System.Web.UI.MasterPage
{
    protected string mKeywords = string.Empty;
    public string Keywords
    {
        get { return this.mKeywords; }
        set { this.mKeywords = value; }
    }

    protected string mDescription = string.Empty;
    public string Description
    {
        get { return this.mDescription; }
        set { this.mDescription = value; }
    }

    protected Dictionary<string, string> mSlideShowImages = new Dictionary<string, string>();
    public Dictionary<string, string> SlideShowImages
    {
        get { return this.mSlideShowImages; }
        set { this.mSlideShowImages = value; }
    }
}
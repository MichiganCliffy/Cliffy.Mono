using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class CliffyMasterPage : BaseMasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //HtmlGenericControl stylesheet = new HtmlGenericControl("link");
        //stylesheet.Attributes.Add("rel", "stylesheet");
        //stylesheet.Attributes.Add("type", "text/css");
        //stylesheet.Attributes.Add("media", "screen,projection");
        //stylesheet.Attributes.Add("href", this.ResolveUrl("~/includes/default.css"));
        //this.Page.Header.Controls.Add(stylesheet);

        HtmlGenericControl script = new HtmlGenericControl("script");
        script.Attributes.Add("type", "text/javascript");
        script.Attributes.Add("src", this.ResolveUrl("~/includes/scripts.js"));
        this.Page.Header.Controls.Add(script);
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (this.mKeywords.Length == 0)
        {
            this.mKeywords = "Clifford, Family, Ann Arbor, Michigan, John Clifford, Kay Clifford, William Clifford";
        }
        this.metaKeywords.Attributes["content"] = this.mKeywords;

        if (this.mDescription.Length == 0)
        {
            this.mDescription = "Informational site for the Clifford Family of Ann Arbor, Michigan.";
        }
        this.metaDescription.Attributes["content"] = this.mDescription;

        if (this.mSlideShowImages.Count > 0)
        {
            this.LoadSlideShowJS();
        }
    }

    private void LoadSlideShowJS()
    {
        HtmlGenericControl slider = new HtmlGenericControl("script");
        slider.Attributes.Add("language", "javascript");
        slider.Attributes.Add("type", "text/javascript");
        slider.Attributes.Add("src", "http://slideshow.triptracker.net/slide.js");
        this.Page.Header.Controls.Add(slider);

        HtmlGenericControl sliderImages = new HtmlGenericControl("script");
        sliderImages.Attributes.Add("language", "javascript");
        sliderImages.Attributes.Add("type", "text/javascript");

        StringBuilder js = new StringBuilder();
        js.Append("\nvar viewer = new PhotoViewer();\n");
        js.Append("viewer.setFontSize(14);\n");
        js.Append("viewer.setBackgroundColor(\"#467aa7\");\n");
        js.Append("viewer.setBorderWidth(2);\n");

        foreach (string imageUrl in this.mSlideShowImages.Keys)
        {
            js.AppendFormat("viewer.add('{0}', '{1}');\n", imageUrl, this.mSlideShowImages[imageUrl].Replace("'", ""));
        }

        sliderImages.InnerHtml = js.ToString();

        this.Page.Header.Controls.Add(sliderImages);
    }
}
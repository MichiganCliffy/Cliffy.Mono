using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Cliffy.Web.UrlRewriter;

/// <summary>
/// Common properties and methods for all cliffordcorner pages.
/// </summary>
public partial class BasePage : UrlRewriterBasePage
{
    protected string mMetaDescription = string.Empty;
    protected List<string> mMetaKeywords = new List<string>();

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (this.Master is BaseMasterPage)
        {
            BaseMasterPage master = (BaseMasterPage)this.Master;

            if (this.mMetaDescription.Length > 0)
            {
                master.Description = this.mMetaDescription;
            }

            if (this.mMetaKeywords.Count > 0)
            {
                StringBuilder keywords = new StringBuilder();
                foreach (string keyword in this.mMetaKeywords)
                {
                    if (keywords.Length > 0) keywords.Append(", ");
                    keywords.Append(keyword);
                }

                if (keywords.Length > 0)
                {
                    master.Keywords = keywords.ToString();
                }
            }
        }
    }

    protected void AddMetaKeyword(string keyword)
    {
        foreach (string existing in this.mMetaKeywords)
        {
            if (string.Compare(keyword, existing, true) == 0)
            {
                return;
            }
        }

        this.mMetaKeywords.Add(keyword.ToLower());
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CacheMgr : System.Web.UI.Page
{
    private List<string> mCachedItems = new List<string>();

    protected void Page_Load(object sender, EventArgs e)
    {
        IDictionaryEnumerator elements = this.Cache.GetEnumerator();
        while (elements.MoveNext())
        {
            if (elements.Key is string)
            {
                string key = (string)elements.Key;
                this.mCachedItems.Add(key);
            }
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.rptItems.Visible = false;
        if (this.mCachedItems.Count > 0)
        {
            this.rptItems.Visible = true;
            this.rptItems.DataSource = this.mCachedItems;
            this.rptItems.DataBind();
        }
    }

    protected void lnkDelete_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName.CompareTo("DeleteItem") == 0)
        {
            if (e.CommandArgument is string)
            {
                string key = (string)e.CommandArgument;
                this.mCachedItems.Remove(key);
                this.Cache.Remove(key);
            }
        }
        else if (e.CommandName.CompareTo("DeleteAll") == 0)
        {
            while (this.mCachedItems.Count > 0)
            {
                this.Cache.Remove(this.mCachedItems[0]);
                this.mCachedItems.RemoveAt(0);
            }
        }
    }
}

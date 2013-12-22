using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
///	List of all tags / labels for the blog.
/// </summary>
public partial class Controls_BlogTags : System.Web.UI.UserControl
{
    private bool mTopOnly = false;
    private int mTopCount = 10;
    private string mPath = "/blogposts/labels/";
    private string mTitle = string.Empty;

    public bool TopOnly
    {
        get { return this.mTopOnly; }
        set { this.mTopOnly = value; }
    }

    public int TopCount
    {
        get { return this.mTopCount; }
        set { this.mTopCount = value; }
    }

    public string Title
    {
        get { return this.mTitle; }
        set { this.mTitle = value; }
    }

    public string Path
    {
        get { return this.mPath; }
        set { this.mPath = value; }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        string path = this.ResolveUrl(string.Format("~{0}", this.mPath));
        string directory = Server.MapPath(path);
        Trace.Write(string.Format("Labels directory is: {0}", directory));

        this.Visible = false;
        if (Directory.Exists(directory))
        {
            string[] files = Directory.GetFiles(directory, "*.aspx");
            if (files.Length > 0)
            {
                this.Visible = true;

                SortedList labels = new SortedList();
                foreach (string file in files)
                {
                    FileInfo data = new FileInfo(file);
                    long key = long.MaxValue - data.Length;
                    while (labels.ContainsKey(key))
                    {
                        key += 1;
                    }

                    labels.Add(key, data.Name.Replace(".aspx", string.Empty));
                }

                if (this.mTopOnly)
                {
                    while (labels.Count > this.mTopCount)
                    {
                        labels.RemoveAt(this.mTopCount);
                    }
                }

                this.rptTags.DataSource = labels.Values;
                this.rptTags.DataBind();
            }
        }
    }

    protected void Page_PreRender(object sender, System.EventArgs e)
    {
        this.hTitle.Visible = false;
        if (this.mTitle.Length > 0)
        {
            this.hTitle.Visible = true;
            this.txtTitle.Text = this.mTitle;
        }
    }

    protected void rptTags_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        switch (e.Item.ItemType)
        {
            case ListItemType.Item:
            case ListItemType.AlternatingItem:
                string raw = (string)e.Item.DataItem;
                string tag = raw.Replace('_', ' ');
                string path = string.Format("~{0}/{1}.aspx", this.mPath, raw);
                HtmlAnchor link = (HtmlAnchor)e.Item.FindControl("lnkTag");
                link.HRef = this.ResolveUrl(path);
                link.InnerText = tag;
                link.Title = tag;
                break;
        }
    }
}
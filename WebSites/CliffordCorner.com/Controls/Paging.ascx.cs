using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
///		Summary description for Paging.
/// </summary>
public partial class Controls_Paging : System.Web.UI.UserControl
{
    protected long mItemTotal = int.MinValue;
    protected long mItemPerPage = int.MaxValue;
    protected long mPageCurrent = 0;
    protected long mPageTotal = 0;
    protected string mPageToUse = string.Empty;
    protected long mPagesPerPage = 5;

    public long PageCurrent
    {
        get { return this.mPageCurrent; }
        set { this.mPageCurrent = value; }
    }

    public long ItemTotal
    {
        get { return this.mItemTotal; }
        set { this.mItemTotal = value; }
    }

    public long ItemPerPage
    {
        get { return this.mItemPerPage; }
        set { this.mItemPerPage = value; }
    }

    public string PageToUse
    {
        get { return this.mPageToUse; }
        set { this.mPageToUse = value; }
    }

    public long PagesPerPage
    {
        get { return this.mPagesPerPage; }
        set { this.mPagesPerPage = value; }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        this.mPageToUse = this.Request.Path;
    }

    public override void DataBind()
    {
        if (this.mItemTotal > int.MinValue)
        {
            this.mPageTotal = Convert.ToInt64(this.mItemTotal / this.mItemPerPage);
            if (this.mItemTotal % this.mItemPerPage > 0)
            {
                this.mPageTotal += 1;
            }
            if (this.mPageTotal <= 1)
            {
                this.Visible = false;
            }
        }
        else
        {
            this.Visible = false;
        }

        if (this.Visible)
        {
            this.phNextPage.Visible = false;
            this.phPrevPage.Visible = false;

            long divider = this.Divider;
            long adder = this.Adder;

            if (this.mPageCurrent == 0)
            {
                this.mPageCurrent = 1;
            }

            if (this.mPageCurrent > divider)
            {
                this.phPrevPage.Visible = true;
                this.lnkPrevPage.HRef = this.ResolveUrl(string.Format("{0}/page{1}", this.mPageToUse, this.mPageCurrent - adder));
            }
            if (this.mPageCurrent <= this.mPageTotal - divider)
            {
                this.phNextPage.Visible = true;
                this.lnkNextPage.HRef = this.ResolveUrl(string.Format("{0}/page{1}", this.mPageToUse, this.mPageCurrent + adder));
            }

            this.mPages.DataSource = this.LoadPages();
            this.mPages.DataBind();
        }

        base.DataBind();
    }

    private long Divider
    {
        get
        {
            long divider = this.mPagesPerPage / 2;
            if (this.mPagesPerPage % 2 > 0)
            {
                divider += 1;
            }
            return divider;
        }
    }

    private long Adder
    {
        get
        {
            return this.mPagesPerPage / 2;
        }
    }

    private ArrayList LoadPages()
    {
        ArrayList pages = new ArrayList();

        long start = 1;
        long finish = this.mPageTotal;
        long divider = this.Divider;
        long adder = this.Adder;

        if (this.mPageTotal > this.mPagesPerPage)
        {
            if (this.mPageCurrent <= divider)
            {
                finish = this.mPagesPerPage;
            }
            else if (this.mPageCurrent > this.mPageTotal - divider)
            {
                start = this.mPageTotal - this.mPagesPerPage;
            }
            else
            {
                start = this.mPageCurrent - adder;
                finish = this.mPageCurrent + adder;
            }
        }

        for (int i = Convert.ToInt32(start); i <= Convert.ToInt32(finish); i++)
        {
            pages.Add(i);
        }

        return pages;
    }

    protected void Pages_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        switch (e.Item.ItemType)
        {
            case ListItemType.Item:
            case ListItemType.AlternatingItem:
            case ListItemType.SelectedItem:
                if (e.Item.DataItem != null)
                {
                    int page = (int)e.Item.DataItem;
                    HtmlAnchor link = (HtmlAnchor)e.Item.FindControl("mPage");

                    link.InnerText = page.ToString();
                    link.HRef = this.ResolveUrl(string.Format("{0}/page{1}", this.mPageToUse, page));

                    if (page == Convert.ToInt32(this.mPageCurrent))
                    {
                        link.Attributes["Class"] = "selected";
                    }
                    else if ((page == 1) && (Convert.ToInt32(this.mPageCurrent) == 0))
                    {
                        link.Attributes["Class"] = "selected";
                    }
                }
                break;
        }
    }
}
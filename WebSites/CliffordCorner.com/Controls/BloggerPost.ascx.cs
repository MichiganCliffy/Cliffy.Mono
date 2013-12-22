using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cliffy.Web.Blogger;

public partial class Controls_BloggerPost : System.Web.UI.UserControl
{
    private BlogPost mDataSource = null;
    public BlogPost DataSource
    {
        get { return this.mDataSource; }
        set { this.mDataSource = value; }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.Visible = false;

        if (this.mDataSource != null)
        {
            this.Visible = true;

            this.txtPublishDate.Text = this.mDataSource.DatePublished.ToLongDateString();
            this.txtTitle.Text = this.mDataSource.Title;
            this.txtContent.Text = this.mDataSource.Description;
        }

        this.pnlAuthor_PreRender(sender, e);
        this.pnlLabels_PreRender(sender, e);
        this.pnlReadMore_PreRender(sender, e);
    }

    protected void pnlAuthor_PreRender(object sender, EventArgs e)
    {
        this.pnlAuthor.Visible = false;

        if (this.mDataSource != null)
        {
            if (!string.IsNullOrEmpty(this.mDataSource.Author))
            {
                this.pnlAuthor.Visible = true;
                this.txtAuthor.Text = string.Format("Posted by {0} at {1:h:mm tt}", this.mDataSource.Author, this.mDataSource.DatePublished);

                if (string.IsNullOrEmpty(this.mDataSource.Link))
                {
                    if ((this.mDataSource.Labels == null) || (this.mDataSource.Labels.Count == 0))
                    {
                        this.pnlAuthor.Style.Add("margin-bottom", "10px");
                    }
                }
            }
        }
    }

    protected void pnlLabels_PreRender(object sender, EventArgs e)
    {
        this.pnlLabels.Visible = false;

        if (this.mDataSource != null)
        {
            if ((this.mDataSource.Labels != null) && (this.mDataSource.Labels.Count > 0))
            {
                this.pnlLabels.Visible = true;

                this.rptLabels.DataSource = this.mDataSource.Labels;
                this.rptLabels.DataBind();

                if (string.IsNullOrEmpty(this.mDataSource.Link))
                {
                    this.pnlLabels.Style.Add("margin-bottom", "10px");
                }
            }
        }
    }

    protected void pnlReadMore_PreRender(object sender, EventArgs e)
    {
        this.pnlReadMore.Visible = false;

        if (this.mDataSource != null)
        {
            if (!string.IsNullOrEmpty(this.mDataSource.Link))
            {
                this.pnlReadMore.Visible = true;

                this.lnkReadMore.NavigateUrl = this.mDataSource.Link;
            }
        }
    }
}

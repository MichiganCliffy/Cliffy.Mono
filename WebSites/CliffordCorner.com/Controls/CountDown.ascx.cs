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

public partial class Controls_CountDown : System.Web.UI.UserControl
{
    private DateTime mDate = DateTime.MinValue;
    private int mHour = int.MinValue;

    public DateTime Date
    {
        get { return this.mDate; }
        set { this.mDate = value; }
    }

    public int Hour
    {
        get { return this.mHour; }
        set { this.mHour = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Visible = true;
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.Visible = false;
        if (this.mDate.CompareTo(DateTime.MinValue) != 0)
        {
            this.Visible = true;

            TimeSpan diff = this.mDate.Subtract(DateTime.Now);
            if (diff.Days < 0)
            {
                // has already passed
                this.txtContent.Text = string.Format("{0} days since", -diff.Days);
            }
            else if (diff.Days == 0)
            {
                // on the day
                if (this.mHour.CompareTo(int.MinValue) != 0)
                {
                    int hours = this.mHour - DateTime.Now.Hour;
                    if (hours > 0)
                    {
                        this.txtContent.Text = string.Format("{0} hours until", hours);
                    }
                    else
                    {
                        this.txtContent.Text = "0 days until";
                    }
                }
                else
                {
                    this.txtContent.Text = "0 days until";
                }
            }
            else
            {
                // it's comming up
                this.txtContent.Text = string.Format("{0} days until", diff.Days);
            }
        }
    }
}

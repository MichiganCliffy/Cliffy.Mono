using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
///	Text displayed when error is encountered when communicating with Flickr.Com.
/// </summary>
public partial class Controls_ErrorFlickr : System.Web.UI.UserControl
{
    private Exception mDataSource = null;

    public Exception DataSource
    {
        get { return this.mDataSource; }
        set { this.mDataSource = value; }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        // Put user code to initialize the page here
    }

    protected void Page_PreRender(object sender, System.EventArgs e)
    {
        if (this.mDataSource != null)
        {
            this.mMessage.InnerText = this.mDataSource.Message;

            StringBuilder stacktrace = new StringBuilder();
            while (this.mDataSource != null)
            {
                if (this.mDataSource.StackTrace != null)
                {
                    stacktrace.Append(this.mDataSource.StackTrace);
                    stacktrace.Append("<br /><br />");
                }

                this.mDataSource = this.mDataSource.InnerException;
            }
            stacktrace.Remove(stacktrace.Length - 12, 12);

            this.mStackTrace.InnerHtml = stacktrace.ToString();
        }
    }
}
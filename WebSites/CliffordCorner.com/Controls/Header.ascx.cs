using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
///	Page Header Info.
/// </summary>
public partial class Controls_Header : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        System.Random radomizer = new System.Random();
        int imageChoice = Convert.ToInt32(4 * radomizer.NextDouble());
        switch (imageChoice)
        {
            case 4:
                mTagLine.InnerText = "What's new with the family";
                break;

            case 3:
                mTagLine.InnerText = "And now for something completely different...";
                break;

            case 2:
                mTagLine.InnerText = "photos by the family, for the family, about the family";
                break;

            default:
                mTagLine.InnerText = "meaningless stories & mindless ramblings regarding our family";
                break;
        }
    }
}
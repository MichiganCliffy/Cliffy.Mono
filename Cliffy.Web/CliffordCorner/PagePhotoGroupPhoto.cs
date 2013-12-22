using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cliffy.Web.CliffordCorner
{
    public class PagePhotoGroupPhoto : PageBase
    {
        public override string Title
        {
            get
            {
                if (this.Photograph != null)
                {
                    return this.Photograph.Title;
                }
                else
                {
                    return base.Title;
                }
            }
            set { base.Title = value; }
        }

        public IPhotograph Photograph { get; set; }
    }
}
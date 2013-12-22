using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliffy.FlickrMongoSync
{
    public class Photograph
    {
        public string PhotoId
        {
            get;
            set;
        }

        public string Secret
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Media
        {
            get;
            set;
        }

        public DateTime DateSaved
        {
            get;
            set;
        }

        public string Photographer
        {
            get;
            set;
        }

        public string UriSource
        {
            get;
            set;
        }

        public List<PhotographUri> UriSizes
        {
            get;
            set;
        }

        public List<string> Tags
        {
            get;
            set;
        }

        public string SetId
        {
            get;
            set;
        }
    }
}
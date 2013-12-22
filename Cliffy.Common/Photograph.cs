using System;
using System.Collections.Generic;

namespace Cliffy.Common
{
    public class Photograph
    {
        public object Id { get; set; }

        public string UniqueId
        {
            get { return string.Concat(PhotoId, "/", Secret); }
        }

        public string PhotoId { get; set; }

        public string Secret { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Media { get; set; }

        public DateTime DateSaved { get; set; }

        public string Photographer { get; set; }

        public string UriSource { get; set; }

        public List<PhotographUri> UriSizes { get; set; }

        public List<string> Tags { get; set; }

        public string SetId { get; set; }

        public object Clone()
        {
            var output = new Photograph
            {
                PhotoId = PhotoId,
                Secret = Secret,
                Title = Title,
                Description = Description,
                Media = Media,
                DateSaved = DateSaved,
                Photographer = Photographer,
                UriSource = UriSource,
                UriSizes = UriSizes,
                Tags = Tags,
                SetId = SetId
            };

            return output;
        }
    }
}
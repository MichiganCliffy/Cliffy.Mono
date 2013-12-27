using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cliffy.Common;

namespace Cliffy.FlickrMongoSync
{
	public class FlickrImporterPhotographs
	{
		public string Id { get; set; }

		public string DefaultPhotoId { get; set; }

		public List<PhotographTag> Tags { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public long Total { get; set; }

		public void AddTags(Photograph item)
		{
			if (Tags == null) {
				Tags = new List<PhotographTag> ();
			}

			if (item.Tags != null)
			{
				foreach (string tag in item.Tags)
				{
					PhotographTag photoTag = Tags.FirstOrDefault(x => string.Compare(x.Tag, tag, true) == 0);
					if (photoTag != null)
					{
						photoTag.Count++;
					}
					else
					{
						photoTag = new PhotographTag()
						{
							Tag = tag,
							Count = 1
						};

						Tags.Add(photoTag);
					}
				}
			}
		}
    }
}
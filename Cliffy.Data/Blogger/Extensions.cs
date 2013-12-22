using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using Cliffy.Common;

namespace Cliffy.Data
{
	public static class Extensions
	{
		public static BlogPost ToBlogPost(this SyndicationItem item)
		{
			var output = new BlogPost {
				Id = item.Id,
				Title = item.Title.Text,
				Description = item.Summary.Text,
				DatePublished = item.PublishDate.LocalDateTime,
				DateUpdated = item.LastUpdatedTime.LocalDateTime,
				Tags= new List<string>()
			};

			if (item.Authors.Count > 0) {
				int startAt = item.Authors [0].Email.IndexOf ("(");

				if (!string.IsNullOrEmpty (item.Authors [0].Name)) {
					output.Author = item.Authors [0].Name;
				} else if (startAt >= 0) {
					for (int i = 0; i < item.Authors [0].Email.Length; i++) {
						if ((i > startAt) && (i < item.Authors [0].Email.Length - 1)) {
							output.Author += item.Authors [0].Email.Substring (i, 1);
						}
					}
				}
			}

			if (item.Links.Count > 0) {
				output.UriSource = item.Links [0].Uri.ToString ();
			}

			if (item.Categories.Count > 0) {
				foreach (SyndicationCategory category in item.Categories) {
					output.Tags.Add (category.Name);
				}
			}

			return output;
		}
	}
}
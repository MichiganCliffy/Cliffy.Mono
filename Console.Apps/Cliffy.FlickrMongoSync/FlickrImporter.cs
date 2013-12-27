using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using FlickrNet;
using Cliffy.Common;
using Cliffy.Data.Mongo;

namespace Cliffy.FlickrMongoSync
{
    public class FlickrImporter : BaseMongoUtil
    {
        const string Sets = "72157606701085631,72157602427960981,72157594373088876,1157473,707782,72157594259055081,72157620904653603,72157621459686057,72157624473398430";
        const string GroupId = "31386902@N00";
        const string UserId = "22671681@N00";
        const string GroupSetId = "Pool";
        const int PerCall = 50;

        public void Import()
        {
            var db = GetDatabase();

            var sets = db.GetCollection("sets_");
            var photos = db.GetCollection("photographs_");

            LoadGroup(sets, photos);
            LoadSets(sets, photos, Sets.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

            db.RenameCollection("photographs_", "photographs", true);
            db.RenameCollection("sets_", "sets", true);
        }

        private void LoadSets(MongoCollection<BsonDocument> sets, MongoCollection<BsonDocument> photos, string[] toLoad)
        {
            foreach (var set in toLoad)
            {
                LoadSet(sets, photos, set);
            }
        }

        private void LoadSet(MongoCollection<BsonDocument> sets, MongoCollection<BsonDocument> photos, string set)
        {
			var flickr = new FlickrNet.Flickr();
            var info = flickr.PhotosetsGetInfo(set);

			var photoSet = new FlickrImporterPhotographs
            {
                DefaultPhotoId = info.PrimaryPhotoId,
                Description = info.Description,
                Id = info.PhotosetId,
                Title = info.Title,
                Total = Convert.ToInt64(info.NumberOfPhotos)
            };

            var pool = flickr.PhotosetsGetPhotos(set, PhotoSearchExtras.All, PrivacyFilter.None);
            foreach (var item in pool)
            {
                var photograph = LoadPhotograph(item);
                photograph.SetId = set;

                photos.Insert(photograph);

				photoSet.AddTags(photograph);
            }

            sets.Insert(photoSet);
        }

        private void LoadGroup(MongoCollection<BsonDocument> sets, MongoCollection<BsonDocument> photos)
        {
			var group = new FlickrImporterPhotographs
            {
                Description = "Shared Group Pool",
                Id = GroupSetId,
                Title = "Shared Group Pool"
            };

			var flickr = new FlickrNet.Flickr();
            var pageCount = int.MaxValue;
            for (var i = 1; i < pageCount; i++)
            {
                var pool = flickr.GroupsPoolsGetPhotos(GroupId, string.Empty, string.Empty, PhotoSearchExtras.Tags | PhotoSearchExtras.DateUploaded | PhotoSearchExtras.Media, i, PerCall);
				var photographs = LoadGroup(photos, pool);

				foreach (var photograph in photographs) {
					group.AddTags (photograph);
				}

                if (pageCount == int.MaxValue)
                {
                    group.Total = Convert.ToInt64(pool.Total);
                    pageCount = pool.Pages;
                    if (pageCount == 1)
                    {
                        break;
                    }
                }
            }

            sets.Insert(group);
        }

		private List<Photograph> LoadGroup(MongoCollection<BsonDocument> table, PhotoCollection photos)
        {
			var output = new List<Photograph>();

            foreach (var photo in photos)
            {
                var photograph = LoadPhotograph(photo);
                photograph.SetId = GroupSetId;
                table.Insert(photograph);

				output.Add (photograph);
            }

            return output;
        }

        private Photograph LoadPhotograph(Photo photo)
        {
            var output = new Photograph
            {
                Id = ObjectId.GenerateNewId(),
		DateSaved = photo.DateUploaded,
                Description = photo.Description,
                PhotoId = photo.PhotoId,
                Media = photo.Media,
                Photographer = photo.OwnerName,
                Secret = photo.Secret,
                Tags = new List<string>(),
                Title = photo.Title,
                UriSizes = new List<PhotographUri>(),
                UriSource = photo.WebUrl
            };

            output.Tags.AddRange(photo.Tags);

            output.UriSizes.Add(
                new PhotographUri { Size = ImageSize.Thumbnail, Uri = photo.SquareThumbnailUrl }
                );

            output.UriSizes.Add(
                new PhotographUri { Size = ImageSize.Small, Uri = photo.SmallUrl }
                );

            output.UriSizes.Add(
                new PhotographUri { Size = ImageSize.Medium, Uri = photo.MediumUrl }
                );

            output.UriSizes.Add(
                new PhotographUri { Size = ImageSize.Large, Uri = photo.LargeUrl }
                );

            output.UriSizes.Add(
                new PhotographUri { Size = ImageSize.Original, Uri = photo.OriginalUrl }
                );

            return output;
        }
    }
}

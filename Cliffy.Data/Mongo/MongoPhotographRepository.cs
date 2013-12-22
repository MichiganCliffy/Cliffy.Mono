using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Cliffy.Common;
using Cliffy.Data;

namespace Cliffy.Data.Mongo
{
    public class MongoPhotographRepository : IPhotographRepository
    {
        public const string TablePhotographs = "photographs";
        public const string TableSets = "sets";

        public Photographs GetAlbum(string[] tags, string setId)
        {
            if (string.IsNullOrWhiteSpace(setId))
            {
                setId = "Pool";
            }

            var db = GetDatabase();

            var info = GetAlbumInfo(db, setId);
            if (info != null)
            {
                var photographs = GetAlbumPhotographs(db, tags, setId);
				info.AddRange(photographs);
            }

			return info;
        }

		private MongoPhotographs GetAlbumInfo(MongoDatabase db, string setId)
        {
            var table = db.GetCollection(TableSets);
			var query = table.AsQueryable<MongoPhotographs>().Where(x => x.Id == setId);
            return query.FirstOrDefault();
        }

        private List<Photograph> GetAlbumPhotographs(MongoDatabase db, string[] tags, string setId)
        {
            var table = db.GetCollection(TablePhotographs);

			var output = table.AsQueryable<Photograph>().Where(x => x.SetId == setId).OrderByDescending(x => x.DateSaved).ToList();
            if ((tags != null) && (tags.Length > 0))
            {
                output = output.Where(x => x.Tags.Any(y => tags.Contains(y))).ToList();
            }

            return output;
        }

        public Photographs GetPhotographs(string[] tags, int count = 25, int page = 0)
        {
            var db = GetDatabase();

			var output = new MongoPhotographs();

            var info = GetAlbumInfo(db, "Pool");
            if (info != null)
            {
                var album = GetAlbumPhotographs(db, tags, "Pool");

                if (tags != null && tags.Any())
                {
					info.Total = album.Count;
                }

                var photographs = album.Skip(page * count).Take(count).ToList();
				info.AddRange(photographs);
            }

            return output;
        }

        public Photograph GetPhotograph(string id)
        {
            // split the id into it's parts
            var parts = id.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            var fields = new Dictionary<string, string>();
            fields.Add("PhotoId", parts[0]);
            fields.Add("Secret", parts[1]);

            var query = new QueryDocument(fields);

            var db = GetDatabase();
            var photos = db.GetCollection(TablePhotographs);
            var photo = photos.FindOneAs<Photograph>(query);
            return photo;
        }

        private MongoClient GetClient()
        {
            var connection = ConfigurationManager.ConnectionStrings["MongoDb"];
            if (connection != null)
            {
                return new MongoClient(connection.ConnectionString);
            }

            return new MongoClient();
        }

        private MongoServer GetServer()
        {
            var client = GetClient();
            return client.GetServer();
        }

        private MongoDatabase GetDatabase()
        {
            var server = GetServer();
            return server.GetDatabase("cliffy");
        }
    }
}
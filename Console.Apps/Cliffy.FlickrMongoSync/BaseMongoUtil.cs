using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cliffy.FlickrMongoSync
{
    public abstract class BaseMongoUtil
    {
        protected MongoDatabase GetDatabase()
        {
            var server = GetServer();
            return server.GetDatabase("cliffy");
        }

        protected MongoServer GetServer()
        {
            var client = GetClient();
            return client.GetServer();
        }

        protected MongoClient GetClient()
        {
            var connection = ConfigurationManager.ConnectionStrings["MongoDb"];
            if (connection != null)
            {
                if (!string.IsNullOrWhiteSpace(connection.ConnectionString))
                {
                    return new MongoClient(connection.ConnectionString);
                }
            }

            // Assume default local
            return new MongoClient();
        }
    }
}

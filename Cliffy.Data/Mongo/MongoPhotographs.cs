using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cliffy.Common;

namespace Cliffy.Data.Mongo
{
	public class MongoPhotographs : Photographs
    {
        public string Id { get; set; }

		public string DefaultPhotoId { get; set; }
	}
}

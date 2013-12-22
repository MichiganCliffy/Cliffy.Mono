using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliffy.FlickrMongoSync
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                switch (args[0].ToLower())
                {
                    case "import":
                        var importer = new FlickrImporter();
                        importer.Import();
                        break;
                }
            }
            else
            {
                var importer = new FlickrImporter();
                importer.Import();
            }
        }
    }
}
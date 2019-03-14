using ContentApi.Domain;
using ContentApi.Helpers;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ContentApi.Database
{
    public class MovieService
    {
        private readonly IMongoCollection<Movie> movies;
        private readonly IGridFSBucket fs;
        public MovieService(string mongoUrl)
        {
            var client = new MongoClient(mongoUrl);
            var database = client.GetDatabase("MovieStore");
            this.fs = new GridFSBucket(database);
            this.movies = database.GetCollection<Movie>("Movies");
        }

        public string Post(Movie movie)
        {
            movies.InsertOne(movie);
            var fileName = GriFsHelper.CreateFileName(ContentType.Movie, FileType.CoverImage, movie.Name);
            var id = fs.UploadFromStream(fileName, movie.CoverImage);
            return id.ToString();
        }

        public Movie GetById(string id)
        {
            var movie = movies.Find(m => m.Id == id).FirstOrDefault();
            var fileName = GriFsHelper.CreateFileName(ContentType.Movie, FileType.CoverImage, movie.Name);

            using (Stream stream = new MemoryStream())
            {
                fs.DownloadToStreamByName(fileName, stream);

                movie.CoverImage = stream;
            }

            return movie;
        }
    }
}

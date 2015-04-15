using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MDL_Exercise_2
{
    class Program
    {
        static void Main(string[] args)
        {
            // New politics artist id:3RbyaF3Pq6iDUKNp04AIcU
            // Their most recent album is 'A Bad Girl in Harlem'
            Console.WriteLine("New Politics latest album: " + GetLatestAlbumByArtist("New Politics"));
            Console.WriteLine();
            while (true)
            {
                Console.WriteLine("Enter an artist to find their latest album...");
                try
                {
                    Console.WriteLine(GetLatestAlbumByArtist(Console.ReadLine()));
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Artist not found");
                }
            }

        }

        static string GetLatestAlbumByArtist(string artist)
        {
            string json = fetchData("https://api.spotify.com/v1/search?type=artist&market=US&limit=1&q=" + artist);
            JObject artistInfo = JObject.Parse(json);
            string artistID = artistInfo["artists"]["items"][0]["id"].ToString();
            if (String.IsNullOrEmpty(artistID)) { throw new ArgumentException("Artist not found."); }
            // use the id to look up their latest album
            json = fetchData("https://api.spotify.com/v1/artists/" + artistID + "/albums?album_type=album&market=US&limit=1");
            JObject albumInfo = JObject.Parse(json);
            return albumInfo["items"][0]["name"].ToString();
        }

        static string fetchData(string url)
        {
            try
            {
                using (var client = new WebClient())
                {
                    return client.DownloadString(url);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error connecting to Server");
            }
        }
    }
}

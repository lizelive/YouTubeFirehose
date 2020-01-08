using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace YouTubeFirehose
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("YouTube Data API: Search");
            Console.WriteLine("========================");

            try
            {
                new Program().Run().Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }


        string pageToken = null;//"CDIQAA";

        private async Task Run()
        {



            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyAoVHFq8E66s8LYgPdye6iNBaBa5Nez8ic",
                ApplicationName = this.GetType().ToString()
            });


            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = ""; // Replace with your search term.
            searchListRequest.MaxResults = 50;



            var targetTimeWidth = TimeSpan.FromSeconds(60);


            var now = DateTime.Parse("2020-01-08T04:38Z");
            now = DateTime.Now;
            var targetTimeStart =  now - TimeSpan.FromDays(1);
            var targetTimeEnd = targetTimeStart + targetTimeWidth;



            
            searchListRequest.Order = SearchResource.ListRequest.OrderEnum.Date;
            searchListRequest.RelevanceLanguage = "en";
            searchListRequest.RegionCode = "us";

            searchListRequest.PageToken = pageToken;
            //specifying this results in livestreams not videos
            if (false)
            {
                searchListRequest.Type = "video";
                searchListRequest.EventType = SearchResource.ListRequest.EventTypeEnum.Completed;
            }

            searchListRequest.PublishedAfter = targetTimeStart;
            searchListRequest.PublishedBefore = targetTimeEnd;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();

            List<string> videos = new List<string>();
            List<string> channels = new List<string>();
            List<string> playlists = new List<string>();


            Console.WriteLine($"found {searchListResponse.PageInfo.TotalResults} videos but retrived {searchListResponse.Items.Count} in {targetTimeWidth} starting at targetTimeStart with {searchListResponse.NextPageToken} token");


            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            foreach (var searchResult in searchListResponse.Items)
            {
                if(searchResult.Id.Kind== "youtube#video")
                {

                        videos.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.VideoId));
                }
            }

            Console.WriteLine(String.Format("Videos:\n{0}\n", string.Join("\n", videos)));
            Console.WriteLine(String.Format("Channels:\n{0}\n", string.Join("\n", channels)));
            Console.WriteLine(String.Format("Playlists:\n{0}\n", string.Join("\n", playlists)));
        }
    }
}

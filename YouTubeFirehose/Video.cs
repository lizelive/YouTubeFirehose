using System;
using System.Collections.Generic;
using System.Text;

namespace YouTubeFirehose
{
    class Video
    {
        /// <summary>
        /// The value that YouTube uses to uniquely identify the channel that published the resource that the search result identifies.
        /// </summary>
        public string ChannelId { get; set; }

        /// <summary>
        /// A description of the search result.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// It indicates if the resource (video or channel) has upcoming/active live broadcast
        //  content. Or it's "none" if there is not any upcoming/active live broadcasts.
        /// </summary>
        public string LiveBroadcastContent { get; set; }

        /// <summary>
        /// System.DateTime representation of Google.Apis.YouTube.v3.Data.SearchResultSnippet.PublishedAtRaw.
        /// </summary>
        public DateTime PublishedAt { get; set; }
        //
        // Summary:
        //     A map of thumbnail images associated with the search result. For each object
        //     in the map, the key is the name of the thumbnail image, and the value is an object
        //     that contains other information about the thumbnail.
        public string ThumbnailUrl { get; set; }


        /// <summary>
        /// The title of the search result.
        /// </summary>
        public string Title { get; set; }
        //
        // Summary:
        //     The ETag of the item.
        public string ETag { get; set; }
    }
}

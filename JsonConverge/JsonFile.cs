using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonConverge
{
    public class JsonFile
    {
        public List<Participant> participants { get; set; }
        public List<Message> messages { get; set; }
        public string title { get; set; }
    }

    public class Message
    {
        public string sender_name { get; set; }
        public long timestamp_ms { get; set; }
        public string content { get; set; }
        public Sticker sticker { get; set; }
        public List<Photo> photos { get; set; }
        public List<Gif> gifs { get; set; }
        public List<Video> videos { get; set; }
        public List<Reaction> reactions { get; set; }
    }

    public class Participant
    {
        public string name { get; set; }
    }

    public class Sticker
    {
        public string uri { get; set; }
    }
    public class Thumbnail
    {
        public string uri { get; set; }
    }
    public class Video
    {
        public string uri { get; set; }
        public long creation_timestamp { get; set; }
        public Thumbnail thumbnail { get; set; }
    }

    public class Gif
    {
        public string uri { get; set; }
    }

    public class Photo
    {
        public string uri { get; set; }
        public long creation_timestamp { get; set; }
    }

    public class Reaction
    {
        public string reaction { get; set; }
        public string Participant { get; set; }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Movies.Core.Entities
{
    public class Search
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Search_Token { get; set; }
        public string ImdbID { get; set; }
        public long Processing_Time_Ms { get; set; }
        public DateTime Timestamp { get; set; }
        public string Ip_Address { get; set; }
    }
}

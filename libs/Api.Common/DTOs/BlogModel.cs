using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Api.Common.DTOs
{
    public class BlogModel
    {
        [BsonRequired]
        [BsonElement("title")]
        [JsonProperty("title")]
        public virtual string? Title { get; set; }

        [BsonRequired]
        [BsonElement("content")]
        [JsonProperty("content")]
        public virtual string? Content { get; set; }
    }
}
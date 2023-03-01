using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Api.Common.Models
{
    public class Blog : BaseEntity
    {
        public Blog()
        {
            BlogId=Guid.NewGuid().ToString("N");
        }
        [BsonId]
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        [BsonElement("blog_id")]
        [JsonProperty("blog_id")]
        public virtual string? BlogId { get; set; }

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
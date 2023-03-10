using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Api.Common.Models
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedDate=DateTime.Now;
            ModifiedDate=DateTime.Now;
        }
        [JsonProperty("created_date")]
        [BsonElement("created_date")]
        public virtual DateTime CreatedDate { get; set; }
        
        [JsonProperty("modified_date")]
        [BsonElement("modified_date")]
        public virtual DateTime ModifiedDate { get; set; }
    }
}
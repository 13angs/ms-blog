using Api.Common.Interfaces;
using Api.Common.Models;
using Newtonsoft.Json;

namespace Api.Common.DTOs
{
  public class BlogMessageCollectorModel : Blog, IMessageCollector
  {
    [JsonProperty("message_type")]
    public string? MessageType { get; set; }
  }
}
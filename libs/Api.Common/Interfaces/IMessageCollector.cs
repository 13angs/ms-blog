using Newtonsoft.Json;

namespace Api.Common.Interfaces
{
    public interface IMessageCollector
    {
        public string? MessageType  { get; set; }
    }
}
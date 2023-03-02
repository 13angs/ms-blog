namespace Api.Common.Interfaces
{
    public interface IRealtime
    {
        public Task Invoke(string url, string from, string message);
        public Task ListenOn(string url, string method);
    }
}
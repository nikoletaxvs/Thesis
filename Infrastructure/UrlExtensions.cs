namespace ThesisOct2023.Infrastructure
{
    public static class UrlExtensions
    {
        /*The PathAndQuery extension method operates on the HttpRequest class,
        which ASP.NET Core uses to describe an HTTP request. The extension
        method generates a URL*/
        public static string PathAndQuery(this HttpRequest request) =>
            request.QueryString.HasValue
            ? $"{request.Path}{request.QueryString}"
            : request.Path.ToString();
    }
}

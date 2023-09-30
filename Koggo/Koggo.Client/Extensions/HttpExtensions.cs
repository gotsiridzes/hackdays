namespace Koggo.Client.Extensions;

public static class HttpExtensions
{
    public static void AddJwtToken(this HttpResponse response, string token)
    {
        response.Cookies.Append("JwtToken", token);
    }

    public static void RemoveJwtToken(this HttpResponse response)
    {
        response.Cookies.Delete("JwtToken");
    }

    public static string? GetJwtToken(this HttpRequest request)
    {
        return request.Cookies["JwtToken"];
    }
}
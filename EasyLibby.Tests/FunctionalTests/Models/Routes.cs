namespace EasyLibby.Tests.FunctionalTests.Models;

public static class Routes
{
    private const string BaseRoute = "";

    public static class Books
    {
        private const string BaseRoute = Routes.BaseRoute + "/books";

        public const string Create = BaseRoute;

        public static string List() => BaseRoute;

        public static string Get(int id) => $"{BaseRoute}/{id}";

        public static string Delete(int id) => $"{BaseRoute}/{id}";

        public static string Update(int id) => $"{BaseRoute}/{id}";
    }
}

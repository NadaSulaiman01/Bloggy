namespace Bloggy.Domain.Shared
{
    //TODO: use a resource file for localization of error messages.
    public static class ErrorMessages
    {
        private static readonly Dictionary<string, string> _map = new()
        {
            { ErrorCodes.BlogNotFound, "The requested blog was not found." },
            { ErrorCodes.DuplicateBlogTitle, "A blog with the same title already exists." },
            { ErrorCodes.ForbiddenAction, "You are not authorized to perform this action." }
        };

        public static string Get(string code)
        {
            if (code is null) return "An unknown error occurred.";
            return _map.TryGetValue(code, out var msg) ? msg : "An unknown error occurred.";
        }
    }
}

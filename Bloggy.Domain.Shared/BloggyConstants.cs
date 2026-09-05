namespace Bloggy.Domain.Shared
{
    public static class StringLengths
    {
        public const int ShortTitleLength = 100;
        public const int ShortContentLength = 500;
        public const int LongContentLength = 2000;
    }
    public static class ErrorCodes
    {
        public const string BlogNotFound = "Bloggy:E000001";
        public const string DuplicateBlogTitle = "Bloggy:E000002";
        public const string ForbiddenAction = "Bloggy:E000003";
    }
}

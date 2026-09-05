namespace Bloggy.Application.Contracts.Common
{
    public interface ICurrentUser
    {
        Guid? Id { get; }
        string? Name { get; }
        string? Email { get; }
    }
}

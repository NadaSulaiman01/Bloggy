namespace Bloggy.Domain.Common
{
    public interface IAuditedObject
    {
        DateTime CreationTime { get; }
        Guid? CreatorId { get; }
    }
}

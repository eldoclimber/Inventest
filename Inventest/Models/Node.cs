namespace Inventest.Models
{
    public record Node(
      Guid TenantId,
      Guid NodeId,
      string Name,
      string NodeType,
      Guid? ParentId,
      List<Guid> Path
    );
}

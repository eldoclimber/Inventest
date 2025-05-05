namespace Inventest.Models
{
    public record ItemPicked(
        Guid TenantId,
        Guid NodeId,
        DateTime OccurredOn,
        string Sku,
        int Quantity,
        List<Guid> Path
    ) : InventoryEvent
    {
        public override string Id =>
            $"{TenantId}:{NodeId}:{OccurredOn:O}:{Sku}";
        public override string PartitionKey =>
            $"{TenantId}#{Path[PartitionLevel]}";

        private const int PartitionLevel = 1; // e.g. building level
    }
}
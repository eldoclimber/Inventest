namespace Inventest.Models
{
    public abstract record InventoryEvent
    {
        public Guid TenantId { get; init; }
        public string Id { get; init; }
        public Guid NodeId { get; init; }
        public DateTime OccurredOn { get; init; }
        public string PartitionKey { get; init; }
    }
}

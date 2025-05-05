namespace Inventest.Models
{
    public record BinInventory(
        Guid NodeId,
        string Sku,
        int Quantity
    )
    {
        public string Id => $"{NodeId}:{Sku}";
        public Guid TenantId { get; init; } // This will be needed as the partition key
    }
}
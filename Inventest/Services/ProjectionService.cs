using Azure.Cosmos;
using Inventest.Models;
using Inventest.Services;
using InventoryApp.Models;
using Microsoft.Azure.Cosmos;

public class ProjectionService
{
    private readonly CosmosContainer _projContainer;
    public ProjectionService(CosmosDbService cosmos) =>
      _projContainer = cosmos.Projections;

    public async Task UpsertCurrentInventoryAsync(
      Guid tenantId, Guid nodeId, string sku, int qtyDelta)
    {
        var id = $"{tenantId}:{nodeId}:{sku}";
        try
        {
            var existing = await _projContainer.ReadItemAsync<BinInventory>(
              id, new PartitionKey(tenantId.ToString())
            );
            var inv = existing.Resource with
            {
                Quantity = existing.Resource.Quantity + qtyDelta
            };
            await _projContainer.ReplaceItemAsync(inv, id, new PartitionKey(tenantId.ToString()));
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            var inv = new BinInventory(nodeId, sku, qtyDelta);
            await _projContainer.CreateItemAsync(inv, new PartitionKey(tenantId.ToString()));
        }
    }
}

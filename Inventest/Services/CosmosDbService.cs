using Microsoft.Azure.Cosmos;  // This is the correct namespace
using Microsoft.Extensions.Configuration;

namespace Inventest.Services  // Update to match your project name
{
    public class CosmosDbService
    {
        public CosmosClient Client { get; }
        public Database Database { get; }  // Changed from CosmosDatabase
        public Container Nodes { get; }  // Changed from CosmosContainer
        public Container Events { get; }  // Changed from CosmosContainer
        public Container Projections { get; }  // Changed from CosmosContainer

        public CosmosDbService(IConfiguration config)
        {
            var cos = config.GetSection("Cosmos");
            Client = new CosmosClient(
              cos["AccountEndpoint"],
              cos["AccountKey"],
              new CosmosClientOptions { ApplicationName = "Inventest" }  // Updated application name
            );

            Database = Client.CreateDatabaseIfNotExistsAsync(cos["DatabaseId"]).GetAwaiter().GetResult();

            // Create containers
            Nodes = Database.CreateContainerIfNotExistsAsync(
              id: cos["Containers:Nodes"],
              partitionKeyPath: "/tenantId",
              throughput: 400
            ).GetAwaiter().GetResult();  // Note: Container is directly returned

            Events = Database.CreateContainerIfNotExistsAsync(
              id: cos["Containers:Events"],
              partitionKeyPath: "/partitionKey",
              throughput: 400
            ).GetAwaiter().GetResult();  // No need for .Container property

            Projections = Database.CreateContainerIfNotExistsAsync(
              id: cos["Containers:Projections"],
              partitionKeyPath: "/tenantId",
              throughput: 400
            ).GetAwaiter().GetResult();  // No need for .Container property
        }
    }
}
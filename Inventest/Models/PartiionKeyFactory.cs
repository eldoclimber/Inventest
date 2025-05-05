// PartitionKeyFactory.cs
using Inventest.Models;

public static class PartitionKeyFactory
{
    public static string For(Node node, int partitionLevel)
      => node.Path.ElementAt(partitionLevel).ToString();
}

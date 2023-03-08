using Staticsoft.PartitionedStorage.Abstractions;
using Staticsoft.SharpPass.Services;

namespace Staticsoft.SharpPass.Server;

public class StorageStatus : ServiceStatus
{
    readonly Partition<StatusPartition> Partition;

    public StorageStatus(Partitions partisions)
        => Partition = partisions.Get<StatusPartition>();

    public Task Check()
        => Partition.Scan();

    class StatusPartition { }
}
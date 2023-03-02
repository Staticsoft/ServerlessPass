using Staticsoft.PartitionedStorage.Abstractions;

namespace Staticsoft.SharpPass.Server;

public class Documents
{
    public readonly PartitionFactory<PasswordProfile> Profiles;

    public Documents(Partitions storage)
        => Profiles = storage.GetFactory<PasswordProfile>();
}

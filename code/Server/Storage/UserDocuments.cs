using Staticsoft.PartitionedStorage.Abstractions;
using Staticsoft.SharpPass.Authentication;

namespace Staticsoft.SharpPass.Server;

public class UserDocuments
{
    public readonly Partition<PasswordProfile> Profiles;

    public UserDocuments(Partitions storage, Identity identity)
        => Profiles = storage.GetFactory<PasswordProfile>().Get(identity.UserId);
}

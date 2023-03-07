using Staticsoft.PartitionedStorage.Abstractions;
using Staticsoft.SharpPass.Authentication;

namespace Staticsoft.SharpPass.Server;

public class UserDocuments
{
    public readonly Partition<PasswordProfilesDocument> Profiles;

    public UserDocuments(Partitions storage, Identity identity)
        => Profiles = storage.GetFactory<PasswordProfilesDocument>().Get(identity.UserId);
}

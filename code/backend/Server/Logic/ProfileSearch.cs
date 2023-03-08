using Staticsoft.PartitionedStorage.Abstractions;

namespace Staticsoft.SharpPass.Server;

public static class ProfileSearch
{
    public static (Item<PasswordProfilesDocument>, int) FindProfileDocument(this Item<PasswordProfilesDocument>[] documents, string profileId)
    {
        foreach (var document in documents)
        {
            var index = FindProfileIndex(document.Data, profileId);
            if (index != -1) return (document, index);
        }

        throw new NotSupportedException();
    }

    static int FindProfileIndex(PasswordProfilesDocument document, string profileId)
    {
        for (var i = 0; i < document.Profiles.Length; i++)
        {
            if (document.Profiles[i].id == profileId)
            {
                return i;
            }
        }
        return -1;
    }
}

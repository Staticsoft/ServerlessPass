using Staticsoft.PartitionedStorage.Abstractions;

namespace Staticsoft.SharpPass.Server;

public class DeletePasswordEndpoint : ParametrizedHttpEndpoint<EmptyRequest, DeletePasswordResponse>
{
    readonly UserProfiles Profiles;

    public DeletePasswordEndpoint(UserProfiles profiles)
        => Profiles = profiles;

    public async Task<DeletePasswordResponse> Execute(string passwordId, EmptyRequest request)
    {
        var profiles = await Profiles.Scan();
        var (profile, index) = FindProfile(profiles, passwordId);
        await Profiles.Save(new Item<PasswordProfilesDocument>()
        {
            Data = new PasswordProfilesDocument()
            {
                Profiles = RemoveProfile(profile.Data.Profiles, index)
            },
            Id = profile.Id,
            Version = profile.Version
        });
        return new();
    }

    static PasswordProfile[] RemoveProfile(PasswordProfile[] profiles, int index)
    {
        var left = profiles[..index];
        var right = profiles[(index + 1)..];
        var updatedProfiles = new PasswordProfile[left.Length + right.Length];
        Array.Copy(left, updatedProfiles, left.Length);
        Array.Copy(right, 0, updatedProfiles, left.Length, right.Length);
        return updatedProfiles;
    }

    static (Item<PasswordProfilesDocument>, int) FindProfile(Item<PasswordProfilesDocument>[] documents, string passwordId)
    {
        foreach (var document in documents)
        {
            var index = FindProfileIndex(document.Data, passwordId);
            if (index != -1) return (document, index);
        }

        throw new NotSupportedException();
    }

    static int FindProfileIndex(PasswordProfilesDocument document, string passwordId)
    {
        for (var i = 0; i < document.Profiles.Length; i++)
        {
            if (document.Profiles[i].id == passwordId)
            {
                return i;
            }
        }
        return -1;
    }
}

﻿namespace Staticsoft.ServerlessPass.Server;

public class PasswordProfilesIdGenerator
{
    public string Generate(DateTime date)
        => Generate(DateTime.MaxValue.Ticks - date.Ticks);

    static string Generate(long ticksTillMaxTime)
        => $"{ToGuid(ticksTillMaxTime)}";

    static Guid ToGuid(long ticks)
        => ToGuid($"{ticks:X16}");

    static Guid ToGuid(string ticks)
        => new($"00000000-0000-0000-{ticks[0..4]}-{ticks[4..16]}");
}

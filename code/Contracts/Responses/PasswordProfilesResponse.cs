﻿using System;

namespace Staticsoft.SharpPass.Contracts;

public class PasswordProfilesResponse
{
    public PasswordProfile[] Results { get; init; } = Array.Empty<PasswordProfile>();

    public int Count
        => Results.Length;

    public string? Previous
        => null;

    public string? Next
        => null;
}
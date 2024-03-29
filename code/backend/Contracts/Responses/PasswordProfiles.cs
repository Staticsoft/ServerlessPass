﻿using System;

namespace Staticsoft.ServerlessPass.Contracts;

public class PasswordProfiles
{
    public PasswordProfile[] results { get; init; } = Array.Empty<PasswordProfile>();

    public int count
        => results.Length;

    public string? previous
        => null;

    public string? next
        => null;
}

﻿using Microsoft.IdentityModel.Tokens;
using SyncUp.Data.Models;

namespace IntelliTect.SyncUp.Data.Models;

public class Group : TenantedBase
{
    public long GroupId { get; init; }

    [MaxLength(500)]
    public required string Name { get; set; }

    [MaxLength(500)]
    public required string SubTitle { get; set; }

    public ICollection<Post> Posts { get; }

    public ICollection<Event> Events { get; } = [];
}
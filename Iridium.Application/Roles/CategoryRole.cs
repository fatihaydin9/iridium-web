﻿using Iridium.Domain.Common;
using Iridium.Infrastructure.Attributes;

namespace Iridium.Application.Roles;

[RoleArea("Category")]
public abstract class CategoryRole : IRole
{
    [RoleName("Read Category Role")]
    public const string Read   = "Category.Read";

    [RoleName("Add Category Role")]
    public const string Insert = "Category.Insert";

    [RoleName("Update Category Role")]
    public const string Update = "Category.Update";

    [RoleName("Delete Category Role")]
    public const string Delete = "Category.Delete";
}

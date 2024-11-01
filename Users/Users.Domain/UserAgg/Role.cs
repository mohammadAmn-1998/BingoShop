﻿using Shared.Domain.SeedWorks.Base;

namespace Users.Domain.UserAgg;

public class Role : BaseEntity<int>
{

	public string Title { get; private set; }
	public List<Permission> Permissions { get; private set; }
	public List<UserRole> UserRoles { get; private set; }
	public Role(string title)
	{
		Title = title;
		Permissions = new();
		UserRoles = new();
	}
	public void Edit(string title)
	{
		Title = title;
	}
}
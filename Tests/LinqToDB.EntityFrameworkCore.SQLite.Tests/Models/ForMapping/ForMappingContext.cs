﻿using LinqToDB.EntityFrameworkCore.BaseTests.Models.ForMapping;
using Microsoft.EntityFrameworkCore;

namespace LinqToDB.EntityFrameworkCore.SQLite.Tests.Models.ForMapping
{
	public class ForMappingContext : ForMappingContextBase
	{
		public ForMappingContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<WithIdentity>(b =>
			{
				b.HasKey(e => e.Id);
			});

			modelBuilder.Entity<NoIdentity>(b =>
			{
				b.HasKey(e => e.Id);
			});
		}
	}
}

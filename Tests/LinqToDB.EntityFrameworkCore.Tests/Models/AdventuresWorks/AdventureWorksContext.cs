// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using LinqToDB.EntityFrameworkCore.Tests.Model;
using NUnit.Framework.Internal;

namespace Microsoft.EntityFrameworkCore.SqlAzure.Model
{
	public class AdventureWorksContext : DbContext
	{
		public AdventureWorksContext(DbContextOptions options)
			: base(options)
		{
		}

		public bool IsFilterNullable { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Address>(
				entity =>
				{
						entity.HasIndex(e => new { e.AddressLine1, e.AddressLine2, e.City, e.StateProvince, e.PostalCode, e.CountryRegion })
							.HasName("IX_Address_AddressLine1_AddressLine2_City_StateProvince_PostalCode_CountryRegion");

						entity.HasIndex(e => e.StateProvince)
							.HasName("IX_Address_StateProvince");

						entity.HasIndex(e => e.rowguid)
							.HasName("AK_Address_rowguid")
							.IsUnique();

						entity.Property(e => e.ModifiedDate)
							.HasColumnType("datetime")
							.HasDefaultValueSql("getdate()");

						entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
					});

			modelBuilder.Entity<Customer>(
				entity =>
					{
						entity.HasIndex(e => e.EmailAddress)
							.HasName("IX_Customer_EmailAddress");

						entity.HasIndex(e => e.rowguid)
							.HasName("AK_Customer_rowguid")
							.IsUnique();

						entity.Property(e => e.ModifiedDate)
							.HasColumnType("datetime")
							.HasDefaultValueSql("getdate()");

						entity.Property(e => e.PasswordHash).HasColumnType("varchar(128)");

						entity.Property(e => e.PasswordSalt).HasColumnType("varchar(10)");

						entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
					});

			modelBuilder.Entity<CustomerAddress>(
				entity =>
					{
						entity.HasKey(e => new { e.CustomerID, e.AddressID })
							.HasName("PK_CustomerAddress_CustomerID_AddressID");

						entity.HasIndex(e => e.rowguid)
							.HasName("AK_CustomerAddress_rowguid")
							.IsUnique();

						entity.Property(e => e.ModifiedDate)
							.HasColumnType("datetime")
							.HasDefaultValueSql("getdate()");

						entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
					});

			modelBuilder.Entity<Product>(
				entity =>
					{
						entity.HasIndex(e => e.ProductNumber)
							.HasName("AK_Product_ProductNumber")
							.IsUnique();

						entity.HasIndex(e => e.rowguid)
							.HasName("AK_Product_rowguid")
							.IsUnique();

						entity.HasIndex(e => e.Name)
							.HasName("AK_Product_Name")
							.IsUnique();

						entity.Property(e => e.DiscontinuedDate).HasColumnType("datetime");

						entity.Property(e => e.ListPrice).HasColumnType("money");

						entity.Property(e => e.ModifiedDate)
							.HasColumnType("datetime")
							.HasDefaultValueSql("getdate()");

						entity.Property(e => e.SellEndDate).HasColumnType("datetime");

						entity.Property(e => e.SellStartDate).HasColumnType("datetime");

						entity.Property(e => e.StandardCost).HasColumnType("money");

						entity.Property(e => e.ThumbNailPhoto).HasColumnType("varbinary(max)");

						entity.Property(e => e.Weight).HasColumnType("decimal");

						entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");

						entity.HasQueryFilter(e => !IsFilterNullable || e.Weight != null || EF.Property<decimal?>(e, "Weight") != null);
					});

			modelBuilder.Entity<ProductCategory>(
				entity =>
					{
						entity.HasIndex(e => e.Name)
							.HasName("AK_ProductCategory_Name")
							.IsUnique();

						entity.HasIndex(e => e.rowguid)
							.HasName("AK_ProductCategory_rowguid")
							.IsUnique();

						entity.Property(e => e.ModifiedDate)
							.HasColumnType("datetime")
							.HasDefaultValueSql("getdate()");

						entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
					});

			modelBuilder.Entity<ProductDescription>(
				entity =>
					{
						entity.HasIndex(e => e.rowguid)
							.HasName("AK_ProductDescription_rowguid")
							.IsUnique();

						entity.Property(e => e.ModifiedDate)
							.HasColumnType("datetime")
							.HasDefaultValueSql("getdate()");

						entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
					});

			modelBuilder.Entity<ProductModel>(
				entity =>
					{
						entity.HasIndex(e => e.Name)
							.HasName("AK_ProductModel_Name")
							.IsUnique();

						entity.HasIndex(e => e.rowguid)
							.HasName("AK_ProductModel_rowguid")
							.IsUnique();

						entity.Property(e => e.ModifiedDate)
							.HasColumnType("datetime")
							.HasDefaultValueSql("getdate()");

						entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
					});

			modelBuilder.Entity<ProductModelProductDescription>(
				entity =>
					{
						entity.HasKey(e => new { e.ProductModelID, e.ProductDescriptionID, e.Culture })
							.HasName("PK_ProductModelProductDescription_ProductModelID_ProductDescriptionID_Culture");

						entity.HasIndex(e => e.rowguid)
							.HasName("AK_ProductModelProductDescription_rowguid")
							.IsUnique();

						entity.Property(e => e.Culture).HasColumnType("nchar(6)");

						entity.Property(e => e.ModifiedDate)
							.HasColumnType("datetime")
							.HasDefaultValueSql("getdate()");

						entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
					});

			modelBuilder.Entity<SalesOrderDetail>(
				entity =>
					{
						entity.HasKey(e => new { e.SalesOrderID, e.SalesOrderDetailID })
							.HasName("PK_SalesOrderDetail_SalesOrderID_SalesOrderDetailID");

						entity.HasIndex(e => e.ProductID)
							.HasName("IX_SalesOrderDetail_ProductID");

						entity.HasIndex(e => e.rowguid)
							.HasName("AK_SalesOrderDetail_rowguid")
							.IsUnique();

						entity.Property(e => e.SalesOrderDetailID).ValueGeneratedOnAdd();

						entity.Property(e => e.LineTotal)
							.HasColumnType("numeric")
							.ValueGeneratedOnAddOrUpdate();

						entity.Property(e => e.ModifiedDate)
							.HasColumnType("datetime")
							.HasDefaultValueSql("getdate()");

						entity.Property(e => e.UnitPrice).HasColumnType("money");

						entity.Property(e => e.UnitPriceDiscount)
							.HasColumnType("money")
							.HasDefaultValueSql("0.0");

						entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
					});

			modelBuilder.Entity<SalesOrder>(
				entity =>
					{
						entity.HasKey(e => e.SalesOrderID)
							.HasName("PK_SalesOrderHeader_SalesOrderID");

						entity.HasIndex(e => e.CustomerID)
							.HasName("IX_SalesOrderHeader_CustomerID");

						entity.HasIndex(e => e.SalesOrderNumber)
							.HasName("AK_SalesOrderHeader_SalesOrderNumber")
							.IsUnique();

						entity.HasIndex(e => e.rowguid)
							.HasName("AK_SalesOrderHeader_rowguid")
							.IsUnique();

						entity.Property(e => e.SalesOrderID).ForSqlServerUseSequenceHiLo("SalesOrderNumber", "SalesLT");

						entity.Property(e => e.CreditCardApprovalCode).HasColumnType("varchar(15)");

						entity.Property(e => e.DueDate).HasColumnType("datetime");

						entity.Property(e => e.Freight)
							.HasColumnType("money")
							.HasDefaultValueSql("0.00");

						entity.Property(e => e.ModifiedDate)
							.HasColumnType("datetime")
							.HasDefaultValueSql("getdate()");

						entity.Property(e => e.OrderDate)
							.HasColumnType("datetime")
							.HasDefaultValueSql("getdate()");

						entity.Property(e => e.RevisionNumber).HasDefaultValueSql("0");

						entity.Property(e => e.SalesOrderNumber).ValueGeneratedOnAddOrUpdate();

						entity.Property(e => e.ShipDate).HasColumnType("datetime");

						entity.Property(e => e.Status).HasDefaultValueSql("1");

						entity.Property(e => e.SubTotal)
							.HasColumnType("money")
							.HasDefaultValueSql("0.00");

						entity.Property(e => e.TaxAmt)
							.HasColumnType("money")
							.HasDefaultValueSql("0.00");

						entity.Property(e => e.TotalDue)
							.HasColumnType("money")
							.ValueGeneratedOnAddOrUpdate();

						entity.Property(e => e.rowguid).HasDefaultValueSql("newid()");
					});

			modelBuilder.HasSequence<int>("SalesOrderNumber", "SalesLT");

			modelBuilder.HasDbFunction(() => TestFunctions.GetDate()).HasSchema("").HasName("GETDATE");
			modelBuilder.HasDbFunction(() => TestFunctions.Len(null)).HasSchema("").HasName("LEN");
		}

		public virtual DbSet<Address> Addresses { get; set; }
		public virtual DbSet<Customer> Customers { get; set; }
		public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<ProductCategory> ProductCategories { get; set; }
		public virtual DbSet<ProductDescription> ProductDescriptions { get; set; }
		public virtual DbSet<ProductModel> ProductModels { get; set; }
		public virtual DbSet<ProductModelProductDescription> ProductModelProductDescriptions { get; set; }
		public virtual DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }
		public virtual DbSet<SalesOrder> SalesOrders { get; set; }
	}
}

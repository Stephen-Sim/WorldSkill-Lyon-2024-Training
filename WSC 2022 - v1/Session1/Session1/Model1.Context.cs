﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Session1
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WSC2022SE_Session1Entities : DbContext
    {
        public WSC2022SE_Session1Entities()
            : base("name=WSC2022SE_Session1Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Amenity> Amenities { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Attraction> Attractions { get; set; }
        public virtual DbSet<CancellationPolicy> CancellationPolicies { get; set; }
        public virtual DbSet<CancellationRefundFee> CancellationRefundFees { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<DimDate> DimDates { get; set; }
        public virtual DbSet<ItemAmenity> ItemAmenities { get; set; }
        public virtual DbSet<ItemAttraction> ItemAttractions { get; set; }
        public virtual DbSet<ItemPicture> ItemPictures { get; set; }
        public virtual DbSet<ItemPrice> ItemPrices { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemType> ItemTypes { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<TransactionType> TransactionTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
    }
}

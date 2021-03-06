﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IndicoPacking.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class IndicoPackingEntities : DbContext
    {
        public IndicoPackingEntities()
            : base("name=IndicoPackingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Carton> Cartons { get; set; }
        public virtual DbSet<OrderDeatilItem> OrderDeatilItems { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Shipment> Shipments { get; set; }
        public virtual DbSet<ShipmentDetail> ShipmentDetails { get; set; }
        public virtual DbSet<ShipmentDetailCarton> ShipmentDetailCartons { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserStatu> UserStatus { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<DistributorClientAddress> DistributorClientAddresses { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceStatu> InvoiceStatus { get; set; }
        public virtual DbSet<Port> Ports { get; set; }
        public virtual DbSet<ShipmentMode> ShipmentModes { get; set; }
        public virtual DbSet<GetShipmentKeysView> GetShipmentKeysViews { get; set; }
        public virtual DbSet<GetInvoiceOrderDetailItemsWithQuatityBreakdown> GetInvoiceOrderDetailItemsWithQuatityBreakdowns { get; set; }
        public virtual DbSet<GetInvoiceOrderDetailItemsWithQuatityGroupByForFactory> GetInvoiceOrderDetailItemsWithQuatityGroupByForFactories { get; set; }
        public virtual DbSet<GetInvoiceOrderDetailItemsWithQuatityGroupByForIndiman> GetInvoiceOrderDetailItemsWithQuatityGroupByForIndimen { get; set; }
        public virtual DbSet<InvoiceDetailsView> InvoiceDetailsViews { get; set; }
        public virtual DbSet<GetWeeklyAddressDetail> GetWeeklyAddressDetails { get; set; }
        public virtual DbSet<UserDetailsView> UserDetailsViews { get; set; }
        public virtual DbSet<GetWeeklyAddressDetailsByHSCode> GetWeeklyAddressDetailsByHSCodes { get; set; }
        public virtual DbSet<GetWeeklyAddressDetailsByDistributor> GetWeeklyAddressDetailsByDistributors { get; set; }
        public virtual DbSet<GetWeeklyAddressDetailsByDistributorForIndiman> GetWeeklyAddressDetailsByDistributorForIndimen { get; set; }
        public virtual DbSet<InvoiceHeaderDetailsView> InvoiceHeaderDetailsViews { get; set; }
        public virtual DbSet<GetCartonLabelInfo> GetCartonLabelInfoes { get; set; }
        public virtual DbSet<OrderDetailsFromIndico> OrderDetailsFromIndicoes { get; set; }
        public virtual DbSet<GetOrderDetaildForGivenWeekView> GetOrderDetaildForGivenWeekViews { get; set; }
    
        public virtual int SynchroniseOrders(Nullable<int> weekNo, Nullable<System.DateTime> weekEndDate)
        {
            var weekNoParameter = weekNo.HasValue ?
                new ObjectParameter("WeekNo", weekNo) :
                new ObjectParameter("WeekNo", typeof(int));
    
            var weekEndDateParameter = weekEndDate.HasValue ?
                new ObjectParameter("WeekEndDate", weekEndDate) :
                new ObjectParameter("WeekEndDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SynchroniseOrders", weekNoParameter, weekEndDateParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> GetOrderDetailsQuatityCount(Nullable<int> weekNo, Nullable<System.DateTime> weekEndDate)
        {
            var weekNoParameter = weekNo.HasValue ?
                new ObjectParameter("WeekNo", weekNo) :
                new ObjectParameter("WeekNo", typeof(int));
    
            var weekEndDateParameter = weekEndDate.HasValue ?
                new ObjectParameter("WeekEndDate", weekEndDate) :
                new ObjectParameter("WeekEndDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("GetOrderDetailsQuatityCount", weekNoParameter, weekEndDateParameter);
        }
    
        public virtual int SynchronizeOrderDetails()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SynchronizeOrderDetails");
        }
    
        public virtual ObjectResult<SPC_GetDetailForPackingList_Result> GetDetailForPackingList(Nullable<int> p_ShipmentDetailId)
        {
            var p_ShipmentDetailIdParameter = p_ShipmentDetailId.HasValue ?
                new ObjectParameter("P_ShipmentDetailId", p_ShipmentDetailId) :
                new ObjectParameter("P_ShipmentDetailId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPC_GetDetailForPackingList_Result>("GetDetailForPackingList", p_ShipmentDetailIdParameter);
        }
    
        public virtual ObjectResult<SPC_GetPackingListDetails_Result> GetPackingListDetails(Nullable<int> p_ShipmentDetailId)
        {
            var p_ShipmentDetailIdParameter = p_ShipmentDetailId.HasValue ?
                new ObjectParameter("P_ShipmentDetailId", p_ShipmentDetailId) :
                new ObjectParameter("P_ShipmentDetailId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPC_GetPackingListDetails_Result>("GetPackingListDetails", p_ShipmentDetailIdParameter);
        }
    
        public virtual ObjectResult<GetItemsOfInvoiceForMyOb_Result> GetItemsOfInvoiceForMyOb(Nullable<int> p_InvoiceID)
        {
            var p_InvoiceIDParameter = p_InvoiceID.HasValue ?
                new ObjectParameter("P_InvoiceID", p_InvoiceID) :
                new ObjectParameter("P_InvoiceID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetItemsOfInvoiceForMyOb_Result>("GetItemsOfInvoiceForMyOb", p_InvoiceIDParameter);
        }
    
        public virtual int SPC_GetPackingListDetailForGivenCartons(Nullable<int> p_ShipmentDetailId, string p_ShipmentDetailCartons)
        {
            var p_ShipmentDetailIdParameter = p_ShipmentDetailId.HasValue ?
                new ObjectParameter("P_ShipmentDetailId", p_ShipmentDetailId) :
                new ObjectParameter("P_ShipmentDetailId", typeof(int));
    
            var p_ShipmentDetailCartonsParameter = p_ShipmentDetailCartons != null ?
                new ObjectParameter("P_ShipmentDetailCartons", p_ShipmentDetailCartons) :
                new ObjectParameter("P_ShipmentDetailCartons", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SPC_GetPackingListDetailForGivenCartons", p_ShipmentDetailIdParameter, p_ShipmentDetailCartonsParameter);
        }
    
        [DbFunction("IndicoPackingEntities", "splitstring")]
        public virtual IQueryable<string> splitstring(string stringToSplit)
        {
            var stringToSplitParameter = stringToSplit != null ?
                new ObjectParameter("stringToSplit", stringToSplit) :
                new ObjectParameter("stringToSplit", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<string>("[IndicoPackingEntities].[splitstring](@stringToSplit)", stringToSplitParameter);
        }
    }
}

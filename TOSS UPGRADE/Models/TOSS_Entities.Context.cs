﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TOSS_UPGRADE.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DB_TOSSEntities : DbContext
    {
        public DB_TOSSEntities()
            : base("name=DB_TOSSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AccountableForm_Assignment> AccountableForm_Assignment { get; set; }
        public virtual DbSet<AccountableForm_Description> AccountableForm_Description { get; set; }
        public virtual DbSet<AccountableForm_Inventory> AccountableForm_Inventory { get; set; }
        public virtual DbSet<AccountableFormTable> AccountableFormTables { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<BankAccount_AccountCode> BankAccount_AccountCode { get; set; }
        public virtual DbSet<BankAccount_AccountType> BankAccount_AccountType { get; set; }
        public virtual DbSet<BankAccountTable> BankAccountTables { get; set; }
        public virtual DbSet<BankTable> BankTables { get; set; }
        public virtual DbSet<CheckInventoryTable> CheckInventoryTables { get; set; }
        public virtual DbSet<CheckMaintenanceTable> CheckMaintenanceTables { get; set; }
        public virtual DbSet<CollectorTable> CollectorTables { get; set; }
        public virtual DbSet<DVTypeTable> DVTypeTables { get; set; }
        public virtual DbSet<FundType_FundName> FundType_FundName { get; set; }
        public virtual DbSet<FundType_FundType> FundType_FundType { get; set; }
        public virtual DbSet<FundType_FundTypeTable> FundType_FundTypeTable { get; set; }
        public virtual DbSet<IRA_Table> IRA_Table { get; set; }
        public virtual DbSet<Level1Modules> Level1Modules { get; set; }
        public virtual DbSet<Level2Modules> Level2Modules { get; set; }
        public virtual DbSet<Level3Modules> Level3Modules { get; set; }
        public virtual DbSet<MemoAccClass_AccountCode> MemoAccClass_AccountCode { get; set; }
        public virtual DbSet<MemoAccClass_RevisionYr> MemoAccClass_RevisionYr { get; set; }
        public virtual DbSet<MemoAccClassTable> MemoAccClassTables { get; set; }
        public virtual DbSet<ParentModule> ParentModules { get; set; }
        public virtual DbSet<PersonalInformation> PersonalInformations { get; set; }
        public virtual DbSet<SignatoriesTable> SignatoriesTables { get; set; }
        public virtual DbSet<Signatory_DepartmentTable> Signatory_DepartmentTable { get; set; }
        public virtual DbSet<Signatory_PositionTable> Signatory_PositionTable { get; set; }
    
        public virtual int SP_AccountableFormInvtList(string sQLStatement)
        {
            var sQLStatementParameter = sQLStatement != null ?
                new ObjectParameter("SQLStatement", sQLStatement) :
                new ObjectParameter("SQLStatement", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_AccountableFormInvtList", sQLStatementParameter);
        }
    
        public virtual int SP_AccountableFormList(string sQLStatement)
        {
            var sQLStatementParameter = sQLStatement != null ?
                new ObjectParameter("SQLStatement", sQLStatement) :
                new ObjectParameter("SQLStatement", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_AccountableFormList", sQLStatementParameter);
        }
    
        public virtual int SP_AccountTypeList(string sQLStatement)
        {
            var sQLStatementParameter = sQLStatement != null ?
                new ObjectParameter("SQLStatement", sQLStatement) :
                new ObjectParameter("SQLStatement", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_AccountTypeList", sQLStatementParameter);
        }
    
        public virtual int SP_AFDescriptionList(string sQLStatement)
        {
            var sQLStatementParameter = sQLStatement != null ?
                new ObjectParameter("SQLStatement", sQLStatement) :
                new ObjectParameter("SQLStatement", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_AFDescriptionList", sQLStatementParameter);
        }
    
        public virtual int SP_BankAccountTable(string sQLStatement)
        {
            var sQLStatementParameter = sQLStatement != null ?
                new ObjectParameter("SQLStatement", sQLStatement) :
                new ObjectParameter("SQLStatement", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_BankAccountTable", sQLStatementParameter);
        }
    
        public virtual int SP_BankTable(string sQLStatement)
        {
            var sQLStatementParameter = sQLStatement != null ?
                new ObjectParameter("SQLStatement", sQLStatement) :
                new ObjectParameter("SQLStatement", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_BankTable", sQLStatementParameter);
        }
    
        public virtual int SP_CheckInventoryList(string sQLStatement)
        {
            var sQLStatementParameter = sQLStatement != null ?
                new ObjectParameter("SQLStatement", sQLStatement) :
                new ObjectParameter("SQLStatement", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_CheckInventoryList", sQLStatementParameter);
        }
    
        public virtual int SP_CheckMaintenanceList(string sQLStatement)
        {
            var sQLStatementParameter = sQLStatement != null ?
                new ObjectParameter("SQLStatement", sQLStatement) :
                new ObjectParameter("SQLStatement", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_CheckMaintenanceList", sQLStatementParameter);
        }
    
        public virtual int SP_DVTypeList(string sQLStatement)
        {
            var sQLStatementParameter = sQLStatement != null ?
                new ObjectParameter("SQLStatement", sQLStatement) :
                new ObjectParameter("SQLStatement", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_DVTypeList", sQLStatementParameter);
        }
    
        public virtual int SP_FundTypesList(string sQLStatement)
        {
            var sQLStatementParameter = sQLStatement != null ?
                new ObjectParameter("SQLStatement", sQLStatement) :
                new ObjectParameter("SQLStatement", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_FundTypesList", sQLStatementParameter);
        }
    
        public virtual int SP_IRAList(string sQLStatement)
        {
            var sQLStatementParameter = sQLStatement != null ?
                new ObjectParameter("SQLStatement", sQLStatement) :
                new ObjectParameter("SQLStatement", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_IRAList", sQLStatementParameter);
        }
    
        public virtual int SP_MemoAccountClassList(string sQLStatement)
        {
            var sQLStatementParameter = sQLStatement != null ?
                new ObjectParameter("SQLStatement", sQLStatement) :
                new ObjectParameter("SQLStatement", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_MemoAccountClassList", sQLStatementParameter);
        }
    
        public virtual int SP_MMAccountCodeList(string sQLStatement)
        {
            var sQLStatementParameter = sQLStatement != null ?
                new ObjectParameter("SQLStatement", sQLStatement) :
                new ObjectParameter("SQLStatement", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_MMAccountCodeList", sQLStatementParameter);
        }
    
        public virtual int SP_PositionList(string sQLStatement)
        {
            var sQLStatementParameter = sQLStatement != null ?
                new ObjectParameter("SQLStatement", sQLStatement) :
                new ObjectParameter("SQLStatement", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_PositionList", sQLStatementParameter);
        }
    
        public virtual int SP_SignatoryDepartmentList(string sQLStatement)
        {
            var sQLStatementParameter = sQLStatement != null ?
                new ObjectParameter("SQLStatement", sQLStatement) :
                new ObjectParameter("SQLStatement", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_SignatoryDepartmentList", sQLStatementParameter);
        }
    
        public virtual int SP_SignatoryTable(string sQLStatement)
        {
            var sQLStatementParameter = sQLStatement != null ?
                new ObjectParameter("SQLStatement", sQLStatement) :
                new ObjectParameter("SQLStatement", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_SignatoryTable", sQLStatementParameter);
        }
    }
}

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
    
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<BankAccount_AccountCode> BankAccount_AccountCode { get; set; }
        public virtual DbSet<BankAccount_AccountType> BankAccount_AccountType { get; set; }
        public virtual DbSet<BankAccountTable> BankAccountTables { get; set; }
        public virtual DbSet<BankTable> BankTables { get; set; }
        public virtual DbSet<FundType_FundName> FundType_FundName { get; set; }
        public virtual DbSet<FundType_FundType> FundType_FundType { get; set; }
        public virtual DbSet<FundType_FundTypeTable> FundType_FundTypeTable { get; set; }
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
        public virtual DbSet<Signatory_DepartmentTable_DepartmentHead> Signatory_DepartmentTable_DepartmentHead { get; set; }
        public virtual DbSet<Signatory_PositionTable> Signatory_PositionTable { get; set; }
    }
}

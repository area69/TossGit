﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOSS_UPGRADE.GlobalFunctions;
using TOSS_UPGRADE.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using TOSS_UPGRADE.Models.FM_GeneralReference;

namespace TOSS_UPGRADE.Controllers
{
    public class FileMaintenanceGeneralReferenceController : Controller
    {
        DB_TOSSEntities TOSSDB = new DB_TOSSEntities();
        DatatypeValidation GlobalFunction = new DatatypeValidation();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        #region Bank & Bank Account
        //Bank Table Partial View
        public ActionResult Get_BankTables()
        {
            FM_GeneralReference_Bank model = new FM_GeneralReference_Bank();
            List<BankList> tbl_Bank = new List<BankList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.BankTable";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_BankTable]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_Bank.Add(new BankList()
                        {
                            BankID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            BankName = GlobalFunction.ReturnEmptyString(dr[1]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getBankList = tbl_Bank.ToList();
            return PartialView("Banks/_BankTable", model.getBankList);
        }

        //Get Add Bank Partial View
        public ActionResult Get_AddBanks(FM_GeneralReference_Bank model)
        {
            return PartialView("Banks/_AddBankTable", model);
        }
        //Add Bank Type
        public JsonResult AddBank(FM_GeneralReference_Bank model)
        {
            BankTable tblBank = new BankTable();
            tblBank.BankName = GlobalFunction.ReturnEmptyString(model.getBankColumns.BankName);
            TOSSDB.BankTables.Add(tblBank);
            TOSSDB.SaveChanges();
            return Json(tblBank);
        }
        //Get Bank Update
        public ActionResult Get_UpdateBanks(FM_GeneralReference_Bank model, int BankID)
        {
            BankTable tblBank = (from e in TOSSDB.BankTables where e.BankID == BankID select e).FirstOrDefault();
            model.getBankColumns.BankName = tblBank.BankName;
            return PartialView("Banks/_UpdateBankTable", model);
        }
        //Update Bank
        public ActionResult UpdateBanks(FM_GeneralReference_Bank model)
        {
            BankTable tblBank = (from e in TOSSDB.BankTables where e.BankID == model.BankID select e).FirstOrDefault();
            tblBank.BankName = model.getBankColumns.BankName;
            TOSSDB.Entry(tblBank);
            TOSSDB.SaveChanges();
            return PartialView("Banks/_UpdateBankTable", model);
        }
        //Delete Bank
        public ActionResult DeleteBank(FM_GeneralReference_Bank model, int BankID)
        {
            BankTable tblBank = (from e in TOSSDB.BankTables where e.BankID == BankID select e).FirstOrDefault();
            TOSSDB.BankTables.Remove(tblBank);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }


        //Dropdown Bank
        public ActionResult GetDynamicBankName()
        {
            FM_GeneralReference_BankAccount model = new FM_GeneralReference_BankAccount();
            model.BankAccountBankList = new SelectList((from s in TOSSDB.BankTables.ToList() orderby s.BankName ascending select new { BankID = s.BankID, BankName = s.BankName }), "BankID", "BankName");
            return PartialView("BankAccounts/_DynamicDDBankName", model);
        }

        //passing Data to temp
        public ActionResult GetSelectedDynamicBankName(int BankNameIDTempID)
        {
            FM_GeneralReference_BankAccount model = new FM_GeneralReference_BankAccount();
            model.BankAccountBankList = new SelectList((from s in TOSSDB.BankTables.ToList() orderby s.BankName ascending select new { BankID = s.BankID, BankName = s.BankName }), "BankID", "BankName");
            model.BankAccountBankID = BankNameIDTempID;
            return PartialView("BankAccounts/_DynamicDDBankName", model);
        }
        public ActionResult GetDynamicAccountCode()
        {
            FM_GeneralReference_BankAccount model = new FM_GeneralReference_BankAccount();
            model.getBankAccountAccountCodeList = new SelectList((from s in TOSSDB.BankAccount_AccountCode.ToList() orderby s.AccountCode ascending select new { AccountCodeID = s.AccountCodeID, AccountCode = s.AccountCode }), "AccountCodeID", "AccountCode");
            return PartialView("BankAccounts/_DynamicDDAccountCode", model);
        }

        public ActionResult GetSelectedDynamicAccountCode(int AccountCodeIDTempID)
        {
            FM_GeneralReference_BankAccount model = new FM_GeneralReference_BankAccount();
            model.getBankAccountAccountCodeList = new SelectList((from s in TOSSDB.BankAccount_AccountCode.ToList() orderby s.AccountCode ascending select new { AccountCodeID = s.AccountCodeID, AccountCode = s.AccountCode }), "AccountCodeID", "AccountCode");
            model.AccountCodeID = AccountCodeIDTempID;
            return PartialView("BankAccounts/_DynamicDDAccountCode", model);
        }
        //Dropdown Account Type
        public ActionResult GetDynamicAccountType()
        {
            FM_GeneralReference_BankAccount model = new FM_GeneralReference_BankAccount();
            model.getBankAccountAccountTypeList = new SelectList((from s in TOSSDB.BankAccount_AccountType.ToList() orderby s.AccountTypeID ascending select new { AccountTypeID = s.AccountTypeID, AccountType = s.AccountType }), "AccountTypeID", "AccountType");
            return PartialView("BankAccounts/_DynamicDDAccountType", model);
        }

        public ActionResult GetSelectedDynamicAccountType(int AccountTypeIDTempID)
        {
            FM_GeneralReference_BankAccount model = new FM_GeneralReference_BankAccount();
            model.getBankAccountAccountTypeList = new SelectList((from s in TOSSDB.BankAccount_AccountType.ToList() orderby s.AccountTypeID ascending select new { AccountTypeID = s.AccountTypeID, AccountType = s.AccountType }), "AccountTypeID", "AccountType");
            model.AccountTypeID = AccountTypeIDTempID;
            return PartialView("BankAccounts/_DynamicDDAccountType", model);
        }

        //Account Type Table Partial View
        public ActionResult Get_AccountTypeTable()
        {
            FM_GeneralReference_BankAccount model = new FM_GeneralReference_BankAccount();
            List<BankAccount_AccountType> tbl_AccountType = new List<BankAccount_AccountType>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.BankAccount_AccountType order by AccountType asc";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_AccountTypeList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_AccountType.Add(new BankAccount_AccountType()
                        {
                            AccountTypeID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            AccountType = GlobalFunction.ReturnEmptyString(dr[1]),
                        });
                    }
                }
                Connection.Close();
            }

            model.getAccountType = tbl_AccountType.ToList();


            return PartialView("BankAccounts/_AccountTypeTable", model.getAccountType);

        }

        //Get Add Account Type Partial View
        public ActionResult Get_AddAccountType()
        {
            FM_GeneralReference_BankAccount model = new FM_GeneralReference_BankAccount();
            return PartialView("BankAccounts/_AddAccountType", model);
        }

        //Add Account Type
        public JsonResult AddAccountType(FM_GeneralReference_BankAccount model)
        {
            BankAccount_AccountType tblAccountType = new BankAccount_AccountType();
            tblAccountType.AccountType = GlobalFunction.ReturnEmptyString(model.getAccountTypeColumns.AccountType);
            TOSSDB.BankAccount_AccountType.Add(tblAccountType);
            TOSSDB.SaveChanges();
            return Json(tblAccountType);
        }

        //Get Update Account Type
        public ActionResult Get_UpdateAccountType(FM_GeneralReference_BankAccount model, int AccountTypeID)
        {
            BankAccount_AccountType tblAccountType = (from e in TOSSDB.BankAccount_AccountType where e.AccountTypeID == AccountTypeID select e).FirstOrDefault();
            model.getAccountTypeColumns.AccountTypeID = tblAccountType.AccountTypeID;
            model.getAccountTypeColumns.AccountType = tblAccountType.AccountType;
            return PartialView("BankAccounts/_UpdateAccountType", model);
        }

        //Update Account Type
        public ActionResult UpdateAccountType(FM_GeneralReference_BankAccount model)
        {
            BankAccount_AccountType tblAccountType = (from e in TOSSDB.BankAccount_AccountType where e.AccountTypeID == model.getAccountTypeColumns.AccountTypeID select e).FirstOrDefault();
            tblAccountType.AccountType = model.getAccountTypeColumns.AccountType;
            TOSSDB.Entry(tblAccountType);
            TOSSDB.SaveChanges();
            return PartialView("BankAccounts/_UpdateAccountType", model);
        }

        //Delete Account Type
        public ActionResult DeleteAccountType(FM_GeneralReference_BankAccount model, int AccountTypeID)
        {
            BankAccount_AccountType tblAccountType = (from e in TOSSDB.BankAccount_AccountType where e.AccountTypeID == AccountTypeID select e).FirstOrDefault();
            TOSSDB.BankAccount_AccountType.Remove(tblAccountType);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }

        //Bank Account Table Partial View
        public ActionResult Get_BankAccountTable()
        {
            FM_GeneralReference_BankAccount model = new FM_GeneralReference_BankAccount();
            List<BankAccountList> tbl_BankAccount = new List<BankAccountList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.BankAccountTable,dbo.BankTable,dbo.FundType_FundName,dbo.BankAccount_AccountCode,dbo.BankAccount_AccountType where dbo.BankTable.BankID=BankAccountTable.BankID and dbo.FundType_FundName.FundID=BankAccountTable.FundID and dbo.BankAccount_AccountCode.AccountCodeID=BankAccountTable.AccountCodeID and dbo.BankAccount_AccountType.AccountTypeID=BankAccountTable.AccountTypeID";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_BankAccountTable]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_BankAccount.Add(new BankAccountList()
                        {
                            BankAccountID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            BankName = GlobalFunction.ReturnEmptyString(dr[9]),
                            AccountName = GlobalFunction.ReturnEmptyString(dr[2]),
                            AccountNo = GlobalFunction.ReturnEmptyString(dr[3]),
                            FundName = GlobalFunction.ReturnEmptyString(dr[11]),
                            CurrentAccount = GlobalFunction.ReturnEmptyString(dr[5]),
                            AccountCode = GlobalFunction.ReturnEmptyString(dr[13]),
                            AccountType = GlobalFunction.ReturnEmptyString(dr[15]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getBankAccountList = tbl_BankAccount.ToList();
            return PartialView("BankAccounts/_BankAccountTable", model.getBankAccountList);
        }

        //Get Add Bank Account Partial View
        public ActionResult Get_AddBankAccount()
        {
            FM_GeneralReference_BankAccount model = new FM_GeneralReference_BankAccount();
            return PartialView("BankAccounts/_AddBankAccount", model);
        }

        //Add Bank Account
        public JsonResult AddBankAccount(FM_GeneralReference_BankAccount model)
        {
            BankAccountTable tblBankAccount = new BankAccountTable();
            tblBankAccount.BankID = model.BankAccountBankID;
            tblBankAccount.AccountName = model.getBankAccountColumns.AccountName;
            tblBankAccount.AccountNo = model.getBankAccountColumns.AccountNo;
           // tblBankAccount.FundID = model.FundID;
            tblBankAccount.CurrentAccount = model.getBankAccountColumns.CurrentAccount;
            tblBankAccount.AccountCodeID = model.AccountCodeID;
            tblBankAccount.AccountTypeID = model.AccountTypeID;
            TOSSDB.BankAccountTables.Add(tblBankAccount);
            TOSSDB.SaveChanges();
            return Json(tblBankAccount);
        }

        //Get Update Bank Account
        public ActionResult Get_UpdateBankAccount(FM_GeneralReference_BankAccount model, int BankAccountID)
        {
            BankAccountTable tblBankAccount = (from e in TOSSDB.BankAccountTables where e.BankAccountID == BankAccountID select e).FirstOrDefault();
            model.getBankAccountColumns.BankAccountID = tblBankAccount.BankAccountID;
            model.BankNameIDTemp = tblBankAccount.BankID;
            model.getBankAccountColumns.AccountName = tblBankAccount.AccountName;
            model.getBankAccountColumns.AccountNo = tblBankAccount.AccountNo;
           // model.FundID = tblBankAccount.FundID;
            model.getBankAccountColumns.CurrentAccount = tblBankAccount.CurrentAccount;
            model.AccountCodeIDTemp = tblBankAccount.AccountCodeID;
            model.AccountTypeIDTemp = tblBankAccount.AccountTypeID;
            return PartialView("BankAccounts/_UpdateBankAccount", model);
        }
        
        //Update Bank Account
        public ActionResult UpdateBankAccount(FM_GeneralReference_BankAccount model)
        {
            BankAccountTable tblBankAccount = (from e in TOSSDB.BankAccountTables where e.BankAccountID == model.getBankAccountColumns.BankAccountID select e).FirstOrDefault();
            tblBankAccount.BankID = model.BankAccountBankID;
            tblBankAccount.AccountName = model.getBankAccountColumns.AccountName;
            tblBankAccount.AccountNo = model.getBankAccountColumns.AccountNo;
           // tblBankAccount.FundID = model.FundID;
            tblBankAccount.CurrentAccount = model.getBankAccountColumns.CurrentAccount;
            tblBankAccount.AccountCodeID = model.AccountCodeID;
            tblBankAccount.AccountTypeID = model.AccountTypeID;
            TOSSDB.Entry(tblBankAccount);
            TOSSDB.SaveChanges();
            return PartialView("BankAccounts/_UpdateBankAccount", model);
        }

        //Delete Bank Account
        public ActionResult DeleteBankAccount(FM_GeneralReference_BankAccount model, int BankAccountID)
        {
            BankAccountTable tblBankAccount = (from e in TOSSDB.BankAccountTables where e.BankAccountID == BankAccountID select e).FirstOrDefault();
            TOSSDB.BankAccountTables.Remove(tblBankAccount);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion
        #region Memo Account Classification
        //Dropdown Memo Account Classification
        public ActionResult GetDynamicMemoAccountTitle()
        {
            FM_GeneralReference_MemoAccountClass model = new FM_GeneralReference_MemoAccountClass();
            model.MMAccountTitleList = new SelectList((from s in TOSSDB.MemoAccClass_AccountCode.ToList() orderby s.AccountCodeID ascending select new { AccountCodeID = s.AccountCodeID, AccountTitle = s.AccountTitle }), "AccountCodeID", "AccountTitle");


            return PartialView("MemoAccountClass/_DynamicDDAccountTitle", model);
        }
        //Get Add Account Code Memo Account Classification Partial View

        public ActionResult GetSelectedDynamicMemoAccountTitle(int MMAccountTitleIDTempID)
        {
            FM_GeneralReference_MemoAccountClass model = new FM_GeneralReference_MemoAccountClass();
            model.MMAccountTitleList = new SelectList((from s in TOSSDB.MemoAccClass_AccountCode.ToList() orderby s.AccountCodeID ascending select new { AccountCodeID = s.AccountCodeID, AccountTitle = s.AccountTitle }), "AccountCodeID", "AccountTitle");
            model.MMAccountTitleID = MMAccountTitleIDTempID;
            return PartialView("MemoAccountClass/_DynamicDDAccountTitle", model);

        }
        //Dropdown Memo Account Revision Year
        public ActionResult GetDynamicMemoRevisionYear()
        {
            FM_GeneralReference_MemoAccountClass model = new FM_GeneralReference_MemoAccountClass();
            model.MMRevisionYearList = new SelectList((from s in TOSSDB.MemoAccClass_RevisionYr.ToList() orderby s.RevisionID ascending select new { RevisionID = s.RevisionID, RevisionYear = s.RevisionYear }), "RevisionID", "RevisionYear");
            return PartialView("MemoAccountClass/_DynamicDDRevisionYear", model);
        }
        public ActionResult GetSelectedDynamicMemoRevisionYear(int MMRevisionYrIDTempID)
        {
            FM_GeneralReference_MemoAccountClass model = new FM_GeneralReference_MemoAccountClass();
            model.MMRevisionYearList = new SelectList((from s in TOSSDB.MemoAccClass_RevisionYr.ToList() orderby s.RevisionID ascending select new { RevisionID = s.RevisionID, RevisionYear = s.RevisionYear }), "RevisionID", "RevisionYear");
            model.MMRevisionYrID = MMRevisionYrIDTempID;
            return PartialView("MemoAccountClass/_DynamicDDRevisionYear", model);
        }

        //Memo Account Classification Table
        public ActionResult Get_MemoAccountClassTable()
        {
            FM_GeneralReference_MemoAccountClass model = new FM_GeneralReference_MemoAccountClass();
            List<MemoAccList> tbl_MemoAccount = new List<MemoAccList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.MemoAccClassTable,dbo.MemoAccClass_AccountCode,MemoAccClass_RevisionYr where  dbo.MemoAccClass_AccountCode.AccountCodeID = dbo.MemoAccClassTable.AccountCodeID  and  dbo.MemoAccClass_RevisionYr.RevisionID = dbo.MemoAccClassTable.RevisionID";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_MemoAccountClassList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_MemoAccount.Add(new MemoAccList()
                        {
                            MMAccountID = Convert.ToInt32(dr[0]),
                            AccountTitle = GlobalFunction.ReturnEmptyString(dr[5]),
                            AccountCode = GlobalFunction.ReturnEmptyString(dr[4]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getMemoAccList = tbl_MemoAccount.ToList();
            return PartialView("MemoAccountClass/_MemoAccClassTable", model.getMemoAccList);
        }

        //Get Add Memo Account Classification Partial View
        public ActionResult Get_AddMemoAccClassTable()
        {
            FM_GeneralReference_MemoAccountClass model = new FM_GeneralReference_MemoAccountClass();
            return PartialView("MemoAccountClass/_AddMemoAccClass", model);
        }

        //Add Memo Account Classification
        public JsonResult AddMemoAccClassTable(FM_GeneralReference_MemoAccountClass model)
        {
            MemoAccClassTable tblMemoAccount = new MemoAccClassTable();
            tblMemoAccount.AccountCodeID = model.MMAccountTitleID;
            tblMemoAccount.RevisionID = model.MMRevisionYrID;
            TOSSDB.MemoAccClassTables.Add(tblMemoAccount);
            TOSSDB.SaveChanges();
            return Json(tblMemoAccount);
        }
        //Get Update Memo Account
        public ActionResult Get_UpdateMemoAccount(FM_GeneralReference_MemoAccountClass model, int MemoAccClassID)
        {
            MemoAccClassTable tblMemoAccount = (from e in TOSSDB.MemoAccClassTables where e.MemoAccClassID == MemoAccClassID select e).FirstOrDefault();
            model.MemoAccClassID = tblMemoAccount.MemoAccClassID;
            model.MMAccountTitleTempID = tblMemoAccount.AccountCodeID;
            model.MMRevisionYrTempID = tblMemoAccount.RevisionID;
            return PartialView("MemoAccountClass/_UpdateMemoAccClass", model);
        }

        //Update Memo Account
        public ActionResult UpdateMemoAccount(FM_GeneralReference_MemoAccountClass model)
        {
            MemoAccClassTable tblMemoAccount = (from e in TOSSDB.MemoAccClassTables where e.MemoAccClassID == model.MemoAccClassID select e).FirstOrDefault();
            tblMemoAccount.AccountCodeID = model.MMAccountTitleID;
            tblMemoAccount.RevisionID = model.MMRevisionYrID;
            TOSSDB.Entry(tblMemoAccount);
            TOSSDB.SaveChanges();
            return PartialView("MemoAccountClass/_UpdateMemoAccClass", model);
        }

        //Delete Memo Account
        public ActionResult DeleteMemoAccount(FM_GeneralReference_MemoAccountClass model, int MemoAccClassID)
        {
            MemoAccClassTable tblMemoAccount = (from e in TOSSDB.MemoAccClassTables where e.MemoAccClassID == MemoAccClassID select e).FirstOrDefault();
            TOSSDB.MemoAccClassTables.Remove(tblMemoAccount);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }

        //Table Account Code Partial View
        public ActionResult Get_AccountCodeTable()
        {
            FM_GeneralReference_MemoAccountClass model = new FM_GeneralReference_MemoAccountClass();
            List<MMAccountTitleList> tbl_AccountCode = new List<MMAccountTitleList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.MemoAccClass_AccountCode";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_MMAccountCodeList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_AccountCode.Add(new MMAccountTitleList()
                        {
                            AccountCodeID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            AccountCode = GlobalFunction.ReturnEmptyString(dr[1]),
                            AccountTitle = GlobalFunction.ReturnEmptyString(dr[2]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getMMAccountTitleList = tbl_AccountCode.ToList();
            return PartialView("MemoAccountClass/AccountCode/_AccountCodeTable", model.getMMAccountTitleList);
        }

        public ActionResult Get_AddAccountCode()
        {
            FM_GeneralReference_MemoAccountClass model = new FM_GeneralReference_MemoAccountClass();
            return PartialView("MemoAccountClass/AccountCode/_AddAccountCode", model);
        }

        //Add Memo Account Classification
        public JsonResult AddAccountCode(FM_GeneralReference_MemoAccountClass model)
        {
            MemoAccClass_AccountCode tblMemoAccountCode = new MemoAccClass_AccountCode();
            tblMemoAccountCode.AccountTitle = model.getMMAccountTitleColumns.AccountTitle;
            tblMemoAccountCode.AccountCode = model.getMMAccountTitleColumns.AccountCode;
            TOSSDB.MemoAccClass_AccountCode.Add(tblMemoAccountCode);
            TOSSDB.SaveChanges();
            return Json(tblMemoAccountCode);
        }

        //Get Update Memo Account
        public ActionResult Get_UpdateAccountCode(FM_GeneralReference_MemoAccountClass model, int AccountCodeID)
        {
            MemoAccClass_AccountCode tblMemoAccount = (from e in TOSSDB.MemoAccClass_AccountCode where e.AccountCodeID == AccountCodeID select e).FirstOrDefault();
            model.getMMAccountTitleColumns.AccountCodeID = tblMemoAccount.AccountCodeID;
            model.getMMAccountTitleColumns.AccountTitle = tblMemoAccount.AccountTitle;
            model.getMMAccountTitleColumns.AccountCode = tblMemoAccount.AccountCode;
            return PartialView("MemoAccountClass/AccountCode/_UpdateAccountCode", model);
        }

        //Update Memo Account
        public ActionResult UpdateAccountCode(FM_GeneralReference_MemoAccountClass model)
        {
            MemoAccClass_AccountCode tblMemoAccount = (from e in TOSSDB.MemoAccClass_AccountCode where e.AccountCodeID == model.getMMAccountTitleColumns.AccountCodeID select e).FirstOrDefault();
            tblMemoAccount.AccountCode = model.getMMAccountTitleColumns.AccountCode;
            tblMemoAccount.AccountTitle = model.getMMAccountTitleColumns.AccountTitle;
            TOSSDB.Entry(tblMemoAccount);
            TOSSDB.SaveChanges();
            return PartialView("MemoAccountClass/AccountCode/_UpdateAccountCode", model);
        }

        //Delete Memo Account
        public ActionResult DeleteAccountCode(FM_GeneralReference_MemoAccountClass model, int AccountCodeID)
        {
            MemoAccClass_AccountCode tblMemoAccount = (from e in TOSSDB.MemoAccClass_AccountCode where e.AccountCodeID == AccountCodeID select e).FirstOrDefault();
            TOSSDB.MemoAccClass_AccountCode.Remove(tblMemoAccount);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion
        #region Internal Revenue Allotment
        //Table Internal Revenue Allotment
        public ActionResult Get_InternalRevenueAllotmentTable()
        {
            FM_GeneralReference_IRA model = new FM_GeneralReference_IRA();
            List<IRAList> tbl_IRA = new List<IRAList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.IRA_Table";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_IRAList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_IRA.Add(new IRAList()
                        {
                            IRAID = Convert.ToInt32(dr[0]),
                            IRAPercentageShare = GlobalFunction.ReturnEmptyDecimal(dr[1]),
                            IRAPercent = GlobalFunction.ReturnEmptyInt(dr[2]),
                            IRABase = GlobalFunction.ReturnEmptyDecimal(dr[3]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getIRAList = tbl_IRA.ToList();
            return PartialView("IRAShares/_InternalRevenueAllotmentTable", model.getIRAList);
        }

        //Get Add Internal Revenue Allotment Partial View
        public ActionResult Get_AddInternalRevenueAllotmentTable()
        {
            FM_GeneralReference_IRA model = new FM_GeneralReference_IRA();
            return PartialView("IRAShares/_AddInternalRevenueAllotment", model);
        }

        //Add Internal Revenue Allotment
        public JsonResult AddInternalRevenueAllotmentTable(FM_GeneralReference_IRA model)
        {
            IRA_Table tblIRA = new IRA_Table();
            tblIRA.IRAPercentageShare = model.getIRAcolumns.IRAPercentageShare;
            tblIRA.IRAPercent = model.getIRAcolumns.IRAPercent;
            tblIRA.IRABase = model.getIRAcolumns.IRABase;
            TOSSDB.IRA_Table.Add(tblIRA);
            TOSSDB.SaveChanges();
            return Json(tblIRA);
        }

        //Get Update Internal Revenue Allotment
        public ActionResult Get_UpdateInternalRevenueAllotment(FM_GeneralReference_IRA model, int IRAID)
        {
            IRA_Table tblMemoAccount = (from e in TOSSDB.IRA_Table where e.IRAID == IRAID select e).FirstOrDefault();
            model.getIRAcolumns.IRAID = tblMemoAccount.IRAID;
            model.getIRAcolumns.IRAPercentageShare = tblMemoAccount.IRAPercentageShare;
            model.getIRAcolumns.IRAPercent = tblMemoAccount.IRAPercent;
            model.getIRAcolumns.IRABase = tblMemoAccount.IRABase;
            return PartialView("IRAShares/_UpdateInternalRevenueAllotment", model);
        }

        //Update Internal Revenue Allotment
        public ActionResult UpdateInternalRevenueAllotment(FM_GeneralReference_IRA model)
        {
            IRA_Table tblMemoAccount = (from e in TOSSDB.IRA_Table where e.IRAID == model.getIRAcolumns.IRAID select e).FirstOrDefault();
            tblMemoAccount.IRAPercentageShare = model.getIRAcolumns.IRAPercentageShare;
            tblMemoAccount.IRAPercent = model.getIRAcolumns.IRAPercent;
            tblMemoAccount.IRABase = model.getIRAcolumns.IRABase;
            TOSSDB.Entry(tblMemoAccount);
            TOSSDB.SaveChanges();
            return PartialView("IRAShares/_UpdateInternalRevenueAllotment", model);
        }

        //Delete Internal Revenue Allotment
        public ActionResult DeleteInternalRevenueAllotment(FM_GeneralReference_IRA model, int IRAID)
        {
            IRA_Table tblMemoAccount = (from e in TOSSDB.IRA_Table where e.IRAID == IRAID select e).FirstOrDefault();
            TOSSDB.IRA_Table.Remove(tblMemoAccount);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
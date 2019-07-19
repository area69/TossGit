using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOSS_UPGRADE.GlobalFunctions;
using TOSS_UPGRADE.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TOSS_UPGRADE.Controllers
{
    public class FileMaintenanceGeneralReferenceController : Controller
    {
        DB_TOSSEntities TOSSDB = new DB_TOSSEntities();
        DatatypeValidation GlobalFunction = new DatatypeValidation();
        [Authorize]
        public ActionResult Index()
        {
            FM_GeneralReference_FundTypes model = new FM_GeneralReference_FundTypes();
            return View(model);
        }
        //Dropdown FundType
        public ActionResult GetDynamicFundType(FM_GeneralReference_FundTypes model,int FundID)
        {
          model.FundTypeList = new SelectList((from s in TOSSDB.FundType_FundType.Where(a => a.FundID == FundID).ToList() select new { FundTypeID = s.FundTypeID, FundTypeTitle = s.FundTypeTitle }), "FundTypeID", "FundTypeTitle");

            return PartialView("FundTypes/_DynamicDDFundType", model);
        }
        //Dropdown FundType
        public ActionResult GetDynamicFundType2(FM_GeneralReference_FundTypes model, int FundID)
        {
            model.FundTypeList = new SelectList((from s in TOSSDB.FundType_FundType.Where(a => a.FundID == FundID).ToList() select new { FundTypeID = s.FundTypeID, FundTypeTitle = s.FundTypeTitle }), "FundTypeID", "FundTypeTitle");

            return PartialView("FundTypes/_DynamicDDFundType", model);
        }
        //Get Add Fund Type Partial View
        public ActionResult Get_AddFundTypes(FM_GeneralReference_FundTypes model)
        {
            return PartialView("FundTypes/_AddFundTypes", model);
        }
        //Add Fund Type
        public JsonResult AddFundTypes(FM_GeneralReference_FundTypes model)
        {
            FundType_FundTypeTable tblFundType = new FundType_FundTypeTable();
            tblFundType.FundID = GlobalFunction.ReturnEmptyInt(model.getFundColumns.FundID);
            tblFundType.FundTypeID = GlobalFunction.ReturnEmptyInt(model.getFundColumns.FundTypeID);
            tblFundType.FundParticulars = GlobalFunction.ReturnEmptyString(model.getFundColumns.FundParticulars);
            TOSSDB.FundType_FundTypeTable.Add(tblFundType);
            TOSSDB.SaveChanges();
            return Json(tblFundType);
        }
        //Fund Type Table Partial View
        public ActionResult Get_FundTypeTables()
        {
            FM_GeneralReference_FundTypes model = new FM_GeneralReference_FundTypes();
            List<FundList> tbl_FundType = new List<FundList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.FundType_FundTypeTable,DB_TOSS.dbo.FundType_FundName,DB_TOSS.dbo.FundType_FundType WHERE (DB_TOSS.dbo.FundType_FundTypeTable.FundID = DB_TOSS.dbo.FundType_FundName.FundID AND DB_TOSS.dbo.FundType_FundTypeTable.FundTypeID = DB_TOSS.dbo.FundType_FundType.FundTypeID) order by FundParticulars asc";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_FundTypesList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_FundType.Add(new FundList()
                        {
                            ListFundTypeID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            FundTitle = GlobalFunction.ReturnEmptyString(dr[5]),
                            FundTypeTitle = GlobalFunction.ReturnEmptyString(dr[7]),
                            Particulars = GlobalFunction.ReturnEmptyString(dr[3])
                        });
                    }
                }
                Connection.Close();
            }

            model.getFundList = tbl_FundType.ToList();

            return PartialView("FundTypes/_FundTypesTable", model.getFundList);

        }
        //Get Fund Type Update
        public ActionResult Get_UpdateFundTypes(FM_GeneralReference_FundTypes model, int ListFundTypeID)
        {
            FundType_FundTypeTable tblFundTypes = (from e in TOSSDB.FundType_FundTypeTable where e.ListFundTypeID == ListFundTypeID select e).FirstOrDefault();
            model.getFundColumns.FundID = tblFundTypes.FundID;
            model.getFundColumns.FundTypeID = tblFundTypes.FundTypeID;
            model.getFundColumns.FundParticulars = tblFundTypes.FundParticulars;
            return PartialView("FundTypes/_UpdateFundTypes", model);
        }
        //Update Fund Type
        public ActionResult UpdateFundTypes(FM_GeneralReference_FundTypes model)
        {
            FundType_FundTypeTable tblFundType = (from e in TOSSDB.FundType_FundTypeTable where e.ListFundTypeID == model.ListFundTypeID select e).FirstOrDefault();
            tblFundType.FundID = model.getFundColumns.FundID;
            tblFundType.FundTypeID = model.getFundColumns.FundTypeID;
            tblFundType.FundParticulars = model.getFundColumns.FundParticulars;
            TOSSDB.Entry(tblFundType);
            TOSSDB.SaveChanges();
            return PartialView("FundTypes/_UpdateFundTypes", model);
        }
        //Delete Fund Type
        public ActionResult DeleteFundTypes(FM_GeneralReference_FundTypes model, int ListFundTypeID)
        {
            FundType_FundTypeTable tblFundTypes = (from e in TOSSDB.FundType_FundTypeTable where e.ListFundTypeID == ListFundTypeID select e).FirstOrDefault();
            TOSSDB.FundType_FundTypeTable.Remove(tblFundTypes);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }

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
            tblBankAccount.BankID =model.BankAccountBankID;
            tblBankAccount.AccountName = model.getBankAccountColumns.AccountName;
            tblBankAccount.AccountNo = model.getBankAccountColumns.AccountNo;
            tblBankAccount.FundID = model.FundID;
            tblBankAccount.CurrentAccount = model.getBankAccountColumns.CurrentAccount;
            tblBankAccount.AccountCodeID = model.AccountCodeID;
            tblBankAccount.AccountTypeID = model.AccountTypeID;
            TOSSDB.BankAccountTables.Add(tblBankAccount);
            TOSSDB.SaveChanges();
            return Json(tblBankAccount);
        }

        //Get Update Account Type
        public ActionResult Get_UpdateBankAccount(FM_GeneralReference_BankAccount model, int BankAccountID)
        {
            BankAccountTable tblBankAccount = (from e in TOSSDB.BankAccountTables where e.BankAccountID == BankAccountID select e).FirstOrDefault();
            model.getBankAccountColumns.BankAccountID = tblBankAccount.BankAccountID;
            model.BankNameIDTemp = tblBankAccount.BankID;
            model.getBankAccountColumns.AccountName = tblBankAccount.AccountName;
            model.getBankAccountColumns.AccountNo = tblBankAccount.AccountNo;
            model.FundID = tblBankAccount.FundID;
            model.getBankAccountColumns.CurrentAccount = tblBankAccount.CurrentAccount;
            model.AccountCodeIDTemp = tblBankAccount.AccountCodeID;
            model.AccountTypeIDTemp = tblBankAccount.AccountTypeID;
            return PartialView("BankAccounts/_UpdateBankAccount", model);
        }

        //Update Account Type
        public ActionResult UpdateBankAccount(FM_GeneralReference_BankAccount model)
        {
            BankAccountTable tblBankAccount = (from e in TOSSDB.BankAccountTables where e.BankAccountID == model.getBankAccountColumns.BankAccountID select e).FirstOrDefault();
            tblBankAccount.BankID = model.BankAccountBankID;
            tblBankAccount.AccountName = model.getBankAccountColumns.AccountName;
            tblBankAccount.AccountNo = model.getBankAccountColumns.AccountNo;
            tblBankAccount.FundID = model.FundID;
            tblBankAccount.CurrentAccount = model.getBankAccountColumns.CurrentAccount;
            tblBankAccount.AccountCodeID = model.AccountCodeID;
            tblBankAccount.AccountTypeID = model.AccountTypeID;
            TOSSDB.Entry(tblBankAccount);
            TOSSDB.SaveChanges();
            return PartialView("BankAccounts/_UpdateBankAccount", model);
        }
    }
}
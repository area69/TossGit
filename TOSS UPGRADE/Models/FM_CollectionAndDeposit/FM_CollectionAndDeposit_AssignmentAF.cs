﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_CollectionAndDeposit
{
    public class FM_CollectionAndDeposit_AssignmentAF
    {
        public FM_CollectionAndDeposit_AssignmentAF()
        {
            getAccountableFormAssign = new List<AccountableForm_Assignment>();
            getAccountableFormAssigncolumns = new AccountableForm_Assignment();
            getAccountableFormAssList = new List<AccountableFormAssignmentList>();
        }
        public AccountableForm_Assignment getAccountableFormAssigncolumns { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.AccountableForm_Assignment> getAccountableFormAssign { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> AccountableFormAssignmentList { get; set; }
        public List<AccountableFormAssignmentList> getAccountableFormAssList { get; set; }
        public int AccountableFormAssignmentID { get; set; }
        public int AccountableFormAssignmentAFID { get; set; }
        public int AccountableFACollectorID { get; set; }
        public int AccountableFormAssignmentFundID { get; set; }
        public int AccountableFormAssignmentEndingORID { get; set; }
        public int AccountableFormAssignmentQuantityID { get; set; }
        public int AccountableFormAssignmentStubNoID { get; set; }

    }
    public class AccountableFormAssignmentList
    {
        public int AssignAFID { get; set; }
        public string CollectorName { get; set; }
        public string FundType { get; set; }
        public string AF { get; set; }
        public int StubNo { get; set; }
        public int Quantity { get; set; }
        public int StratingOR { get; set; }
        public int EndingOR { get; set; }
        public string Date { get; set; }
    }
}
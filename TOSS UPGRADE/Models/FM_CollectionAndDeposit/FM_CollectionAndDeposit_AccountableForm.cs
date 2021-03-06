﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_CollectionAndDeposit
{
    public class FM_CollectionAndDeposit_AccountableForm
    {
        public FM_CollectionAndDeposit_AccountableForm()
        {
            getAccountableForm = new List<AccountableFormTable>();
            getAccountableFormcolumns = new AccountableFormTable();
            getAccountableFormList = new List<AccountableFormList>();


            getDescription = new List<AccountableForm_Description>();
            getDescriptionColumns = new AccountableForm_Description();
        }
        public List<AccountableFormList> getAccountableFormList { get; set; }
        public AccountableFormTable getAccountableFormcolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> AccountableFormList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.AccountableFormTable> getAccountableForm { get; set; }
        public int DescriptionID { get; set; }
        public int DescriptionTempID { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> DescriptionList { get; set; }

        public AccountableForm_Description getDescriptionColumns { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.AccountableForm_Description> getDescription { get; set; }
    }
    public class AccountableFormList
    {
        public int AccountFormID { get; set; }
        public string AccountFormName { get; set; }
        public string Description { get; set; }
        public bool isCTC { get; set; }
    }
}
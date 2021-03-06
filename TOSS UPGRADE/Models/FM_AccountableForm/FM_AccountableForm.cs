﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_AccountableForm
{
    public class FM_AccountableForm
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_AccountableForm()
        {
            getAccountableForm = new List<AccountableFormTable>();
            getAccountableFormcolumns = new AccountableFormTable();
            getAccountableFormList = new List<AccountableFormList>();
            
        }
        public List<AccountableFormList> getAccountableFormList { get; set; }
        public AccountableFormTable getAccountableFormcolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> AccountableFormList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.AccountableFormTable> getAccountableForm { get; set; }
    }

public class AccountableFormList
{
    public int AccountFormID { get; set; }
    public string AccountFormName { get; set; }
    public string Description { get; set; }
    public bool isCTC { get; set; }
}
}
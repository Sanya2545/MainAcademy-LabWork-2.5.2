using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM_v2.Models
{
    public class UsrPwd
    {
        public string UserName { get; set; }
        public string Id { get; set; }
        public string Pwd{get; set;}
        public string Cnfpwd { get; set; }

        public bool PwdValidate()
        {
            bool res = false;
            if (Pwd == Cnfpwd) 
            res=true;
           return res;
        }
    }
}
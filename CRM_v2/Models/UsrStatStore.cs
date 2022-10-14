using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web.UI;
using CRM_v2.DAL;
using CRM_v2.Models;



namespace CRM_v2.Models
{
    public class UsrStatStore
    {
        public void WriteStat(string _userid, string _lastaction)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
                UsrStat write_st = new UsrStat(_userid, _lastaction);
                unitOfWork.UsrStatRepository.Insert(write_st);
                unitOfWork.Save();
        }
    }
}
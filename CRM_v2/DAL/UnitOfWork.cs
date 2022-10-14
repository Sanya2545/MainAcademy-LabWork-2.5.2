using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM_v2.Models;

namespace CRM_v2.DAL
{
    public class UnitOfWork : IDisposable
    {
        private CRMContext context = new CRMContext();
        private GenericRepository<UsrStat> usrStatRepository;
        private GenericRepository<CtrlActStat> ctrlactStatRepository;

        public GenericRepository<CtrlActStat> CtrlActStatRepository
        {
            get
            {

                if (this.ctrlactStatRepository == null)
                {
                    this.ctrlactStatRepository = new GenericRepository<CtrlActStat>(context);
                }
                return ctrlactStatRepository;
            }
        }
        public GenericRepository<UsrStat> UsrStatRepository
        {
            get
            {

                if (this.usrStatRepository == null)
                {
                    this.usrStatRepository = new GenericRepository<UsrStat>(context);
                }
                return usrStatRepository;
            }
        }



        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
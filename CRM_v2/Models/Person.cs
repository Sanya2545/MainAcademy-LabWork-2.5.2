using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM_v2.Models
{
    public class Person
    {
        public long IdPers { get; set; }
        public string IdAuth { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public long IdStaff { get; set; }
        public long IdClient { get; set; }
        public long IdAddress { get; set; }
        public long IdRole { get; set; }
        public string TelNumb_1 { get; set; }
        public string TelNumb_2 { get; set; }
        public string e_mail { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public long IdHist { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime ModTime { get; set; }
    }
}
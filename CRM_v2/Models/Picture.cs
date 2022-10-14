using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM_v2.Models
{
    public class Picture
    {
        public Picture(string path, string description)
        {
            Path = path;
            Description = description;
        }
        public string Path { get; set; }
        public string Description { get; set; }
    }
}
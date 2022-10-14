using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CRM_v2.Models
{
    public class PictureModel : List<Picture>
    {
        public PictureModel()
        {
            string PictDir = HttpContext.Current.Server.MapPath("~/Images/");
            XDocument imageMetaData = XDocument.Load(PictDir + @"/ImageMetaData.xml");
            var pictures = from image in imageMetaData.Descendants("image")
                         select new Picture(image.Element("filename").Value,
                         image.Element("description").Value);
            this.AddRange(pictures.ToList<Picture>());
        }

        public void Add(Picture image, HttpPostedFileBase file)
        {
            string PictDir = HttpContext.Current.Server.MapPath("~/Images/");
            file.SaveAs(PictDir + image.Path);

            this.Add(image);
            XElement xml = new XElement("images",
                    from i in this
                    orderby image.Path
                    select new XElement("image",
                              new XElement("filename", i.Path),
                              new XElement("description", i.Description))
                    );

            XDocument doc = new XDocument(xml);

            doc.Save(PictDir + "/ImageMetaData.xml");
        }
    }
}

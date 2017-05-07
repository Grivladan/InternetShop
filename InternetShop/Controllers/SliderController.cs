using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternetShop.Controllers
{
    public class SliderController : Controller
    {        
        public ActionResult GetSliderImages()
        {
            string[] filePaths = Directory.GetFiles(Server.MapPath("~/Content/Images/"));

            List<Slider> files = new List<Slider>();
            foreach (string filePath in filePaths)
            {
                string fileName = Path.GetFileName(filePath);
                files.Add(new Slider
                {
                    title = fileName.Split('.')[0].ToString(),
                    src = "../Content/Images/" + fileName
                });
            }

            return View(files);
        }
    }
}
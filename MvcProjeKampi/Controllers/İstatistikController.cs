using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class İstatistikController : Controller
    {
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        public ActionResult Index()
        {
            using (Context c = new Context())
            {

                var Soru1 = c.Categories.Count();
                //view e veri göndermek için
                ViewData["CategoryCount"] = Soru1;
                var Soru2 = c.Headings.Count(x => x.CategoryID == 21).ToString();
                ViewData["CategoryHeadingCount"] = Soru2;
                var Soru3 = c.Writers.Count(x => x.WriterName.Contains("a") || x.WriterName.Contains("A")).ToString();
                // var model3 = c.writers.Where(x => x.WriterName.Contains("a") || x.WriterName.Contains("A")).Count();
                ViewData["WriterWithALetterCount"] = Soru3;
                //discorddan yardım aldım 4.sorgu için
                var Soru4 = c.Categories.Where(u => u.CategoryID == c.Headings.GroupBy(x => x.CategoryID).OrderByDescending(x => x.Count())
                .Select(x => x.Key).FirstOrDefault()).Select(x => x.CategoryName).FirstOrDefault();
                ViewData["CategoryNameMaxHeading"] = Soru4;
                var Soru5 = c.Categories.Where(x => x.CategoryStatus == true).Count() - c.Categories.Where(x => x.CategoryStatus == false).Count();
                ViewData["DifferenceOfTruAndFalse"] = Soru5;
                return View();

            }

        }
    }
}
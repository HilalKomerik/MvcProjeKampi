using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class CategoryController : Controller
    {
        CategoryManager cm = new CategoryManager(new EfCategoryDal()); //bl oluşturdğum sınıfı çağırmak istiyoruz.

        // GET: Category/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetCatgoryList()
        {
            var categoryvalues = cm.GetList();
            return View(categoryvalues);
        }

        [HttpGet] //Sayfa yüklendiği zaman çalışıcak 
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost] //Ekranda bir butona bastığım zaman(post edildiği zaman) çalışıcak
        public ActionResult AddCategory(Category p)
        {
            CategoryValidator categoryValidator = new CategoryValidator();
            ValidationResult results = categoryValidator.Validate(p); //result isimli değişken oluşturdum categoryValidaordan sınıfında olan değerler göre Validate yap(kontrolünü yap)
            if (results.IsValid)//eğer sonuç validate uygunsa 
            {
                cm.CategoryAdd(p);
                return RedirectToAction("GetCatgoryList");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName,item.ErrorMessage);
                }
            }
            return View();
        }
    }
}
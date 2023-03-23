
using Microsoft.AspNetCore.Mvc;
using Kursach.Models.Domain;
using Kursach.Models.Filter;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Kursach.Controllers
{
    public class CompanyController : Controller
    {
        private readonly DBContext _dbContext;
        
        public CompanyController(DBContext dbCtx)
        {
            _dbContext = dbCtx;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult AddCompany()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult AddCompany(Company company)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _dbContext.Company.Add(company);
                _dbContext.SaveChanges();
                TempData["msg"] = "Added";
                return RedirectToAction("AddCompany");
            }
            catch (Exception ex)
            {
                TempData["msg"] = "couldnt added";
                return View();
            }

        }
        
        public IActionResult DisplayCompanies(FilterModel filterModel)
        {
            var companyName = string.IsNullOrEmpty(filterModel.CompanyName) ? "" : filterModel.CompanyName.ToLower();
            
            var countries = _dbContext.Company.Select(c => c.Country).Distinct().ToList();
            countries.Insert(0, "None");
            
            var companies = _dbContext.Company.Where(c => c.Name.ToLower().StartsWith(companyName));;
            
            if(countries.Exists(c => c == filterModel.CountryName) && filterModel.CountryName != "None")
                companies = companies.Where(c => c.Country.ToLower() == filterModel.CountryName.ToLower());
           

            ViewBag.Companies = companies.ToList();
            ViewBag.Countries = countries;
            return View(filterModel);
        }
        
        
        public IActionResult EditCompany(Company company)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _dbContext.Company.Update(company);
                _dbContext.SaveChanges();
                TempData["msg"] = "Added";
                return RedirectToAction("DisplayCompanies");
            }
            catch (Exception ex)
            {
                TempData["msg"] = "couldnt updtated";
                return View();
            }
        }
            public IActionResult DeleteCompany(int id) 
        {
            try
            {
                var company = _dbContext.Company.Find(id);
                if (company != null)
                {
                    _dbContext.Company.Remove(company);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
            
            return RedirectToAction("DisplayCompanies"); 
        }
    }

}

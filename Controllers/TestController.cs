using Microsoft.AspNetCore.Mvc;
using DIA.Web.ViewModels;
using System.Linq;
using System;

namespace DIA.Web.Controllers
{
    public class TestController : Controller
    {
        private TestService testService;

        public TestController()
        {
            this.testService = new TestService();
        }

        [Route("[controller]")]
        public IActionResult Index(string currentFilter, string searchString, int? page)
        {

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
                        
            var x = new TestListViewModel();
            x.Tests = this.testService.GetTests(pageNumber, pageSize);

            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    x.Tests = x.Tests.Where(c => c.Name.ToUpper().Contains(searchString.ToUpper()));
            //}

            return View(x);
        }
    }
}

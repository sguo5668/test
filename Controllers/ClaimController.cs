using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DIA.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using PagedList.Core.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;


namespace DIA.Web.Controllers
{
    public class ClaimController : Controller
    {
        IPersonRepository _repoPerson;
        IPersonPhoneRepository _repoPersonPhone;
        IPersonAddressRepository _repoPersonAddress;
        IClaimRepository _repo;
        // Create a field to store the mapper object
        private readonly IMapper _mapper;
		private readonly IStringLocalizer<ClaimController> _localizer;
	 
		private static DI2501AClaimantForm x = new DI2501AClaimantForm();
		private readonly IMemoryCache _MemoryCache;

		private readonly IRepository<ReferenceTableValue> _service;
		string key = "referencetablevalue";
		//List<ReferenceTableValue> ReferenceTableValues;


		public ClaimController( 
            IClaimRepository claimRepository,
            IPersonRepository personRepository,
            IPersonPhoneRepository personPhoneRepository,
            IPersonAddressRepository personAddressRepository,
            IMapper mapper,
			IMemoryCache memCache,
			IRepository<ReferenceTableValue> serv,
			IStringLocalizer<ClaimController> localizer)
        {
            _repo = claimRepository;
            _mapper = mapper;
			_localizer = localizer;
            _repoPerson = personRepository;
            _repoPersonPhone = personPhoneRepository;
            _repoPersonAddress = personAddressRepository;
			_MemoryCache = memCache;
			_service = serv;

		
		}

        [Route("[controller]")]
        [Route("[controller]/[action]/{id?}")]
        [Route("[controller]/{id?}")]
        public async Task<IActionResult> Index(string currentFilter,     string searchString,     int? page)
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

            //   var claim = repo.GetHistoryClaim(id);
            var claim = _repo.GetAll();
			var claimHistory = _mapper.Map<IEnumerable<ClaimHistory>>(claim);
 

            if (!String.IsNullOrEmpty(searchString))
            {
                claimHistory = claimHistory.Where(c => c.ClaimIDNumber.ToUpper().Contains(searchString.ToUpper()));
            }

			//int pageSize = 1;
			//return View(await PaginatedList<ClaimHistory>.CreateAsync(<IEnumerable<ClaimHistory>> (claimHistory), page ?? 1, pageSize));


			//ReferenceTableValues = _MemoryCache.Get(key) as List<ReferenceTableValue>;

			return View(claimHistory);
        }

        public IActionResult Create(int id)
        {
            return View("W6Step1");
        }


        [HttpGet]
        [Route("[controller]/[action]")]
        public IActionResult Get(int id)
        {
      

            return View("index");
        }

        //private DI2501AClaimantForm GetDI2501AClaimantForm()
        //{
        //    var formsession = HttpContext.Session.GetObject<DI2501AClaimantForm>("DI2501AClaimantForm");

        //    if (formsession == null)
        //    {

        //        var x = new DI2501AClaimantForm();
        //        HttpContext.Session.SetObject("DI2501AClaimantForm", x);

        //        //HttpContext.Session.SetObjectAsJson("DI2501AClaimantForm", new DI2501AClaimantForm());
        //    }
        //    return HttpContext.Session.GetObject<DI2501AClaimantForm>("DI2501AClaimantForm");
        //}

        [Route("[controller]/Create/[action]")]
        [HttpGet]
        [ActionName("Step1")]
        public ActionResult Create()
        {
                 return View("W6Step1");
	    }


		[Route("[controller]/Create/[action]")]
		[HttpPost]
		[ActionName("Step1")]
		public ActionResult Step1(string BtnNext, string BtnCancel)
		{

			if (BtnNext != null)
			{
				x.ClaimType = 1;

                x.Person = _repoPerson.Get(1);
                x.PersonAddress = _repoPersonAddress.Get(1);
                x.PersonPhoneNumber = _repoPersonPhone.Get(1);
			 
				x.ReferenceLookup = _MemoryCache.Get(key) as List<ReferenceTableValue>;
				return View("W6Step2",x);

				//return RedirectToAction("Step2", "Claim");
				//return RedirectToAction("Action", new Microsoft.AspNetCore.Routing.RouteValueDictionary(x));
				//	return View("W6Step2", x);

			//	return RedirectToAction("Step2", "Claim", new RouteValueDictionary(x));

			}
		 			 
				return View("index");
		 }


		[Route("[controller]/Create/[action]")]
        [HttpPost]
        [ActionName ("Step2")]
          public IActionResult Step2(DI2501AClaimantForm DI2501AClaimantForm, string BtnPrevious, string BtnNext )
        {
            if (BtnNext != null)
            {
				x.FormOtherName = DI2501AClaimantForm.FormOtherName;
               
				return View("W6Step3",x);
            }
            return View();

        }

        [Route("[controller]/Create/[action]")]
        [HttpPost]
        [ActionName("Step3")]
        public IActionResult Step3(DI2501AClaimantForm DI2501AClaimantForm, string BtnPrevious, string BtnNext)
        {
            if (BtnPrevious != null)
            {
                return View("W6Step2",x);
            }

            if (BtnNext != null)
            {
				x.DI2501AForm = DI2501AClaimantForm.DI2501AForm;
				return View("W6Step3");
            }

            return View();
        }

		[HttpPost]
		[Route("[controller]")]
		public IActionResult SetLanguage(string culture, string returnUrl)
		{
			Response.Cookies.Append(
				CookieRequestCultureProvider.DefaultCookieName,
				CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
				new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
			);

			return LocalRedirect(returnUrl);
		}

	}
}
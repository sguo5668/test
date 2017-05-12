using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace DIA.Web.ViewModels
{
	public class DI2501AClaimantForm
	{
		//private readonly IMemoryCache _MemoryCache;
		//List<ReferenceTableValue> ReferenceTableValues;
		//string _key = "referencetablevalue";
		//public DI2501AClaimantForm
		//	(IMemoryCache MemoryCache, string key)

		//{
		//	_MemoryCache = MemoryCache;
		//	_key = key;
		//}

				public int ClaimType { get; set; }
				public int PersonID { get; set; }
				public Form Form { get; set; }
				public FormOtherName FormOtherName { get; set; }
				public DI2501AForm DI2501AForm { get; set; }
				public Person Person { get; set; }
				public PersonPhoneNumber PersonPhoneNumber { get; set; }
				public PersonAddress PersonAddress { get; set; }

		//public List<ReferenceTableValue> ReferenceLookup

		//{
		//	get
		//	{
		//		 ReferenceTableValues = _MemoryCache.Get(_key) as List<ReferenceTableValue>;

		//		return ReferenceTableValues;
		//	}
		//	//set
		//	//{
		//	// System.Web.HttpRuntime.Cache["selectedSuppliers"] = value;
		//	//}
		//}

		public List<ReferenceTableValue> ReferenceLookup { get; set; }
	}
}
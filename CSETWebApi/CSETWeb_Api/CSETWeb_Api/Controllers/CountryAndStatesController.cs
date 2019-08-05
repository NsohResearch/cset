//////////////////////////////// 
// 
//   Copyright 2019 Battelle Energy Alliance, LLC  
// 
// 
//////////////////////////////// 
using CSETWeb_Api.BusinessManagers;
using CSETWeb_Api.Helpers;
using CSETWeb_Api.Models;
using DataLayerCore.Model;
using Microsoft.EntityFrameworkCore;
using Nelibur.ObjectMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
namespace CSETWeb_Api.Controllers
{
    //[CSETAuthorize]
    public class CountryAndStatesController : ApiController
    {
        private CSET_Context db = new CSET_Context();

        /// <summary>
        /// Assessment demographics.
        /// </summary>
        public CountryAndStatesController() : base()
        {

        }

        [Route("api/country/Countries")]
        public async Task<List<COUNTRIES>> GetCountry()
        {
            List<COUNTRIES> list = await db.COUNTRIES.ToListAsync<COUNTRIES>();
            var tmplist = list.OrderBy(s => s.Display_Name).ToList();

            var otherItem = list.Find(x => x.Display_Name.Equals("United States of America", System.StringComparison.CurrentCultureIgnoreCase));
            if (otherItem != null)
            {
                list.Remove(otherItem);
                list.Insert(0, otherItem);
            }

            return list.Select(s => new COUNTRIES { COUNTRIES_ID = s.COUNTRIES_ID, Display_Name = s.Display_Name }).ToList();
        }

        [Route("api/Demographics/States")]
        public async Task<List<STATES_AND_PROVINCES>> GetStatesAndProvinces(string country_code)
        {
            List<STATES_AND_PROVINCES> list = await db.STATES_AND_PROVINCES.Where(x => x.Country_Code == country_code).ToListAsync<STATES_AND_PROVINCES>();
            var tmplist = list.OrderBy(s => s.Display_Name).ToList();

            var otherItem = list.Find(x => x.Display_Name.Equals("other", System.StringComparison.CurrentCultureIgnoreCase));
            if (otherItem != null)
            {

            }

            return list.Select(s => new STATES_AND_PROVINCES { STATES_AND_PROVINCES_ID = s.STATES_AND_PROVINCES_ID, Display_Name = s.Display_Name }).ToList();
        }


        ///// <summary>
        ///// Returns an instance of Demographics for the 
        ///// </summary>        
        ///// <returns></returns>
        //[HttpGet]
        //[Route("api/country/getCountries")]
        //public List<Country> GetCountries()
        //{
        //    int assessmentId = Auth.AssessmentForUser();
        //    return TinyMapper.Map<List<Country>>(db.COUNTRIES.ToList());
        //}
        ///// <summary>
        ///// Returns an instance of Demographics for the 
        ///// </summary>        
        ///// <returns></returns>
        //[HttpGet]
        //[Route("api/country/getStateProvince")]
        //public List<Country> GetStateProvince(string countrycode)
        //{
        //    int assessmentId = Auth.AssessmentForUser();
        //    return TinyMapper.Map<List<StateProvince>>(db.STATES_AND_PROVINCES.Where(x=> x. == countrycode).ToList());
        //}

    }

    public class Country
    {
        public string ISO_code { get; set; }        
        public string Display_Name { get; set; }        
        public int COUNTRIES_ID { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using MVCApp.Helper;

namespace MVCApp.Controllers
{
    public class ApiConnectController : Controller
    {
        private string webAPiUrl = ConfigurationManager.AppSettings["WebApiUrl"];
        CommonMethods commonMethods = new CommonMethods();

        /// <summary>
        /// Action to get the response from '/api/values' API, and this Web API has the [Authorize] attribute
        /// </summary>
        /// <returns></returns>
        public ActionResult GetValues()
        {
            // Retrieving the Bearer token that we saved in 'AccountController/Login' method
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            var token = claims.First(m => m.Type == "Token").Value;

            string response = commonMethods.HttpGETResponse(webAPiUrl + "/api/values", token);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
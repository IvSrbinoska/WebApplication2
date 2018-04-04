using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class CaseController : Controller
    {
        

        // GET: Case
        public ActionResult Index()
        {
            return View();
        }

        static async Task<List<Case>> GetAllCases()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:26815/");
                List<Case> cases = null;
                HttpResponseMessage response = await client.GetAsync("api/case/");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    cases = JsonConvert.DeserializeObject<List<Case>>(jsonString);
                }
                return cases;
            }

        }

        public async Task<ActionResult> AllCases()
        {
            List<Case> list = await GetAllCases();
            return View(list);
        }

        public async Task<Case> GetCaseByID(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:26815/");
                Case objCase = new Case();
                HttpResponseMessage response = await client.GetAsync("api/case/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    objCase = JsonConvert.DeserializeObject<Case>(jsonString);
                }
                return objCase;

            }

        }

        public ActionResult Get()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Get(int id)
        {
            var cs = await GetCaseByID(id);
            ViewBag.myCase = cs;

            return View("~/Views/Case/ShowCase.cshtml");
        }


        public ActionResult ShowCase()
        {
            return View();
        }

        public ActionResult Post()
        {
            return View();
        }

        private async Task<dynamic> createCaseAsync(Case myCase)
        {
            return await Task.FromResult<dynamic>(1);
        }

        [HttpPost]
        public async Task<ActionResult> NewCase(Case myCase)
        {
          ViewBag.Message = "Your case page";
            // call web API za da se kreira objektot
            using (var client = new HttpClient())
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(myCase), Encoding.UTF8,
                    "application/json");

                HttpResponseMessage response = await client.PostAsync("http://localhost:26815/api/case", stringContent);
                if ((int)response.StatusCode == 204)
                {
                    return View("~/Views/Case/New.cshtml", myCase);
                }
          
            }

            //ViewBag.myCase = await createCaseAsync(myCase);
            return View("~/Views/Case/New.cshtml");
        }

        
        public ActionResult New(Case myCase)
        {
            return View(myCase);
        }
    }
}
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
            return View("~/Views/Case/New.cshtml");
        }
        

        public ActionResult New(Case myCase)
        {
            return View(myCase);
        }

        /*[HttpPost]
        static async Task<Case> CreateCaseAsync(Case mycase)
        {
            using (var client = new HttpClient())
            {
                

                client.BaseAddress = new Uri("http://localhost:26815/");

                Case cases = null;

                HttpResponseMessage response = await client.PostAsJsonAsync("api/case/", mycase);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    cases = JsonConvert.DeserializeObject<Case>(jsonString);

                }
                return cases;
            }
        }
        
        public async Task<ActionResult> New(Case mycase)
        {
            ViewBag.Message = "Your Case page!";
            ViewBag.myCase = await CreateCaseAsync(mycase);
            return View("~/Views/Case/New.cshtml", mycase);
        }*/


        public async Task<ActionResult> Edit(Case mycase)
        {
            //var mycase =  await GetByCaseIDAsync(id);
            // ViewBag.myCase = myCase;

            return View(mycase);
        }




        [HttpPost]
        public async Task<ActionResult> Edit(Case mycase, int id)
        {
            int idd = mycase.ID;

            var myCase = await EditCaseAsync(idd, mycase);

            // ViewBag.myCase = myCase;

            return View("~/Views/Case/Details.cshtml", mycase);
        }

        static async Task<Case> EditCaseAsync(int id, Case mycase)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:/26815");

                Case cases = null;

                HttpResponseMessage response = await client.PutAsJsonAsync("api/case/" + id, mycase);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    cases = JsonConvert.DeserializeObject<Case>(jsonString);
                }
                return cases;
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var myCase = await GetCaseByID(id);
            var mycase = await DeleteCaseAsync(myCase);
            // return View(mycase);
            return RedirectToAction("AllCases");
        }

        static async Task<Case> DeleteCaseAsync(Case mycase)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:/26815");
                Case cases = null;
                HttpResponseMessage response = await client.DeleteAsync("api/case/" + mycase.ID);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    cases = JsonConvert.DeserializeObject<Case>(jsonString);
                }
                return cases;
            }
        }
        //test
        public async Task<ActionResult> Details(int id)
        { 
            var myCase = await GetCaseByID(id);

            //ViewBag.myCase = myCase;

            return View("~/Views/Case/Details.cshtml", myCase);

        }

    }
}
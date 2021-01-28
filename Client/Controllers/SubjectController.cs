using Client.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class SubjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LoadSubject()
        {
            List<MapelVM> mapelVMs = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44386/api/")
            };
            var responseTask = client.GetAsync("Subjects");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                mapelVMs = JsonConvert.DeserializeObject<List<MapelVM>>(json);

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(mapelVMs);
        }

        [HttpPost]
        public ActionResult AddSubject(MapelVM subject)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44386/api/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                string data = JsonConvert.SerializeObject(subject);
                var contentData = new StringContent(data, Encoding.UTF8, "application/json");
                var response = client.PostAsync("Subjects/create", contentData).Result;
                if (response.IsSuccessStatusCode)
                {
                    return Json(response.Content.ReadAsStringAsync().Result.ToString());
                }
                else
                {
                    return Content("GAGAL");
                }
            }
        }

        [HttpGet]
        public ActionResult GetById(int Id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44386/api/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                string data = JsonConvert.SerializeObject(Id);
                var contentData = new StringContent(data, Encoding.UTF8, "application/json");
                var response = client.GetAsync("Subjects/" + Id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return Json(response.Content.ReadAsStringAsync().Result.ToString());
                }
                else
                {
                    return Content("GAGAL");
                }

            }
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44386/api/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                string data = JsonConvert.SerializeObject(Id);
                var contentData = new StringContent(data, Encoding.UTF8, "application/json");
                var response = client.DeleteAsync("Subjects/" + Id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return Json(response.Content.ReadAsStringAsync().Result.ToString());
                }
                else
                {
                    return Content("GAGAL");
                }

            }
        }

        [HttpPut]
        public ActionResult UpdateSubject(MapelVM mapelVM)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44386/api/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                string data = JsonConvert.SerializeObject(mapelVM);
                var contentData = new StringContent(data, Encoding.UTF8, "application/json");
                var response = client.PutAsync("Subjects", contentData).Result;
                if (response.IsSuccessStatusCode)
                {
                    return Json(response.Content.ReadAsStringAsync().Result.ToString());
                }
                else
                {
                    return Content("GAGAL");
                }

            }
        }

    }

}

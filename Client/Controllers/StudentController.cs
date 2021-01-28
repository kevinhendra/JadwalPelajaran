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
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoadStudent()
        {
            List<StudentVM> studentVMs = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44386/api/")
            };
            var responseTask = client.GetAsync("Students");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                studentVMs = JsonConvert.DeserializeObject<List<StudentVM>>(json);

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(studentVMs);
        }

        [HttpPost]
        public ActionResult AddStudent(StudentVM student)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44386/api/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                string data = JsonConvert.SerializeObject(student);
                var contentData = new StringContent(data, Encoding.UTF8, "application/json");
                var response = client.PostAsync("Students", contentData).Result;
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
                var response = client.GetAsync("Students/" + Id).Result;
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
        public ActionResult UpdateStudent(StudentVM studentVM)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44386/api/");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                string data = JsonConvert.SerializeObject(studentVM);
                var contentData = new StringContent(data, Encoding.UTF8, "application/json");
                var response = client.PutAsync("Students", contentData).Result;
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
                var response = client.DeleteAsync("Students/" + Id).Result;
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

﻿using Client.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class LessonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoadLesson()
        {
            List<LessonVM> lessonVMs = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44386/api/")
            };
            var responseTask = client.GetAsync("Lessons");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                lessonVMs = JsonConvert.DeserializeObject<List<LessonVM>>(json);

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(lessonVMs);
        }
    }


}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Text;
using UniversityBusiness;
using UniversityMVC.Models;

namespace UniversityMVC.Controllers
{
    public class SpecializationController : BaseController
    {
        public class SpecializationResponse
        {
            [JsonProperty("value")]
            public List<Specialization> Value { get; set; }
        }
        public SpecializationController(IConfiguration configuration) : base(configuration) { }
        public async Task<IActionResult> Index(string SearchString)
        {
            string str = "";
            str = SpecializationAPIURL;
            if (!string.IsNullOrEmpty(SearchString))
            {
                str += "?$filter=(contains(tolower(SpecializationName), '" + SearchString.ToLower() + "'))";
            }
            HttpResponseMessage res = await _httpClient.GetAsync(str);
            if (!res.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            string rData = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<SpecializationResponse>(rData);


            return View(response.Value);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Specialization spe)
        {
            if (ModelState.IsValid)
            {
                var speModel = new
                {
                    spe.SpecializationName
                };
                var json = JsonConvert.SerializeObject(speModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage res = await _httpClient.PostAsync(SpecializationAPIURL, content);
                /*                string responseContent = await res.Content.ReadAsStringAsync();*/
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Server Error");

            }

            return View(spe);
        }

        public async Task<IActionResult> Edit(int id)
        {
            string str = "";
            str = SpecializationAPIURL;
            HttpResponseMessage res = await _httpClient.GetAsync($"{str}({id})");
            if (!res.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            string rData = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Specialization>(rData);
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Specialization spe)
        {
            if (ModelState.IsValid)
            {
                var speModel = new
                {
                    spe.SpecializationId,
                    spe.SpecializationName
                };
                var json = JsonConvert.SerializeObject(speModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage res = await _httpClient.PutAsync($"{SpecializationAPIURL}/{id}", content);
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Server Error");

            }

            return View(spe);
        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage res = await _httpClient.GetAsync($"{SpecializationAPIURL}({id})");
            if (!res.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            string rData = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Specialization>(rData);
            return View(response);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage res = await _httpClient.DeleteAsync($"{SpecializationAPIURL}({id})");
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Server Error");
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage res = await _httpClient.GetAsync($"{SpecializationAPIURL}({id})");
            if (!res.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            string rData = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Specialization>(rData);
            return View(response);
        }
    }
}

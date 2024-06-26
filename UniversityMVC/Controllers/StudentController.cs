using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static UniversityMVC.Controllers.SpecializationController;
using UniversityMVC.Models;
using System.Diagnostics;
using UniversityBusiness;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace UniversityMVC.Controllers
{
    public class StudentController : BaseController
    {
        public class StudentResponse
        {
            [JsonProperty("value")]
            public List<Student> Value { get; set; }
        }
        public StudentController(IConfiguration configuration) : base(configuration) { }
        public async Task<IActionResult> Index(string SearchString, int? specializationId)
        {
            string str = "";
            string url = StudentAPIURL+ "?$expand=Specialization";
            if (string.IsNullOrEmpty(SearchString)&&!specializationId.HasValue)
            {
                str = url;
            }
            else if (!string.IsNullOrEmpty(SearchString) && specializationId.HasValue)
            {
                str = url + "&$filter=SpecializationId eq "+ specializationId + " and (contains(tolower(StudentName), '" + SearchString.ToLower() + "'))";
            }
            else if (specializationId.HasValue)
            {
                str = url + "&$filter=SpecializationId eq " + specializationId;
            }
            else
            {
                str = url + "&$filter=(contains(tolower(StudentName), '" + SearchString.ToLower() + "'))";
            }
            HttpResponseMessage res = await _httpClient.GetAsync(str);
            if (!res.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            string rData = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<StudentResponse>(rData);

            HttpResponseMessage specializationRes = await _httpClient.GetAsync(SpecializationAPIURL);
            if (!specializationRes.IsSuccessStatusCode)
            {
                throw new Exception("Unable to load specializations.");
            }
            string specializationData = await specializationRes.Content.ReadAsStringAsync();
            var specializationResponse = JsonConvert.DeserializeObject<SpecializationResponse>(specializationData);

            ViewBag.Specialization = new SelectList(specializationResponse.Value, "SpecializationId", "SpecializationName");

            return View(response.Value);
        }

        public async Task<IActionResult> Create()
        {
            await LoadSpecializationsAsync();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student s)
        {
            if (ModelState.IsValid)
            {
                var sModel = new
                {
                    s.StudentName,
                    s.SpecializationId
                };
                var json = JsonConvert.SerializeObject(sModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage res = await _httpClient.PostAsync(StudentAPIURL, content);
                /*                string responseContent = await res.Content.ReadAsStringAsync();*/
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Server Error");

            }

            await LoadSpecializationsAsync();

            return View(s);
        }

        public async Task<IActionResult> Edit(int id)
        {
            string str = "";
            str = StudentAPIURL;
            HttpResponseMessage res = await _httpClient.GetAsync($"{str}({id})");
            if (!res.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            string rData = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Student>(rData);


            await LoadSpecializationsAsync();
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student s)
        {
            if (ModelState.IsValid)
            {
                var sModel = new
                {
                    s.StudentId,
                    s.StudentName,
                    s.SpecializationId
                };
                var json = JsonConvert.SerializeObject(sModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage res = await _httpClient.PutAsync($"{StudentAPIURL}/{id}", content);
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Server Error");

            }

            await LoadSpecializationsAsync();
            return View(s);
        }

        private async Task LoadSpecializationsAsync()
        {
            HttpResponseMessage specializationRes = await _httpClient.GetAsync(SpecializationAPIURL);
            if (!specializationRes.IsSuccessStatusCode)
            {
                throw new Exception("Unable to load specializations.");
            }
            string specializationData = await specializationRes.Content.ReadAsStringAsync();
            var specializationResponse = JsonConvert.DeserializeObject<IEnumerable<Specialization>>(specializationData);

            ViewBag.Specialization = new SelectList(specializationResponse, "SpecializationId", "SpecializationName");
        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage res = await _httpClient.GetAsync($"{StudentAPIURL}({id})");
            if (!res.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            string rData = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Student>(rData);
            return View(response);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage res = await _httpClient.DeleteAsync($"{StudentAPIURL}({id})");
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Server Error");
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage res = await _httpClient.GetAsync($"{StudentAPIURL}({id})");
            if (!res.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            string rData = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Student>(rData);
            return View(response);
        }

    }
}

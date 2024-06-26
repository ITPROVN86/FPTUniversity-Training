using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UniversityBusiness
{
    public class Specialization
    {
        [JsonProperty("SpecializationId")]
        [Display(Name ="Mã Chuyên ngành")]
        public int SpecializationId { get; set; }
        [JsonProperty("SpecializationName")]
        [Display(Name ="Tên Chuyên ngành")]
        public string SpecializationName { get; set; }
        public virtual ICollection<Student>? Students { get; set; }
    }
}

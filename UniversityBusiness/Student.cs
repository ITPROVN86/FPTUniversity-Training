using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityBusiness
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int SpecializationId { get; set; }
        public virtual Specialization? Specialization { get; set; }
    }
}

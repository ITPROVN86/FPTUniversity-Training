using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityBusiness;

namespace UniversityDataAccess.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudentById(int id);
        Task Add(Student s);
        Task Update(Student s);
        Task Delete(Student s);
    }
}

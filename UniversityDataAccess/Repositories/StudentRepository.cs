using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityBusiness;
using UniversityDataAccess.DAO;

namespace UniversityDataAccess.Repositories
{
    public class StudentRepository: IStudentRepository
    {
        public Task<IEnumerable<Student>> GetStudents() => StudentDAO.Instance.GetStudents();
        public Task<Student> GetStudentById(int id) => StudentDAO.Instance.GetStudentById(id);
        public Task Add(Student s) => StudentDAO.Instance.Add(s);
        public Task Update(Student s) => StudentDAO.Instance.Update(s);
        public Task Delete(Student s) => StudentDAO.Instance.Delete(s);
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityBusiness;

namespace UniversityDataAccess.DAO
{
    public class StudentDAO
    {
        private static StudentDAO instance;
        public static StudentDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new StudentDAO();
                return instance;
            }
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            var _context = new UniversityDbContext();
            var list = await _context.Students.Include(s=>s.Specialization).ToListAsync();
            return list;
        }

        public async Task<Student> GetStudentById(int id)
        {
            var _context = new UniversityDbContext();
            var student = await _context.Students.SingleOrDefaultAsync(s => s.StudentId == id);
            return student;
        }

        public async Task Add(Student s)
        {
            var _context = new UniversityDbContext();
            _context.Students.Add(s);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Student s)
        {
            var _context = new UniversityDbContext();
            var student = await GetStudentById(s.StudentId);

            if (student != null)
            {
                _context.Students.Update(s);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(Student s)
        {
            var _context = new UniversityDbContext();
            var student = await GetStudentById(s.StudentId);
            if (student != null)
            {
                _context.Students.Remove(s);
                await _context.SaveChangesAsync();
            }
        }
    }
}

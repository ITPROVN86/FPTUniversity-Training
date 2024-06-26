using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityBusiness;

namespace UniversityDataAccess.DAO
{
    public class SpecializationDAO
    {
        private static SpecializationDAO instance;
        public static SpecializationDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new SpecializationDAO();
                return instance;
            }
        }

        public async Task<IEnumerable<Specialization>> GetSpecializations()
        {
            var _context = new UniversityDbContext();
            var list = await _context.Specializations.ToListAsync();
            return list;
        }

        public async Task<Specialization> GetSpecializationById(int id)
        {
            var _context = new UniversityDbContext();
            var specialization = await _context.Specializations.SingleOrDefaultAsync(s=>s.SpecializationId==id);
            return specialization;
        }

        public async Task Add(Specialization s)
        {
            var _context = new UniversityDbContext();
            _context.Specializations.Add(s);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Specialization s)
        {
            var _context = new UniversityDbContext();
            var specialization = await GetSpecializationById(s.SpecializationId);
            if(specialization != null)
            {
                _context.Specializations.Update(s);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(Specialization s)
        {
            var _context = new UniversityDbContext();
            var specialization = await GetSpecializationById(s.SpecializationId);
            if (specialization != null)
            {
                _context.Specializations.Remove(s);
                await _context.SaveChangesAsync();
            }
        }
    }
}

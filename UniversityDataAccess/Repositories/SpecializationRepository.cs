using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityBusiness;
using UniversityDataAccess.DAO;

namespace UniversityDataAccess.Repositories
{
    public class SpecializationRepository: ISpecializationRepository
    {
        public Task<IEnumerable<Specialization>> GetSpecializations()=>SpecializationDAO.Instance.GetSpecializations();
        public Task<Specialization> GetSpecializationById(int id) => SpecializationDAO.Instance.GetSpecializationById(id);
        public Task Add(Specialization s) => SpecializationDAO.Instance.Add(s);
        public Task Update(Specialization s)=> SpecializationDAO.Instance.Update(s);
        public Task Delete(Specialization s) => SpecializationDAO.Instance.Delete(s);
    }
}

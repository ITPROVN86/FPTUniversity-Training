using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityBusiness;

namespace UniversityDataAccess.Repositories
{
    public interface ISpecializationRepository
    {
        Task<IEnumerable<Specialization>> GetSpecializations();
        Task<Specialization> GetSpecializationById(int id);
        Task Add(Specialization s);
        Task Update(Specialization s);
        Task Delete(Specialization s);
    }
}

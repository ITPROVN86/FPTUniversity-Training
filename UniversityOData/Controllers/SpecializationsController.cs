using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UniversityBusiness;
using UniversityDataAccess.Repositories;

namespace UniversityOData.Controllers
{
    [Route("odata/Specializations")]
    [ApiController]
    public class SpecializationsController : ODataController
    {
        ISpecializationRepository specializationRepository;

        public SpecializationsController()
        {
            specializationRepository = new SpecializationRepository();
        }

        [EnableQuery]
        // GET: api/Specializations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Specialization>>> Get()
        {
            var list = await specializationRepository.GetSpecializations();
            return Ok(list);
        }

        // GET: api/Specializations/5
        public async Task<ActionResult> Get([FromODataUri] int key)
        {
            var specialization = await specializationRepository.GetSpecializationById(key);

            if (specialization == null)
            {
                return NotFound();
            }

            return Ok(specialization);
        }

        // PUT: api/Specializations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{key}")]
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] Specialization specialization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existSpecialization = await specializationRepository.GetSpecializationById(key);
            if (existSpecialization == null)
            {
                return NotFound();
            }
            existSpecialization.SpecializationName = specialization.SpecializationName;
            specializationRepository.Update(existSpecialization);

            return Created(specialization);
        }

        // POST: api/Specializations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Specialization specialization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            specializationRepository.Add(specialization);

            return Created(specialization);
        }

        // DELETE: api/Specializations/5
        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var specialization = await specializationRepository.GetSpecializationById(key);
            if (specialization == null)
            {
                return NotFound();
            }

            specializationRepository.Delete(specialization);

            return NoContent();
        }
    }
}

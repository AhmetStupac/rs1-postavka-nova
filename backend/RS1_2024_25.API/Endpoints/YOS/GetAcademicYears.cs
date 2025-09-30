using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data;

namespace RS1_2024_25.API.Endpoints.YOS
{
    [Route("yos")]
    [ApiController]
    public class GetAcademicYears(ApplicationDbContext db) : ControllerBase
    {

        [HttpGet("academic-years")]
        public async Task<ActionResult<List<AcademicYearResponse>>> HandleAsync(CancellationToken cancellationToken)
        {
           
            var result = await db.AcademicYears.Select( ay => new AcademicYearResponse
            {
                Id = ay.ID,
                Name = ay.Description
            }).ToListAsync(cancellationToken);


            return Ok(result);
        }

    }

    public class AcademicYearResponse
    {
        public int Id{ get; set; }
        public string Name { get; set; }
    }
}

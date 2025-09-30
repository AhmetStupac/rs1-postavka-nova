using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data;
using RS1_2024_25.API.Data.Models.SharedTables;
using RS1_2024_25.API.Data.Models.TenantSpecificTables.Modul1_Auth;
using RS1_2024_25.API.Data.Models.TenantSpecificTables.Modul2_Basic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_25.API.Endpoints.YOS
{
    [Route("yos")]
    [ApiController]
    public class YosGetByStudentId(ApplicationDbContext db) : ControllerBase
    {
        [HttpGet("get/{id}")]
        public async Task<ActionResult<List<YOSGetResponse>>> HandleAsync(int id, CancellationToken cancellationToken)
        {
            await db.YearsOfStudy.LoadAsync(cancellationToken);
            await db.AcademicYears.LoadAsync(cancellationToken);

            var result = await db.YearsOfStudy
                .Where(s=> s.StudentId == id).Select(yos=> new YOSGetResponse
                {
                    Id = yos.Id,
                    StudentId = yos.StudentId,
                    Snimio = yos.Snimio.Email,
                    AkademskaGodinaId = yos.AkademskaGodinaId,
                    AkademskaGodina = yos.AkademskaGodina.Description,
                    GodinaStudija = yos.GodinaStudija,
                    Obnova = yos.Obnova,
                    DatumUpisa = DateOnly.FromDateTime(yos.DatumUpisa),
                    CijenaSkolarine = yos.CijenaSkolarine
                }).ToListAsync(cancellationToken);

            return Ok(result);
        }
    }


    public class YOSGetResponse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Snimio { get; set; }
        public int AkademskaGodinaId { get; set; }
        public string AkademskaGodina { get; set; }
        public int GodinaStudija { get; set; }
        public bool Obnova { get; set; }
        public DateOnly DatumUpisa { get; set; }
        public float CijenaSkolarine { get; set; }
    }
}

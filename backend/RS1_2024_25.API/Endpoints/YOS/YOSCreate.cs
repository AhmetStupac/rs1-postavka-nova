using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RS1_2024_25.API.Data;
using RS1_2024_25.API.Data.Models.SharedTables;

namespace RS1_2024_25.API.Endpoints.YOS
{
    [Route("yos")]
    [ApiController]
    public class YOSCreate(ApplicationDbContext db) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<ActionResult<int>> HandleAsync(YOSCreateRequest req, CancellationToken cancellationToken)
        {

            var yos = new YearOfStudy
            {
                StudentId = req.StudentId,
                SnimioId = req.SnimioId,
                AkademskaGodinaId = req.AkademskaGodinaId,
                GodinaStudija = req.GodinaStudija,
                DatumUpisa = req.DatumUpisa,
            };

            bool isRenewing = db.YearsOfStudy.Where(s => s.StudentId == req.StudentId && s.GodinaStudija == req.GodinaStudija)
                .FirstOrDefault() != null;

            yos.Obnova = isRenewing;
            yos.CijenaSkolarine = isRenewing ?400f : 1800f;

            if (db.YearsOfStudy.ToList().Exists(s => s.StudentId == req.StudentId && s.AkademskaGodinaId == req.AkademskaGodinaId))
                return BadRequest();


            await db.YearsOfStudy.AddAsync(yos, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);

            return Ok(yos.Id);
        }
    }

    public class YOSCreateRequest
    {
        public int StudentId { get; set; }
        public int SnimioId { get; set; }
        public int AkademskaGodinaId { get; set; }
        public string AkademskaGodina { get; set; }
        public int GodinaStudija { get; set; }
        public DateTime DatumUpisa { get; set; }
    }
}

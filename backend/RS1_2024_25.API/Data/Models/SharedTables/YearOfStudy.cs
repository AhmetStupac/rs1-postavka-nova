using RS1_2024_25.API.Data.Models.TenantSpecificTables.Modul1_Auth;
using RS1_2024_25.API.Data.Models.TenantSpecificTables.Modul2_Basic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_25.API.Data.Models.SharedTables
{
    public class YearOfStudy
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }
        public Student Student { get; set; }
        [ForeignKey(nameof(MyAppUser))]
        public int SnimioId { get; set; }
        public MyAppUser Snimio { get; set; }
        [ForeignKey(nameof(AcademicYear))]
        public int AkademskaGodinaId { get; set; }
        public AcademicYear AkademskaGodina { get; set; }
        public int GodinaStudija { get; set; }
        public bool Obnova { get; set; }
        public DateTime DatumUpisa { get; set; }
        public float CijenaSkolarine { get; set; }
    }
}

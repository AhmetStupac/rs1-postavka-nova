//using FluentValidation;
//using RS1_2024_25.API.Data;

//namespace RS1_2024_25.API.Endpoints.YOS
//{
//    public class YOSValidators() : AbstractValidator<YOSCreateRequest>
//    {

//        public YOSValidators(ApplicationDbContext db)
//        {
//            RuleFor(x => x.GodinaStudija).NotEmpty().GreaterThan(0).LessThan(5).WithMessage("Godina studija mora biti između 1 i 4");

//            RuleFor(x => x.AkademskaGodinaId).NotEmpty().GreaterThan(0).WithMessage("akademska godina id error");

//            RuleFor(x => x.StudentId).NotEmpty().GreaterThan(0).Must(y => db.StudentsAll.Where(s=> s.ID == y)

//        }
//    }
//}

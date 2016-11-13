using EFDAL;

namespace OwinAspNetCore
{
    public class Dvd_partakersController : OdataMaster<dvd_partaker>
    {
        public Dvd_partakersController(ISomeDependency someDependency) : base(someDependency)
        {
            this.table = db.dvd_partakers;
        }
    }
}

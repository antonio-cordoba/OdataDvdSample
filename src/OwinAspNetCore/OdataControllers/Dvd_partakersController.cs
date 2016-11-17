using EFDAL;

namespace OwinAspNetCore
{
    public class Dvd_partakersController : OdataMaster<dvd_partaker>
    {
        public Dvd_partakersController(GreenBoxEntities dbc) : base(dbc)
        {
            this.table = db.dvd_partakers;
        }
    }
}

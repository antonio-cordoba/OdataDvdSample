using EFDAL;

namespace OwinAspNetCore
{
    public class DvdsController : OdataMaster<dvd>
    {
        public DvdsController(ISomeDependency someDependency) : base(someDependency)
        {
            this.table = db.dvds;
        }
    }
}

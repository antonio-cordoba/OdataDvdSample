using EFDAL;

namespace OwinAspNetCore
{
    public class PartakersController : OdataMaster<partaker>
    {
        public PartakersController(ISomeDependency someDependency) : base(someDependency)
        {
            this.table = db.partakers;
        }
    }
}

using EFDAL;

namespace OwinAspNetCore
{
    public class StatusesController : OdataMaster<status>
    {
        public StatusesController(ISomeDependency someDependency) : base(someDependency)
        {
            this.table = db.status;
        }
    }
}
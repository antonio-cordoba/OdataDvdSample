using EFDAL;

namespace OwinAspNetCore
{
    public class AspectsController : OdataMaster<aspect>
    {
        public AspectsController(ISomeDependency someDependency) : base(someDependency)
        {
            this.table = db.aspects;
        }
    }
}

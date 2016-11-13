using EFDAL;

namespace OwinAspNetCore
{
    public class CapacitiesController : OdataMaster<capacity>
    {
        public CapacitiesController(ISomeDependency someDependency) : base(someDependency)
        {
            this.table = db.capacities;
        }
    }
}

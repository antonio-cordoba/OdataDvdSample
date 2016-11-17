using EFDAL;

namespace OwinAspNetCore
{
    public class CapacitiesController : OdataMaster<capacity>
    {
        public CapacitiesController(GreenBoxEntities dbc) : base(dbc)
        {
            this.table = db.capacities;
        }
    }
}

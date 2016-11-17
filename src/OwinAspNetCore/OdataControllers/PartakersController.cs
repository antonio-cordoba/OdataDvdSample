using EFDAL;

namespace OwinAspNetCore
{
    public class PartakersController : OdataMaster<partaker>
    {
        public PartakersController(GreenBoxEntities dbc) : base(dbc)
        {
            this.table = db.partakers;
        }
    }
}

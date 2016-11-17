using EFDAL;

namespace OwinAspNetCore
{
    public class StatusesController : OdataMaster<status>
    {
        public StatusesController(GreenBoxEntities dbc) : base(dbc)
        {
            this.table = db.status;
        }
    }
}
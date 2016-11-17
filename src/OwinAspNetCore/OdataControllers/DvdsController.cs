using EFDAL;

namespace OwinAspNetCore
{
    public class DvdsController : OdataMaster<dvd>
    {
        public DvdsController(GreenBoxEntities dbc) : base(dbc)
        {
            this.table = db.dvds;
        }
    }
}

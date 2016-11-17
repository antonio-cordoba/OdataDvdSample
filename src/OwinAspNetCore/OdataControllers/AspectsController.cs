using EFDAL;

namespace OwinAspNetCore
{
    public class AspectsController : OdataMaster<aspect>
    {
        public AspectsController(GreenBoxEntities dbc) : base(dbc)
        {
            this.table = db.aspects;
        }
    }
}

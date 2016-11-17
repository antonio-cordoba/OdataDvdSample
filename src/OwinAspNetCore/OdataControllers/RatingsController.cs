using EFDAL;

namespace OwinAspNetCore
{
    public class RatingsController : OdataMaster<rating>
    {
        public RatingsController(GreenBoxEntities dbc) : base(dbc)
        {
            this.table = db.ratings;
        }
    }
}

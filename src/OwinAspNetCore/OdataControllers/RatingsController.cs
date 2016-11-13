using EFDAL;

namespace OwinAspNetCore
{
    public class RatingsController : OdataMaster<rating>
    {
        public RatingsController(ISomeDependency someDependency) : base(someDependency)
        {
            this.table = db.ratings;
        }
    }
}

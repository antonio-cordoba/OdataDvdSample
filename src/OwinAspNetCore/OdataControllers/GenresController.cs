using EFDAL;

namespace OwinAspNetCore
{
    public class GenresController : OdataMaster<genre>
    {
        public GenresController(ISomeDependency someDependency) : base(someDependency)
        {
            this.table = db.genres;
        }
    }
}

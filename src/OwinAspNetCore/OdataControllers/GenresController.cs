using EFDAL;

namespace OwinAspNetCore
{
    public class GenresController : OdataMaster<genre>
    {
        public GenresController(GreenBoxEntities dbc) : base(dbc)
        {
            this.table = db.genres;
        }
    }
}

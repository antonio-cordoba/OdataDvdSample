using EFDAL;

namespace OwinAspNetCore
{
    public class StudiosController : OdataMaster<studio>
    {
        public StudiosController(GreenBoxEntities dbc) : base(dbc)
        {
            this.table = db.studios;
        }
    }
}

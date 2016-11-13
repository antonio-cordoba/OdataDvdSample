using EFDAL;

namespace OwinAspNetCore
{
    public class StudiosController : OdataMaster<studio>
    {
        public StudiosController(ISomeDependency someDependency) : base(someDependency)
        {
            this.table = db.studios;
        }
    }
}

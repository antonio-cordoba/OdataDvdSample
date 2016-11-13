using System.Data.Entity;

namespace EFDAL
{
    public partial class GreenBoxEntities : DbContext
    {
        public GreenBoxEntities(string ConnectionString)
            : base(ConnectionString)
        {
        }
    }
}
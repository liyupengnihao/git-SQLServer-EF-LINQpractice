using System;
using System.Collections.Generic;
using System.Data.Entity;//DbContext所在空间
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 连表查询语句.EF6
{
    public class MyDBContext : DbContext
    {
        public MyDBContext() : base("Server=localhost;Database=Text;Trusted_Connection=true")
        {           
        }
        public DbSet<UserTModelForEF> UserT1 { get; set; }
    }
}

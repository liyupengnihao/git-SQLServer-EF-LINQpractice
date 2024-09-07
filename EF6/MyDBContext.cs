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
        public MyDBContext() : base("Server=localhost;Database=Text;Trusted_Connection=true")//调用父类构造函数
        {
            
        }
        public DbSet<UserTModelForEF> UserT1 { get; set; }
        //DbSet<TEntity> 提供了一组方法，允许你以LINQ（语言集成查询）的形式查询数据库中的数据，同时它也支持添加、删除和修改数据记录。
        public DbSet<UserScoresTModelForEF> UserScoresT1 {  get; set; }
        //DbSet继承了IQueryable接口，可以实现生成sql语句
    }
}

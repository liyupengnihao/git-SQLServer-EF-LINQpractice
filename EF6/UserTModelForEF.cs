using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//  确定主键命名空间
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 连表查询语句.EF6
{
    [Table("UserT1")]//用于指定数据库中的表模型
    // 模型UserTModelForEF对应表，不会再重新建表
    //  不指定的话会新键两张表，其中没有数据只有属性
    public class UserTModelForEF
    {
        [Key]// 确定主键为UsersName
        public string UsersName { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string Gender { get; set; }
    }
    //加table和key是让EF可以检索并执行操作
    public class UserTModelForEFNew
    {
        public string UsersName { get; set; }
        public string Gender { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 连表查询语句.EF6
{
    [Table("UserScoresT1")]
    public class UserScoresTModelForEF
    {
        [Key]
        public int ID {  get; set; }
        public string UsersName { get; set; }
        public double? Chinese { get; set; }//映射为float有异常，用double就可以
        public double? English { get; set; }
        public double? Math { get; set; }
        public DateTime? RecordTime { get; set; }
    }
}

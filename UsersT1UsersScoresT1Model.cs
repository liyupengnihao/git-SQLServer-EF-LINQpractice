using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 多语言支持04;

namespace 连表查询语句
{
    internal class UsersT1UsersScoresT1Model
    {
        public string UsersName { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string Gender { get; set; }
        public float Chinese { get; set; }
        public float English { get; set; }
        public float Math { get; set; }
        public DateTime RecordTime { get; set; }//  当前语言文本不支持？
    }
}

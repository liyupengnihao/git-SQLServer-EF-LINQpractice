﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 连表查询语句.EF6
{
    public class UserTAndUserScoresTModelForEF
    {
        public string UsersName { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string Gender { get; set; }
        public double? Chinese { get; set; }
        public double? English { get; set; }
        public double? Math { get; set; }
        public DateTime? RecordTime { get; set; }
    }
}

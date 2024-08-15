using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 多语言支持04;

namespace 连表查询语句
{
    internal class UsersT1UsersScoresT1Operation
    {
        public static List<UsersT1UsersScoresT1Model> GetUnionTwoTable()
        {
            //  两个表的内容
            string sql = "select u.UsersName,u.Password,u.NickName,u.Gender,s.Chinese,s.English,s.Math,s.RecordTime from UserT1 as u -- 取别名\r\n  left join\r\n  UserScoresT1 as s\r\n  on s.UsersName=u.UsersName;";//  列名也可以取别名
            DataTable table = SQLHelper.SelectData(sql);
            List<UsersT1UsersScoresT1Model> modelUnion = new List<UsersT1UsersScoresT1Model>();

            for (int i = 0; i<table.Rows.Count; i++)
            {
                UsersT1UsersScoresT1Model model = loopRightNullOrEmpty(table, i);
                modelUnion.Add(model);
            }
            return modelUnion;
        }
        public static UsersT1UsersScoresT1Model loopRightNullOrEmpty(DataTable table, int i)
        {
            UsersT1UsersScoresT1Model model = new UsersT1UsersScoresT1Model();
            if (!string.IsNullOrEmpty(table.Rows[i]["UsersName"].ToString()))// 按列还能来个循环
                model.UsersName=table.Rows[i]["UsersName"].ToString();
            if (!string.IsNullOrEmpty(table.Rows[i]["Chinese"].ToString()))
                model.Chinese=Convert.ToSingle(table.Rows[i]["Chinese"]);
            if (!string.IsNullOrEmpty(table.Rows[i]["English"].ToString()))
                model.English= Convert.ToSingle(table.Rows[i]["English"]);//(float)table.Rows[i]["English"];// 无法强制转换
            if (!string.IsNullOrEmpty(table.Rows[i]["Math"].ToString()))
                model.Math=float.Parse(table.Rows[i]["Math"].ToString());
            if (!string.IsNullOrEmpty(table.Rows[i]["RecordTime"].ToString()))
                model.RecordTime=DateTime.Parse(table.Rows[i]["RecordTime"].ToString()); //(DateTime)table.Rows[i]["RecordTime"];// 可能会有异常
            if (!string.IsNullOrEmpty(table.Rows[i]["Password"].ToString()))
                model.Password=table.Rows[i]["Password"].ToString();
            if (!string.IsNullOrEmpty(table.Rows[i]["NickName"].ToString()))
                model.NickName=table.Rows[i]["NickName"].ToString();
            //if (!string.IsNullOrEmpty(table.Rows[i]["Gender"].ToString()))
                model.Gender=string.IsNullOrEmpty(table.Rows[i]["Gender"].ToString()) ? "": table.Rows[i]["Gender"].ToString();//   三元表达式
            return model;
        }
    }
}

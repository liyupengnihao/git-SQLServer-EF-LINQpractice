using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 多语言支持04;
using 连表查询语句.EF6;


namespace 连表查询语句
{
    //    找表且建表  
    //select tt.*from
    //(select*,
    //ROW_NUMBER() over(partition by UsersName order by RecordTime desc)AS RowNum-- 按UsersName在分组，按RecordTime来降序排列，一个新列命名为RowNum
    //from UserScoresT1) as tt-- 得到一个新表
    //where tt.RowNum=1;-- 找RowNum为1的

    internal class Program
    {
        static void Main(string[] args)
        {
            #region 简介ToList，已注释
            //MyDBContext myDb = new MyDBContext();//   创建连接并打开
            //                                     //List<UserTModelForEF> listUsers=myDb.UserT.ToList();//  myDb.UserT得到一个sql语句，用ToList来把sql语句提交给数据库
            //                                     ////  myDb.UserT为一个查询对象
            //                                     ////myDb.Dispose();//   释放连接

            //LINQ查询
            //var lstUsers = myDb.UserT1.Where(e => e.UsersName=="111");//   此为一个sql语句，用.ToList等来提交给数据库
            //string sql = lstUsers.ToString();//转换为string

            //////using (MyDBContext myDb = new MyDBContext())
            //////{
            //////    List<UserTModelForEF> list = myDb.UserT1.ToList();
            //////}

            ////////myDb.Dispose();//释放连接,using也释放

            #endregion
            #region 更直观显示与接近sql语言的方式，已注释
            //using (MyDBContext myDB = new MyDBContext())
            //{
            //    DbSet<UserTModelForEF> listQuery = myDB.UserT1;//查询对象
            //    List<UserTModelForEF> list = listQuery.ToList();

            //    //接近sql的方法
            //    var query = from usertOne in myDB.UserT1 select usertOne;//一个查询对象，与前面的相同,usertOne为别名
            //    List<UserTModelForEF>  lst=query.ToList();

            //    //特定属性
            //    var queryOne = from usertTwo in myDB.UserT1 select usertTwo.UsersName;
            //    List<string> ls= queryOne.ToList();

            //    //映射到新表中
            //    var queryTwo=from usertThree in myDB.UserT1 select new UserTModelForEFNew { UsersName = usertThree.UsersName,Gender=usertThree.Gender };
            //    List<UserTModelForEFNew> l= queryTwo.ToList();
            //}
            #endregion
            //  语言判断
            Console.WriteLine("请选择展示性别的语言");
            Console.WriteLine("1：中文 2：English 3：繁体中文,输入错误数字默认为中文");
            string number = Console.ReadLine();
            //  用语音文档
            #region 语言文档（部分）
            if (number=="2")// 此处的1 2 是分辨语言的，在把对应语言的内容输入进去
            {
                InfoHelper.Gender1="Male";
                InfoHelper.Gender2="Female";
                InfoHelper.Gender3="unknown";
                InfoHelper.Info1="Welcome to the xxx system";
                InfoHelper.Info6="Welcome to your login, dear @NickName";// 用@来占位，后面替换

            }
            else if (number=="3")
            {
                InfoHelper.Gender1="繁体男";
                InfoHelper.Gender2="繁体女";
                InfoHelper.Gender3="繁体未知性别";
                InfoHelper.Info1="歡迎使用xxx系統";
                InfoHelper.Info6="歡迎你登錄，尊敬的@NickName";
            }
            else
            {
                InfoHelper.Gender1="男";
                InfoHelper.Gender2="女";
                InfoHelper.Gender3="未知性别";
                InfoHelper.Info1="欢迎使用xxx系统";
                InfoHelper.Info6="欢迎你登录，尊敬的@NickName";

            }
            #endregion


            //  DataTable table = null;//   老版登录界面用
            //UserT1Model userTM = null;//  sql语言时
            UserTModelForEF userTM = new UserTModelForEF();
            //  界面部分
            Console.WriteLine(InfoHelper.Info1);//  下面都可以重复如此
            while (true)
            {
                Console.Write("请输入账号名:");
                string inputeName = Console.ReadLine();
                Console.Write("请输入密码:");
                string inputePwd = Console.ReadLine();



                //userTM=UserT1Operation.Login(inputeName, inputePwd);
                using (MyDBContext myDB = new MyDBContext())
                {
                    //FirstOrDefault用于返回序列中满足指定的条件或默认值，如果找到这样的元素的第一个元素
                    userTM=myDB.UserT1.FirstOrDefault(e => e.UsersName==inputeName&&e.Password==inputePwd);
                }

                if (userTM!=null)
                {
                    break;
                }

                #region 老版登录界面，以注释
                //  为看的舒心用方法
                //sql = $"select*from UserT1 where UsersName='{inputeName}'and Password='{inputePwd}'";
                //table = SelectData(sql);
                //if (table.Rows.Count<=0)
                //{
                //    Console.WriteLine("账号或密码不正确");

                //}
                //else
                //{
                //    userTM=UserT1Operation.operation(table, 0);//   用方法完成下面这个
                //    //  另一个方法
                //    userTM=UserT1Operation.DataRowToUserTModel(table.Rows[0]);
                //    #region 方法实现的内容
                //    ////  怕数据库的属性写错
                //    //userTM.UsersName=table.Rows[0]["UsersName"].ToString();
                //    //userTM.Password=table.Rows[0]["Password"].ToString();
                //    //userTM.NickName=table.Rows[0]["NickName"].ToString();
                //    //userTM.Gender=table.Rows[0]["Gender"].ToString();
                //    #endregion
                //    break;
                //}
                #endregion

            }
            Console.WriteLine("登录成功");
            Console.WriteLine($"欢迎你登录，尊敬的{userTM.NickName}");//   打出昵称
                                                             //  换语言且替换      string类型下有Replace
            InfoHelper.Info6=InfoHelper.Info6.Replace("@NickName", userTM.NickName);
            Console.WriteLine(InfoHelper.Info6);



            Console.WriteLine("你可看到全部的数据，除了密码");


            //  读取性别，如果是空或许null，提醒用户输入信息
            string gender = userTM.Gender;
            //  验证用户密码和性别输入
            if (string.IsNullOrEmpty(gender))//  空或者null则返回true
            {
                Console.WriteLine($"尊敬的{userTM.NickName}你好，你的消息性别还不完整，请输入性别");

                int count = UserT1Operation.UPDataGender(userTM);
                Console.WriteLine($"影响了{count}行");
                #region 不用方法，已注释
                //while (true)
                //{
                //    Console.WriteLine("请输入1：男 2：女 3:未知性别");
                //    string inputeGender = Console.ReadLine();
                //    if (!(inputeGender=="1"||inputeGender=="2"||inputeGender=="3"))
                //        continue;// 把一二传进去

                //    #region 老版传男女
                //    //if (inputeGender=="1")
                //    //{
                //    //    gender="男";
                //    //}
                //    //else if (inputeGender=="2")
                //    //{
                //    //    gender="女";
                //    //}
                //    //else
                //    //    continue;
                //    #endregion

                //    //  再根据性别来更新数据库
                //    string updataGender = $"update UserT1 set Gender='{inputeGender}'where UsersName='{userTM.UsersName}'";//   更新单一属性下对应主键的数据数据
                //    int count = SQLHelper.EditData(updataGender);
                //    if (count>0)
                //    {
                //        Console.WriteLine($"修改{userTM.UsersName}的性别成功");
                //        break;
                //    }
                //    else
                //    {
                //        Console.WriteLine("修改失败,请重新输入");
                //        continue;
                //    }

                //}
                #endregion

            }


            #region 展示数据库中所有的数据
            #region EF6
            //1查询UserT1,再查询UserScoreT1
            //1)展示所有UserScoresT
            //2)展示最新UserScoresT
            using (MyDBContext myDB = new MyDBContext())
            {
                Console.WriteLine("用户名，昵称，性别，语文成绩，英语成绩，数学成绩，时间");
                //查询所有UserT数据
                List<UserTModelForEF> lastAllUsers = myDB.UserT1.ToList();
                #region 所有数据
                for (int i = 0; i<lastAllUsers.Count; i++)
                {
                    if (lastAllUsers[i].Gender=="1")
                        lastAllUsers[i].Gender=InfoHelper.Gender1;
                    else if (lastAllUsers[i].Gender=="2")
                        lastAllUsers[i].Gender=InfoHelper.Gender2;
                    else if (lastAllUsers[i].Gender=="3")
                        lastAllUsers[i].Gender=InfoHelper.Gender3;
                    //查询所有UserScoreT1里的数据

                    //接收当前循环到的usersname(直接转不能用)
                    string currentUserName = lastAllUsers[i].UsersName;
                    List<UserScoresTModelForEF> listAllUserScores = myDB.UserScoresT1.Where(e => e.UsersName==currentUserName).ToList();
                    //Whrer每个符合的数据

                    foreach (UserScoresTModelForEF item in listAllUserScores)
                    {
                        Console.WriteLine($"{lastAllUsers[i].UsersName+"\t"}{lastAllUsers[i].NickName+"\t"}{lastAllUsers[i].Gender+"\t"}{item.Chinese+"\t"}{item.English+"\t"}{item.Math+"\t"}{item.RecordTime+"\t"}");
                    }
                }
                #endregion
                #region 最新数据
                Console.WriteLine("最新数据");
                for (int i = 0; i<lastAllUsers.Count; i++)
                {
                    if (lastAllUsers[i].Gender=="1")
                        lastAllUsers[i].Gender=InfoHelper.Gender1;
                    else if (lastAllUsers[i].Gender=="2")
                        lastAllUsers[i].Gender=InfoHelper.Gender2;
                    else if (lastAllUsers[i].Gender=="3")
                        lastAllUsers[i].Gender=InfoHelper.Gender3;
                    string currentUserName = lastAllUsers[i].UsersName;


                    List<UserScoresTModelForEF> currentTime = myDB.UserScoresT1.Where(e => e.UsersName==currentUserName).OrderByDescending(e => e.RecordTime).ToList();
                    //OrderByDescending根据键降序排序,First用来提交给数据库并取第一个数值，ToList用来提交给数据库,返回序列中的第一个元素；FirstOrDefault如果序列中不包含任何元素，则返回默认值
                    if (currentTime.Count==0)
                    {
                        Console.WriteLine($"{currentUserName} 无成绩数据");
                    }
                    else
                    {

                        Console.WriteLine($"{lastAllUsers[i].UsersName+"\t"}{lastAllUsers[i].NickName+"\t"}{lastAllUsers[i].Gender+"\t"}{currentTime[0].Chinese+"\t"}{currentTime[0].English+"\t"}{currentTime[0].Math+"\t"}{currentTime[0].RecordTime+"\t"}");
                    }

                }
                #endregion
            }
            #endregion

            //2、连表查询
            //1)展示所有的UserScoresT
            //2）展示最新的UserScoresT
            using (MyDBContext myDB = new MyDBContext())
            {
                var query = from user in myDB.UserT1//查询表UserT1别名user
                            join userScorest in myDB.UserScoresT1//左连接
                            on user.UsersName equals userScorest.UsersName//连接条件
                            //内连接
                            select new UserTAndUserScoresTModelForEF//得到的数据赋值给新表
                            {
                                UsersName=user.UsersName,
                                NickName=user.NickName,
                                Gender=user.Gender,
                                Password=user.Password,
                                Chinese=userScorest.Chinese,
                                English=userScorest.English,
                                Math=userScorest.Math,
                                RecordTime=userScorest.RecordTime
                            };//得到sql语句
                List<UserTAndUserScoresTModelForEF> list = query.ToList();
                foreach (var item in list)
                {
                    Console.WriteLine($"{item.UsersName+"\t"}{item.NickName+"\t"}{item.Gender+"\t"}{item.Chinese+"\t"}{item.English+"\t"}{item.Math+"\t"}{item.RecordTime+"\t"}");

                }
            }

            Console.WriteLine("模拟左连接");
            using (MyDBContext myDB = new MyDBContext())
            {
                var query = from user in myDB.UserT1//查询表UserT1别名user
                            join userScorest in myDB.UserScoresT1//左连接
                            on user.UsersName equals userScorest.UsersName
                            into utus//into把连接结果放入临时结果中
                            from userScorest in utus.DefaultIfEmpty()
                                //（模拟）左连接
                                //使用 from userScorest in utus.DefaultIfEmpty()，它遍历了 utus 中的每个分组，并且对于每个分组，即使没有匹配的 userScorest（即分组为空），也会通过 DefaultIfEmpty() 方法返回一个空的 userScorest（实际上是 userScorest 类型的默认值，对于引用类型是 null）。这种方式实际上模拟了左外连接的行为
                            select new UserTAndUserScoresTModelForEF//得到的数据赋值给新表
                            {
                                UsersName=user.UsersName,
                                NickName=user.NickName,
                                Gender=user.Gender,
                                Password=user.Password,
                                Chinese=userScorest.Chinese,
                                English=userScorest.English,
                                Math=userScorest.Math,
                                RecordTime=userScorest.RecordTime
                            };//得到sql语句
                List<UserTAndUserScoresTModelForEF> list = query.ToList();
                foreach (var item in list)
                {
                    Console.WriteLine($"{item.UsersName+"\t"}{item.NickName+"\t"}{item.Gender+"\t"}{item.Chinese+"\t"}{item.English+"\t"}{item.Math+"\t"}{item.RecordTime+"\t"}");

                }
            }

            Console.WriteLine("最新成绩");
            using (MyDBContext myDB = new MyDBContext())
            {
                //LINQ查询语法
                var query = from user in myDB.UserT1
                            join userScore in myDB.UserScoresT1
                            on user.UsersName equals userScore.UsersName
                            into utus
                            from userScorest in utus.OrderByDescending(e => e.RecordTime).Take(1).DefaultIfEmpty()//Take(1)
                            select new UserTAndUserScoresTModelForEF
                            {
                                UsersName=user.UsersName,
                                NickName=user.NickName,
                                Gender=user.Gender,
                                Password=user.Password,
                                Chinese=userScorest.Chinese,
                                English=userScorest.English,
                                Math=userScorest.Math,
                                RecordTime=userScorest.RecordTime
                            };
                List<UserTAndUserScoresTModelForEF> list = query.ToList();
                          
            }

            #region 连表查询，sql语言,已注释
            //List<UsersT1UsersScoresT1Model> twoTable = UsersT1UsersScoresT1Operation.GetUnionTwoTable();//  还未显示结果
            //Console.WriteLine("用户名，昵称，性别，语文成绩，英语成绩，数学成绩，时间");
            //for (int i = 0; i<twoTable.Count; i++)
            //{

            //    if (twoTable[i].Gender=="1")
            //        twoTable[i].Gender=InfoHelper.Gender1;
            //    else if (twoTable[i].Gender=="2")
            //        twoTable[i].Gender=InfoHelper.Gender2;
            //    else if (twoTable[i].Gender=="3")
            //        twoTable[i].Gender=InfoHelper.Gender3;
            //    Console.WriteLine($"{twoTable[i].UsersName+"\t"}{twoTable[i].NickName+"\t"}{twoTable[i].Gender+"\t"}{twoTable[i].Chinese+"\t"}{twoTable[i].English+"\t"}{twoTable[i].Math+"\t"}{twoTable[i].RecordTime+"\t"}");

            //}
            #endregion

            #region 老一表查询方法，已注释
            //string sql = "select*from UserT1";
            //Console.WriteLine("用户名 昵称 性别");
            //DataTable tableTwo = SQLHelper.SelectData(sql);
            //List<UserT1Model> users = new List<UserT1Model>();
            //for (int i = 0; i<tableTwo.Rows.Count; i++)
            //{
            //    UserT1Model model = new UserT1Model();
            //    model=UserT1Operation.operation(tableTwo, i);//  用方法完成下面这个步骤
            //    //  另一个相同作用的方法方法
            //    model=UserT1Operation.DataRowToUserTModel(tableTwo.Rows[i]);
            //    #region 方法实现的内容
            //    //model.UsersName=tableTwo.Rows[i]["UsersName"].ToString();// 还可以把这一步抽象为方法
            //    //model.Password=tableTwo.Rows[i]["Password"].ToString();
            //    //model.NickName=tableTwo.Rows[i]["NickName"].ToString();
            //    //model.Gender=tableTwo.Rows[i]["Gender"].ToString();
            //    #endregion
            //    users.Add(model);
            //}
            //for (int i = 0; i<users.Count; i++)
            //{
            //    if (users[i].Gender=="1")
            //        users[i].Gender=InfoHelper.Gender1;
            //    else if (users[i].Gender=="2")
            //        users[i].Gender=InfoHelper.Gender2;
            //    else if (users[i].Gender=="3")
            //        users[i].Gender=InfoHelper.Gender3;
            //    Console.WriteLine($"{users[i].UsersName}   {users[i].NickName}   {users[i].Gender}");//    $用来字符串拼接
            //}
            #endregion

            #region 查询兩表所有数据老方法 已注释
            ////  查询所有数据老方法
            //List<UserT1Model> usersOne = UserT1Operation.AllUsers();

            //for (int i = 0; i<usersOne.Count; i++)
            //{
            //    if (usersOne[i].Gender=="1")
            //        usersOne[i].Gender=InfoHelper.Gender1;
            //    else if (usersOne[i].Gender=="2")
            //        usersOne[i].Gender=InfoHelper.Gender2;
            //    else if (usersOne[i].Gender=="3")
            //        usersOne[i].Gender=InfoHelper.Gender3;

            //    //  查询其成绩
            //    List<UserScoresT1Model> usersScore = UserScoresT1Operation.GetScoreByUsersName(usersOne[i].UsersName);
            //    Console.WriteLine($"{usersOne[i].UsersName}用户输入成绩如下");
            //    Console.WriteLine("入录时间  语文成绩  数学成绩  英语成绩");
            //    for (int j = 0; j<usersScore.Count; j++)
            //    {

            //        Console.WriteLine($"{usersOne[i].UsersName}   {usersOne[i].NickName}   {usersOne[i].Gender}   {usersScore[j].RecordTime}   {usersScore[j].Chinese}   {usersScore[j].Math}   {usersScore[j].English}");//    $用来字符串拼接
            //    }
            //    Console.WriteLine($"{usersOne[i].UsersName}   {usersOne[i].NickName}   {usersOne[i].Gender}");

            //}
            #endregion

            #region 老版遍历 不显示结果，已注释
            //for (int i = 0; i<tableTwo.Rows.Count; i++)
            //{
            //    string genderForSQL = tableTwo.Rows[i]["Gender"].ToString();

            //    //  调用语言文档（此处用类InfoHelper）
            //    if (genderForSQL=="1")//    此处的1 2是数据库中的1 2（分辨男女的）
            //        genderForSQL=InfoHelper.Gender1;
            //    else if (genderForSQL=="2")
            //        genderForSQL=InfoHelper.Gender2;
            //    else if (genderForSQL=="3")
            //        genderForSQL=InfoHelper.Gender3;

            #region 不用语言文档的麻烦用法
            //if (language==1)
            //{
            //    if (genderForSQL=="1")
            //        genderForSQL="男";
            //    else if (genderForSQL=="2")
            //        genderForSQL="女";
            //}

            //if (language==2)
            //{
            //    if (genderForSQL=="1")
            //        genderForSQL="Male";
            //    else if (genderForSQL=="2")
            //        genderForSQL="Famale";                   
            //}
            #endregion

            //Console.WriteLine($"{tableTwo.Rows[i]["UsersName"]}   {tableTwo.Rows[i]["NickName"]}   {genderForSQL}");//    $用来字符串拼接
            //}
            #endregion
            #endregion
            Console.ReadKey();
        }
    }
}

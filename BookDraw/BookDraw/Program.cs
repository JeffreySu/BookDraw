using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookDraw
{
    public class MemberInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var tencentSharePrice = 284.80;//“腾讯控股（hk00700）”2017年7月14日股票收盘价格
            //1874为模拟数字
            var forkCount = 1874;//https://github.com/JeffreySu/WeiXinMPSDK 2017年7月14日20:00:00的Fork数量
            var groupCount = 500;//每组分组人数

            var memberGrooupList = new List<List<MemberInfo>>();

            Console.WriteLine("抽奖规则：");
            Console.WriteLine("1、按照已支付支付1元的次序，对所有抽奖名单进行排序（名单位于OneYuanCrowdFunding.txt下）");
            Console.WriteLine("2、根据2017年7月4日“腾讯控股”股票收盘价格*10并取整作为基数（如价格为2843.70，则基数为2837），乘以2017年7月14日20:00时，https://github.com/JeffreySu/WeiXinMPSDK 项目的Fork的数字，作为参数");
            Console.WriteLine("3、所有的抽奖人员按照顺序以500人为一组（目前只有一组），组内编号为“参数/本组人员总数 的余数”的人员即为获奖人员。");
            Console.WriteLine("注意：当前为模拟测试数据！");
            Console.WriteLine("按任意键开始...");
            Console.ReadKey();

            Console.WriteLine("开始导入众筹1元名单\r\n");
            Console.WriteLine("编号\t姓名\t手机\t\t金额");
            Console.WriteLine("------------------------------------");

            using (var fw = new FileStream("..\\..\\OneYuanCrowdFunding.txt", FileMode.Open))
            {
                using (var sr = new StreamReader(fw, Encoding.UTF8/*Encoding.GetEncoding("GB2312")*/))
                {
                    var data = sr.ReadToEnd();

                    List<MemberInfo> group = null;

                    var lineData = data.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < lineData.Count(); i++)
                    {
                        if (i % groupCount == 0)
                        {
                            group = new List<MemberInfo>();
                            memberGrooupList.Add(group);
                        }

                        var lineStr = lineData[i];

                        Console.WriteLine(lineStr);

                        var memberData = lineStr.Split(new[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);

                        var memberInfo = new MemberInfo()
                        {
                            Id = int.Parse(memberData[0]),
                            Name = memberData[1],
                            Phone = memberData[2]
                        };

                        group.Add(memberInfo);
                    }
                }
            }

            Console.WriteLine("导入名单完成，总数：" + memberGrooupList.Sum(z => z.Count));
            Console.WriteLine("分组数量：" + memberGrooupList.Count());

            Console.WriteLine("“腾讯控股（hk00700）”2017年7月14日股票收盘价格："+ tencentSharePrice);
            var prar = (int) (tencentSharePrice * 10);
            Console.WriteLine("参数："+ prar);
            Console.WriteLine("https://github.com/JeffreySu/WeiXinMPSDK 2017年7月14日20:00:00的Fork数量："+ forkCount);
            var baseNumber = prar * forkCount;
            Console.WriteLine("相乘结果："+ baseNumber);

            Console.WriteLine("开始计算...");

            for (int i = 0; i < memberGrooupList.Count; i++)
            {
                Console.WriteLine("开始抽第{0}组",i+1);
                var group = memberGrooupList[i];
                var winIndex = baseNumber % group.Count();
                var winner = group.Skip(winIndex).Take(1).First();
                Console.WriteLine("中奖者：{0}\t{1}\t{2}", winner.Id,winner.Name,winner.Phone);
            }


            Console.WriteLine("按任意键退出...");
            Console.ReadKey();
        }

    }
}

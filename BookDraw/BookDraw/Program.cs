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
            Console.WriteLine("开始导入众筹1元名单\r\n");
            Console.WriteLine("编号\t姓名\t手机\t\t金额");
            Console.WriteLine("------------------------------------");

            using (var fw = new FileStream("..\\..\\OneYuanCrowdFunding.txt", FileMode.Open))
            {
                using (var sr = new StreamReader(fw, Encoding.GetEncoding("GB2312")))
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

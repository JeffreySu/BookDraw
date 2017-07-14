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
            var memberList = new List<MemberInfo>();
            Console.WriteLine("开始导入众筹1元名单");

            using (var fw = new FileStream("..\\..\\OneYuanCrowdFunding.txt", FileMode.Open))
            {
                using (var sr = new StreamReader(fw,Encoding.GetEncoding("GB2312")))
                {
                    var data = sr.ReadToEnd();

                    data.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries)
                        .ToList()
                        .ForEach(lineStr =>
                        {
                            Console.WriteLine(lineStr);

                            var memberData = lineStr.Split(new[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);

                            var memberInfo = new MemberInfo()
                            {
                                Id = int.Parse(memberData[0]),
                                Name = memberData[1],
                                Phone = memberData[2]
                            };

                            memberList.Add(memberInfo);
                        });
                }
            }
            Console.WriteLine("导入名单完成，总数："+ memberList.Count);


            Console.WriteLine("按任意键退出...");
            Console.ReadKey();
        }
    }
}

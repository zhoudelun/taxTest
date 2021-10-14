using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public  class Program
    {
        static void Main(string[] args)
        {
            string[] input1 = new string[] { "1 book at 12.49", "1 music CD at 14.99", "1 chocolate bar at 0.85" };
            string[] input2 = new string[] { "1 imported box of chocolates at 10.00", "1 imported bottle of perfume at 47.50" };
            string[] input3 = new string[] { "1 imported bottle of perfume at 27.99", "1 bottle of perfume at 18.99", "1 packet of headache pills at 9.75", "1 box of imported chocolates at 11.25" };

            string outStr = CalcReceiptDetails(input1);
            Console.WriteLine(outStr);
            outStr = CalcReceiptDetails(input2);
            Console.WriteLine(outStr);
            outStr = CalcReceiptDetails(input3);
            Console.WriteLine(outStr);
            Console.Read();
        }

        public   static string CalcReceiptDetails(string[] goods)
        {
            string result = string.Empty;
            StringBuilder sb = new StringBuilder();
            string rgx = @"((\d+) ([\w|\s]*)) at (\d+.\d+)?";
            string salesGoods = @"book|chocolate|pills";//10% except
            double taxSum = 0, totalSum = 0;
            StringBuilder sbOut = new StringBuilder();
            foreach (var item in goods)
            {
                var m = Regex.Match(item, rgx);
                int count = m.Groups.Count;
                if (count != 5)
                {
                    return "input invalid!";
                }
                sbOut.AppendLine(Regex.Replace(item, rgx, me =>
                {
                    string goodsName = me.Groups[3].Value;
                    double goodsPrice = double.Parse(me.Groups[4].Value);
                    double tax = 0, total = 0;
                    if (!Regex.IsMatch(goodsName, salesGoods))
                    {
                        tax += goodsPrice * 0.1;
                    }
                    if (goodsName.Contains("imported"))
                    {
                        tax +=  goodsPrice * 0.05 ;
                    }
                    tax = Math.Ceiling(tax / 0.05) * 0.05;
                    taxSum += tax;
                    total += goodsPrice + tax;
                    totalSum += total;
                    return $"{me.Groups[1].Value}: {total:F}";
                }));
            }
            sbOut.AppendLine($@"Sales Taxes: {taxSum:F}");
            sbOut.AppendLine($@"Total: { totalSum:F}");
            return sbOut.ToString();
        }

        //Input 1:

        //1 book at 12.49
        //1 music CD at 14.99
        //1 chocolate bar at 0.85

        //Input 2:

        //1 imported box of chocolates at 10.00
        //1 imported bottle of perfume at 47.50

        //Input 3:

        //1 imported bottle of perfume at 27.99
        //1 bottle of perfume at 18.99
        //1 packet of headache pills at 9.75
        //1 box of imported chocolates at 11.25

        //OUTPUT

        //Output 1:

        //1 book : 12.49
        //1 music CD: 16.49
        //1 chocolate bar: 0.85
        //Sales Taxes: 1.50
        //Total: 29.83

        //Output 2:

        //1 imported box of chocolates: 10.50
        //1 imported bottle of perfume: 54.65
        //Sales Taxes: 7.65
        //Total: 65.15

        //Output 3:

        //1 imported bottle of perfume: 32.19
        //1 bottle of perfume: 20.89
        //1 packet of headache pills: 9.75
        //1 imported box of chocolates: 11.85
        //Sales Taxes: 6.70
        //Total: 74.68 
    }
}

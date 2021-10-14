using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{ 
    /// <summary>
    /// 进货
    /// </summary>
    public class Product
    { 
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public int Unit { get; set; }

        /// <summary>
        /// 进货价
        /// </summary>
        public double BuyPrice { get; set; }

        /// <summary>
        /// 格式化输出
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Quantity} {Name} at {BuyPrice:F}";
        }
    }

    /// <summary>
    /// 销售品
    /// </summary>
    public class SaleItem : Product
    {

        /// <summary>
        /// 免税
        /// </summary>
        public bool Exempted
        {
            get
            {
                return Regex.IsMatch(Name, "book|chocolate|pills");
            }
        }

        /// <summary>
        /// 进口
        /// </summary>
        public bool Imported
        {
            get
            {
                return Name.Contains("imported");
            }
        }

        /// <summary>
        /// 销售价
        /// </summary>
        public double SalesPrice {
            get {
                return BuyPrice + SalesTax;
            }
        }

        /// <summary>
        /// 基础税
        /// </summary>
        public double BasicTax {
            get
            {
                return Exempted?0: BuyPrice * 0.1;
            }
        }

        /// <summary>
        /// 进口税
        /// </summary>
        public double ImportedTax {
            get
            {
                return Imported?BuyPrice * 0.05:0;
            }
        }

        /// <summary>
        /// 税
        /// </summary>
        public double SalesTax
        {
            get
            {
                return Math.Ceiling((BasicTax+ImportedTax ) / 0.05) * 0.05;
            }
        } 

        /// <summary>
        /// 格式化输出
        /// </summary>
        /// <returns></returns>
        public new string ToString()
        {
            return $"{Quantity} {Name}: {SalesPrice:F}";
        } 

        /// <summary>
        /// 利润
        /// </summary>
        public double Profit { get; set; }
    }

    /// <summary>
    /// 购物车
    /// </summary>
    public class SaleItems
    {
        public List<SaleItem> saleItems { get; set; }

        public double SalesTax { 
            get
            {
                return saleItems.Sum(s => s.SalesTax);
            }
        }

        public double SalesPrice {
            get {
                return saleItems.Sum(s => s.SalesPrice); 
            }
        }

        /// <summary>
        /// 输出
        /// </summary>
        /// <param name="products"></param>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (Product p in saleItems)
            {
                sb.AppendLine(p.ToString());
            }
            sb.AppendLine(); 
            foreach (var s in saleItems)
            {
                sb.AppendLine(s.ToString());  
            }
            sb.AppendLine($@"Sales Taxes: {SalesTax:F}");
            sb.AppendLine($@"Total: { SalesPrice:F}");
            return sb.ToString();
        }

    }
}

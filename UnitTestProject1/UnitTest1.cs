using System;
using System.Collections.Generic;
using System.Text;
using ConsoleApp1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {

        string[] input1 = new string[] { "1 book at 12.49", "1 music CD at 14.99", "1 chocolate bar at 0.85" };
        string[] input2 = new string[] { "1 imported box of chocolates at 10.00", "1 imported bottle of perfume at 47.50" };
        string[] input3 = new string[] { "1 imported bottle of perfume at 27.99", "1 bottle of perfume at 18.99", "1 packet of headache pills at 9.75", "1 box of imported chocolates at 11.25" };

        [TestMethod]
        public void TestMethod0()
        {
            Assert.AreEqual("a", "a");
        }
        [TestMethod]
        public void TestMethod1()
        {
            string it1 = Program.CalcReceiptDetails(input1);
            Assert.AreEqual("1 book: 12.49\r\n1 music CD: 16.49\r\n1 chocolate bar: 0.85\r\nSales Taxes: 1.50\r\nTotal: 29.83\r\n", it1);
        }
        [TestMethod]
        public void TestMethod2()
        {
            string it2 = Program.CalcReceiptDetails(input2);
            Assert.AreEqual("1 imported box of chocolates: 10.50\r\n1 imported bottle of perfume: 54.65\r\nSales Taxes: 7.65\r\nTotal: 65.15\r\n", it2);
        }
        [TestMethod]
        public void TestMethod3()
        {
            string it3 = Program.CalcReceiptDetails(input3);
            Assert.AreEqual("1 imported bottle of perfume: 32.19\r\n1 bottle of perfume: 20.89\r\n1 packet of headache pills: 9.75\r\n1 box of imported chocolates: 11.85\r\nSales Taxes: 6.70\r\nTotal: 74.68\r\n", it3);
        }
        [TestMethod]
        public void TestMethod4()
        {
            Assert.AreNotEqual("a", "b");
        }


        [TestMethod]
        public void TestMethod5()
        {
            SaleItem saleItem1 = new SaleItem { Quantity = 1, Name = "book", BuyPrice = 12.49 },
                saleItem2 = new SaleItem { Quantity = 1, Name = "music CD", BuyPrice = 14.99 },
                saleItem3 = new SaleItem { Quantity = 1, Name = "chocolate bar", BuyPrice = 0.85 };
            List<SaleItem> list = new List<SaleItem>
            {
               saleItem1,saleItem2,saleItem3
            };
            var saleItems= new SaleItems();
            saleItems.saleItems = list;

            Assert.AreEqual(saleItem1.Exempted, true);
            Assert.AreEqual(saleItem1.BasicTax, 0);
            Assert.AreEqual(saleItem1.Imported,false); 
            Assert.AreEqual(saleItem1.SalesTax, 0);

            Assert.AreEqual(saleItem1.SalesTax, 0);
            Assert.AreEqual(saleItem1.SalesPrice, 12.49);
            Assert.AreEqual(saleItems.SalesTax, 1.50);
            Assert.AreEqual(Math.Round( saleItems.SalesPrice,2), 29.83);
            string output = saleItems.ToString();//输入输出
        } 
    }
}

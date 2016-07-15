using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.Demo.TestSource
{
   public class DataSourceFactory
    {

       public static List<Student> CreateListSource()
       {
           List<Student> ss = new List<Student>();

           Student s = new Student() { Id="1",Pid="",Name="周杰伦",Sex="男", Age=33,Merryerd=true,BordTime=DateTime.Now.AddYears(33),Jindu=44};

           s = new Student() { Id = "2", Pid = "1", Name = "刘德华", Sex = "女" ,Age = 33, Merryerd = true, BordTime = DateTime.Now.AddYears(33), Jindu = 44 };

            ss.Add(s);
            s = new Student() { Id = "3", Pid = "2", Name = "刘晓庆", Sex = "男", Age = 33, Merryerd = true, BordTime = DateTime.Now.AddYears(33), Jindu = 44 };
            ss.Add(s);
            s = new Student() { Id = "4", Pid = "1", Name = "蔡依林", Sex = "男", Age = 33, Merryerd = true, BordTime = DateTime.Now.AddYears(33), Jindu = 44 };
            ss.Add(s);
            s = new Student() { Id = "5", Pid = "3", Name = "卓玛", Sex = "男", Age = 33, Merryerd = true, BordTime = DateTime.Now.AddYears(33), Jindu = 44 };
            ss.Add(s);
            s = new Student() { Id = "6", Pid = "4", Name = "柯南", Sex = "女", Age = 33, Merryerd = true, BordTime = DateTime.Now.AddYears(33), Jindu = 44 };
            ss.Add(s);
            s = new Student() { Id = "7", Pid = "4", Name = "刘能", Sex = "女", Age = 33, Merryerd = true, BordTime = DateTime.Now.AddYears(33), Jindu = 44 };
            ss.Add(s);
            s = new Student() { Id = "8", Pid = "4", Name = "范志毅", Sex = "女", Age = 33, Merryerd = true, BordTime = DateTime.Now.AddYears(33), Jindu = 44 };
            ss.Add(s);
            s = new Student() { Id = "9", Pid = "3", Name = "福原爱", Sex = "男", Age = 33, Merryerd = true, BordTime = DateTime.Now.AddYears(33), Jindu = 44 };
            ss.Add(s);

           return ss;
       }

       public static List<ProductRecord> CreateRecordListSource()
       {
           ProductRecord[] records = new ProductRecord[11];
           records[0] = new ProductRecord("Product Name", "Chai", "Teatime Chocolate Biscuits", "Ipoh Coffee", 0);
           records[1] = new ProductRecord("Category", 1, 2, 1, 1);
           records[2] = new ProductRecord("Supplier", "Exotic Liquids", "Specialty Biscuits, Ltd.", "Leka Trading", 2);
           records[3] = new ProductRecord("Quantity Per Unit", "10 boxes x 20 bags", "10 boxes x 12 pieces", "16 - 500 g tins", 3, 0);
           records[4] = new ProductRecord("Unit Price", 18.00, 9.20, 46.00, 4, 0);
           records[5] = new ProductRecord("Units in Stock", 39, 25, 17, 5, 0);
           records[6] = new ProductRecord("Discontinued", true, false, true, 6, 0);
           records[7] = new ProductRecord("Last Order", new DateTime(2010, 12, 14), new DateTime(2010, 7, 20), new DateTime(2010, 1, 7), 7);
           records[8] = new ProductRecord("Relevance", 70, 90, 50, 8);
           records[9] = new ProductRecord("Contact Name", "Shelley Burke", "Robb Merchant", "Sven Petersen", 9, 2);
           records[10] = new ProductRecord("Phone", "(100)555-4822", "(111)555-1222", "(120)555-1154", 10, 2);
           return records.ToList();
       }

       public static List<ProductRecord> CreateRecordTestSource()
       {
           ProductRecord[] records = new ProductRecord[11];
           records[0] = new ProductRecord("Product Name", "Chai", "Teatime Chocolate Biscuits", "Ipoh Coffee", 0);
           records[1] = new ProductRecord("Category", 1, 2, 1, 1);
           records[2] = new ProductRecord("Supplier", "Exotic Liquids", "Specialty Biscuits, Ltd.", "Leka Trading", 2);
           records[3] = new ProductRecord("Quantity Per Unit", "10 boxes x 20 bags", "10 boxes x 12 pieces", "16 - 500 g tins", 3, 0);
           records[4] = new ProductRecord("Unit Price", 18.00, 9.20, 46.00, 4, 0);
           records[5] = new ProductRecord("Units in Stock", 39, 25, 17, 5, 0);
           records[6] = new ProductRecord("Discontinued", true, false, true, 6, 0);
           records[7] = new ProductRecord("Last Order", new DateTime(2010, 12, 14), new DateTime(2010, 7, 20), new DateTime(2010, 1, 7), 7);
           records[8] = new ProductRecord("Relevance", 70, 90, 50, 8);
           records[9] = new ProductRecord("Contact Name", "Shelley Burke", "Robb Merchant", "Sven Petersen", 9, 2);
           records[10] = new ProductRecord("Phone", "(100)555-4822", "(111)555-1222", "(120)555-1154", 10, 2);

           List<string> temps = new List<string>()
           {
               "33","rere","tete","retret"
           };

           return records.ToList();
       }
    }
}

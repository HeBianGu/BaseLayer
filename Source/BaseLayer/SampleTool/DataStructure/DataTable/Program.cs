using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Datatable
{
    class Program
    {
        static void Main(string[] args)
        {


            //获取第一个数据源DataTable
            DataTable dt1 = new DataTable();// DBHelper.GetDataTable("select top 10  ksdid,user_id,user_pwd from ksd_user_info");

            IEnumerable<DataRow> query1 = dt1.AsEnumerable().Where(t => t.Field<string>("user_id").StartsWith("66")).ToList();
            //获取第二个数据源DataTable
            DataTable dt2 = query1.CopyToDataTable();

            /*
            //比较两个数据源的交集
            IEnumerable<DataRow> query2 = dt1.AsEnumerable().Intersect(dt2.AsEnumerable(), DataRowComparer.Default);
            //两个数据源的交集集合      
            DataTable dt3 = query2.CopyToDataTable();
       

            //获取两个数据源的并集
            IEnumerable<DataRow> query2 = dt1.AsEnumerable().Union(dt2.AsEnumerable(), DataRowComparer.Default);
            //两个数据源的并集集合
            DataTable dt3 = query2.CopyToDataTable();
              */

            //获取两个数据源的差集
            IEnumerable<DataRow> query2 = dt1.AsEnumerable().Except(dt2.AsEnumerable(), DataRowComparer.Default);
            //两个数据源的差集集合
            DataTable dt3 = query2.CopyToDataTable();

        }
    }
}

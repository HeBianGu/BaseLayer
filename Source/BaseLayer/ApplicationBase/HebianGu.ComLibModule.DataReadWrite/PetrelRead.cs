using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
PRESSURE
3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 
3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 
3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 
3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 
3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 3600 
3600 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 
3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 
3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 
3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 
3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 3608 
3608 3608 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 
3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 
3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 
3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 
3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 3616 
3616 3616 3616 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 
3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 
3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 
3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 
3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 3623 
3623 3623 3623 3623 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 
3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 
3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 
3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 
3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 3633 
3633 3633 3633 3633 3633 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 
3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 
3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 
3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 
3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 3650 
3650 3650 3650 3650 3650 3650 
/
 */
namespace HebianGu.ComLibModule.DataReadWrite
{
    public class PretrlRead
    {
        /// <summary> 按三维格式读取到三维表格中 </summary>
        public List<GridTable> ReadToTables(List<string> lines, int tableCount, int xCount, int yCount)
        {
            List<GridTable> tables = new List<GridTable>();

            for (int i = 1; i <= tableCount; i++)
            {
                if (tables.Exists(l => l.IndexNum == i))
                {
                    //  存在该层不处理
                    continue;
                }
                GridTable t = new GridTable();
                t.Parent = this;
                t.IndexNum = i;
                t.XCount = xCount;
                t.YCount = yCount;
                t.Build();
                tables.Add(t);
            }


            int vIndex = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                //  过滤无效行
                if (lines[i].Trim() == "/" || lines[i].Trim().StartsWith("--"))
                {
                    continue;
                }
                string[] pl = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                for (int j = vIndex; j < vIndex + pl.Length; j++)
                {
                    //  表格索引
                    int tableIndex = j / (xCount * yCount);

                    //  行索引
                    int rowIndex = (j % (xCount * yCount)) / yCount;

                    //  列索引
                    int colIndex = (j % (xCount * yCount)) % yCount;

                    //  插入数据
                    tables[tableIndex].Rows[rowIndex][colIndex] = pl[j - vIndex];

                }

                vIndex += pl.Length;
            }

            return tables;
        }
    }

    public class GridTable : DataTable
    {
        public GridTable()
        {

        }
        private int indexNum;
        /// <summary> 层号 </summary>
        public int IndexNum
        {
            get { return indexNum; }
            set { indexNum = value; }
        }

        int xCount;

        public int XCount
        {
            get { return xCount; }
            set { xCount = value; }
        }

        int yCount;

        public int YCount
        {
            get { return yCount; }
            set { yCount = value; }
        }

        //TableKey parent = null;

        //public TableKey Parent
        //{
        //    get { return parent; }
        //    set { parent = value; }
        //}
        public void Build()
        {
            this.TableName = IndexNum.ToString(); ;
            for (int x = 1; x <= xCount; x++)
            {
                this.Columns.Add(x.ToString());
            }
            for (int y = 0; y < yCount; y++)
            {
                DataRow dr = this.NewRow();
                //for (int index = 0; index < xCount; index++)
                //{
                //    //dr[index] = "4"; 
                //}
                this.Rows.Add(dr);
            }
        }


    }
}

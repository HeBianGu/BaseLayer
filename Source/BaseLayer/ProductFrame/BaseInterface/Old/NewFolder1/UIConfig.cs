using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if  false

namespace OPT.Product.BaseInterface
{
    public enum BSEControlType
    {
        None = 0,
        Edit = 1,
        UnitEdit = 5,
        ComboBoxDD = 2,
        ComboBoxDL = 3,
        CheckComboBox = 4,
        UnitComboBoxDD,
        UnitComboBoxDL,
        MultiColumnTable = 201
    }

    public interface IBSUIConfigMultiColumn
    {
        //列的个数
        Int32 Count { get; }
        //每一列的信息
        IEnumerable<IBSUIConfig> Columns { get; }
        //每一列所占的比例(宽度)
        IEnumerable<UInt16> ColumnRatios { get; }
        //新增特性，用来解决201表和属性表的中心拆分线对齐的问题。
        //如果此项的值不等于-1，则此项表示201表的哪一条拆分线和中心拆分线对齐。
        //如果等于-1，则表示没有任何一条线需要和中心线对齐。
        Int16 MiddleColumn { get; }
    }
    public class BSRatio : BLKeyValue<UInt16, UInt16>
    {
        UInt16 _part1 = 0;
        UInt16 _part2 = 0;
        public UInt16 Part1 { get { return _part1; } set { _part1 = value; } }
        public UInt16 Part2 { get { return _part2; } set { _part2 = value; } }

        public BSRatio() { }
        public BSRatio(UInt16 part1, UInt16 part2)
        {
            _part1 = part1;
            _part2 = part2;
        }
    }

    /// <summary>
    /// 此接口用来提供属性表所需要的信息
    /// </summary>
    public interface IBSUIConfig
    {
        //名字
        Int32 ID { get; }
        //名字
        string Name { get; }
        //表示此节点是否显示（对整个节点有效，包括它的标题行及其子节点）
        bool Show { get; }
        //仅对复合节点有效，标识此复合属性在属性表中显示时，标题行是否显示
        bool ShowTitle { get; }
        //仅对复合节点有效，标识此复合属性是否继续展开其下的子节点
        bool Expand { get; }
        //是否被用户隐藏:
        //对于整个属性表，增加一个按钮，当用户可以通过此按钮调出一个设置界面，
        //用户可以在此界面上选择哪些节点可以被暂时隐藏掉，
        //也可以通过此界面把之前隐藏掉的节点再恢复出来。
        bool UserHide { get; set; }
        //只读
        bool ReadOnly { get; }
        //单位是否只读
        bool ValueReadOnly { get; }
        //是否折叠
        bool Fold { get; }
        //单位
        IBxUnit Unit { get; set; }
        //控件类型
        BSEControlType ControlType { get; }
        //只对于201类型的控件有效，用来承载列信息
        IBSUIConfigMultiColumn MultiColumn { get; }
        //左右拆分的比例
        BSRatio Ratio { get; }
        //TODO :补充完整IBSUIConfig

    }

    public interface IBSUIConfigable
    {
        IBSUIConfig UIConfig { get; }
    }

   // public delegate 
    public interface IBSUIValue
    {
        string GetUIValue();
        bool SetUIValue(string val); 
    }
}
#endif
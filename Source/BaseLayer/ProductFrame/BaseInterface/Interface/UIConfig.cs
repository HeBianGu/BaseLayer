using System;
using System.Collections;
using System.Collections.Generic;

namespace OPT.Product.BaseInterface
{
    public interface IBxSubColumn
    {
        IBxUIConfig UIConfig { get; }
        UInt16 WidthRation { get; }
        string Code { get; }
    }

    public interface IBxSubColumns
    {
        IEnumerable<IBxSubColumn> Columns { get; }
        Int32 Count { get; }
        IBxSubColumn GetAt(int index);
        int IndexOf(IBxSubColumn column);
        int CenterColumn { get; }
        IBxSubColumn this[int index] { get; }
    }

    public interface IBxRange
    {
        double? Min { get; }
        double? Max { get; }
        bool MinValid { get; }
        bool MaxValid { get; }
        bool IsValid(double val);
    }

    /// <summary>
    /// 此接口用来提供属性表所需要的信息
    /// </summary>
    public interface IBxUIConfig
    {
        //id
        int ID { get; }
        string FullID { get; }
        //名字
        string Name { get; }
        //表示此节点是否显示（对整个节点有效，包括它的标题行及其子节点）
        bool? Show { get; }
        //仅对复合节点有效，标识此复合属性在属性表中显示时，标题行是否显示
        bool? ShowTitle { get; }
        //仅对复合节点有效，标识此复合属性是否继续展开其下的子节点
        bool? Expand { get; }
        //是否被用户隐藏:
        //对于整个属性表，增加一个按钮，当用户可以通过此按钮调出一个设置界面，
        //用户可以在此界面上选择哪些节点可以被暂时隐藏掉，
        //也可以通过此界面把之前隐藏掉的节点再恢复出来。
        bool? UserHide { get; set; }
        //只读
        bool? ReadOnly { get; }
        //单位是否只读
        bool? ValueReadOnly { get; }
        //是否折叠
        bool? Fold { get; }
        //控件类型
        Int32 ControlType { get; }
        //单位
        IBxUnit Unit { get; set; }
        //小数位数
        int DecimalDigits { get; set; }
        //表示此Item属于哪一个分组，只对根节点下的第一级子节点有效
        string ColumnName { get; }
        string ColumnID { get; }
        //只对于201类型的控件有效，用来承载列信息
        //IBLUIConfigMultiColumn MultiColumn { get; }
        //左右拆分的比例
        // BLRatio Ratio { get; }

        //图标
        string Icon { get; }
        //取值范围
        IBxRange Range { get; }
        string MenuWidth { get; }

        //提示信息
        string HelpString { get; }

        IBxSubColumns SubColumns { get; }

        //TODO :补充完整IBSUIConfig

    }

    // public interface IBxUIConfig : IBxUIConfig
    //{
    //UInt16 WidthRation { get; }
    // }


    public interface IBxUIConfigable
    {
        IBxUIConfig UIConfig { get; }
    }

    //public interface IBxUIConfigableEx : IBxUIConfigable
    //{
    //    void InitConfigID(string xmlConfigID, Int32 itemID);
    //}




}
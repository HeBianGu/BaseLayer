using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HebianGu.ComLibModule.ControlEx
{
    /// <summary> Combobox扩展方法 </summary>
    public static class ComboboxEx
    {
        /// <summary> 绑定数据到枚举 只绑定名称 </summary>
        public static void BindEnum<T>(this ComboBox cmb) where T : struct,IComparable
        {
            //  是枚举
            if (typeof(T).IsEnum)
            {
                cmb.DataSource = System.Enum.GetNames(typeof(T));
            }
        }

        /// <summary> 获取选中的枚举值 通过名称获取 </summary>
        public static T GetSelectValue<T>(this ComboBox cmb) where T : struct,IComparable
        {
            T testenum = (T)Enum.Parse(typeof(T), cmb.SelectedItem.ToString(), false);

            return testenum;
        }

        /// <summary> 绑定枚举的Desc特性名称 </summary>
        public static void BindEnumShowDesc<T>(this ComboBox cmb)
            where T : struct,IComparable
        {
            //  是枚举
            if (typeof(T).IsEnum)
            {
                List<string> objs = new List<string>();

                FieldInfo[] files = typeof(T).GetFields();

                foreach (var v in files)
                {
                    DescriptionAttribute r = v.GetCustomAttribute<DescriptionAttribute>();
                    if (r != null)
                    {
                        objs.Add(r.Description);
                    }
                }

                cmb.DataSource = objs;
            }
        }

        /// <summary> 绑定枚举Field </summary>
        public static void BindEnumItem<T>(this ComboBox cmb)
            where T : struct,IComparable
        {
            //  是枚举
            if (typeof(T).IsEnum)
            {
                List<string> objs = new List<string>();

                FieldInfo[] files = typeof(T).GetFields();
                cmb.Items.AddRange(files);
            }
        }


        #region - 绑定枚举显示 Desc特性名称 -

        /// <summary> 绑定枚举的Desc特性名称 </summary>
        public static void BindEnumItemShowDesc<T>(this ComboBox cmb)
            where T : struct,IComparable
        {
            //  是枚举
            if (typeof(T).IsEnum)
            {
                List<string> objs = new List<string>();

                FieldInfo[] files = typeof(T).GetFields();

                List<EnumDisplayModel> models = new List<EnumDisplayModel>();

                foreach (var v in files)
                {
                    EnumDisplayModel model = new EnumDisplayModel();
                    model.Field = v;
                    DescriptionAttribute r = v.GetCustomAttribute<DescriptionAttribute>();
                    if (r != null)
                    {
                        model.Description = r.Description;
                        models.Add(model);
                    }
                }

                cmb.DataSource = models;

                cmb.DisplayMember = "Description";
            }
        }

        /// <summary> 获取选中的枚举值 </summary>
        public static T GetEnumItemByDesc<T>(this ComboBox cmb) where T : struct,IComparable
        {
            EnumDisplayModel model = cmb.SelectedItem as EnumDisplayModel;

            T testenum = (T)Enum.Parse(typeof(T), model.Field.Name, false);

            return testenum;
        }
        #endregion
    }

    class EnumDisplayModel
    {
        FieldInfo field;

        public FieldInfo Field
        {
            get { return field; }
            set { field = value; }
        }
        string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}

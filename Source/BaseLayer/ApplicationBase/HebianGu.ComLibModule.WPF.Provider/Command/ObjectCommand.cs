#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/3/29 14:47:59
 * 文件名：ObjectCommand
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HebianGu.ComLibModule.WPF.Provider
{
    /// <summary> 带有执行和判断执行的命令 </summary>
    public class ObjectCommand<T> : ICommand
    {
        Action<T> _excute;
        /// <summary> 执行的命令 </summary>
        public Action<T> Excute
        {
            get
            {
                return _excute;
            }

            set
            {
                _excute = value;
            }
        }
        Predicate<T> _canExcute;

        public event EventHandler CanExecuteChanged;

        /// <summary> 判断是否可以执行的方法 </summary>
        public Predicate<T> CanExcute
        {
            get
            {
                return _canExcute;
            }

            set
            {
                _canExcute = value;
            }
        }

        public ObjectCommand(Action<T> act, Predicate<T> match)
        {
            _excute = act;
            _canExcute = match;
        }


        /// <summary> 判断是否可以自行 </summary>
        public bool CanExecute(object parameter)
        {
            if (_canExcute == null) return true;

            return _canExcute((T)parameter);
        }


        /// <summary> 执行命令 </summary>
        public void Execute(object parameter)
        {
            if (_canExcute == null || _canExcute((T)parameter))
            {
                Excute((T)parameter);
            }
        }
    }
}

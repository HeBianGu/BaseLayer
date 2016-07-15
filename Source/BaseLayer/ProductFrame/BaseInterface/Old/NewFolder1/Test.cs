using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OPT.PEOffice6.BaseLayer1;


namespace OPT.PEOffice6.Test
{
    public class TestVisibleObject : IBLUIElement, IBLVisibleObject, IMessageFilter
    {
        #region IBLUIElement 成员
        IBLUIConfig IBLUIElement.UIConfig
        {
            get { throw new NotImplementedException(); }
        }
        IBLCompound IBLElement.Parent
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region IBLVisibleObject 成员
        Point IBLVisibleObject.Location
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        Size IBLVisibleObject.Size
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        int IBLVisibleObject.ZOrder
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        void IBLVisibleObject.OnPaint(PaintEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IMessageFilter 成员
        //返回值为true， 表示消息已被处理，不要再往后传递，因此消息被截获             
        //返回值为false，表示消息未被处理，需要再往后传递，因此消息未被截获    
        public bool PreFilterMessage(ref Message m)
        {
           if (m.Msg == BLWin32.WM_LBUTTONDOWN) // WM_LBUTTONDOWN  0x0201          
            {
                //System.Windows.Forms.MessageBox.Show("鼠标左键按下:" + MsgParams.ToPoint(ref m).ToString());
                //return true;
            }
           else if (m.Msg == BLWin32.WM_NCACTIVATE)
           {
               System.Windows.Forms.MessageBox.Show("鼠标左键按下2:" + MsgParams.ToPoint(ref m).ToString());
               return true;
           }
           else if (m.Msg == BLWin32.WM_ACTIVATE)
           {
               System.Windows.Forms.MessageBox.Show("鼠标左键按下3:" + MsgParams.ToPoint(ref m).ToString());
               return true;
           }
            return false;
        }
        #endregion
    }


}

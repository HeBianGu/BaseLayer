using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.FrameWorkMode.PassvationMode
{

    /// <summary> 订单审批过程索引管理器 </summary>
    public delegate bool OrderExamineApproveManagerHandler(Order order, ref OrderExamineApproveManagerHandler Mananger);


    /// <summary> 订单审批流管理器 </summary>
    [Serializable]
    public class OrderExamineApproveManager
    {
        /// <summary> 创建审批流程 </summary>
        public static OrderExamineApproveManager CreateFlows()
        {
            OrderExamineApproveManager result = new OrderExamineApproveManager();

            //  绑定信息员审批流程
            Infomationer inforMationer = new Infomationer();
            result.Flows += inforMationer.CheckPrices;
            result.Flows += inforMationer.CheckNumber;


            //  绑定业务经理审批流程
            BusinessManager businessManager = new BusinessManager();
            result.Flows += businessManager.CallPhoneConfirm;
            result.Flows += businessManager.SendEmailNotice;

            //  绑定总经理审批流程
            GeneralManager generalManager = new GeneralManager();
            result.Flows += generalManager.FinalConfirm;
            result.Flows += generalManager.SignAndRecord;

            return result;
        }

        /// <summary> 流程索引 </summary>
        public OrderExamineApproveManagerHandler Flows;


        /// <summary> 开始审批流程 </summary>
        public void RunFlows(Order order)
        {
            this.Flows(order, ref this.Flows);
        }
    }

    /// <summary> 信息员 </summary>
    [Serializable]
    public class Infomationer
    {
        /// <summary> 检查价格 </summary>
        public bool CheckPrices(Order order, ref OrderExamineApproveManagerHandler Mananger)
        {
            return false;

        }

        /// <summary> 检查数量 </summary>
        public bool CheckNumber(Order order, ref OrderExamineApproveManagerHandler Mananger)
        {
            return false;
        }
    }

    /// <summary> 业务经理 </summary>
    [Serializable]
    public class BusinessManager
    {
        /// <summary> 电话确认订单 </summary>
        public bool CallPhoneConfirm(Order order, ref OrderExamineApproveManagerHandler Mananger)
        {
            return false;
        }

        /// <summary> 发邮件通知 </summary>
        public bool SendEmailNotice(Order order, ref OrderExamineApproveManagerHandler Mananger)
        {
            return false;
        }

    }

    /// <summary> 总经理 </summary>
    [Serializable]
    public class GeneralManager
    {
        /// <summary> 最终确认 </summary>
        public bool FinalConfirm(Order order, ref OrderExamineApproveManagerHandler Mananger)
        {
            return false;
        }


        /// <summary> 签名并且记录备案 </summary>
        public bool SignAndRecord(Order order, ref OrderExamineApproveManagerHandler Mananger)
        {
            return false;
        }
    }

}

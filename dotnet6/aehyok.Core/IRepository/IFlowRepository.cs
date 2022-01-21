using aehyok.Core.Data;
using aehyok.Core.EntityFrameCore.MySql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.IRepository
{
    /// <summary>
    /// 流程接口定义
    /// </summary>
    public interface IFlowRepository: IDependency
    {
        /// <summary>
        /// 流程类型列表
        /// </summary>
        /// <returns></returns>
        Task<List<FlowEntityType>> GetFlowEntityTypeList();

        /// <summary>
        /// 保存流程类型
        /// </summary>
        /// <param name="flowEntityType"></param>
        /// <returns></returns>
        Task<int> SaveFlowEntityType(FlowEntityType flowEntityType);

        /// <summary>
        /// 获取流程类型详情
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        Task<FlowEntityType> GetFlowEntityType(string flowId);

        /// <summary>
        /// 删除流程类型
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        Task<int> DeleteFlowEntityType(string flowId);

        /// <summary>
        /// 流程状态列表
        /// </summary>
        /// <returns></returns>
        Task<List<FlowEntityState>> GetFlowEntityStateList(string flowId);

        /// <summary>
        /// 通过状态Id获取状态详情
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns></returns>
        Task<FlowEntityState> GetFlowEntityState(string stateId);

        /// <summary>
        /// 保存流程状态
        /// </summary>
        /// <param name="flowEntityState"></param>
        /// <returns></returns>
        Task<int> SaveFlowEntityState(FlowEntityState flowEntityState);

        /// <summary>
        /// 流程动作列表
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns></returns>
        Task<List<FlowStateTransition>> GetFlowEntityTransitionList(string stateId);

        /// <summary>
        /// 通过动作Id获取动作详情
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        Task<FlowStateTransition> GetFlowStateTransition(string actionId);

        /// <summary>
        /// 保存流程动作
        /// </summary>
        /// <param name="flowStateTransition"></param>
        /// <returns></returns>
        Task<int> SaveFlowStateTransition(FlowStateTransition flowStateTransition);
    }
}

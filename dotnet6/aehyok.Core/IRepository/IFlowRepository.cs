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
        /// <param name="id"></param>
        /// <returns></returns>
        Task<FlowEntityType> GetFlowEntityTypeById(string id);

        /// <summary>
        /// 删除流程类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DeleteFlowEntityType(string id);

        /// <summary>
        /// 流程状态列表
        /// </summary>
        /// <returns></returns>
        Task<List<FlowEntityState>> GetFlowEntityStateList(string flowId);

        /// <summary>
        /// 流程动作列表
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        Task<List<FlowStateTransition>> GetFlowEntityTransitions(string actionId);
    }
}

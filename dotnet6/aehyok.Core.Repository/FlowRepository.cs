//using aehyok.Core.EntityFrameCore.MySql.Data;
//using aehyok.Core.EntityFrameCore.MySql.Models;
//using aehyok.Core.IRepository;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace aehyok.Core.Repository
//{
//    public class FlowRepository : IFlowRepository
//    {
//        public async Task<int> DeleteFlowEntityType(string flowId)
//        {
//            var context = new MyDbContext();
//            var item = context.FlowEntityTypes.FirstOrDefault(t => t.Id == flowId);
//            context.FlowEntityTypes.Remove(item);
//            var result = await context.SaveChangesAsync();
//            return result;
//        }

//        public Task<FlowEntityState> GetFlowEntityState(string stateId)
//        {
//            var context = new MyDbContext();
//            var result = context.FlowEntityStates.FirstOrDefaultAsync(t => t.Id == stateId);
//            return result;
//        }

//        public async Task<List<FlowEntityState>> GetFlowEntityStateList(string flowId)
//        {
//            var context = new MyDbContext();
//            var list = await context.FlowEntityStates.Where(item => item.FlowId == flowId).ToListAsync();
//            return list;
//        }

//        public Task<List<FlowStateTransition>> GetFlowEntityTransitionList(string stateId)
//        {
//            var context = new MyDbContext();
//            var result = context.FlowStateTransitions.Where(t => t.StateId == stateId).ToListAsync();
//            return result;
//        }

//        public async Task<List<FlowStateTransition>> GetFlowEntityTransitions(string stateId)
//        {
//            var context = new MyDbContext();
//            var list = await context.FlowStateTransitions.Where(item => item.StateId == stateId).ToListAsync();
//            return list;
//        }

//        public Task<FlowEntityType> GetFlowEntityType(string flowId)
//        {
//            var context = new MyDbContext();
//            return context.FlowEntityTypes.FirstOrDefaultAsync(t => t.Id == flowId);
//        }

//        public async Task<List<FlowEntityType>> GetFlowEntityTypeList()
//        {
//            var context = new MyDbContext();
//            var list = await context.FlowEntityTypes.ToListAsync();
//            return list;
//        }

//        public Task<FlowStateTransition> GetFlowStateTransition(string actionId)
//        {
//            var context = new MyDbContext();
//            var result = context.FlowStateTransitions.FirstOrDefaultAsync(t => t.Id == actionId);
//            return result;
//        }

//        public async Task<int> SaveFlowEntityState(FlowEntityState flowEntityState)
//        {
//            if (flowEntityState != null && flowEntityState.Id != null)
//            {
//                var context = new MyDbContext();
//                context.Update(flowEntityState);
//                var result = await context.SaveChangesAsync();
//                return result;
//            }
//            else
//            {
//                var context = new MyDbContext();
//                await context.FlowEntityStates.AddAsync(flowEntityState);
//                var result = await context.SaveChangesAsync();
//                return result;
//            }
//        }

//        public async Task<int> SaveFlowEntityType(FlowEntityType flowEntityType)
//        {
//            if (flowEntityType != null && flowEntityType.Id != null)
//            {
//                var context = new MyDbContext();
//                context.Update(flowEntityType);
//                var result = await context.SaveChangesAsync();
//                return result;
//            }
//            else
//            {
//                var context = new MyDbContext();
//                await context.FlowEntityTypes.AddAsync(flowEntityType);
//                var result = await context.SaveChangesAsync();
//                return result;
//            }

//        }

//        public async Task<int> SaveFlowStateTransition(FlowStateTransition flowStateTransition)
//        {
//            if (flowStateTransition != null && flowStateTransition.Id != null)
//            {
//                var context = new MyDbContext();
//                context.Update(flowStateTransition);
//                var result = await context.SaveChangesAsync();
//                return result;
//            }
//            else
//            {
//                var context = new MyDbContext();
//                await context.FlowStateTransitions.AddAsync(flowStateTransition);
//                var result = await context.SaveChangesAsync();
//                return result;
//            }
//        }
//    }
//}

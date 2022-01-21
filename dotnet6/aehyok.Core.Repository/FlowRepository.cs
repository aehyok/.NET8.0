using aehyok.Core.EntityFrameCore.MySql.Data;
using aehyok.Core.EntityFrameCore.MySql.Models;
using aehyok.Core.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Repository
{
    public class FlowRepository : IFlowRepository
    {
        public async Task<int> DeleteFlowEntityType(string flowId)
        {
            var context = new MyDbConext();
            var item = context.FlowEntityTypes.FirstOrDefault(t => t.Id == flowId);
            context.FlowEntityTypes.Remove(item);
            var result = await context.SaveChangesAsync();
            return result;
        }

        public Task<FlowEntityState> GetFlowEntityState(string stateId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<FlowEntityState>> GetFlowEntityStateList(string flowId)
        {
            var context = new MyDbConext();
            var list = await context.FlowEntityStates.Where(item => item.FlowId == flowId).ToListAsync();
            return list;
        }

        public Task<List<FlowStateTransition>> GetFlowEntityTransitionList(string stateId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<FlowStateTransition>> GetFlowEntityTransitions(string stateId)
        {
            var context = new MyDbConext();
            var list = await context.FlowStateTransitions.Where(item => item.StateId == stateId).ToListAsync();
            return list;
        }

        public Task<FlowEntityType> GetFlowEntityTypeById(string flowId)
        {
            var context = new MyDbConext();
            return context.FlowEntityTypes.FirstOrDefaultAsync(t => t.Id == flowId);
        }

        public async Task<List<FlowEntityType>> GetFlowEntityTypeList()
        {
            var context = new MyDbConext();
            var list = await context.FlowEntityTypes.ToListAsync();
            return list;
        }

        public Task<FlowStateTransition> GetFlowStateTransition(string actionId)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveFlowEntityState(FlowEntityState flowEntityState)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveFlowEntityType(FlowEntityType flowEntityType)
        {
            if (flowEntityType != null && flowEntityType.Id != null)
            {
                var context = new MyDbConext();
                context.Update(flowEntityType);
                var result = await context.SaveChangesAsync();
                return result;
            }
            else
            {
                var context = new MyDbConext();
                await context.FlowEntityTypes.AddAsync(flowEntityType);
                var result = await context.SaveChangesAsync();
                return result;
            }

        }

        public Task<FlowStateTransition> SaveFlowStateTransition(FlowStateTransition flowStateTransition)
        {
            throw new NotImplementedException();
        }
    }
}

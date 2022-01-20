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
        public async Task<int> DeleteFlowEntityType(string id)
        {
            var context = new MyDbConext();
            var item = context.FlowEntityTypes.FirstOrDefault(t => t.Id == id);
            context.FlowEntityTypes.Remove(item);
            var result = await context.SaveChangesAsync();
            return result;
        }

        public Task<List<FlowEntityState>> GetFlowEntityStateList(int flowId)
        {
            throw new NotImplementedException();
        }

        public Task<List<FlowEntityState>> GetFlowEntityStateList(string flowId)
        {
            throw new NotImplementedException();
        }

        public Task<List<FlowStateTransition>> GetFlowEntityTransitions(int actionId)
        {
            throw new NotImplementedException();
        }

        public Task<List<FlowStateTransition>> GetFlowEntityTransitions(string actionId)
        {
            throw new NotImplementedException();
        }

        public Task<FlowEntityType> GetFlowEntityTypeById(string id)
        {
            var context = new MyDbConext();
            return context.FlowEntityTypes.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<FlowEntityType>> GetFlowEntityTypeList()
        {
            var context = new MyDbConext();
            var list = await context.FlowEntityTypes.ToListAsync();
            return list;
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
    }
}

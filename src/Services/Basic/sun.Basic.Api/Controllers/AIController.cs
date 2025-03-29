using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sun.Core.Dtos.Query;
using sun.Core.Dtos;
using sun.Core.Services;
using X.PagedList;
using sun.Basic.Services;
using sun.Basic.Dtos.Query;
using sun.Basic.Domains;
using sun.Basic.Dtos;
using sun.EntityFrameworkCore.Repository;
using Ardalis.Specification;

namespace sun.Basic.Api.Controllers
{
    /// <summary>
    /// AI相关接口方法
    /// </summary>
    /// <param name="spService"></param>
    public class AIController(ISystemPromptService spService) : BasicControllerBase
    {
        /// <summary>
        /// 提示词列表
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        public async Task<List<SystemPromptDto>> GetListAsync([FromQuery] SystemPromptQueryDto model)
        {
            var spec = Specifications<SystemPrompt>.Create();

            spec.Query.OrderBy(a => a.DisplayOrder);

            if (model.SystemPromptType > 0)
            {
                spec.Query.Where(a => a.SystemPromptType == model.SystemPromptType);
            }

            return await spService.GetListAsync <SystemPromptDto>(spec);
        }
    }
}

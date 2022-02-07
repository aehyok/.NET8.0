using aehyok.Core.Data.Model;
using aehyok.Core.EntityFramework.MySql;
using aehyok.Core.EntityFramework.MySql.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.Core.WebApi.Controllers
{
    /// <summary>
    /// Form表单通用保存
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class FormController : BaseApiController
    {
        private readonly IRepository<SystemForm> _formRepository;

        public FormController(IRepository<SystemForm> formRepository)
        {
            _formRepository = formRepository;
        }

        /// <summary>
        /// 获取表单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  List<SystemForm> GetSystemFormList()
        {
            var list = this._formRepository.GetList();
            return list;
        }

        /// <summary>
        /// 获取单个表单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<SystemForm> GetSystemForm(string id)
        {
            return await this._formRepository.GetAsync(id);
        }

        /// <summary>
        /// 通过id删除form表单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> DeleteSystemForm(string id)
        {
            return await this._formRepository.DeleteAsync(id);
        }

        /// <summary>
        /// 插入新的form表单记录
        /// </summary>
        /// <param name="systemForm"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> InsertSystemForm(SystemForm systemForm)
        {
            return await this._formRepository.InsertAsync(systemForm);
        }

        /// <summary>
        /// 更新form表单
        /// </summary>
        /// <param name="systemForm"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> UpdateSystemForm(SystemForm systemForm)
        {
            return await this._formRepository.UpdateAsync(systemForm);
        }
    }
}

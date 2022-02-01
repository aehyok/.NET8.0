using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aehyok.Core.EntityFramework.MySql;
using aehyok.Core.EntityFramework.MySql.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace aehyok.Core.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DictionaryController : BaseApiController
    {
        private readonly IRepository<SystemDictionaryType> _dictionaryTypeRepository;
        public DictionaryController(IRepository<SystemDictionaryType> dictionaryTypeRepository)
        {
            this._dictionaryTypeRepository = dictionaryTypeRepository;
        }
        /// <summary>
        /// 获取所有菜单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public List<SystemDictionaryType> GetMenuList()
        {
            var list = this._dictionaryTypeRepository.GetList();
            return list;
        }

        /// <summary>
        /// 通过菜单Id获取菜单详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<SystemDictionaryType> GetMenu(string id)
        {
            return await this._dictionaryTypeRepository.GetAsync(id);
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<SystemDictionaryType> AddMenu(SystemDictionaryType systemDictionaryType)
        {
            return await this._dictionaryTypeRepository.InsertAsync(systemDictionaryType);
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<int> UpdateMenu(SystemDictionaryType systemDictionaryType)
        {
            return await this._dictionaryTypeRepository.UpdateAsync(systemDictionaryType);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<int> DeleteDictionaryType(string dictionaryId)
        {

            return await this._dictionaryTypeRepository.DeleteAsync(dictionaryId);
        }
    }
}


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
    /// <summary>
    /// 字典管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DictionaryController : BaseApiController
    {
        private readonly IRepository<SystemDictionaryType> _dictionaryTypeRepository;
        private readonly IRepository<SystemDictionary> _dictionaryRepository;
        public DictionaryController(IRepository<SystemDictionaryType> dictionaryTypeRepository, IRepository<SystemDictionary> dictionaryRepository)
        {
            this._dictionaryTypeRepository = dictionaryTypeRepository;
            this._dictionaryRepository = dictionaryRepository;
        }

        #region 字典类型
        /// <summary>
        /// 获取字典类型列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public List<SystemDictionaryType> GetDictionaryTypeList()
        {
            var list = this._dictionaryTypeRepository.GetList();
            return list;
        }

        /// <summary>
        /// 通过字典类型Id获取字典类型详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<SystemDictionaryType> GetDictionaryType(string dictionaryTypeId)
        {
            return await this._dictionaryTypeRepository.GetAsync(dictionaryTypeId);
        }

        /// <summary>
        /// 添加字典
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<SystemDictionaryType> AddDictionaryType(SystemDictionaryType systemDictionaryType)
        {
            return await this._dictionaryTypeRepository.InsertAsync(systemDictionaryType);
        }

        /// <summary>
        /// 修改字典类型
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<int> UpdateDictionaryType(SystemDictionaryType systemDictionaryType)
        {
            return await this._dictionaryTypeRepository.UpdateAsync(systemDictionaryType);
        }

        /// <summary>
        /// 删除字典类型
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<int> DeleteDictionaryType(string dictionaryTypeId)
        {

            return await this._dictionaryTypeRepository.DeleteAsync(dictionaryTypeId);
        }
        #endregion

        #region 字典
        /// <summary>
        /// 获取字典列表通过字典类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public List<SystemDictionary> GetDictionaryList(string typeCode)
        {
            var list = this._dictionaryRepository.GetQueryable().Where(item => item.TypeCode == typeCode).ToList();
            return list;
        }

        /// <summary>
        /// 通过字典类型Id获取字典类型详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<SystemDictionary> GetDictionary(string dictionaryId)
        {
            return await this._dictionaryRepository.GetAsync(dictionaryId);
        }

        /// <summary>
        /// 添加字典
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<SystemDictionary> AddDictionary(SystemDictionary systemDictionary)
        {
            return await this._dictionaryRepository.InsertAsync(systemDictionary);
        }

        /// <summary>
        /// 修改字典
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<int> UpdateDictionary(SystemDictionary systemDictionary)
        {
            return await this._dictionaryRepository.UpdateAsync(systemDictionary);
        }

        /// <summary>
        /// 删除字典
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<int> DeleteDictionary(string dictionaryId)
        {

            return await this._dictionaryRepository.DeleteAsync(dictionaryId);
        }
        #endregion
    }
}


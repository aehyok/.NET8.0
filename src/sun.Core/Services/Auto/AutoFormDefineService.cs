using AutoMapper;
using Microsoft.EntityFrameworkCore;
using sun.Core.Domains;
using sun.Core.Domains.Auto;
using sun.Core.Dtos;
using sun.Core.Extensions;
using sun.EntityFrameworkCore.Repository;
using sun.Infrastructure;
using sun.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Services
{
    public class AutoFormDefineService(DbContext dbContext, IMapper mapper, IRedisService redisService, IAutoGuideLineService guidelineService) : ServiceBase<AutoFormDefine>(dbContext, mapper), IAutoFormDefineService, IScopedDependency
    {
        public Task<bool> BuildFormByDefine(string formName, string tableNamePrefix, DbTransaction dbTransaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> BuildQuestionnaireByDefine(string formName, string tableNamePrefix, DbTransaction dbTransaction = null)
        {
            throw new NotImplementedException();
        }

        public async Task<AutoFormDefine> GetFormAsync(string formName)
        {
            var key = $"{CoreRedisConstants.CollectFormCache}{formName}";
            var form = await redisService.GetAsync<AutoFormDefine>(key);
            if (form is null)
            {
                form = await GetAsync(item => item.Name == formName && !item.IsDeleted, true);
                if (form is not null)
                {
                    await RedisHelper.SetAsync(key, form);
                }
            }
            return form;
        }

        public override async Task<AutoFormDefine> InsertAsync(AutoFormDefine model, CancellationToken cancellationToken = default)
        {
            model = await base.InsertAsync(model, cancellationToken);

            await RedisHelper.SetAsync($"{CoreRedisConstants.CollectFormCache}{model.Name}", model);
            return model;
        }

        public override async Task<int> UpdateAsync(AutoFormDefine model, CancellationToken cancellationToken = default)
        {
            var result = await base.UpdateAsync(model, cancellationToken);

            await RedisHelper.SetAsync($"{CoreRedisConstants.CollectFormCache}{model.Name}", model);
            return result;
        }

        /// <summary>
        /// 模型初始化
        /// </summary>
        /// <param name="formName"></param>
        /// <param name="action">1新增，2编辑</param>
        /// <param name="parameters"></param>
        /// <param name="areaid"></param>
        /// <returns></returns>
        public async Task<InitResponseDto> InitForm(string formName, int action, Dictionary<string, object> parameters, long areaid)
        {
            DataTable dt = new DataTable();
            var form = await this.GetFormAsync(formName);

            if (form != null)
            {
                // 处理指标参数，将参数名前没有@的都加上@
                Dictionary<string, object> sqlParameters = parameters.RebuilderMetaDataQueryParemeter();

                // 如果action明确是编辑，那么通过取数据指标里取数据
                if (action == 2)
                {
                    dt = await GetExistDataAsync(form, sqlParameters, areaid);
                }

                // 如果没有获取到数据，那么通过初始化指标取数据
                if (dt == null || dt.Rows.Count == 0)
                {
                    dt = await GetInitDataAsync(form, sqlParameters, areaid);
                }
            }

            // 如果所获取数据没有id字段，则添加id字段；如果没有数据，则添加一行；
            dt.CheckIdColumnAndAddRow();

            // 处理返回数据
            string inputDefine = form.InputDefine.Replace("\r", "").Replace("\n", "");
            if (dt.Rows.Count > 0)
            {
                return new InitResponseDto { FormData = dt.ConvertExpandoObject(), InputDefine = inputDefine, ChildrenInputDefine = form.ChildrenInputDefine };
            }
            else
            {
                return new InitResponseDto { FormData = { }, InputDefine = inputDefine, ChildrenInputDefine = form.ChildrenInputDefine };
            }
        }

        /// <summary>
        /// 通过初始化指标取初始化数据
        /// </summary>
        /// <param name="form"></param>
        /// <param name="sqlParameters"></param>
        /// <param name="areaid"></param>
        /// <returns></returns>
        private async Task<DataTable> GetInitDataAsync(AutoFormDefine form, Dictionary<string, object> sqlParameters, long areaid)
        {
            DataTable dt = null;
            string keyword = string.Empty;

            string sql = form.GetInitSQL;
            if (!string.IsNullOrWhiteSpace(sql))
            {
                // 处理查询参数中的areaid
                if (sqlParameters.ContainsKey("@areaid"))
                {
                    sqlParameters["@areaid"] = areaid.ToString();
                }

                // 处理查询参数中的keyword
                if (sqlParameters.ContainsKey(@"keyword"))
                {
                    keyword = (string)sqlParameters["@keyword"];
                }

                //按指标查询
                if (sql.StartsWith("#"))
                {
                    dt = await guidelineService.QueryCustomFormGuideline(sql, sqlParameters, keyword, areaid);
                }
                else
                {
                    //dt = await GuidelineAccessor.QueryGuideline(sql, sqlParameters, keyword, areaid);
                }
            }
            return dt;
        }

        /// <summary>
        /// 获取已有数据记录
        /// </summary>
        /// <param name="form"></param>
        /// <param name="sqlParameters"></param>
        /// <param name="areaid"></param>
        /// <returns></returns>
        private async Task<DataTable> GetExistDataAsync(AutoFormDefine form, Dictionary<string, object> sqlParameters, long areaid)
        {
            DataTable dt = null;
            string keyword = string.Empty;

            string sql = form.GetDataSQL;
            if (!string.IsNullOrWhiteSpace(sql))
            {

                // 处理查询参数中的areaid
                if (sqlParameters.ContainsKey("@areaid"))
                {
                    sqlParameters["@areaid"] = areaid;
                }

                // 处理查询参数中的keyword
                if (sqlParameters.ContainsKey(@"keyword"))
                {
                    keyword = (string)sqlParameters["@keyword"];
                }

                //按指标查询
                if (sql.StartsWith("#"))
                {
                    dt = await guidelineService.QueryCustomFormGuideline(sql, sqlParameters, keyword, areaid);
                }
                else
                {
                    //dt = await GuidelineAccessor.QueryGuideline(sql, sqlParameters, keyword, areaid);
                }

            }
            return dt;
        }

        public Task<bool> RemoveFormData(string formName, Dictionary<string, object> parameters, long userId, long areaid)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SubmitForm(string formName, Dictionary<string, object> formData, long userId, long areaid, DbTransaction dbTransaction = null)
        {
            throw new NotImplementedException();
        }
    }
}
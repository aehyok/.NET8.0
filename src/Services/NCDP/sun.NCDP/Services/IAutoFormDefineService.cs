using sun.Core.Domains;
using sun.Core.Dtos;
using sun.EntityFrameworkCore.Repository;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Services
{
    public interface IAutoFormDefineService: IServiceBase<FormDefine>
    {
        /// <summary>
        /// 获取form表单定义
        /// </summary>
        /// <param name="formName"></param>
        /// <returns></returns>
        Task<FormDefine> GetFormAsync(string formName);
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="formName">模型名称</param>
        /// <param name="action">1新增2编辑</param>
        /// <param name="parameters"></param>
        /// <param name="areaid"></param>
        /// <returns></returns>
        Task<InitResponseDto> InitForm(string formName, int action, Dictionary<string, object> parameters, long areaid);

        /// <summary>
        /// 提交表单数据
        /// </summary>
        /// <param name="formName"></param>
        /// <param name="formData"></param>
        /// <param name="userId"></param>
        /// <param name="areaid"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        Task<bool> SubmitForm(string formName, Dictionary<string, object> formData, long userId, long areaid, DbTransaction dbTransaction = null);


        /// <summary>
        /// 删除表单数据
        /// </summary>
        /// <param name="formName"></param>
        /// <param name="parameters"></param>
        /// <param name="userId"></param>
        /// <param name="areaid"></param>
        /// <returns></returns>
        Task<bool> RemoveFormData(string formName, Dictionary<string, object> parameters, long userId, long areaid);

        /// <summary>
        /// 编译录入模型定义，生成写入表定义、数据表及相关指标定义
        /// </summary>
        /// <param name="formName"></param>
        /// <param name="tableNamePrefix">表名前缀</param>
        /// <param name="dbTransaction">表名前缀</param>
        /// <returns></returns>
        Task<bool> BuildFormByDefine(string formName, string tableNamePrefix, DbTransaction dbTransaction = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formName"></param>
        /// <param name="tableNamePrefix"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        Task<bool> BuildQuestionnaireByDefine(string formName, string tableNamePrefix, DbTransaction dbTransaction = null);
    }
}

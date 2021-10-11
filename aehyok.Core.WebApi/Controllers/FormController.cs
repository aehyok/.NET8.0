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
    public class FormController : BaseApiController
    {
        public FormController()
        {
            this._logger.Debug("form Debugger");
        }
        [HttpPost]
        public dynamic Save(dynamic json)
        {
            this._logger.Debug(json);

            return "OK1111111";
            //if (string.IsNullOrEmpty(confirm_parm))
            //{
            //    confirm_parm = "2";
            //}
            //string inputModelName = Request.Params["inputModelName_ym"];
            //bool isNew = Convert.ToBoolean(Request.Params["isNewData_ym"]);
            //InputModelHelper ora = new InputModelHelper(inputModelName, this.CurrentRequestUser);
            //string errorMsg = "";
            //MD_InputEntity entity = ora.GetInputEntity(base.CurrentRequestUser, ref errorMsg, confirm_parm);
            //entity.IsNewData = isNew;
            //if (Request.QueryString["isCheck_ym"] == "1" && errorMsg != "")//isCheck_ym=1代表检测必填项
            //    return errorMsg;
            //var operationResult = InputModelClient.SaveEntity(entity, CurrentRequestUser);
            //if (!operationResult.Success)
            //    return operationResult.Message;
            //return "";
        }
    }
}

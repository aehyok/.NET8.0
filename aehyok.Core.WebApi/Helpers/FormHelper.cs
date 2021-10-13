using aehyok.Core.Data.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.Core.WebApi.Helpers
{
    public class FormHelper
    {
		public MD_InputEntity GetInputEntity(SinoRequestUser sinoRequestUser, ref string errorMessage, string confirm_parm)
		{
			string formId = "aehyok";
			MD_InputEntity inputEntity = new MD_InputEntity(formId);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();

			//TODO 获取Form表单配置信息
			MD_InputModel inputModel = new MD_InputModel(); // GetInputModelByModelName();
			List<MD_InputModel_ColumnGroup> columnGroups = inputModel.Groups;
			if (columnGroups.Count > 0)
			{
				foreach (MD_InputModel_ColumnGroup group in columnGroups)
				{
					if ((group.GroupType.ToUpper() != "QUERYMODEL") && (group.GroupType.ToUpper() != "APPREG"))
						if (group.Columns != null)
						{
							foreach (MD_InputModel_Column mdColumn in group.Columns)
							{
								SaveInputDataToEntityByColumns(mdColumn, inputEntity, sinoRequestUser);
								#region 代码表
								if (mdColumn.ColumnType == "代码表")
								{
									if (inputEntity.InputData.ContainsKey(mdColumn.ColumnName))
									{
										var inputCodeValue = inputEntity.InputData[mdColumn.ColumnName];
										if (string.IsNullOrWhiteSpace(inputCodeValue))//如果取得得代码表值为空
										{
											if (mdColumn.Required)
											{
												AddErrorMessage(dictionary, mdColumn);
												continue;
											}
										}
										else
										{
											bool unlawful = false;
                                            var fullRefCodeData = new List<Object>();// SinoSZJS.Common.V2.Common.OraRefTableFactory.GetFullRefCodeData(mdColumn.EditFormat);
											#region 多选
											if (!string.IsNullOrEmpty(mdColumn.CanEditRule))
											{
												#region 输入值为多个
												if (inputCodeValue.IndexOf(",") > -1)
												{
													var inputCodeValueArray = inputCodeValue.Split(',');

													foreach (string str in inputCodeValueArray)
													{
														//if (fullRefCodeData.FindIndex(q => q.Code == str) == -1)
														//{
														//	unlawful = true;
														//	break;
														//}
													}
												}
												#endregion

												#region 输入值为单个
												else
												{
													//if (fullRefCodeData.FindIndex(q => q.Code == inputCodeValue) == -1)
													//{
													//	unlawful = true;
													//}
												}
												#endregion
											}
											#endregion
											#region 单选
											else
											{
												//if (fullRefCodeData.FindIndex(q => q.Code == inputCodeValue) == -1)
												//{
												//	unlawful = true;
												//}
											}
											#endregion
											if (unlawful)
											{
												dictionary.Add(mdColumn.ColumnName, mdColumn.ColumnName + "");
												AddErrorMessage(dictionary, mdColumn);
												continue;
											}
										}

									}
									else
									{
										AddErrorMessage(dictionary, mdColumn);
									}
								}
								#endregion
								#region 必填项
								if (mdColumn.Required)
								{
									if (inputEntity.InputData.ContainsKey(mdColumn.ColumnName))
									{
										object value = inputEntity.InputData[mdColumn.ColumnName];
										if (value == null || value.ToString() == "")
										{
											AddErrorMessage(dictionary, mdColumn);
										}
										else
										{
											if (mdColumn.MaxInputLength > 0)
											{
												if (value.ToString().IsDigit() && value.ToString().Length > mdColumn.MaxInputLength)
												{
													dictionary.Add(mdColumn.ColumnName, mdColumn.DisplayName + "输入的值非法！");
												}
											}
										}
									}
									else
									{
										AddErrorMessage(dictionary, mdColumn);
									}
								}
								#endregion
							}
						}
				}
			}
			else
			{
				foreach (MD_InputModel_Column md in inputModel.Columns)
				{
					SaveInputDataToEntityByColumns(md, inputEntity, sinoRequestUser);
				}
			}
			//errorMessage = dictionary.Count > 0 ? string.Join("\n", dictionary.Values.ToArray()) : "";
			var json = JsonConvert.SerializeObject(dictionary, Formatting.Indented);
			string error = "";
			//根据confirm_parm参数 返回不同格式的errorMessage数据。
			//confirm_parm参数为1返回Common.Modal所需要的Json格式。
			//confirm_parm参数为2返回Common.alert所需要的string字符串格式。
			if (confirm_parm == "1")//返回为Common.Modal  (Json格式)
			{
				errorMessage = dictionary.Count > 0 ? json : "";
			}
			else if (confirm_parm == "2")//返回为Common.Alert  (string字符串)
			{
				foreach (var error_lst in dictionary)
				{
					error += "" + error_lst.Value + "<br/>";
				}
				errorMessage = dictionary.Count > 0 ? error : "";
			}
			else//如果不传confirm_parm参数1或者2，默认返回为Common.Modal (Json格式)。
			{
				errorMessage = dictionary.Count > 0 ? json : "";
			}
			return inputEntity;

		}


        private void AddErrorMessage(Dictionary<string, string> errorList, MD_InputModel_Column column)
        {
            string message = string.Format("{0} 不能为空！", column.DisplayName);
            errorList.Add(column.ColumnName, message);
        }

        /// <summary>
        /// 把数据列存到实体
        /// </summary>
        /// <param name="column"></param>
        /// <param name="entity"></param>
        /// <param name="sinoRequestUser"></param>
        private void SaveInputDataToEntityByColumns(MD_InputModel_Column column, MD_InputEntity entity, SinoRequestUser sinoRequestUser)
        {
            if (entity.InputData == null) { entity.InputData = new Dictionary<string, string>(); }
            switch (column.ColumnType)
            {
                case "组织机构":
                    //string orgCode = string.IsNullOrEmpty(HttpContext.Current.Request.Form[column.ColumnName]) ? "" : HttpContext.Current.Request.Form[column.ColumnName].ToString();
                    string orgCode = string.IsNullOrEmpty("") ? "" : "".ToString();
                    if (orgCode.Trim().IndexOf('[') > -1)
                    {
                        string code = orgCode.Substring(orgCode.IndexOf('[') + 1, orgCode.IndexOf(']') - orgCode.IndexOf('[') - 1);
                        entity.InputData.Add(column.ColumnName, code);
                    }
                    else
                    {
                        entity.InputData.Add(column.ColumnName, "");
                    }
                    break;
                case "代码表":
                case "移送管辖移往单位":
                case "派转移往单位":
                    //string refCodeList = string.IsNullOrEmpty(HttpContext.Current.Request.Form[column.ColumnName]) ? "" : HttpContext.Current.Request.Form[column.ColumnName].ToString();
                    string refCodeList = string.IsNullOrEmpty("") ? "" : "".ToString();
                    //先判断是否为多选代码表，暂时判断只要不为空就为多选
                    if (!string.IsNullOrEmpty(column.CanEditRule))
                    {
                        if (refCodeList.Contains(','))
                        {
                            entity.InputData.Add(column.ColumnName, refCodeList);
                        }
                        else
                        {
                            string code = String.Empty;
                            if (refCodeList.Contains('['))
                            {
                                code = refCodeList.Substring(refCodeList.IndexOf('[') + 1, refCodeList.IndexOf(']') - refCodeList.IndexOf('[') - 1);
                            }
                            else
                            {
                                code = refCodeList;
                            }
                            entity.InputData.Add(column.ColumnName, code);
                        }

                    }
                    else
                    {
                        if (refCodeList.Trim().IndexOf(',') > -1)
                        {
                            entity.InputData.Add(column.ColumnName, refCodeList);
                        }
                        else
                        {
                            if (refCodeList.Length > 1)
                            {
                                int temp = 0;
                                if (int.TryParse(refCodeList, out temp))
                                {
                                    entity.InputData.Add(column.ColumnName, refCodeList);
                                }
                                else
                                {
                                    string code = refCodeList.Substring(refCodeList.IndexOf('[') + 1, refCodeList.IndexOf(']') - refCodeList.IndexOf('[') - 1);
                                    entity.InputData.Add(column.ColumnName, code);
                                }
                            }
                            else
                            {
                                entity.InputData.Add(column.ColumnName, "");
                            }
                        }
                    }

                    break;
                case "DATE":
                case "日期型":
                    //if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form[column.ColumnName]))
                    if (!string.IsNullOrEmpty(""))
                    {
                        //entity.InputData.Add(column.ColumnName, DateTime.Parse(HttpContext.Current.Request.Form[column.ColumnName]).ToString("s"));
                    }
                    break;
                case "RADIO":
                    //string value = HttpContext.Current.Request.Form[column.ColumnName];
                    string value = "";//
                    if (!string.IsNullOrEmpty(value))
                    {
                        var array = value.Split(',');
                        entity.InputData.Add(column.ColumnName, array[0]);
                    }
                    break;
                case "VARCHAR":
                case "字符型":
                case "多行文本":
                case "NUMBER":
                case "数值型":
                case "目标单位":
                    switch (column.EditFormat)
                    {
                        case "当前单位":
                            if (!string.IsNullOrEmpty(""))
                            {
                                //entity.InputData.Add(column.ColumnName, sinoRequestUser.BaseInfo.CurrentPost.PostDwId);
                            }
                            break;
                        case "当前用户":
                            //if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["MenuId"]))
                            if (!string.IsNullOrEmpty(""))
                            {
                                entity.InputData.Add(column.ColumnName, sinoRequestUser.BaseInfo.UserId);
                            }
                            break;
                        default:
                            if (column.EditFormat.ToUpper() == "HSBM")
                            {
                                //todo:还待处理HSBM保存时只保存HS编码
                                //string code = HttpContext.Current.Request.Form[column.ColumnName];
                                string code ="";
                                if (!string.IsNullOrEmpty(code))
                                {
                                    if (code.IsDigit())
                                    {
                                        entity.InputData.Add(column.ColumnName, code);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            code = code.Substring(code.IndexOf('[') + 1, code.IndexOf(']') - code.IndexOf('[') - 1);
                                            entity.InputData.Add(column.ColumnName, code);
                                        }
                                        catch (Exception exception)
                                        {
                                            string errorMessage = string.Format("HS编码[{0}]保存时格式异常{1}", code, exception.Message);
                                            //LogClient.WriteSystemLog(errorMessage, "ERROR");
                                        }
                                    }
                                }
                                else
                                {
                                    entity.InputData.Add(column.ColumnName, "");
                                }
                            }
                            else if (column.EditFormat.ToUpper() == "PERSONLIST")
                            {
                                if (column.InputRule == "MutiSelect")
                                {
                                    string tempText = "";// HttpContext.Request.Form[column.ColumnName];

                                    if (!string.IsNullOrEmpty(tempText))
                                    {
                                        if (tempText == column.ToolTipText)
                                        {
                                            tempText = "";
                                        }
                                    }
                                    entity.InputData.Add(column.ColumnName, tempText);
                                }
                                else
                                {
                                    string userId = "";// HttpContext.Current.Request.Form[column.ColumnName];
                                    entity.InputData.Add(column.ColumnName, !string.IsNullOrEmpty(userId) ? userId : "");
                                }

                            }
                            else
                            {
                                string tempText = "";// HttpContext.Current.Request.Form[column.ColumnName];

                                if (!string.IsNullOrEmpty(tempText))
                                {
                                    if (tempText == column.ToolTipText)
                                    {
                                        tempText = "";
                                    }
                                }
                                entity.InputData.Add(column.ColumnName, tempText);
                            }
                            break;
                    }
                    break;
                default:
                    //entity.InputData.Add(column.ColumnName, (HttpContext.Current.Request.Form[column.ColumnName].LastIndexOf(',') == HttpContext.Current.Request.Form[column.ColumnName].Length - 1) ? HttpContext.Current.Request.Form[column.ColumnName].Substring(0, HttpContext.Current.Request.Form[column.ColumnName].Length - 1) : HttpContext.Current.Request.Form[column.ColumnName]);
                    entity.InputData.Add(column.ColumnName, ("".LastIndexOf(',') =="".Length - 1) ? "".Substring(0, "".Length - 1) : "");
                    break;
            }
        }
    }
}

using aehyok.Core.Data.Model;
using aehyok.Core.MySql;
using aehyok.Core.MySqlDataAccessor;
using aehyok.Core.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Repository.Accessor
{
    public class InputModelAccessor
    {
        #region 录入模型默认值
        private const string SqlGetUserDefaultValue = @" select DEFAULTVALUE from flow_defaultvalue where USERID=:UserId AND POSTID=:PostId AND ENTITYTYPE=:EntityType";
        private const string SqlGetPost = @"select gw.gwid from qx2_gwdyb gw join qx2_gwjsgxb gx on gx.gwid=gw.gwid join qx_jsdyb js on js.jsid=gx.jsid join qx_jsqxgxb gx2 on gx2.jsid=js.jsid where gw.gwdwid=:DWID and gx2.qxid=:MENUID*1000";

        public static string GetPostId(SinoRequestUser sinoRequestUser, string cs)
        {
            if (cs == "")
            {
                return cs;
            }
            else
            {
                decimal bzid = 0;//
                string postid;

                string dwid = cs;
                using (MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, SqlGetPost, new MySqlParameter[2] { new MySqlParameter(":DWID", Convert.ToDecimal(dwid)), new MySqlParameter(":MENUID", Convert.ToDecimal(7030000010141)) }))
                {

                    while (dr.Read())
                    {
                        bzid = dr.GetDecimal(0);//取得post值

                    }
                }
                postid = bzid.ToString();
                return postid;
            }

        }
        /// <summary>
        /// 读取默认值与inputdata合并。
        /// </summary>
        /// <returns></returns>
        public bool GetDocDefaultValue(string docTypeDetailId, string inputModelName, MD_InputEntity entityData, SinoRequestUser user)
        {

            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            dictionary = InputModelAccessor.GetDocDefaultValue(docTypeDetailId, inputModelName, user);
            if (dictionary != null && entityData != null)
            {
                foreach (string key in dictionary.Keys)
                {
                    object value = dictionary[key];
                    if (value != null && value.ToString().Trim() != "")
                    {
                        if (entityData.InputData.ContainsKey(key))
                        {
                            if (string.IsNullOrEmpty(entityData.InputData[key]))
                            {
                                entityData.InputData[key] = dictionary[key];
                            }
                        }
                        else
                        {
                            entityData.InputData.Add(key, dictionary[key]);
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 获取录入模型设置的默认值
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetUserDefaultValue(string inputModelName, SinoRequestUser sinoRequestUser, string cs)
        {


            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string postId = InputModelAccessor.GetPostId(sinoRequestUser, cs);
            using (MySqlConnection MySqlConnection = MysqlDBHelper.OpenConnection())
            {
                if (!string.IsNullOrEmpty(cs))
                {
                    try
                    {
                        MySqlDataReader myMySqlDataReader;
                        //科室默认值判断 whc_10/31/2017      
                        //获取postid
                        //
                        //MySqlCommand ksCommand = new MySqlCommand(SqlGetUserDefaultValue, MySqlConnection);
                        //ksCommand.Parameters.Add(":UserId", -1);
                        //ksCommand.Parameters.Add(":PostId", postId);
                        //ksCommand.Parameters.Add(":EntityType", inputModelName);
                        //myMySqlDataReader = ksCommand.ExecuteReader();

                        //using (myMySqlDataReader)
                        //{
                        //    while (myMySqlDataReader.Read())
                        //    {
                        //        OracleBlob myOracleClob = myMySqlDataReader.GetOracleBlob(0);
                        //        //myOracleClob.Position = 0;
                        //        //IFormatter formatter = new BinaryFormatter();
                        //        //dictionary = formatter.Deserialize(myOracleClob) as Dictionary<string, string>;
                        //        using (MemoryStream stream = new MemoryStream(myOracleClob.Value))
                        //        {
                        //            stream.Position = 0;
                        //            IFormatter formatter = new BinaryFormatter();
                        //            dictionary = formatter.Deserialize(stream) as Dictionary<string, string>;
                        //        }
                        //    }
                        //}
                    }
                    catch (MySqlException exception)
                    {
                        //OracleLogWriter.WriteSystemLog(string.Format("MySqlException 取用户录入模型的默认值时失败，USERID={1} POSTID={2} ENTITYTYPE={3}！错误信息:{0}", exception.Message,
                        //                sinoRequestUser.BaseInfo.UserId, sinoRequestUser.BaseInfo.CurrentPost.PostId, inputModelName), "ERROR");

                    }
                    catch (Exception exception)
                    {
                        //OracleLogWriter.WriteSystemLog(string.Format("Exception 取用户录入模型的默认值时失败，USERID={1} POSTID={2} ENTITYTYPE={3}！错误信息:{0}", exception.Message,
                        //                sinoRequestUser.BaseInfo.UserId, sinoRequestUser.BaseInfo.CurrentPost.PostId, inputModelName), "ERROR");

                    }
                }
                else
                {
                    try
                    {
                        MySqlDataReader myMySqlDataReader;
                        MySqlCommand MySqlCommand = new MySqlCommand(SqlGetUserDefaultValue, MySqlConnection);
                        //MySqlCommand.Parameters.Add(":UserId", decimal.Parse(sinoRequestUser.BaseInfo.UserId));
                        //MySqlCommand.Parameters.Add(":PostId", decimal.Parse(sinoRequestUser.BaseInfo.CurrentPost.PostId));
                        //MySqlCommand.Parameters.Add(":EntityType", inputModelName);
                        myMySqlDataReader = MySqlCommand.ExecuteReader();
                        //科室默认值判断 whc_10/31/2017
                        if (!myMySqlDataReader.HasRows)
                        {
                            MySqlCommand ksCommand = new MySqlCommand(SqlGetUserDefaultValue, MySqlConnection);
                            //ksCommand.Parameters.Add(":UserId", -1);
                            //ksCommand.Parameters.Add(":PostId", decimal.Parse(sinoRequestUser.BaseInfo.CurrentPost.PostId));
                            myMySqlDataReader = ksCommand.ExecuteReader();
                        }
                        using (myMySqlDataReader)
                        {
                            while (myMySqlDataReader.Read())
                            {
                                //OracleBlob myOracleClob = myMySqlDataReader.GetOracleBlob(0);
                                //using (MemoryStream stream = new MemoryStream(myOracleClob.Value))
                                //{
                                //    stream.Position = 0;
                                //    IFormatter formatter = new BinaryFormatter();
                                //    dictionary = formatter.Deserialize(stream) as Dictionary<string, string>;
                                //}
                                //myOracleClob.Position = 0;
                                //IFormatter formatter = new BinaryFormatter();
                                //dictionary = formatter.Deserialize(myOracleClob) as Dictionary<string, string>;
                            }
                        }
                    }
                    catch (MySqlException exception)
                    {
                        //OracleLogWriter.WriteSystemLog(string.Format("MySqlException 取用户录入模型的默认值时失败，USERID={1} POSTID={2} ENTITYTYPE={3}！错误信息:{0}", exception.Message,
                        //                sinoRequestUser.BaseInfo.UserId, sinoRequestUser.BaseInfo.CurrentPost.PostId, inputModelName), "ERROR");

                    }
                    catch (Exception exception)
                    {
                        //OracleLogWriter.WriteSystemLog(string.Format("Exception 取用户录入模型的默认值时失败，USERID={1} POSTID={2} ENTITYTYPE={3}！错误信息:{0}", exception.Message,
                        //                sinoRequestUser.BaseInfo.UserId, sinoRequestUser.BaseInfo.CurrentPost.PostId, inputModelName), "ERROR");

                    }
                }



                MySqlConnection.Close();
            }
            return dictionary;
        }

        private const string SqlGetDocDefaultValue = @" select DEFAULTVALUE from flow_defaultvalue where USERID=:UserId AND POSTID=:PostId AND ENTITYTYPE=:EntityType and ENTITYTYPESUB1=:DocTypeDetailId";
        /// <summary>
        /// 获取文书录入模型设置的默认值(特殊类型 =1)
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetDocDefaultValue(string docTypeDetailId, string inputModelName, SinoRequestUser sinoRequestUser)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            using (MySqlConnection MySqlConnection = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    MySqlDataReader myMySqlDataReader;
                    MySqlCommand MySqlCommand = new MySqlCommand(SqlGetDocDefaultValue, MySqlConnection);
                    //MySqlCommand.Parameters.Add(":UserId", decimal.Parse(sinoRequestUser.BaseInfo.UserId));
                    //MySqlCommand.Parameters.Add(":PostId", decimal.Parse(sinoRequestUser.BaseInfo.CurrentPost.PostId));
                    //MySqlCommand.Parameters.Add(":EntityType", inputModelName);
                    //MySqlCommand.Parameters.Add(":DocTypeDetailId", docTypeDetailId);
                    myMySqlDataReader = MySqlCommand.ExecuteReader();

                    //科室默认值判断 whc_10/31/2017
                    if (!myMySqlDataReader.HasRows)
                    {
                        MySqlCommand ksCommand = new MySqlCommand(SqlGetUserDefaultValue, MySqlConnection);
                        //ksCommand.Parameters.Add(":UserId", -1);
                        //ksCommand.Parameters.Add(":PostId", decimal.Parse(sinoRequestUser.BaseInfo.CurrentPost.PostId));
                        //ksCommand.Parameters.Add(":EntityType", inputModelName);
                        myMySqlDataReader = ksCommand.ExecuteReader();
                    }

                    using (myMySqlDataReader)
                    {
                        while (myMySqlDataReader.Read())
                        {
                            //OracleBlob myOracleClob = myMySqlDataReader.GetOracleBlob(0);
                            ////myOracleClob.Position = 0;//这样花费时间较长
                            ////IFormatter formatter = new BinaryFormatter();
                            ////dictionary = formatter.Deserialize(myOracleClob) as Dictionary<string, string>;
                            //using (MemoryStream stream = new MemoryStream(myOracleClob.Value))//这样花费的时间较短
                            //{
                            //    stream.Position = 0;
                            //    IFormatter formatter = new BinaryFormatter();
                            //    dictionary = formatter.Deserialize(stream) as Dictionary<string, string>;
                            //}
                        }
                    }
                }
                catch (MySqlException exception)
                {
                    //OracleLogWriter.WriteSystemLog(string.Format("MySqlException 取用户录入模型的默认值时失败，USERID={1} POSTID={2} ENTITYTYPE={3}！错误信息:{0}", exception.Message,
                    //                sinoRequestUser.BaseInfo.UserId, sinoRequestUser.BaseInfo.CurrentPost.PostId, inputModelName), "ERROR");

                }
                catch (Exception exception)
                {
                    //OracleLogWriter.WriteSystemLog(string.Format("Exception 取用户录入模型的默认值时失败，USERID={1} POSTID={2} ENTITYTYPE={3}！错误信息:{0}", exception.Message,
                    //                sinoRequestUser.BaseInfo.UserId, sinoRequestUser.BaseInfo.CurrentPost.PostId, inputModelName), "ERROR");

                }
                MySqlConnection.Close();
            }
            return dictionary;
        }

        #region 保存岗位配置时检查是否已存在用户配置（不存在即保存） type=1 特殊文书类型 whc_11/1/2017

        public static bool CheckUserDefaulValue(string docTypeDetailId, string inputModelName, SinoRequestUser sinoRequestUser, int type)
        {
            bool check = true;
            using (MySqlConnection MySqlConnection = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    MySqlDataReader myMySqlDataReader;
                    MySqlCommand MySqlCommand = new MySqlCommand();
                    string checkSql = type == 1 ? SqlGetDocDefaultValue : SqlGetUserDefaultValue;
                    MySqlCommand = new MySqlCommand(checkSql, MySqlConnection);
                    //MySqlCommand.Parameters.Add(":UserId", decimal.Parse(sinoRequestUser.BaseInfo.UserId));
                    //MySqlCommand.Parameters.Add(":PostId", decimal.Parse(sinoRequestUser.BaseInfo.CurrentPost.PostId));
                    //MySqlCommand.Parameters.Add(":EntityType", inputModelName);
                    if (type == 1)
                    {
                        //MySqlCommand.Parameters.Add(":DocTypeDetailId", docTypeDetailId);
                    }
                    myMySqlDataReader = MySqlCommand.ExecuteReader();

                    check = myMySqlDataReader.HasRows;
                }
                catch (MySqlException exception)
                {
                    //OracleLogWriter.WriteSystemLog(string.Format("MySqlException 取用户录入模型的默认值时失败，USERID={1} POSTID={2} ENTITYTYPE={3}！错误信息:{0}", exception.Message,
                    //                sinoRequestUser.BaseInfo.UserId, sinoRequestUser.BaseInfo.CurrentPost.PostId, inputModelName), "ERROR");

                }
                catch (Exception exception)
                {
                    //OracleLogWriter.WriteSystemLog(string.Format("Exception 取用户录入模型的默认值时失败，USERID={1} POSTID={2} ENTITYTYPE={3}！错误信息:{0}", exception.Message,
                    //                sinoRequestUser.BaseInfo.UserId, sinoRequestUser.BaseInfo.CurrentPost.PostId, inputModelName), "ERROR");

                }
                MySqlConnection.Close();
            }
            return check;
        }
        #endregion

        private const string SqlDelSaveDocDefaultValue = @"delete from flow_defaultvalue  where USERID=:USERID AND POSTID=:POSTID AND ENTITYTYPE=:ENTITYTYPE and ENTITYTYPESUB1=:DocTypeDetailId";
        private const string SqlAddSaveDocDefaultValue = @"insert into flow_defaultvalue (ID,USERID,POSTID,ENTITYTYPE,DEFAULTVALUE,ENTITYTYPESUB1)
                                                             values (:ID,:USERID,:POSTID,:ENTITYTYPE,:DEFAULTVALUE,:DocTypeDetailId)";
        /// <summary>
        /// 保存用户设置的文书录入模型默认值(特殊类型 =1)
        /// </summary>
        /// <param name="docTypeDetailId"></param>
        /// <param name="inputModelName"></param>
        /// <param name="entityData"></param>
        /// <returns></returns>
        public static bool SaveDocDefaultValue(string docTypeDetailId, string inputModelName, Dictionary<string, string> entityData, SinoRequestUser sinoRequestUser)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            formatter.Serialize(stream, entityData);
            stream.Seek(0, SeekOrigin.Begin);
            byte[] blob = new byte[stream.Length];
            stream.Read(blob, 0, blob.Length);
            stream.Close();

            string modelName;
            bool isKS = false;
            if (inputModelName.Contains("-"))
            {
                isKS = Convert.ToBoolean(inputModelName.Split('-')[0]);//判断是否为科室默认值 whc_12/4/2017
                modelName = inputModelName.Split('-')[1];
            }
            else
            {
                modelName = inputModelName;
            }

            using (MySqlConnection MySqlConnection = MysqlDBHelper.OpenConnection())
            {
                MySqlTransaction MySqlTransaction = MySqlConnection.BeginTransaction();
                try
                {
                    MySqlCommand deleteMySqlCommand = new MySqlCommand(SqlDelSaveDocDefaultValue, MySqlConnection);
                    if (isKS)
                    {
                        //deleteMySqlCommand.Parameters.Add(":USERID", -1);
                    }
                    else
                    {
                        //deleteMySqlCommand.Parameters.Add(":USERID", decimal.Parse(sinoRequestUser.BaseInfo.UserId));
                    }
                    //deleteMySqlCommand.Parameters.Add(":POSTID", decimal.Parse(sinoRequestUser.BaseInfo.CurrentPost.PostId));
                    //deleteMySqlCommand.Parameters.Add(":ENTITYTYPE", modelName);
                    //deleteMySqlCommand.Parameters.Add(":DocTypeDetailId", docTypeDetailId);
                    deleteMySqlCommand.ExecuteNonQuery();

                    MySqlCommand insertMySqlCommand = new MySqlCommand(SqlAddSaveDocDefaultValue, MySqlConnection);
                    //insertMySqlCommand.Parameters.Add(":ID", Guid.NewGuid().ToString());
                    if (isKS)
                    {
                        //insertMySqlCommand.Parameters.Add(":USERID", -1);
                    }
                    else
                    {
                        //insertMySqlCommand.Parameters.Add(":USERID", decimal.Parse(sinoRequestUser.BaseInfo.UserId));
                    }
                    //insertMySqlCommand.Parameters.Add(":POSTID", decimal.Parse(sinoRequestUser.BaseInfo.CurrentPost.PostId));
                    //insertMySqlCommand.Parameters.Add(":ENTITYTYPE", modelName);
                    //insertMySqlCommand.Parameters.Add(new MySqlParameter(":DEFAULTVALUE", OracleDbType.Blob, blob.Length, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, blob));
                    //insertMySqlCommand.Parameters.Add(":DocTypeDetailId", docTypeDetailId);
                    insertMySqlCommand.ExecuteNonQuery();

                    MySqlTransaction.Commit();
                    MySqlConnection.Close();

                    if (!CheckUserDefaulValue(docTypeDetailId, modelName, sinoRequestUser, 1))
                    {
                        SaveDocDefaultValue(docTypeDetailId, "false-" + modelName, entityData, sinoRequestUser);
                    }
                    return true;
                }
                catch (MySqlException MySqlException)
                {
                    //OracleLogWriter.WriteSystemLog(
                    //    string.Format("MySqlException 保存用户录入模型的默认值时失败，USERID={1} POSTID={2} ENTITYTYPE={3}！错误信息:{0}", MySqlException.Message,
                    //                sinoRequestUser.BaseInfo.UserId, sinoRequestUser.BaseInfo.CurrentPost.PostId, modelName), "ERROR");

                    MySqlTransaction.Rollback();
                    MySqlConnection.Close();
                    return false;
                }
                catch (Exception exception)
                {
                    //OracleLogWriter.WriteSystemLog(
                    //    string.Format("Exception 保存用户录入模型的默认值时失败，USERID={1} POSTID={2} ENTITYTYPE={3}！错误信息:{0}", exception.Message,
                    //                sinoRequestUser.BaseInfo.UserId, sinoRequestUser.BaseInfo.CurrentPost.PostId, modelName), "ERROR");

                    MySqlTransaction.Rollback();
                    MySqlConnection.Close();
                    return false;
                }
            }
        }


        private const string SqlDelSaveUserDefaultValue = @"delete from flow_defaultvalue  where USERID=:USERID AND POSTID=:POSTID AND ENTITYTYPE=:ENTITYTYPE";
        private const string SqlAddSaveUserDefaultValue = @"insert into flow_defaultvalue (ID,USERID,POSTID,ENTITYTYPE,DEFAULTVALUE)
                                                             values (:ID,:USERID,:POSTID,:ENTITYTYPE,:DEFAULTVALUE)";
        /// <summary>
        /// 保存用户设置的录入模型默认值
        /// </summary>
        /// <param name="inputModelName"></param>
        /// <param name="entityData"></param>
        /// <returns></returns>
        public static bool SaveUserDefaultValue(string inputModelName, Dictionary<string, string> entityData, SinoRequestUser sinoRequestUser)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            formatter.Serialize(stream, entityData);
            stream.Seek(0, SeekOrigin.Begin);
            byte[] blob = new byte[stream.Length];
            stream.Read(blob, 0, blob.Length);
            stream.Close();

            string modelName;
            bool isKS = false;
            if (inputModelName.Contains("-"))
            {
                isKS = Convert.ToBoolean(inputModelName.Split('-')[0]);//判断是否为科室默认值 whc_12/4/2017
                modelName = inputModelName.Split('-')[1];
            }
            else
            {
                modelName = inputModelName;
            }


            using (MySqlConnection MySqlConnection = MysqlDBHelper.OpenConnection())
            {
                MySqlTransaction MySqlTransaction = MySqlConnection.BeginTransaction();
                try
                {
                    MySqlCommand deleteMySqlCommand = new MySqlCommand(SqlDelSaveUserDefaultValue, MySqlConnection);
                    if (isKS)
                    {
                        //deleteMySqlCommand.Parameters.Add(":USERID", -1);
                    }
                    else
                    {
                        //deleteMySqlCommand.Parameters.Add(":USERID", decimal.Parse(sinoRequestUser.BaseInfo.UserId));
                    }
                    //deleteMySqlCommand.Parameters.Add(":POSTID", decimal.Parse(sinoRequestUser.BaseInfo.CurrentPost.PostId));
                    //deleteMySqlCommand.Parameters.Add(":ENTITYTYPE", modelName);
                    deleteMySqlCommand.ExecuteNonQuery();

                    MySqlCommand insertMySqlCommand = new MySqlCommand(SqlAddSaveUserDefaultValue, MySqlConnection);
                    //insertMySqlCommand.Parameters.Add(":ID", Guid.NewGuid().ToString());
                    if (isKS)
                    {
                        //insertMySqlCommand.Parameters.Add(":USERID", -1);
                    }
                    else
                    {
                        //insertMySqlCommand.Parameters.Add(":USERID", decimal.Parse(sinoRequestUser.BaseInfo.UserId));
                    }
                    //insertMySqlCommand.Parameters.Add(":POSTID", decimal.Parse(sinoRequestUser.BaseInfo.CurrentPost.PostId));
                    //insertMySqlCommand.Parameters.Add(":ENTITYTYPE", modelName);
                    //insertMySqlCommand.Parameters.Add(new MySqlParameter(":DEFAULTVALUE", OracleDbType.Blob, blob.Length, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, blob));
                    insertMySqlCommand.ExecuteNonQuery();

                    MySqlTransaction.Commit();
                    MySqlConnection.Close();

                    if (!CheckUserDefaulValue(null, modelName, sinoRequestUser, 0))
                    {
                        SaveUserDefaultValue("false-" + modelName, entityData, sinoRequestUser);
                    }
                    return true;
                }
                catch (MySqlException MySqlException)
                {
                    //OracleLogWriter.WriteSystemLog(
                    //    string.Format("MySqlException 保存用户录入模型的默认值时失败，USERID={1} POSTID={2} ENTITYTYPE={3}！错误信息:{0}", MySqlException.Message,
                    //                sinoRequestUser.BaseInfo.UserId, sinoRequestUser.BaseInfo.CurrentPost.PostId, modelName), "ERROR");

                    MySqlTransaction.Rollback();
                    MySqlConnection.Close();
                    return false;
                }
                catch (Exception exception)
                {
                    //OracleLogWriter.WriteSystemLog(
                    //    string.Format("Exception 保存用户录入模型的默认值时失败，USERID={1} POSTID={2} ENTITYTYPE={3}！错误信息:{0}", exception.Message,
                    //                sinoRequestUser.BaseInfo.UserId, sinoRequestUser.BaseInfo.CurrentPost.PostId, modelName), "ERROR");

                    MySqlTransaction.Rollback();
                    MySqlConnection.Close();
                    return false;
                }
            }
        }
        /// <summary>
        /// 保存用户在文书单位配置界面设置的录入模型默认值。
        /// </summary>
        /// <param name="inputModelName"></param>
        /// <param name="entityData"></param>
        /// <returns></returns>
        public static bool SaveDwidDefaultValue(string inputModelName, Dictionary<string, string> entityData, SinoRequestUser sinoRequestUser, string cs)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            formatter.Serialize(stream, entityData);
            stream.Seek(0, SeekOrigin.Begin);
            byte[] blob = new byte[stream.Length];
            stream.Read(blob, 0, blob.Length);
            stream.Close();
            string postId = InputModelAccessor.GetPostId(sinoRequestUser, cs);
            string modelName;
            modelName = inputModelName.Split('-')[1];
            using (MySqlConnection MySqlConnection = MysqlDBHelper.OpenConnection())
            {
                MySqlTransaction MySqlTransaction = MySqlConnection.BeginTransaction();
                try
                {
                    MySqlCommand deleteMySqlCommand = new MySqlCommand(SqlDelSaveUserDefaultValue, MySqlConnection);
                    //deleteMySqlCommand.Parameters.Add(":USERID", -1);
                    //deleteMySqlCommand.Parameters.Add(":POSTID", postId);
                    //deleteMySqlCommand.Parameters.Add(":ENTITYTYPE", modelName);
                    deleteMySqlCommand.ExecuteNonQuery();

                    MySqlCommand insertMySqlCommand = new MySqlCommand(SqlAddSaveUserDefaultValue, MySqlConnection);
                    //insertMySqlCommand.Parameters.Add(":ID", Guid.NewGuid().ToString());
                    //insertMySqlCommand.Parameters.Add(":USERID", -1);
                    //insertMySqlCommand.Parameters.Add(":POSTID", postId);
                    //insertMySqlCommand.Parameters.Add(":ENTITYTYPE", modelName);
                    //insertMySqlCommand.Parameters.Add(new MySqlParameter(":DEFAULTVALUE", OracleDbType.Blob, blob.Length, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, blob));
                    insertMySqlCommand.ExecuteNonQuery();
                    MySqlTransaction.Commit();
                    MySqlConnection.Close();
                    return true;
                }
                catch (MySqlException MySqlException)
                {
                    //OracleLogWriter.WriteSystemLog(
                    //    string.Format("MySqlException 保存用户录入模型的默认值时失败，USERID={1} POSTID={2} ENTITYTYPE={3}！错误信息:{0}", MySqlException.Message,
                    //                sinoRequestUser.BaseInfo.UserId, sinoRequestUser.BaseInfo.CurrentPost.PostId, modelName), "ERROR");

                    MySqlTransaction.Rollback();
                    MySqlConnection.Close();
                    return false;
                }
                catch (Exception exception)
                {
                    //OracleLogWriter.WriteSystemLog(
                    //    string.Format("Exception 保存用户录入模型的默认值时失败，USERID={1} POSTID={2} ENTITYTYPE={3}！错误信息:{0}", exception.Message,
                    //                sinoRequestUser.BaseInfo.UserId, sinoRequestUser.BaseInfo.CurrentPost.PostId, modelName), "ERROR");

                    MySqlTransaction.Rollback();
                    MySqlConnection.Close();
                    return false;
                }
            }
        }
        #endregion

        #region 通过录入模型名称获取模型定义
        /// <summary>
        /// 通过录入模型名称获取模型定义
        /// </summary>
        private const string SqlGetInputModelByName = @"select IV_ID,NAMESPACE,IV_NAME,DESCRIPTION,DISPLAYNAME,DISPLAYORDER,IV_CS,TID,DELRULE,DWDM,
                                                         INTEGRATEDAPP,RESTYPE,BEFOREWRITE,AFTERWRITE 
                                                         from MD_INPUTVIEW where NAMESPACE=:NS and IV_NAME=:IVNAME ";
        public static MD_InputModel GetInputModelByName(string inputModelName)
        {
            MD_InputModel md_InputModel = new MD_InputModel();
            string[] inputNames = inputModelName.Split('.');
            if (inputNames.Length < 2) return md_InputModel;
            string nameSpace = inputNames[0];
            string modelName = inputNames[1];

            using (MySqlConnection MySqlConnection = MysqlDBHelper.OpenConnection())
            {
                MySqlCommand MySqlCommand = new MySqlCommand(SqlGetInputModelByName, MySqlConnection);
                //MySqlCommand.Parameters.Add(":NS", nameSpace);
                //MySqlCommand.Parameters.Add(":IVNAME", modelName);
                MySqlDataReader MySqlDataReader = MySqlCommand.ExecuteReader();
                while (MySqlDataReader.Read())
                {
                    md_InputModel.Id = MySqlDataReader.IsDBNull(0) ? "" : MySqlDataReader.GetDecimal(0).ToString();
                    md_InputModel.NameSpace = nameSpace;
                    md_InputModel.ModelName = modelName;
                    md_InputModel.Descript = MySqlDataReader.IsDBNull(3) ? "" : MySqlDataReader.GetString(3);
                    md_InputModel.DisplayName = MySqlDataReader.IsDBNull(4) ? "" : MySqlDataReader.GetString(4);
                    md_InputModel.DisplayOrder = MySqlDataReader.IsDBNull(5) ? (int)0 : Convert.ToInt32(MySqlDataReader.GetDecimal(5));
                    md_InputModel.Param = MySqlDataReader.IsDBNull(6) ? "" : MySqlDataReader.GetString(6);
                    md_InputModel.DeleteRule = MySqlDataReader.IsDBNull(8) ? "" : MySqlDataReader.GetString(8);
                    md_InputModel.DWDM = MySqlDataReader.IsDBNull(9) ? "" : MySqlDataReader.GetString(9);
                    md_InputModel.IntegretedApplication = MySqlDataReader.IsDBNull(10) ? "" : MySqlDataReader.GetString(10);
                    md_InputModel.ResourceType = MySqlDataReader.IsDBNull(11) ? "" : MySqlDataReader.GetString(11);
                    md_InputModel.BeforeWrite = MySqlDataReader.IsDBNull(12) ? "" : MySqlDataReader.GetString(12);
                    md_InputModel.AfterWrite = MySqlDataReader.IsDBNull(13) ? "" : MySqlDataReader.GetString(13);
                    //string tableName = md_InputModel.Param.GetMetaByName2("TABLE");
                    //string orderField = md_InputModel.Param.GetMetaByName2("ORDER");
                    //string modelType = md_InputModel.Param.GetMetaByName2("TYPE");
                    //string paramType = md_InputModel.Param.GetMetaByName2("PARAMTYPE");
                    //md_InputModel.ModelType = (modelType == "") ? "GRID" : modelType.ToUpper();
                    //md_InputModel.IsMixModel = (md_InputModel.ModelType == "MIX");
                    //md_InputModel.ParamterType = (paramType == "") ? "OTHER" : paramType.ToUpper();
                    //md_InputModel.InitGuideLine = md_InputModel.Param.GetMetaByName2("INITZB");
                    //md_InputModel.GetDataGuideLine = md_InputModel.Param.GetMetaByName2("GETZB");
                    //md_InputModel.GetNewRecordGuideLine = md_InputModel.Param.GetMetaByName2("NEWZB");
                    //md_InputModel.OrderField = orderField;
                    //md_InputModel.TableName = tableName;
                    md_InputModel.WriteTableNames = GetWriteDesTableOfInputModel(md_InputModel, MySqlConnection);   //获取录入模型写入表定义
                    md_InputModel.ChildInputModel = GetChildInputModel(md_InputModel, MySqlConnection);

                }
                MySqlDataReader.Close();
            }
            if (md_InputModel != null)
            {
                GetInputModelColumnGroups(md_InputModel);  //获取录入模型字段组定义
                md_InputModel.Columns = GetInputModelColumnDefine(md_InputModel.Id);    ////获取录入模型字段列表定义
            }
            return md_InputModel;
        }
        #endregion

        #region 获取录入模型字段组定义
        /// <summary>
        /// 获取录入模型字段组定义
        /// </summary>
        private const string SqlGetInputModelColumnGroups = @"select IVG_ID,DISPLAYTITLE,DISPLAYORDER,GROUPTYPE,APPREGURL,GROUPCS from md_inputgroup 
                                                                where IV_ID=:IVID order by DISPLAYORDER asc";
        private static void GetInputModelColumnGroups(MD_InputModel mdInputModel)
        {
            if (mdInputModel.Groups == null) { mdInputModel.Groups = new List<MD_InputModel_ColumnGroup>(); }
            mdInputModel.Groups.Clear();
            using (MySqlConnection MySqlConnection = MysqlDBHelper.OpenConnection())
            {
                MySqlCommand MySqlCommand = new MySqlCommand(SqlGetInputModelColumnGroups, MySqlConnection);
                //MySqlCommand.Parameters.Add(":IVID", decimal.Parse(mdInputModel.Id));

                MySqlDataReader MySqlDataReader = MySqlCommand.ExecuteReader();
                while (MySqlDataReader.Read())
                {
                    MD_InputModel_ColumnGroup mdInputModelColumnGroup = new MD_InputModel_ColumnGroup();
                    mdInputModelColumnGroup.GroupId = MySqlDataReader.IsDBNull(0) ? "0" : MySqlDataReader.GetDecimal(0).ToString();
                    mdInputModelColumnGroup.ModelId = mdInputModel.Id;
                    mdInputModelColumnGroup.DisplayTitle = MySqlDataReader.IsDBNull(1) ? "" : MySqlDataReader.GetString(1);
                    mdInputModelColumnGroup.DisplayOrder = MySqlDataReader.IsDBNull(2) ? 0 : Convert.ToInt32(MySqlDataReader.GetDecimal(2));
                    mdInputModelColumnGroup.GroupType = MySqlDataReader.IsDBNull(3) ? "" : MySqlDataReader.GetString(3);
                    mdInputModelColumnGroup.AppRegUrl = MySqlDataReader.IsDBNull(4) ? "" : MySqlDataReader.GetString(4).ToString();
                    mdInputModelColumnGroup.GroupParam = MySqlDataReader.IsDBNull(5) ? "" : MySqlDataReader.GetString(5).ToString();
                    GetColumnsOfInputGroup(mdInputModelColumnGroup, MySqlConnection);
                    mdInputModel.Groups.Add(mdInputModelColumnGroup);
                }
                MySqlDataReader.Close();
            }
        }
        #endregion

        #region 获取录入模型字段组的字段列表定义
        /// <summary>
        /// 获取录入模型字段组的字段列表定义
        /// </summary>
        private const string SqlGetColumnsOfInputGroup = @"select IVC_ID,IV_ID,TCID,DWDM,
                                                            INPUTDEFAULT,INPUTRULE,CANEDITRULE,CANDISPLAY,
                                                            COLUMNNAME,COLUMNORDER,COLUMNTYPE,READONLY,
                                                            DISPLAYNAME,ISCOMPUTE,COLUMNWIDTH,COLUMNHEIGHT,
                                                            TEXTALIGNMENT,EDITFORMAT,DISPLAYFORMAT,REQUIRED,TOOLTIP,DATACHANGEDEVENT,MAXLENGTH,
                                                            DEFAULTSHOW
                                                            from MD_INPUTVIEWCOLUMN 
                                                            where IV_ID=:IVID and IVG_ID=:IVGID
                                                            order by COLUMNORDER";
        private static void GetColumnsOfInputGroup(MD_InputModel_ColumnGroup md_InputModel_ColumnGroup, MySqlConnection MySqlConnection)
        {
            // List<MD_InputModel_Column> _ret = new List<MD_InputModel_Column>();
            MySqlCommand MySqlCommand = new MySqlCommand(SqlGetColumnsOfInputGroup, MySqlConnection);
            //MySqlCommand.Parameters.Add(":IVID", decimal.Parse(md_InputModel_ColumnGroup.ModelId));
            //MySqlCommand.Parameters.Add(":IVGID", decimal.Parse(md_InputModel_ColumnGroup.GroupId));

            using (MySqlDataReader MySqlDataReader = MySqlCommand.ExecuteReader())
            {
                while (MySqlDataReader.Read())
                {
                    MD_InputModel_Column md_InputModel_Column = new MD_InputModel_Column();

                    md_InputModel_Column.ColumnId = MySqlDataReader.IsDBNull(0) ? "" : MySqlDataReader.GetDecimal(0).ToString();
                    md_InputModel_Column.ColumnName = MySqlDataReader.IsDBNull(8) ? "" : MySqlDataReader.GetString(8);
                    md_InputModel_Column.DisplayName = MySqlDataReader.IsDBNull(12) ? "" : MySqlDataReader.GetString(12);
                    md_InputModel_Column.ColumnType = MySqlDataReader.IsDBNull(10) ? "" : MySqlDataReader.GetString(10);
                    md_InputModel_Column.DisplayOrder = MySqlDataReader.IsDBNull(9) ? 0 : Convert.ToInt32(MySqlDataReader.GetDecimal(9));
                    md_InputModel_Column.InputModelId = MySqlDataReader.IsDBNull(1) ? "" : MySqlDataReader.GetDecimal(1).ToString();
                    md_InputModel_Column.CanSave = MySqlDataReader.IsDBNull(11) ? true : (MySqlDataReader.GetDecimal(11) < 1);
                    md_InputModel_Column.CanDisplay = MySqlDataReader.IsDBNull(7) ? true : (MySqlDataReader.GetString(7).ToUpper() == "Y");
                    md_InputModel_Column.IsCompute = MySqlDataReader.IsDBNull(13) ? false : (MySqlDataReader.GetDecimal(13) > 0);
                    md_InputModel_Column.ReadOnly = MySqlDataReader.IsDBNull(11) ? false : (MySqlDataReader.GetDecimal(11) > 0);
                    md_InputModel_Column.DWDM = MySqlDataReader.IsDBNull(3) ? "" : MySqlDataReader.GetString(3);
                    md_InputModel_Column.DefaultValue = MySqlDataReader.IsDBNull(4) ? "" : MySqlDataReader.GetString(4);
                    md_InputModel_Column.InputRule = MySqlDataReader.IsDBNull(5) ? "" : MySqlDataReader.GetString(5);
                    md_InputModel_Column.CanEditRule = MySqlDataReader.IsDBNull(6) ? "" : MySqlDataReader.GetString(6);
                    md_InputModel_Column.Width = MySqlDataReader.IsDBNull(14) ? 1 : Convert.ToInt32(MySqlDataReader.GetDecimal(14));
                    md_InputModel_Column.LineHeight = MySqlDataReader.IsDBNull(15) ? 1 : Convert.ToInt32(MySqlDataReader.GetDecimal(15));
                    md_InputModel_Column.TextAlign = MySqlDataReader.IsDBNull(16) ? 0 : Convert.ToInt32(MySqlDataReader.GetDecimal(16));
                    md_InputModel_Column.EditFormat = MySqlDataReader.IsDBNull(17) ? "" : MySqlDataReader.GetString(17);
                    md_InputModel_Column.DisplayFormat = MySqlDataReader.IsDBNull(18) ? "" : MySqlDataReader.GetString(18);
                    md_InputModel_Column.Required = MySqlDataReader.IsDBNull(19) ? false : (MySqlDataReader.GetDecimal(19) > 0);
                    md_InputModel_Column.ToolTipText = MySqlDataReader.IsDBNull(20) ? "" : MySqlDataReader.GetString(20);
                    md_InputModel_Column.MaxInputLength = MySqlDataReader.IsDBNull(22) ? 0 : Convert.ToInt32(MySqlDataReader.GetDecimal(22));
                    md_InputModel_Column.DefaultShow = MySqlDataReader.IsDBNull(23) ? true : (MySqlDataReader.GetDecimal(23) > 0);
                    md_InputModel_Column.DataChangedEvent = MySqlDataReader.IsDBNull(21) ? "" : MySqlDataReader.GetString(21);
                    if (md_InputModel_ColumnGroup.Columns == null) { md_InputModel_ColumnGroup.Columns = new List<MD_InputModel_Column>(); }
                    md_InputModel_ColumnGroup.Columns.Add(md_InputModel_Column);
                }
                MySqlDataReader.Close();
            }
        }
        #endregion

        #region 获取录入模型写入表
        /// <summary>
        /// 获取录入模型写入表
        /// </summary>
        private const string SqlGetWriteDesTableOfInputModel = @"select  ID,TABLENAME,TABLETITLE,ISLOCK,DISPLAYORDER,SAVEMODE
                                                                    from MD_INPUTTABLE where IV_ID = :IVID order by DISPLAYORDER ";
        private static List<MD_InputModel_SaveTable> GetWriteDesTableOfInputModel(MD_InputModel inputModel, MySqlConnection MySqlConnection)
        {
            List<MD_InputModel_SaveTable> md_InputModel_SaveTableList = new List<MD_InputModel_SaveTable>();
            MySqlParameter[] MySqlParameter = { new MySqlParameter(":IVID", inputModel.Id) };
            using (MySqlDataReader MySqlDataReader = MysqlDBHelper.ExecuteReader(MySqlConnection, CommandType.Text, SqlGetWriteDesTableOfInputModel, MySqlParameter))
            {
                while (MySqlDataReader.Read())
                {
                    MD_InputModel_SaveTable md_InputModel_SaveTable = new MD_InputModel_SaveTable();
                    md_InputModel_SaveTable.Id = MySqlDataReader.IsDBNull(0) ? "" : MySqlDataReader.GetDecimal(0).ToString();
                    md_InputModel_SaveTable.TableName = MySqlDataReader.IsDBNull(1) ? "" : MySqlDataReader.GetString(1);
                    md_InputModel_SaveTable.TableTitle = MySqlDataReader.IsDBNull(2) ? "" : MySqlDataReader.GetString(2);
                    md_InputModel_SaveTable.IsLock = MySqlDataReader.IsDBNull(3) ? true : (MySqlDataReader.GetDecimal(3) > 0);
                    md_InputModel_SaveTable.DisplayOrder = MySqlDataReader.IsDBNull(4) ? 0 : Convert.ToInt32(MySqlDataReader.GetDecimal(4));
                    md_InputModel_SaveTable.InputModelId = inputModel.Id;
                    md_InputModel_SaveTable.SaveMode = MySqlDataReader.IsDBNull(5) ? "" : MySqlDataReader.GetString(5);

                    ///获取写入表字段定义
                    GetInputModelSaveTableColumn(md_InputModel_SaveTable, MySqlConnection);
                    md_InputModel_SaveTableList.Add(md_InputModel_SaveTable);

                }
                return md_InputModel_SaveTableList;
            }
        }
        #endregion

        #region 获取写入表字段定义
        /// <summary>
        /// 获取写入表字段定义
        /// </summary>
        private const string SqlGetInputModelSaveTableColumn = @"select ID,SRCCOL,DESCOL,METHOD,DESDES from MD_INPUTTABLECOLUMN where IVT_ID=:TID";
        private static void GetInputModelSaveTableColumn(MD_InputModel_SaveTable md_InputModel_SaveTable, MySqlConnection MySqlConnection)
        {

            MySqlParameter[] MySqlParameter = { new MySqlParameter(":TID", decimal.Parse(md_InputModel_SaveTable.Id)) };
            using (MySqlDataReader MySqlDataReader = MysqlDBHelper.ExecuteReader(MySqlConnection, CommandType.Text, SqlGetInputModelSaveTableColumn, MySqlParameter))
            {
                while (MySqlDataReader.Read())
                {
                    MD_InputModel_SaveTableColumn md_InputModel_SaveTableColumn = new MD_InputModel_SaveTableColumn();
                    md_InputModel_SaveTableColumn.Id = MySqlDataReader.IsDBNull(0) ? "" : MySqlDataReader.GetDecimal(0).ToString();
                    md_InputModel_SaveTableColumn.SrcColumn = MySqlDataReader.IsDBNull(1) ? "" : MySqlDataReader.GetString(1);
                    md_InputModel_SaveTableColumn.DesColumn = MySqlDataReader.IsDBNull(2) ? "" : MySqlDataReader.GetString(2);
                    md_InputModel_SaveTableColumn.Method = MySqlDataReader.IsDBNull(3) ? "" : MySqlDataReader.GetString(3);
                    md_InputModel_SaveTableColumn.Descript = MySqlDataReader.IsDBNull(4) ? "" : MySqlDataReader.GetString(4);
                    if (md_InputModel_SaveTable.Columns == null) { md_InputModel_SaveTable.Columns = new List<MD_InputModel_SaveTableColumn>(); }
                    md_InputModel_SaveTable.Columns.Add(md_InputModel_SaveTableColumn);
                }
            }
        }
        #endregion

        #region 获取录入模型子模型定义
        /// <summary>
        /// 获取录入模型子模型定义
        /// </summary>
        private const string SqlGetChildInputModel = @"select  t.ID,t.IV_ID,t.CIV_ID,t.PARAM, iv.NAMESPACE CNS ,iv.IV_NAME CIVNAME,t. DISPLAYORDER,iv.integratedapp,iv.restype,iV.Displayname,t.ShowCondition,t.SelectMode
                                                        from MD_INPUTVIEWCHILD t,MD_INPUTVIEW iv  where t.IV_ID = :IVID and t.CIV_ID =iv.IV_ID
                                                        order by t.DISPLAYORDER";
        private static List<MD_InputModel_Child> GetChildInputModel(MD_InputModel inputModel, MySqlConnection MySqlConnection)
        {
            List<MD_InputModel_Child> listChild = new List<MD_InputModel_Child>();
            MySqlCommand MySqlCommand = new MySqlCommand(SqlGetChildInputModel, MySqlConnection);
            //MySqlCommand.Parameters.Add(":IVID", inputModel.Id);
            using (MySqlDataReader MySqlDataReader = MySqlCommand.ExecuteReader())
            {
                while (MySqlDataReader.Read())
                {
                    string nameSpace = MySqlDataReader.IsDBNull(4) ? "" : MySqlDataReader.GetString(4);
                    string viewName = MySqlDataReader.IsDBNull(5) ? "" : MySqlDataReader.GetString(5);
                    string parameter = MySqlDataReader.IsDBNull(3) ? "" : MySqlDataReader.GetString(3);
                    MD_InputModel_Child child = new MD_InputModel_Child();
                    child.Id = MySqlDataReader.IsDBNull(0) ? "" : MySqlDataReader.GetDecimal(0).ToString();
                    child.InputModelName = string.Format("{0}.{1}", inputModel.NameSpace, inputModel.ModelName);
                    child.ChildModelName = string.Format("{0}.{1}", nameSpace, viewName);
                    child.DisplayName = MySqlDataReader.IsDBNull(9) ? "" : MySqlDataReader.GetString(9);
                    child.DisplayOrder = MySqlDataReader.IsDBNull(6) ? 0 : Convert.ToInt32(MySqlDataReader.GetDecimal(6));
                    child.IntegratedApp = MySqlDataReader.IsDBNull(7) ? "" : MySqlDataReader.GetString(7);
                    child.ResType = MySqlDataReader.IsDBNull(8) ? "" : MySqlDataReader.GetString(8);
                    child.ShowCondition = MySqlDataReader.IsDBNull(10) ? "" : MySqlDataReader.GetString(10);
                    child.SelectMode = MySqlDataReader.IsDBNull(11) ? 0 : Convert.ToInt16(MySqlDataReader.GetDecimal(11));
                    child.ChildModel = GetInputModelByName(string.Format("{0}.{1}", nameSpace, viewName));    //added by lqm 2013.3.14注释
                    child.Parameters = new List<MD_InputModel_ChildParam>();
                    foreach (string stringParameter in StrUtils.GetMetasByName2("PARAM", parameter))
                    {
                        string[] array = stringParameter.Split(':');
                        MD_InputModel_ChildParam childParameter = new MD_InputModel_ChildParam();
                        childParameter.Name = array[0];
                        childParameter.DataType = array[1];
                        childParameter.Value = array[2];

                        child.Parameters.Add(childParameter);
                    }

                    listChild.Add(child);
                }
                //added by lqm 2013.3.14注释
                foreach (MD_InputModel_Child child in listChild)
                {
                    if (child.ChildModel != null)
                    {
                        GetInputModelColumnGroups(child.ChildModel);
                    }
                    if (child.ChildModel != null)
                    {
                        child.ChildModel.Columns = GetInputModelColumnDefine(child.ChildModel.Id);
                    }
                }
            }
            return listChild;
        }
        #endregion

        #region 获取录入模型字段定义的列表
        /// <summary>
        /// 获取录入模型字段定义的列表
        /// </summary>
        private const string SqlGetInputModelColumnDefine = @"select IVC_ID,IV_ID,TCID,DWDM,
                                                                INPUTDEFAULT,INPUTRULE,CANEDITRULE,CANDISPLAY,
                                                                COLUMNNAME,COLUMNORDER,COLUMNTYPE,READONLY,
                                                                DISPLAYNAME,ISCOMPUTE,COLUMNWIDTH,COLUMNHEIGHT,
                                                                TEXTALIGNMENT,EDITFORMAT,DISPLAYFORMAT,REQUIRED,TOOLTIP,DATACHANGEDEVENT,MAXLENGTH,
                                                                DEFAULTSHOW
                                                                from MD_INPUTVIEWCOLUMN
                                                                where IV_ID=:IVID and IVG_ID=0
                                                                order by COLUMNORDER";
        private static List<MD_InputModel_Column> GetInputModelColumnDefine(string inputModelId)
        {
            List<MD_InputModel_Column> md_InputModel_ColumnList = new List<MD_InputModel_Column>();

            using (MySqlConnection MySqlConnection = MysqlDBHelper.OpenConnection())
            {
                MySqlCommand MySqlCommand = new MySqlCommand(SqlGetInputModelColumnDefine, MySqlConnection);
                //MySqlCommand.Parameters.Add(":IVID", decimal.Parse(inputModelId));

                MySqlDataReader MySqlDataReader = MySqlCommand.ExecuteReader();
                while (MySqlDataReader.Read())
                {
                    MD_InputModel_Column md_InputModel_Column = new MD_InputModel_Column();
                    md_InputModel_Column.ColumnId = MySqlDataReader.IsDBNull(0) ? "" : MySqlDataReader.GetDecimal(0).ToString();
                    md_InputModel_Column.ColumnName = MySqlDataReader.IsDBNull(8) ? "" : MySqlDataReader.GetString(8);
                    md_InputModel_Column.DisplayName = MySqlDataReader.IsDBNull(12) ? "" : MySqlDataReader.GetString(12);
                    md_InputModel_Column.ColumnType = MySqlDataReader.IsDBNull(10) ? "" : MySqlDataReader.GetString(10);
                    md_InputModel_Column.DisplayOrder = MySqlDataReader.IsDBNull(9) ? 0 : Convert.ToInt32(MySqlDataReader.GetDecimal(9));
                    md_InputModel_Column.InputModelId = MySqlDataReader.IsDBNull(1) ? "" : MySqlDataReader.GetDecimal(1).ToString();
                    md_InputModel_Column.CanSave = MySqlDataReader.IsDBNull(11) ? true : (MySqlDataReader.GetDecimal(11) < 1);
                    md_InputModel_Column.CanDisplay = MySqlDataReader.IsDBNull(7) ? true : (MySqlDataReader.GetString(7).ToUpper() == "Y");
                    md_InputModel_Column.IsCompute = MySqlDataReader.IsDBNull(13) ? false : (MySqlDataReader.GetDecimal(13) > 0);
                    md_InputModel_Column.ReadOnly = MySqlDataReader.IsDBNull(11) ? false : (MySqlDataReader.GetDecimal(11) > 0);
                    md_InputModel_Column.DWDM = MySqlDataReader.IsDBNull(3) ? "" : MySqlDataReader.GetString(3);
                    md_InputModel_Column.DefaultValue = MySqlDataReader.IsDBNull(4) ? "" : MySqlDataReader.GetString(4);
                    md_InputModel_Column.InputRule = MySqlDataReader.IsDBNull(5) ? "" : MySqlDataReader.GetString(5);
                    md_InputModel_Column.CanEditRule = MySqlDataReader.IsDBNull(6) ? "" : MySqlDataReader.GetString(6);
                    md_InputModel_Column.Width = MySqlDataReader.IsDBNull(14) ? 1 : Convert.ToInt32(MySqlDataReader.GetDecimal(14));
                    md_InputModel_Column.LineHeight = MySqlDataReader.IsDBNull(15) ? 1 : Convert.ToInt32(MySqlDataReader.GetDecimal(15));
                    md_InputModel_Column.TextAlign = MySqlDataReader.IsDBNull(16) ? 0 : Convert.ToInt32(MySqlDataReader.GetDecimal(16));
                    md_InputModel_Column.EditFormat = MySqlDataReader.IsDBNull(17) ? "" : MySqlDataReader.GetString(17);
                    md_InputModel_Column.DisplayFormat = MySqlDataReader.IsDBNull(18) ? "" : MySqlDataReader.GetString(18);
                    md_InputModel_Column.Required = MySqlDataReader.IsDBNull(19) ? false : (MySqlDataReader.GetDecimal(19) > 0);
                    md_InputModel_Column.ToolTipText = MySqlDataReader.IsDBNull(20) ? "" : MySqlDataReader.GetString(20);
                    md_InputModel_Column.MaxInputLength = MySqlDataReader.IsDBNull(22) ? 0 : Convert.ToInt32(MySqlDataReader.GetDecimal(22));
                    md_InputModel_Column.DataChangedEvent = MySqlDataReader.IsDBNull(21) ? "" : MySqlDataReader.GetString(21);
                    md_InputModel_Column.DefaultShow = MySqlDataReader.IsDBNull(23) ? true : (MySqlDataReader.GetDecimal(23) > 0);
                    md_InputModel_ColumnList.Add(md_InputModel_Column);
                }
                MySqlDataReader.Close();
            }

            return md_InputModel_ColumnList;
        }
        #endregion

        #region 获取主键
        /// <summary>
        /// 获取主键
        /// </summary>
        private const string SqlGetDbPrimayKeyList = @" SELECT TABLE_NAME,COLUMN_NAME,COLUMN_POSITION  FROM XTV_PK_COLUMNS T WHERE T.TABLE_NAME=UPPER(:TNAME)  ";
        public static List<string> GetDbPrimayKeyList(string tableName)
        {
            List<string> ret = new List<string>();
            using (MySqlConnection MySqlConnection = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    MySqlParameter[] MySqlParameterArray = { new MySqlParameter(":TNAME", tableName) };
                    using (MySqlDataReader MySqlDataReader = MysqlDBHelper.ExecuteReader(MySqlConnection, CommandType.Text, SqlGetDbPrimayKeyList, MySqlParameterArray))
                    {
                        while (MySqlDataReader.Read())
                        {
                            string primaryName = MySqlDataReader.IsDBNull(1) ? "" : MySqlDataReader.GetString(1);
                            ret.Add(primaryName);
                        }
                    }
                }
                catch (MySqlException MySqlException)
                {
                    throw new Exception(string.Format("MySqlException获取表{0}的主键字段时发生错误！{1}", tableName, MySqlException.Message));
                }
                catch (Exception exception)
                {
                    throw new Exception(string.Format("Exception 获取表{0}的主键字段时发生错误！{1}", tableName, exception.Message));
                }
            }
            return ret;
        }
        #endregion
    }
}

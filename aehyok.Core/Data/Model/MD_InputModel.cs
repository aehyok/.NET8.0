using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
    public class MD_InputModel
    {
        /// <summary>
        /// 录入模型Id
        /// </summary>
        
        public string Id { get; set; }

        /// <summary>
        /// 录入模型命名空间
        /// </summary>
        
        public string NameSpace { get; set; }

        /// <summary>
        /// 录入模型名称
        /// </summary>
        
        public string ModelName { get; set; }

        /// <summary>
        /// 录入模型类型
        /// </summary>
        
        public string ModelType { get; set; }

        /// <summary>
        /// 录入模型描述
        /// </summary>
        
        public string Descript { get; set; }

        /// <summary>
        /// 录入模型名称
        /// </summary>
        
        public string DisplayName { get; set; }

        /// <summary>
        /// 录入模型显示顺序
        /// </summary>
        
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 录入模型记录删除规则（暂时用不到此字段）
        /// </summary>
        
        public string DeleteRule { get; set; }

        /// <summary>
        /// 单位代码
        /// </summary>
        
        public string DWDM { get; set; }

        /// <summary>
        /// 初始化算法指标
        /// </summary>
        
        public string InitGuideLine { get; set; }

        /// <summary>
        /// 取数据算法指标
        /// </summary>
        
        public string GetDataGuideLine { get; set; }     //下面文件无此字段

        /// <summary>
        /// 取新记录算法指标
        /// </summary>
        
        public string GetNewRecordGuideLine { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        
        public string Param { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        
        public string ParamterType { get; set; }

        /// <summary>
        /// 是否为混合模型
        /// </summary>
        
        public bool IsMixModel { get; set; }

        /// <summary>
        /// 资源类型
        /// </summary>
        
        public string ResourceType { get; set; }

        /// <summary>
        /// 集成应用
        /// </summary>
        
        public string IntegretedApplication { get; set; }

        /// <summary>
        /// 写入前操作
        /// </summary>
        
        public string BeforeWrite { get; set; }

        /// <summary>
        /// 写入后操作
        /// </summary>
        
        public string AfterWrite { get; set; }

        /// <summary>
        /// 录入模型全名称(命名空间+录入模型名称)
        /// </summary>
        public string ModelFullName
        {
            get
            {
                return string.Format("{0}.{1}", NameSpace, ModelName);
            }
        }

        /// <summary>
        /// 显示名称
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return DisplayName;
        }

        /// <summary>
        /// 录入模型字段列表
        /// </summary>
        
        public List<MD_InputModel_Column> Columns { get; set; }

        /// <summary>
        /// 录入模型字段分组
        /// </summary>
        
        public List<MD_InputModel_ColumnGroup> Groups { get; set; }

        /// <summary>
        /// 子模型列表
        /// </summary>
        
        public List<MD_InputModel_Child> ChildInputModel { get; set; }

        /// <summary>
        /// 写入表列表
        /// </summary>
        
        public List<MD_InputModel_SaveTable> WriteTableNames { get; set; }

        /// <summary>
        /// 
        /// </summary>
        
        public string OrderField { get; set; }

        /// <summary>
        /// 表名称
        /// </summary>
        
        public string TableName { get; set; }

        /// <summary>
        /// 空构造函数
        /// </summary>
        public MD_InputModel() { }


        /// <summary>
        /// 自构造函数
        /// </summary>
        /// <param name="inputModel"></param>
        public MD_InputModel(MD_InputModel inputModel)
        {
            AfterWrite = inputModel.AfterWrite;
            BeforeWrite = inputModel.BeforeWrite;
            ChildInputModel = inputModel.ChildInputModel;
            Columns = inputModel.Columns;
            DWDM = inputModel.DWDM;
            DeleteRule = inputModel.DeleteRule;
            Descript = inputModel.Descript;
            DisplayName = inputModel.DisplayName;
            DisplayOrder = inputModel.DisplayOrder;
            GetDataGuideLine = inputModel.GetDataGuideLine;
            GetNewRecordGuideLine = inputModel.GetNewRecordGuideLine;
            Groups = inputModel.Groups;
            Id = inputModel.Id;
            InitGuideLine = inputModel.InitGuideLine;
            IntegretedApplication = inputModel.IntegretedApplication;
            IsMixModel = inputModel.IsMixModel;
            ModelName = inputModel.ModelName;
            ModelType = inputModel.ModelType;
            NameSpace = inputModel.NameSpace;
            OrderField = inputModel.OrderField;
            Param = inputModel.Param;
            ParamterType = inputModel.ParamterType;
            ResourceType = inputModel.ResourceType;
            TableName = inputModel.TableName;
            WriteTableNames = inputModel.WriteTableNames;
        }


        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        public MD_InputModel(string id, string nameSpace, string modelName, string descript, string displayName, int displayOrder, string param, string deleteRule, string dwdm, string inputegretedApplication, string resourecType)
        {
            Id = id;
            NameSpace = nameSpace;
            ModelName = modelName;
            Descript = descript;
            DisplayName = displayName;
            DisplayOrder = displayOrder;
            Param = param;
            DeleteRule = deleteRule;
            DWDM = dwdm;
            IntegretedApplication = inputegretedApplication;
            ResourceType = resourecType;

        }
    }
}

using aehyok.Infrastructure.Dtos;
using System.ComponentModel.DataAnnotations;

namespace aehyok.Core.Dtos
{
    public class ApiResourceDto: DtoBase
    {
        /// <summary>
        /// 接口名称
        /// </summary>
        [MaxLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// 接口标识
        /// </summary>
        [MaxLength(256)]
        public string Code { get; set; }

        /// <summary>
        /// 所有接口按 Controller 分组，分组名称为 Controller 注释
        /// </summary>
        [MaxLength(256)]
        public string GroupName { get; set; }

        /// <summary>
        /// 路由匹配模式
        /// </summary>
        [MaxLength(256)]
        public string RoutePattern { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        [MaxLength(256)]
        public string NameSpace { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        [MaxLength(256)]
        public string ControllerName { get; set; }

        /// <summary>
        /// 操作名称
        /// </summary>
        [MaxLength(256)]
        public string ActionName { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        [MaxLength(256)]
        public string RequestMethod { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsChecked { get; set; }
    }
}

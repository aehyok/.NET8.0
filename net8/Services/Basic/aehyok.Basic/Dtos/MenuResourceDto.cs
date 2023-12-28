using aehyok.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Dtos
{
    public class MenuResourceDto : DtoBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 路由
        /// </summary>
        public string RoutePattern { get; set; }

        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public List<MenuResourceDto> Operations { get; set; }
    }
}

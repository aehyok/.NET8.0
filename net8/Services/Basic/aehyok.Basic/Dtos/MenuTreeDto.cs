using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Dtos
{
    public class MenuTreeDto : MenuDto
    {
        /// <summary>
        /// 下级菜单
        /// </summary>
        public List<MenuTreeDto> Children { get; set; } = [];
    }
}

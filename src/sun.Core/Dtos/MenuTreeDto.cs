namespace sun.Core.Dtos
{
    public class MenuTreeDto : MenuDto
    {
        /// <summary>
        /// 下级菜单
        /// </summary>
        public List<MenuTreeDto> Children { get; set; } = new List<MenuTreeDto>();
    }
}

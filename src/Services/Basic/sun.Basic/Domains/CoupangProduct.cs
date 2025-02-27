using sun.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Basic.Domains
{
    //参考文档地址 https://developers.coupangcorp.com/hc/zh-cn/articles/360033645054-%E5%95%86%E5%93%81%E5%88%97%E8%A1%A8%E8%8C%83%E5%9B%B4%E6%9F%A5%E8%AF%A2

    /// <summary>
    /// 电商平台商品
    /// </summary>
    public class CoupangProduct: AuditedEntity
    {
        /// <summary>
        /// 注册商品ID  
        /// </summary>
        public long SellerProductId { get; set; }

        /// <summary>
        /// 注册商品名 订单上的商品名
        /// </summary>
        public string SellerProductName { get; set; }

        /// <summary>
        /// 显示品类代码 可通过品类列表查询API或下载品类信息Excel查看
        /// </summary>
        public int DisplayCategoryCode { get; set; }

        /// <summary>
        /// 卖家ID 酷澎提供给卖家的固有代码 
        /// </summary>
        public string VendorId { get; set; }

        /// <summary>
        /// 销售开始日期 格式为"yyyy-MM-dd'T'HH:mm:ss" 
        /// </summary>
        public DateTime SaleStartedAt { get; set; }

        /// <summary>
        /// 销售结束日期  格式为"yyyy-MM-dd'T'HH:mm:ss" , *年份可选择至2099年  
        /// </summary>
        public DateTime SaleEndedAt { get; set; }

        /// <summary>
        /// 品牌 输入品牌的韩文/英文标准名称
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 注册商品状态 审批中/临时保存/待审批/已批准/部分批准/未通过审批/商品已删除
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// 注册销售的时间  格式"yyyy-MM-ddTHH:mm:ss" 
        /// </summary>
        public new DateTime CreatedAt { get; set; }
    }
}

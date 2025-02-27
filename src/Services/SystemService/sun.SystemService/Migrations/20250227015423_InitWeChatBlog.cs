using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sun.SystemService.Migrations
{
    /// <inheritdoc />
    public partial class InitWeChatBlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoupangOrder",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    orderId = table.Column<long>(type: "bigint", nullable: false, comment: "订单号码"),
                    OrderedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "订单日期/时间 yyyy-MM-dd'T'HH:mm:ss"),
                    OrdererName = table.Column<string>(type: "longtext", nullable: true, comment: "订购人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrdererEmail = table.Column<string>(type: "longtext", nullable: true, comment: "订购人邮箱")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrdererSafeNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrdererOrdererNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PaidAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "付款完成时的日期/时间"),
                    Status = table.Column<string>(type: "longtext", nullable: true, comment: "订单状态")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShippingPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false, comment: "配送费"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否删除"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "创建人id"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "修改人id"),
                    Remark = table.Column<string>(type: "longtext", nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoupangOrder", x => x.Id);
                },
                comment: "电商平台订单")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CoupangOrderItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VendorItemPackageId = table.Column<int>(type: "int", nullable: false, comment: "vendorItemPackageId数值\r\nvendorItemPackageId\r\n未使用/不存在则返回0"),
                    VendorItemPackageName = table.Column<string>(type: "longtext", nullable: true, comment: "vendorItemPackageName字符串\r\nvendorItemPackageName")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductId = table.Column<int>(type: "int", nullable: false, comment: "productId数值\r\n产品ID\r\n未使用/不存在则返回0"),
                    VendorItemId = table.Column<int>(type: "int", nullable: false, comment: "vendorItemId数值\r\n产品单项ID/OptionID"),
                    VendorItemName = table.Column<string>(type: "longtext", nullable: true, comment: "vendorItemName字符串\r\n产品显示名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShippingCount = table.Column<int>(type: "int", nullable: false, comment: "shippingCount数值\r\nshippingCount = 订购时的产品数量\r\nholdCountForCancel = 被取消待退款的产品数量\r\ncancelCount = 已经确认要被取消的产品数量\r\n可配送产品数量 = shippingCount - (holdCountForCancel + cancelCount )"),
                    HoldCountForCancel = table.Column<int>(type: "int", nullable: false, comment: "holdCountForCancel数值\r\n被取消待退款的产品数量"),
                    CancelCount = table.Column<int>(type: "int", nullable: false, comment: "cancelCount数值\r\n已经确认要被取消的产品数量"),
                    SalesPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false, comment: "salesPrice数值\r\n产品单项的价格"),
                    DiscountPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false, comment: "discountPrice数值\r\n打折金额总和, discountPrice(打折金额总和) = instantCouponDiscount(立减优惠券) + downloadableCoupon(下载优惠券) + coupangDiscount(Coupang 专享优惠)"),
                    InstantCouponDiscount = table.Column<decimal>(type: "decimal(65,30)", nullable: false, comment: "instantCouponDiscount数值\r\n立减优惠券数额"),
                    DownloadableCouponDiscount = table.Column<decimal>(type: "decimal(65,30)", nullable: false, comment: "downloadableCouponDiscount数值\r\n下载优惠券数额"),
                    CoupangDiscount = table.Column<decimal>(type: "decimal(65,30)", nullable: false, comment: "coupangDiscount数值\r\nCoupang 专享优惠\r\n酷澎专享购物车/品类折扣等金额"),
                    ExternalVendorSkuCode = table.Column<string>(type: "longtext", nullable: true, comment: "externalVendorSkuCode字符串\r\n卖家产品 SKU 代码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EtcInfoHeader = table.Column<string>(type: "longtext", nullable: true, comment: "etcInfoHeader字符串\r\n各商品单项输入项\r\n可选输入")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EtcInfoValue = table.Column<string>(type: "longtext", nullable: true, comment: "etcInfoValue字符串\r\n各商品单项输入项的用户输入值\r\n可选输入\r\n此字段存在，但是没有任何值. 如果需要，请使用以下 etcInfoValues.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EtcInfoValues = table.Column<string>(type: "longtext", nullable: true, comment: "etcInfoValues数组\r\n用户产品单项各属性输入值列表\r\n可选输入")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SellerProductId = table.Column<int>(type: "int", nullable: false, comment: "sellerProductId数值\r\n卖家产品ID"),
                    SellerProductName = table.Column<string>(type: "longtext", nullable: true, comment: "sellerProductName字符串\r\n注册产品名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SellerProductItemName = table.Column<string>(type: "longtext", nullable: true, comment: "sellerProductItemName字符串\r\n产品单项名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstSellerProductItemName = table.Column<string>(type: "longtext", nullable: true, comment: "firstSellerProductItemName字符串\r\n原始注册产品单项名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EstimatedShippingDate = table.Column<string>(type: "longtext", nullable: true, comment: "estimatedShippingDate字符串\r\n收到订单后预计发货日期\r\nyyyy-MM-dd")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PlannedShippingDate = table.Column<string>(type: "longtext", nullable: true, comment: "plannedShippingDate字符串\r\n计划发货日期(拆分发货订单)\r\nyyyy-MM-dd")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceNumberUploadDate = table.Column<string>(type: "longtext", nullable: true, comment: "invoiceNumberUploadDate字符串\r\n运单号上传日期\r\nyyyy-MM-dd'T'HH:mm:ss")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExtraProperties = table.Column<string>(type: "longtext", nullable: true, comment: "extraProperties对象\r\n卖家商品属性的其它信息\r\nkey:value 类型")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PricingBadge = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "pricingBadge布尔\r\n是否为最低价\r\ntrue/false"),
                    UsedProduct = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "usedProduct布尔\r\n是否为二手货\r\ntrue/false"),
                    ConfirmDate = table.Column<string>(type: "longtext", nullable: true, comment: "confirmDate字符串\r\n确认收货日期时间\r\nyyyy-MM-dd HH:mm:ss")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryChargeTypeName = table.Column<string>(type: "longtext", nullable: true, comment: "deliveryChargeTypeName字符串\r\n配送收费类型\r\nnot-free, free")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Canceled = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "canceled布尔\r\n订单是否被取消\r\ntrue/false"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否删除"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "创建人id"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "修改人id"),
                    Remark = table.Column<string>(type: "longtext", nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoupangOrderItem", x => x.Id);
                },
                comment: "电商平台订单明细")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CoupangProduct",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SellerProductId = table.Column<long>(type: "bigint", nullable: false, comment: "注册商品ID  "),
                    SellerProductName = table.Column<string>(type: "longtext", nullable: true, comment: "注册商品名 订单上的商品名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DisplayCategoryCode = table.Column<int>(type: "int", nullable: false, comment: "显示品类代码 可通过品类列表查询API或下载品类信息Excel查看"),
                    VendorId = table.Column<string>(type: "longtext", nullable: true, comment: "卖家ID 酷澎提供给卖家的固有代码 ")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SaleStartedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "销售开始日期 格式为\"yyyy-MM-dd'T'HH:mm:ss\" "),
                    SaleEndedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "销售结束日期  格式为\"yyyy-MM-dd'T'HH:mm:ss\" , *年份可选择至2099年  "),
                    Brand = table.Column<string>(type: "longtext", nullable: true, comment: "品牌 输入品牌的韩文/英文标准名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StatusName = table.Column<string>(type: "longtext", nullable: true, comment: "注册商品状态 审批中/临时保存/待审批/已批准/部分批准/未通过审批/商品已删除")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "注册销售的时间  格式\"yyyy-MM-ddTHH:mm:ss\" "),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否删除"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "创建人id"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "修改人id"),
                    Remark = table.Column<string>(type: "longtext", nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoupangProduct", x => x.Id);
                },
                comment: "电商平台商品")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CoupangStore",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(type: "longtext", nullable: true, comment: "店铺类型")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true, comment: "店铺名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShortName = table.Column<string>(type: "longtext", nullable: true, comment: "店铺简称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccessKey = table.Column<string>(type: "longtext", nullable: true, comment: "AccessKey")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecretKey = table.Column<string>(type: "longtext", nullable: true, comment: "AccessKey")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpireTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "密钥过期时间"),
                    IsAutoRun = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否自动采集"),
                    Status = table.Column<string>(type: "longtext", nullable: true, comment: "状态（停用 正常）")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否删除"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "创建人id"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "修改人id"),
                    Remark = table.Column<string>(type: "longtext", nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoupangStore", x => x.Id);
                },
                comment: "电商平台店铺")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QRCodeStyle",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Width = table.Column<int>(type: "int", nullable: false, comment: "宽"),
                    Height = table.Column<int>(type: "int", nullable: false, comment: "高"),
                    Top = table.Column<int>(type: "int", nullable: false, comment: "上边距"),
                    Left = table.Column<int>(type: "int", nullable: false, comment: "左边距"),
                    Margin = table.Column<int>(type: "int", nullable: false, comment: "边距"),
                    BackgroundColor = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "背景颜色")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DotType = table.Column<int>(type: "int", nullable: false, comment: "码点类型"),
                    DotColor = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "码点颜色")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CornerDotType = table.Column<int>(type: "int", nullable: false, comment: "码眼样式"),
                    CornerDotColor = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "码眼颜色")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CornerSquareColor = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "码眼边框颜色")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Logo = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "Logo 图片地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否删除"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "创建人id"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "修改人id"),
                    Remark = table.Column<string>(type: "longtext", nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRCodeStyle", x => x.Id);
                },
                comment: "二维码样式")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WeChatBlog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AId = table.Column<string>(type: "longtext", nullable: true, comment: "微信公众号aid")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Title = table.Column<string>(type: "longtext", nullable: true, comment: "公众号文章")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cover = table.Column<string>(type: "longtext", nullable: true, comment: "封面图")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Digest = table.Column<string>(type: "longtext", nullable: true, comment: "摘要")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Create_Time = table.Column<long>(type: "bigint", nullable: false, comment: "创建时间"),
                    Update_Time = table.Column<long>(type: "bigint", nullable: false, comment: "修改时间"),
                    Link = table.Column<string>(type: "longtext", nullable: true, comment: "文章详情链接")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ItemShowType = table.Column<int>(type: "int", nullable: false, comment: "公众号发布内容类型"),
                    AuthorName = table.Column<string>(type: "longtext", nullable: true, comment: "作者")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否删除"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "创建人id"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "修改人id"),
                    Remark = table.Column<string>(type: "longtext", nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeChatBlog", x => x.Id);
                },
                comment: "公众号文章列表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QRCodeTemplate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RegionId = table.Column<long>(type: "bigint", nullable: false, comment: "区域 Id"),
                    TypeCode = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "类别代码，字典项")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Width = table.Column<int>(type: "int", nullable: false, comment: "宽度"),
                    Height = table.Column<int>(type: "int", nullable: false, comment: "高度"),
                    Background = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "背景图，如果内容是链接，背景就是图片，否则就是颜色（十六进制颜色代码#FFB6C1）")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDefault = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否默认"),
                    Preview = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true, comment: "预览效果（图片url）")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QRCodeStyleId = table.Column<long>(type: "bigint", nullable: false, comment: "模板中二维码样式 Id"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否删除"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "创建人id"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "修改人id"),
                    Remark = table.Column<string>(type: "longtext", nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRCodeTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QRCodeTemplate_QRCodeStyle_QRCodeStyleId",
                        column: x => x.QRCodeStyleId,
                        principalTable: "QRCodeStyle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QRCodeTemplate_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "二维码模板")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QRCodeTemplateText",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QRCodeTemplateId = table.Column<long>(type: "bigint", nullable: false, comment: "模板 Id"),
                    Width = table.Column<int>(type: "int", nullable: false, comment: "宽度"),
                    Height = table.Column<int>(type: "int", nullable: false, comment: "高度"),
                    Top = table.Column<int>(type: "int", nullable: false, comment: "距离顶部像素"),
                    Left = table.Column<int>(type: "int", nullable: false, comment: "距离左边像素"),
                    FontSize = table.Column<int>(type: "int", nullable: false, comment: "文字大小"),
                    TextBold = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "加粗"),
                    TextAlign = table.Column<int>(type: "int", nullable: false, comment: "文字对齐方式"),
                    TextContent = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true, comment: "文字内容")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TextColor = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "文字颜色")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BackgroundColor = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, comment: "背景颜色")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否删除"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "创建人id"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: "修改人id"),
                    Remark = table.Column<string>(type: "longtext", nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRCodeTemplateText", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QRCodeTemplateText_QRCodeTemplate_QRCodeTemplateId",
                        column: x => x.QRCodeTemplateId,
                        principalTable: "QRCodeTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "二维码模板文字块")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_QRCodeTemplate_QRCodeStyleId",
                table: "QRCodeTemplate",
                column: "QRCodeStyleId");

            migrationBuilder.CreateIndex(
                name: "IX_QRCodeTemplate_RegionId",
                table: "QRCodeTemplate",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_QRCodeTemplateText_QRCodeTemplateId",
                table: "QRCodeTemplateText",
                column: "QRCodeTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoupangOrder");

            migrationBuilder.DropTable(
                name: "CoupangOrderItem");

            migrationBuilder.DropTable(
                name: "CoupangProduct");

            migrationBuilder.DropTable(
                name: "CoupangStore");

            migrationBuilder.DropTable(
                name: "QRCodeTemplateText");

            migrationBuilder.DropTable(
                name: "WeChatBlog");

            migrationBuilder.DropTable(
                name: "QRCodeTemplate");

            migrationBuilder.DropTable(
                name: "QRCodeStyle");
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using aehyok.Core.EntityFrameCore.MySql.Models;

namespace aehyok.Core.EntityFrameCore.MySql.Data
{
    public partial class MyDbConext : DbContext
    {
        public MyDbConext()
        {
        }

        public MyDbConext(DbContextOptions<MyDbConext> options)
            : base(options)
        {
        }

        public virtual DbSet<BasicUser> BasicUsers { get; set; } = null!;
        public virtual DbSet<MdComputecolumn> MdComputecolumns { get; set; } = null!;
        public virtual DbSet<MdFunction> MdFunctions { get; set; } = null!;
        public virtual DbSet<MdInputgroup> MdInputgroups { get; set; } = null!;
        public virtual DbSet<MdInputtable> MdInputtables { get; set; } = null!;
        public virtual DbSet<MdInputview> MdInputviews { get; set; } = null!;
        public virtual DbSet<MdInputviewchild> MdInputviewchildren { get; set; } = null!;
        public virtual DbSet<MdInputviewcolumn> MdInputviewcolumns { get; set; } = null!;
        public virtual DbSet<MdNode> MdNodes { get; set; } = null!;
        public virtual DbSet<MdPageset> MdPagesets { get; set; } = null!;
        public virtual DbSet<MdPagesetgroup> MdPagesetgroups { get; set; } = null!;
        public virtual DbSet<MdParameter> MdParameters { get; set; } = null!;
        public virtual DbSet<MdQuerylog> MdQuerylogs { get; set; } = null!;
        public virtual DbSet<MdReftablelist> MdReftablelists { get; set; } = null!;
        public virtual DbSet<MdSavequery> MdSavequeries { get; set; } = null!;
        public virtual DbSet<MdTable> MdTables { get; set; } = null!;
        public virtual DbSet<MdTable2view> MdTable2views { get; set; } = null!;
        public virtual DbSet<MdTablecolumn> MdTablecolumns { get; set; } = null!;
        public virtual DbSet<MdTargetanalysis> MdTargetanalyses { get; set; } = null!;
        public virtual DbSet<MdTbnamespace> MdTbnamespaces { get; set; } = null!;
        public virtual DbSet<MdView> MdViews { get; set; } = null!;
        public virtual DbSet<MdView2app> MdView2apps { get; set; } = null!;
        public virtual DbSet<MdView2gl> MdView2gls { get; set; } = null!;
        public virtual DbSet<MdView2view> MdView2views { get; set; } = null!;
        public virtual DbSet<MdView2viewgroup> MdView2viewgroups { get; set; } = null!;
        public virtual DbSet<MdViewExright> MdViewExrights { get; set; } = null!;
        public virtual DbSet<MdViewgroup> MdViewgroups { get; set; } = null!;
        public virtual DbSet<MdViewgroupitem> MdViewgroupitems { get; set; } = null!;
        public virtual DbSet<MdViewtable> MdViewtables { get; set; } = null!;
        public virtual DbSet<MdViewtablecolumn> MdViewtablecolumns { get; set; } = null!;
        public virtual DbSet<MdZbdisplayitem> MdZbdisplayitems { get; set; } = null!;
        public virtual DbSet<TjZbztmcdyb> TjZbztmcdybs { get; set; } = null!;
        public virtual DbSet<TjZdyzbdyb> TjZdyzbdybs { get; set; } = null!;
        public virtual DbSet<TjZdyzbdybC> TjZdyzbdybCs { get; set; } = null!;
        public virtual DbSet<XtSystemlog> XtSystemlogs { get; set; } = null!;
        public virtual DbSet<XtUserlog> XtUserlogs { get; set; } = null!;
        public virtual DbSet<XtUsertoken> XtUsertokens { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=139.186.205.7;port=3306;uid=aehyok;pwd=M9y2512!;database=metadata;allowzerodatetime=True;convertzerodatetime=True;charset=utf8mb4;sslmode=none", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.2.32-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_unicode_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<BasicUser>(entity =>
            {
                entity.ToTable("BasicUser");

                entity.HasComment("用户")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Account, "account")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(10)")
                    .HasColumnName("id");

                entity.Property(e => e.Account)
                    .HasMaxLength(256)
                    .HasColumnName("account")
                    .HasComment("用户账号，兼容微信id");

                entity.Property(e => e.Address)
                    .HasMaxLength(2048)
                    .HasColumnName("address")
                    .HasDefaultValueSql("''")
                    .HasComment("地址");

                entity.Property(e => e.AreaId)
                    .HasColumnType("int(10)")
                    .HasColumnName("areaId")
                    .HasComment("所属区域");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("createdAt")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.DepartmentIds)
                    .HasMaxLength(256)
                    .HasColumnName("departmentIds")
                    .HasDefaultValueSql("''")
                    .HasComment("所属部门Id");

                entity.Property(e => e.Description)
                    .HasMaxLength(2048)
                    .HasColumnName("description")
                    .HasDefaultValueSql("''")
                    .HasComment("描述/职务");

                entity.Property(e => e.Email)
                    .HasMaxLength(64)
                    .HasColumnName("email")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.HouseholdId)
                    .HasColumnType("int(11)")
                    .HasColumnName("householdId")
                    .HasComment("户码Id");

                entity.Property(e => e.IsAuth)
                    .HasColumnType("int(10)")
                    .HasColumnName("isAuth")
                    .HasComment("公众用户是否已认证，0未审核， 1待审核，2审核通过，3审核不通过");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.IsGrid)
                    .HasColumnType("int(10)")
                    .HasColumnName("isGrid")
                    .HasComment("是否网格员，0否， 1一级网格员，2二级网格员(网格长)");

                entity.Property(e => e.IsLeader)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("isLeader")
                    .HasComment("是否主管人员");

                entity.Property(e => e.LoginedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("loginedAt")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(64)
                    .HasColumnName("mobile")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.NickName)
                    .HasMaxLength(256)
                    .HasColumnName("nickName")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.ParkAreaId)
                    .HasColumnType("int(10)")
                    .HasColumnName("parkAreaId")
                    .HasComment("园区id");

                entity.Property(e => e.Password)
                    .HasMaxLength(256)
                    .HasColumnName("password");

                entity.Property(e => e.PopulationId)
                    .HasColumnType("int(10)")
                    .HasColumnName("populationId")
                    .HasComment("户籍人口表Id");

                entity.Property(e => e.PortraitFileId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("portraitFileId")
                    .HasComment("头像id");

                entity.Property(e => e.RoleIds)
                    .HasMaxLength(64)
                    .HasColumnName("roleIds")
                    .HasDefaultValueSql("'0'")
                    .HasComment("角色id");

                entity.Property(e => e.Sex)
                    .HasColumnName("sex")
                    .HasComment("男性1，女性2，未知0");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Type)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("type")
                    .HasComment("公众1村委2政务3企业4");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("updatedAt")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Wxopenid)
                    .HasMaxLength(256)
                    .HasColumnName("wxopenid")
                    .HasComment("小程序openid");
            });

            modelBuilder.Entity<MdComputecolumn>(entity =>
            {
                entity.ToTable("md_computecolumn");

                entity.HasComment("计算字段");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("id")
                    .HasComment("GUID");

                entity.Property(e => e.Columname)
                    .HasMaxLength(200)
                    .HasColumnName("columname")
                    .HasComment("字段名称");

                entity.Property(e => e.Columndes)
                    .HasMaxLength(3000)
                    .HasColumnName("columndes")
                    .HasComment("字段说明");

                entity.Property(e => e.Columnexp)
                    .HasMaxLength(3000)
                    .HasColumnName("columnexp")
                    .HasComment("字段表达式");

                entity.Property(e => e.Columnmeta)
                    .HasMaxLength(3000)
                    .HasColumnName("columnmeta")
                    .HasComment("元数据定义");

                entity.Property(e => e.Createdate)
                    .HasColumnName("createdate")
                    .HasComment("创建时间");

                entity.Property(e => e.Ispublic)
                    .HasColumnType("int(11)")
                    .HasColumnName("ispublic")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否公用 0:个人使用  1.公用");

                entity.Property(e => e.Tablename)
                    .HasMaxLength(200)
                    .HasColumnName("tablename")
                    .HasComment("所在表名称");

                entity.Property(e => e.Userid)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("userid")
                    .HasComment("私人用户ID");

                entity.Property(e => e.Viewname)
                    .HasMaxLength(200)
                    .HasColumnName("viewname")
                    .HasComment("查询模型全称：命名空间 .查询模型");
            });

            modelBuilder.Entity<MdFunction>(entity =>
            {
                entity.HasKey(e => e.Funid)
                    .HasName("PRIMARY");

                entity.ToTable("md_function");

                entity.HasComment("函数定义表");

                entity.Property(e => e.Funid)
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever()
                    .HasColumnName("funid")
                    .HasComment("主键");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .HasColumnName("description")
                    .HasComment("函数说明描述");

                entity.Property(e => e.Displayname)
                    .HasMaxLength(50)
                    .HasColumnName("displayname")
                    .HasComment("函数显示名称");

                entity.Property(e => e.Dwdm)
                    .HasMaxLength(12)
                    .HasColumnName("dwdm")
                    .HasComment("单位代码");

                entity.Property(e => e.Functionname)
                    .HasMaxLength(50)
                    .HasColumnName("functionname")
                    .HasComment("函数名");

                entity.Property(e => e.Namespace)
                    .HasMaxLength(50)
                    .HasColumnName("namespace")
                    .HasComment("命名空间");

                entity.Property(e => e.Parameta)
                    .HasMaxLength(3000)
                    .HasColumnName("parameta")
                    .HasComment("调用参数元数据， 格式<name>xxx</name><type>xxx</type>,<name>xxx</name><type>xxx</type>");

                entity.Property(e => e.Resulttype)
                    .HasMaxLength(20)
                    .HasColumnName("resulttype")
                    .HasComment("返回类型：VARCHAR,NUMBER,DATETIME");

                entity.Property(e => e.Type)
                    .HasColumnType("int(11)")
                    .HasColumnName("type")
                    .HasComment("1.计算函数  2统计函数");
            });

            modelBuilder.Entity<MdInputgroup>(entity =>
            {
                entity.HasKey(e => e.IvgId)
                    .HasName("PRIMARY");

                entity.ToTable("md_inputgroup");

                entity.HasComment("录入数据分组");

                entity.Property(e => e.IvgId)
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever()
                    .HasColumnName("ivg_id")
                    .HasComment("组ID");

                entity.Property(e => e.Appregurl)
                    .HasMaxLength(1000)
                    .HasColumnName("appregurl")
                    .HasComment("如果是应用注册类型，则此处为URL");

                entity.Property(e => e.Displayorder)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayorder")
                    .HasComment("组顺序");

                entity.Property(e => e.Displaytitle)
                    .HasMaxLength(300)
                    .HasColumnName("displaytitle")
                    .HasComment("组显示名称");

                entity.Property(e => e.Groupcs)
                    .HasMaxLength(4000)
                    .HasColumnName("groupcs")
                    .HasComment("分组参数");

                entity.Property(e => e.Grouptype)
                    .HasMaxLength(50)
                    .HasColumnName("grouptype")
                    .HasComment("分组类型（DEFAULT:正常     APPREG:应用注册）");

                entity.Property(e => e.IvId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("iv_id")
                    .HasComment("录入模型ID");
            });

            modelBuilder.Entity<MdInputtable>(entity =>
            {
                entity.ToTable("md_inputtable");

                entity.HasComment("录入模型对应的数据表结构定义");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever()
                    .HasColumnName("id")
                    .HasComment("主键");

                entity.Property(e => e.Displayorder)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayorder")
                    .HasComment("顺序");

                entity.Property(e => e.Islock)
                    .HasColumnType("int(11)")
                    .HasColumnName("islock")
                    .HasComment("是否锁定结构。 0：未锁定，可以修改结构  1：锁定，不可修改结构");

                entity.Property(e => e.IvId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("iv_id")
                    .HasComment("录入模型ID");

                entity.Property(e => e.Savemode)
                    .HasMaxLength(100)
                    .HasColumnName("savemode")
                    .HasComment("保存模式  （Normal 正常   OnlyInsert 仅插入 ，默认是Normal模式");

                entity.Property(e => e.Tablename)
                    .HasMaxLength(100)
                    .HasColumnName("tablename")
                    .HasComment("表名称");

                entity.Property(e => e.Tabletitle)
                    .HasMaxLength(200)
                    .HasColumnName("tabletitle")
                    .HasComment("显示名称");
            });

            modelBuilder.Entity<MdInputview>(entity =>
            {
                entity.HasKey(e => e.IvId)
                    .HasName("PRIMARY");

                entity.ToTable("md_inputview");

                entity.HasComment("录入视图");

                entity.Property(e => e.IvId)
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever()
                    .HasColumnName("iv_id")
                    .HasComment("主键");

                entity.Property(e => e.Afterwrite)
                    .HasMaxLength(400)
                    .HasColumnName("afterwrite")
                    .HasComment("写入数据表后执行命令");

                entity.Property(e => e.Beforewrite)
                    .HasMaxLength(400)
                    .HasColumnName("beforewrite")
                    .HasComment("写入数据表前执行命令");

                entity.Property(e => e.Delrule)
                    .HasMaxLength(4000)
                    .HasColumnName("delrule")
                    .HasComment("删除记录规则");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description")
                    .HasComment("说明");

                entity.Property(e => e.Displayname)
                    .HasMaxLength(50)
                    .HasColumnName("displayname")
                    .HasComment("显示名称");

                entity.Property(e => e.Displayorder)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayorder")
                    .HasComment("显示顺序");

                entity.Property(e => e.Dwdm)
                    .HasMaxLength(12)
                    .HasColumnName("dwdm")
                    .HasComment("单位代码");

                entity.Property(e => e.Integratedapp)
                    .HasMaxLength(1000)
                    .HasColumnName("integratedapp")
                    .HasComment("集成应用");

                entity.Property(e => e.IvCs)
                    .HasMaxLength(4000)
                    .HasColumnName("iv_cs")
                    .HasComment("录入视图参数");

                entity.Property(e => e.IvName)
                    .HasMaxLength(50)
                    .HasColumnName("iv_name")
                    .HasComment("录入视图名称");

                entity.Property(e => e.Namespace)
                    .HasMaxLength(50)
                    .HasColumnName("namespace")
                    .HasComment("命名空间");

                entity.Property(e => e.Restype)
                    .HasMaxLength(1000)
                    .HasColumnName("restype")
                    .HasComment("资源类型");

                entity.Property(e => e.Tid)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("tid")
                    .HasComment("录入对应的表名称");
            });

            modelBuilder.Entity<MdInputviewchild>(entity =>
            {
                entity.ToTable("md_inputviewchild");

                entity.HasComment("录入模型子模型");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever()
                    .HasColumnName("id")
                    .HasComment("主键");

                entity.Property(e => e.CivId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("civ_id")
                    .HasComment("子模型ID");

                entity.Property(e => e.Displayorder)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayorder")
                    .HasComment("显示顺序");

                entity.Property(e => e.IvId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("iv_id")
                    .HasComment("录入模型ID");

                entity.Property(e => e.Param)
                    .HasMaxLength(4000)
                    .HasColumnName("param")
                    .HasComment("参数");

                entity.Property(e => e.Selectmode)
                    .HasColumnType("int(11)")
                    .HasColumnName("selectmode")
                    .HasDefaultValueSql("'0'")
                    .HasComment("数据选择方式  0：CheckBox  1：RadioButton?");

                entity.Property(e => e.Showcondition)
                    .HasMaxLength(400)
                    .HasColumnName("showcondition")
                    .HasComment("展开显示条件");
            });

            modelBuilder.Entity<MdInputviewcolumn>(entity =>
            {
                entity.HasKey(e => e.IvcId)
                    .HasName("PRIMARY");

                entity.ToTable("md_inputviewcolumn");

                entity.HasComment("录入视图列信息");

                entity.Property(e => e.IvcId)
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever()
                    .HasColumnName("ivc_id")
                    .HasComment("录入视图列ID");

                entity.Property(e => e.Candisplay)
                    .HasMaxLength(1)
                    .HasColumnName("candisplay")
                    .HasComment("是否可显示 {‘Y','N'}")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Caneditrule)
                    .HasMaxLength(4000)
                    .HasColumnName("caneditrule")
                    .HasComment("本字段是否可修改的规则")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Columnheight)
                    .HasColumnType("int(11)")
                    .HasColumnName("columnheight")
                    .HasComment("列高");

                entity.Property(e => e.Columnname)
                    .HasMaxLength(400)
                    .HasColumnName("columnname")
                    .HasComment("字段名")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Columnorder)
                    .HasColumnType("int(11)")
                    .HasColumnName("columnorder")
                    .HasComment("字段顺序");

                entity.Property(e => e.Columntype)
                    .HasMaxLength(100)
                    .HasColumnName("columntype")
                    .HasComment("字段类型")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Columnwidth)
                    .HasColumnType("int(11)")
                    .HasColumnName("columnwidth")
                    .HasComment("列宽");

                entity.Property(e => e.Datachangedevent)
                    .HasMaxLength(4000)
                    .HasColumnName("datachangedevent")
                    .HasComment("数据变更后的相应处理")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Defaultshow)
                    .HasColumnType("int(11)")
                    .HasColumnName("defaultshow")
                    .HasDefaultValueSql("'1'")
                    .HasComment("是否默认显示状态（1为显示 2为不显示）");

                entity.Property(e => e.Displayformat)
                    .HasMaxLength(100)
                    .HasColumnName("displayformat")
                    .HasComment("显示格式")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Displayname)
                    .HasMaxLength(100)
                    .HasColumnName("displayname")
                    .HasComment("显示名称")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Dwdm)
                    .HasMaxLength(12)
                    .HasColumnName("dwdm")
                    .HasComment("单位代码")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Editformat)
                    .HasMaxLength(100)
                    .HasColumnName("editformat")
                    .HasComment("修改录入格式")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Inputdefault)
                    .HasMaxLength(4000)
                    .HasColumnName("inputdefault")
                    .HasComment("默认值")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Inputrule)
                    .HasMaxLength(4000)
                    .HasColumnName("inputrule")
                    .HasComment("本字段在录入时的检验规则及自动完成规则")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Iscompute)
                    .HasColumnType("int(11)")
                    .HasColumnName("iscompute")
                    .HasComment("是否计算字段 1.是计算字段 0：不是");

                entity.Property(e => e.IvId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("iv_id")
                    .HasComment("录入视图ID");

                entity.Property(e => e.IvgId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ivg_id")
                    .HasDefaultValueSql("'0'")
                    .HasComment("所在的分组ID");

                entity.Property(e => e.Maxlength)
                    .HasColumnType("int(11)")
                    .HasColumnName("maxlength")
                    .HasDefaultValueSql("'0'")
                    .HasComment("录入的最大长度（小于0为不限制长度，大于0为限制长度）");

                entity.Property(e => e.Readonly)
                    .HasColumnType("int(11)")
                    .HasColumnName("readonly")
                    .HasComment("是否只读   1：只读  0：可修改");

                entity.Property(e => e.Required)
                    .HasColumnType("int(11)")
                    .HasColumnName("required")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否必填   0：非必填   1：必填");

                entity.Property(e => e.Tablename)
                    .HasMaxLength(100)
                    .HasColumnName("tablename")
                    .HasComment("写入的表名,当此字段为空时,默认为MD_INPUTVIEW参数中定义的TABLE.")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Tcid)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("tcid")
                    .HasComment("对应的表列ID");

                entity.Property(e => e.Textalignment)
                    .HasColumnType("int(11)")
                    .HasColumnName("textalignment")
                    .HasComment("显示位置: 0:默认 1: LEFT 2:CENTER 3:RIGHT");

                entity.Property(e => e.Tooltip)
                    .HasMaxLength(4000)
                    .HasColumnName("tooltip")
                    .HasComment("注释文本")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<MdNode>(entity =>
            {
                entity.ToTable("md_nodes");

                entity.HasComment("节点定义");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id")
                    .HasComment("节点ID");

                entity.Property(e => e.Descript)
                    .HasMaxLength(2000)
                    .HasColumnName("descript")
                    .HasComment("描述");

                entity.Property(e => e.Displayorder)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayorder")
                    .HasComment("顺序");

                entity.Property(e => e.Displaytitle)
                    .HasMaxLength(200)
                    .HasColumnName("displaytitle")
                    .HasComment("显示名称");

                entity.Property(e => e.Dwdm)
                    .HasMaxLength(100)
                    .HasColumnName("dwdm")
                    .HasComment("节点编码");

                entity.Property(e => e.Nodename)
                    .HasMaxLength(200)
                    .HasColumnName("nodename")
                    .HasComment("节点名称");

                entity.Property(e => e.Systemtype)
                    .HasMaxLength(20)
                    .HasColumnName("systemtype")
                    .HasComment("节点类型");
            });

            modelBuilder.Entity<MdPageset>(entity =>
            {
                entity.ToTable("md_pageset");

                entity.HasComment("页面集");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever()
                    .HasColumnName("id")
                    .HasComment("GUID");

                entity.Property(e => e.Defaultdw)
                    .HasMaxLength(100)
                    .HasColumnName("defaultdw")
                    .HasComment("默认单位");

                entity.Property(e => e.Defaultenddate)
                    .HasMaxLength(100)
                    .HasColumnName("defaultenddate")
                    .HasComment("默认结束日期");

                entity.Property(e => e.Defaultstartdate)
                    .HasMaxLength(100)
                    .HasColumnName("defaultstartdate")
                    .HasComment("默认开始日期");

                entity.Property(e => e.Displaymode)
                    .HasColumnType("int(11)")
                    .HasColumnName("displaymode")
                    .HasComment("显示模式0:单栏单指标1:双栏单指标2:双栏双指标");

                entity.Property(e => e.Displayorder)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayorder")
                    .HasComment("显示顺序");

                entity.Property(e => e.Dwdm)
                    .HasMaxLength(12)
                    .HasColumnName("dwdm")
                    .HasComment("单位代码");

                entity.Property(e => e.Groupid)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("groupid")
                    .HasComment("分组ID");

                entity.Property(e => e.Lmmeta1)
                    .HasMaxLength(2000)
                    .HasColumnName("lmmeta1")
                    .HasComment("第一个栏目的元数据");

                entity.Property(e => e.Lmmeta2)
                    .HasMaxLength(2000)
                    .HasColumnName("lmmeta2")
                    .HasComment("第二个栏目的元数据");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title")
                    .HasComment("页面标题");

                entity.Property(e => e.Zbmeta1)
                    .HasMaxLength(1000)
                    .HasColumnName("zbmeta1")
                    .HasComment("第一个指标的元数据");

                entity.Property(e => e.Zbmeta2)
                    .HasMaxLength(1000)
                    .HasColumnName("zbmeta2")
                    .HasComment("第二个指示的元数据");

                entity.Property(e => e.Zbtype)
                    .HasMaxLength(10)
                    .HasColumnName("zbtype")
                    .HasComment("R:报表指标   Z:自定义指标");

                entity.Property(e => e.Zdyzbsf)
                    .HasMaxLength(4000)
                    .HasColumnName("zdyzbsf")
                    .HasComment("自定义指标算法");
            });

            modelBuilder.Entity<MdPagesetgroup>(entity =>
            {
                entity.HasKey(e => e.Groupid)
                    .HasName("PRIMARY");

                entity.ToTable("md_pagesetgroup");

                entity.Property(e => e.Groupid)
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever()
                    .HasColumnName("groupid")
                    .HasComment("分组ID");

                entity.Property(e => e.Displayname)
                    .HasMaxLength(100)
                    .HasColumnName("displayname")
                    .HasComment("分组名称");

                entity.Property(e => e.Displayorder)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayorder")
                    .HasComment("显示顺序");

                entity.Property(e => e.Dwdm)
                    .HasMaxLength(12)
                    .HasColumnName("dwdm")
                    .HasComment("单位代码");

                entity.Property(e => e.Namespace)
                    .HasMaxLength(100)
                    .HasColumnName("namespace")
                    .HasComment("所属命名空间");
            });

            modelBuilder.Entity<MdParameter>(entity =>
            {
                entity.ToTable("md_parameter");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("id")
                    .HasComment("GUID")
                    .UseCollation("utf8mb4_bin");

                entity.Property(e => e.Pdata)
                    .HasColumnName("pdata")
                    .HasComment("参数值");

                entity.Property(e => e.Pname)
                    .HasMaxLength(50)
                    .HasColumnName("pname")
                    .HasComment("参数名称");
            });

            modelBuilder.Entity<MdQuerylog>(entity =>
            {
                entity.ToTable("md_querylog");

                entity.HasComment("查询日志");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("id")
                    .HasComment("ID");

                entity.Property(e => e.Bz)
                    .HasMaxLength(1000)
                    .HasColumnName("bz")
                    .HasComment("备注");

                entity.Property(e => e.Lx)
                    .HasMaxLength(10)
                    .HasColumnName("lx")
                    .HasComment("{1-模型 2-指标 }");

                entity.Property(e => e.QueryStr)
                    .HasMaxLength(4000)
                    .HasColumnName("query_str")
                    .HasComment("查询语句");

                entity.Property(e => e.Sj)
                    .HasColumnType("datetime")
                    .HasColumnName("sj")
                    .HasComment("执行时间");

                entity.Property(e => e.Usetime)
                    .HasColumnType("int(11)")
                    .HasColumnName("usetime")
                    .HasComment("查询用时（毫秒数）");

                entity.Property(e => e.Yhid)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("yhid")
                    .HasComment("用户ID");
            });

            modelBuilder.Entity<MdReftablelist>(entity =>
            {
                entity.HasKey(e => e.Rtid)
                    .HasName("PRIMARY");

                entity.ToTable("md_reftablelist");

                entity.Property(e => e.Rtid)
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever()
                    .HasColumnName("rtid");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.Property(e => e.Downloadmode)
                    .HasColumnType("int(11)")
                    .HasColumnName("downloadmode")
                    .HasComment("数据下载模式 1：一次性全部下载 2：分级下载");

                entity.Property(e => e.Dwdm)
                    .HasMaxLength(12)
                    .HasColumnName("dwdm")
                    .HasComment("单位代码");

                entity.Property(e => e.Hidecode)
                    .HasColumnType("int(11)")
                    .HasColumnName("hidecode")
                    .HasComment("是否在使用代码表时隐藏代码 0:不隐藏  1隐藏");

                entity.Property(e => e.Namespace)
                    .HasMaxLength(50)
                    .HasColumnName("namespace");

                entity.Property(e => e.Reftablelevelformat)
                    .HasMaxLength(20)
                    .HasColumnName("reftablelevelformat");

                entity.Property(e => e.Reftablemode)
                    .HasColumnType("int(11)")
                    .HasColumnName("reftablemode")
                    .HasComment("代码表模式：1：正常模式 2参数比较下载模式");

                entity.Property(e => e.Reftablename)
                    .HasMaxLength(50)
                    .HasColumnName("reftablename");
            });

            modelBuilder.Entity<MdSavequery>(entity =>
            {
                entity.ToTable("md_savequery");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Ispublic)
                    .HasColumnType("int(11)")
                    .HasColumnName("ispublic");

                entity.Property(e => e.Sj).HasColumnName("sj");

                entity.Property(e => e.Sydwid)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("sydwid");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title");

                entity.Property(e => e.Tjsf)
                    .HasColumnType("blob")
                    .HasColumnName("tjsf");

                entity.Property(e => e.Type)
                    .HasMaxLength(100)
                    .HasColumnName("type");

                entity.Property(e => e.Viewid)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("viewid");

                entity.Property(e => e.Yhid)
                    .HasColumnType("int(11)")
                    .HasColumnName("yhid");
            });

            modelBuilder.Entity<MdTable>(entity =>
            {
                entity.HasKey(e => e.Tid)
                    .HasName("PRIMARY");

                entity.ToTable("md_table");

                entity.HasComment("表定义");

                entity.Property(e => e.Tid)
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever()
                    .HasColumnName("tid");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description")
                    .HasComment("描述");

                entity.Property(e => e.Displayname)
                    .HasMaxLength(100)
                    .HasColumnName("displayname")
                    .HasComment("显示名称");

                entity.Property(e => e.Dwdm)
                    .HasMaxLength(12)
                    .HasColumnName("dwdm")
                    .HasComment("单位代码");

                entity.Property(e => e.Extsecret)
                    .HasMaxLength(1000)
                    .HasColumnName("extsecret")
                    .HasComment("EXTSECRET");

                entity.Property(e => e.Mainkey)
                    .HasMaxLength(50)
                    .HasColumnName("mainkey")
                    .HasComment("主键字段");

                entity.Property(e => e.Namespace)
                    .HasMaxLength(50)
                    .HasColumnName("namespace")
                    .HasComment("命名空间");

                entity.Property(e => e.Restype)
                    .HasMaxLength(100)
                    .HasColumnName("restype")
                    .HasComment("本表的资源类型");

                entity.Property(e => e.Secretfun)
                    .HasMaxLength(500)
                    .HasColumnName("secretfun")
                    .HasComment("安全判别函数名");

                entity.Property(e => e.Tablename)
                    .HasMaxLength(50)
                    .HasColumnName("tablename")
                    .HasComment("表名称");

                entity.Property(e => e.Tabletype)
                    .HasMaxLength(50)
                    .HasColumnName("tabletype")
                    .HasComment("表类型 VIEW TABLE");
            });

            modelBuilder.Entity<MdTable2view>(entity =>
            {
                entity.ToTable("md_table2view");

                entity.HasComment("表关联到的查询模型的定义");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("id")
                    .HasComment("GUID");

                entity.Property(e => e.Conditionstr)
                    .HasMaxLength(4000)
                    .HasColumnName("conditionstr")
                    .HasComment("关联条件定义");

                entity.Property(e => e.Confine)
                    .HasMaxLength(1000)
                    .HasColumnName("confine")
                    .HasComment("限制条件");

                entity.Property(e => e.Tid)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("tid")
                    .HasComment("表ID");

                entity.Property(e => e.Viewname)
                    .HasMaxLength(1000)
                    .HasColumnName("viewname")
                    .HasComment("关联到的查询模型名称");
            });

            modelBuilder.Entity<MdTablecolumn>(entity =>
            {
                entity.HasKey(e => e.Tcid)
                    .HasName("PRIMARY");

                entity.ToTable("md_tablecolumn");

                entity.HasComment("表的列定义");

                entity.Property(e => e.Tcid)
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever()
                    .HasColumnName("tcid");

                entity.Property(e => e.Candisplay)
                    .HasColumnType("int(11)")
                    .HasColumnName("candisplay")
                    .HasComment("是否显示");

                entity.Property(e => e.Columnname)
                    .HasMaxLength(50)
                    .HasColumnName("columnname")
                    .HasComment("列名称");

                entity.Property(e => e.Colwidth)
                    .HasColumnType("int(11)")
                    .HasColumnName("colwidth")
                    .HasComment("显示宽度");

                entity.Property(e => e.Ctag)
                    .HasMaxLength(100)
                    .HasColumnName("ctag")
                    .HasComment("概念标签");

                entity.Property(e => e.Displayformat)
                    .HasMaxLength(50)
                    .HasColumnName("displayformat")
                    .HasComment("显示格式");

                entity.Property(e => e.Displayheight)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayheight")
                    .HasComment("显示行数");

                entity.Property(e => e.Displaylength)
                    .HasColumnType("int(11)")
                    .HasColumnName("displaylength")
                    .HasComment("显示列宽");

                entity.Property(e => e.Displayorder)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayorder")
                    .HasComment("显示顺序");

                entity.Property(e => e.Displaytitle)
                    .HasMaxLength(50)
                    .HasColumnName("displaytitle")
                    .HasComment("显示名称");

                entity.Property(e => e.Dmblevelformat)
                    .HasMaxLength(20)
                    .HasColumnName("dmblevelformat")
                    .HasComment("分级代码表格式");

                entity.Property(e => e.Dwdm)
                    .HasMaxLength(12)
                    .HasColumnName("dwdm")
                    .HasComment("单位代码");

                entity.Property(e => e.Isnullable)
                    .HasMaxLength(10)
                    .HasColumnName("isnullable")
                    .HasComment("可否为空");

                entity.Property(e => e.Length)
                    .HasColumnType("int(11)")
                    .HasColumnName("length")
                    .HasComment("数据长度");

                entity.Property(e => e.Precision)
                    .HasColumnType("int(11)")
                    .HasColumnName("precision")
                    .HasComment("小数位");

                entity.Property(e => e.Refdmb)
                    .HasMaxLength(50)
                    .HasColumnName("refdmb")
                    .HasComment("引用代码表");

                entity.Property(e => e.Refwordtb)
                    .HasMaxLength(50)
                    .HasColumnName("refwordtb")
                    .HasComment("参考用词表，用于规范录入");

                entity.Property(e => e.Scale)
                    .HasColumnType("int(11)")
                    .HasColumnName("scale");

                entity.Property(e => e.Secretlevel)
                    .HasColumnType("int(11)")
                    .HasColumnName("secretlevel")
                    .HasComment("安全级别");

                entity.Property(e => e.Tid)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("tid");

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .HasColumnName("type")
                    .HasComment("数据类型");
            });

            modelBuilder.Entity<MdTargetanalysis>(entity =>
            {
                entity.ToTable("md_targetanalysis");

                entity.HasComment("目标分析");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Displayorder)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayorder")
                    .HasComment("显示顺序");

                entity.Property(e => e.Displaytitle)
                    .HasMaxLength(50)
                    .HasColumnName("displaytitle")
                    .HasComment("显示标题");

                entity.Property(e => e.Fsmeta)
                    .HasMaxLength(4000)
                    .HasColumnName("fsmeta")
                    .HasComment("元数据");

                entity.Property(e => e.Lx)
                    .HasMaxLength(10)
                    .HasColumnName("lx")
                    .HasComment("类型 B 基本信息　CSZB 带参数的指标　WCSZB 无参数指标");

                entity.Property(e => e.Mc)
                    .HasMaxLength(50)
                    .HasColumnName("mc")
                    .HasComment("分析内容名称");

                entity.Property(e => e.Sf)
                    .HasMaxLength(4000)
                    .HasColumnName("sf")
                    .HasComment("算法");
            });

            modelBuilder.Entity<MdTbnamespace>(entity =>
            {
                entity.HasKey(e => e.Namespace)
                    .HasName("PRIMARY");

                entity.ToTable("md_tbnamespace");

                entity.HasComment("表命名空间");

                entity.Property(e => e.Namespace)
                    .HasMaxLength(50)
                    .HasColumnName("namespace");

                entity.Property(e => e.Concepts)
                    .HasMaxLength(1000)
                    .HasColumnName("concepts")
                    .HasComment("包含的概念组");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.Property(e => e.Displayorder)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayorder");

                entity.Property(e => e.Displaytitle)
                    .HasMaxLength(50)
                    .HasColumnName("displaytitle");

                entity.Property(e => e.Dwdm)
                    .HasMaxLength(12)
                    .HasColumnName("dwdm");

                entity.Property(e => e.Menuposition)
                    .HasMaxLength(50)
                    .HasColumnName("menuposition")
                    .HasComment("所在菜单");

                entity.Property(e => e.Owner)
                    .HasMaxLength(50)
                    .HasColumnName("owner");
            });

            modelBuilder.Entity<MdView>(entity =>
            {
                entity.HasKey(e => e.Viewid)
                    .HasName("PRIMARY");

                entity.ToTable("md_view");

                entity.HasComment("视图");

                entity.Property(e => e.Viewid)
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever()
                    .HasColumnName("viewid");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .HasColumnName("description");

                entity.Property(e => e.Displayname)
                    .HasMaxLength(100)
                    .HasColumnName("displayname");

                entity.Property(e => e.Displayorder)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayorder")
                    .HasDefaultValueSql("'0'")
                    .HasComment("显示顺序");

                entity.Property(e => e.Dwdm)
                    .HasMaxLength(12)
                    .HasColumnName("dwdm")
                    .HasComment("单位代码");

                entity.Property(e => e.Extmeta)
                    .HasMaxLength(4000)
                    .HasColumnName("extmeta")
                    .HasComment("扩展META");

                entity.Property(e => e.Icstype)
                    .HasMaxLength(20)
                    .HasColumnName("icstype")
                    .HasComment("接口类型 : ORA_JSIS、SQL_GDFS ");

                entity.Property(e => e.IsGdcx)
                    .HasColumnType("int(11)")
                    .HasColumnName("is_gdcx")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否固定查询（0-否 1-是）");

                entity.Property(e => e.IsGlcx)
                    .HasColumnType("int(11)")
                    .HasColumnName("is_glcx")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否关联查询（0-否 1-是）");

                entity.Property(e => e.IsSjsh)
                    .HasColumnType("int(11)")
                    .HasColumnName("is_sjsh")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否数据审核（0-否 1-是）");

                entity.Property(e => e.Namespace)
                    .HasMaxLength(50)
                    .HasColumnName("namespace");

                entity.Property(e => e.Viewname)
                    .HasMaxLength(50)
                    .HasColumnName("viewname");
            });

            modelBuilder.Entity<MdView2app>(entity =>
            {
                entity.ToTable("md_view2app");

                entity.HasComment("查询模型关联注册的应用");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("id");

                entity.Property(e => e.Displayheight)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayheight")
                    .HasComment("显示区高度");

                entity.Property(e => e.Displayorder)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayorder")
                    .HasComment("显示顺序");

                entity.Property(e => e.Integratedapp)
                    .HasMaxLength(1000)
                    .HasColumnName("integratedapp")
                    .HasComment("集成应用名称");

                entity.Property(e => e.Meta)
                    .HasMaxLength(4000)
                    .HasColumnName("meta")
                    .HasComment("其它元数据定义");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title")
                    .HasComment("显示标题");

                entity.Property(e => e.Url)
                    .HasMaxLength(4000)
                    .HasColumnName("url")
                    .HasComment("注册显示区URL");

                entity.Property(e => e.Viewid)
                    .HasMaxLength(50)
                    .HasColumnName("viewid")
                    .HasComment("视图ID");
            });

            modelBuilder.Entity<MdView2gl>(entity =>
            {
                entity.ToTable("md_view2gl");

                entity.HasComment("模型关联指标定义");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("id");

                entity.Property(e => e.Displayorder)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayorder")
                    .HasComment("显示顺序");

                entity.Property(e => e.Displaytitle)
                    .HasMaxLength(200)
                    .HasColumnName("displaytitle")
                    .HasComment("显示标题");

                entity.Property(e => e.Extendmeta)
                    .HasMaxLength(4000)
                    .HasColumnName("extendmeta")
                    .HasComment("扩展定义");

                entity.Property(e => e.Targetcs)
                    .HasMaxLength(4000)
                    .HasColumnName("targetcs")
                    .HasComment("指标对应参数");

                entity.Property(e => e.Targetgl)
                    .HasMaxLength(50)
                    .HasColumnName("targetgl")
                    .HasComment("目标指标");

                entity.Property(e => e.Viewid)
                    .HasMaxLength(50)
                    .HasColumnName("viewid")
                    .HasComment("模型ID");
            });

            modelBuilder.Entity<MdView2view>(entity =>
            {
                entity.ToTable("md_view2view");

                entity.HasComment("查询模型到查询模型之间的关联定义");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("id")
                    .HasComment("GUID");

                entity.Property(e => e.Displayorder)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayorder")
                    .HasComment("显示顺序");

                entity.Property(e => e.Displaytitle)
                    .HasMaxLength(200)
                    .HasColumnName("displaytitle")
                    .HasComment("显示名称");

                entity.Property(e => e.Groupid)
                    .HasMaxLength(50)
                    .HasColumnName("groupid")
                    .HasComment("分组ID");

                entity.Property(e => e.Relationstr)
                    .HasMaxLength(4000)
                    .HasColumnName("relationstr")
                    .HasComment("关联条件");

                entity.Property(e => e.Targetviewname)
                    .HasMaxLength(300)
                    .HasColumnName("targetviewname")
                    .HasComment("关联到的模型名称");

                entity.Property(e => e.V2vmeta)
                    .HasMaxLength(4000)
                    .HasColumnName("v2vmeta")
                    .HasComment("用于保存一个METADATA信息");

                entity.Property(e => e.Viewid)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("viewid")
                    .HasComment("模型ID");
            });

            modelBuilder.Entity<MdView2viewgroup>(entity =>
            {
                entity.ToTable("md_view2viewgroup");

                entity.HasComment("模型关联模型的分组定义");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("id")
                    .HasComment("GUID");

                entity.Property(e => e.Displayorder)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayorder")
                    .HasComment("显示顺序");

                entity.Property(e => e.Displaytitle)
                    .HasMaxLength(200)
                    .HasColumnName("displaytitle")
                    .HasComment("显示名称");

                entity.Property(e => e.Viewid)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("viewid")
                    .HasComment("模型ID");
            });

            modelBuilder.Entity<MdViewExright>(entity =>
            {
                entity.ToTable("md_view_exright");

                entity.HasComment("查询模型_扩展权限");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("id")
                    .HasComment("主键");

                entity.Property(e => e.Displayorder)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayorder")
                    .HasDefaultValueSql("'0'")
                    .HasComment("显示顺序");

                entity.Property(e => e.Fid)
                    .HasMaxLength(50)
                    .HasColumnName("fid")
                    .HasComment("父权限ID");

                entity.Property(e => e.Rtitle)
                    .HasMaxLength(200)
                    .HasColumnName("rtitle")
                    .HasComment("显示名称");

                entity.Property(e => e.Rvalue)
                    .HasMaxLength(50)
                    .HasColumnName("rvalue")
                    .HasComment("权限值（做为参数代入用,可以为空，表示此值将不做为参数代入，仅用于显示）");

                entity.Property(e => e.Viewid)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("viewid")
                    .HasComment("查询模型ID");
            });

            modelBuilder.Entity<MdViewgroup>(entity =>
            {
                entity.HasKey(e => e.Vgid)
                    .HasName("PRIMARY");

                entity.ToTable("md_viewgroup");

                entity.Property(e => e.Vgid)
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever()
                    .HasColumnName("vgid");

                entity.Property(e => e.Displayname)
                    .HasMaxLength(100)
                    .HasColumnName("displayname");

                entity.Property(e => e.Displayorder)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayorder")
                    .HasComment("显示顺序");

                entity.Property(e => e.Dwdm)
                    .HasMaxLength(12)
                    .HasColumnName("dwdm")
                    .HasComment("单位代码");

                entity.Property(e => e.IsGdcx)
                    .HasColumnType("int(11)")
                    .HasColumnName("is_gdcx")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否固定查询（0-否 1-是）");

                entity.Property(e => e.IsGlcx)
                    .HasColumnType("int(11)")
                    .HasColumnName("is_glcx")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否关联查询（0-否 1-是）");

                entity.Property(e => e.IsSjsh)
                    .HasColumnType("int(11)")
                    .HasColumnName("is_sjsh")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否数据审核（0-否 1-是）");

                entity.Property(e => e.Namespace)
                    .HasMaxLength(50)
                    .HasColumnName("namespace");

                entity.Property(e => e.Viewgroupname)
                    .HasMaxLength(50)
                    .HasColumnName("viewgroupname");
            });

            modelBuilder.Entity<MdViewgroupitem>(entity =>
            {
                entity.HasKey(e => e.Vgiid)
                    .HasName("PRIMARY");

                entity.ToTable("md_viewgroupitem");

                entity.Property(e => e.Vgiid)
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever()
                    .HasColumnName("vgiid");

                entity.Property(e => e.Displayorder)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayorder");

                entity.Property(e => e.Dwdm)
                    .HasMaxLength(12)
                    .HasColumnName("dwdm");

                entity.Property(e => e.Vgid)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("vgid");

                entity.Property(e => e.Viewid)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("viewid");
            });

            modelBuilder.Entity<MdViewtable>(entity =>
            {
                entity.HasKey(e => e.Vtid)
                    .HasName("PRIMARY");

                entity.ToTable("md_viewtable");

                entity.HasComment("视图表");

                entity.Property(e => e.Vtid)
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever()
                    .HasColumnName("vtid");

                entity.Property(e => e.Cancondition)
                    .HasMaxLength(10)
                    .HasColumnName("cancondition")
                    .HasComment("本表是否可以做为查询的条件字段");

                entity.Property(e => e.Displayname)
                    .HasMaxLength(100)
                    .HasColumnName("displayname")
                    .HasComment("显示名称");

                entity.Property(e => e.Displayorder)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayorder")
                    .HasComment("显示顺序");

                entity.Property(e => e.Displaytype)
                    .HasColumnType("int(11)")
                    .HasColumnName("displaytype")
                    .HasDefaultValueSql("'0'")
                    .HasComment("显示方式，0: Grid方式，1:Form方式");

                entity.Property(e => e.Dwdm)
                    .HasMaxLength(12)
                    .HasColumnName("dwdm")
                    .HasComment("单位代码");

                entity.Property(e => e.Fatherid)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("fatherid");

                entity.Property(e => e.Integratedapp)
                    .HasMaxLength(1000)
                    .HasColumnName("integratedapp")
                    .HasComment("集成应用，可多项。(为空表示相应资源均会集成）");

                entity.Property(e => e.Priority)
                    .HasColumnType("int(11)")
                    .HasColumnName("priority")
                    .HasComment("优先级(数值越小优先级越高)");

                entity.Property(e => e.Tablerelation)
                    .HasMaxLength(300)
                    .HasColumnName("tablerelation")
                    .HasComment("副表与主表的连接关系表达式（ 主表时为空串）");

                entity.Property(e => e.Tabletype)
                    .HasMaxLength(20)
                    .HasColumnName("tabletype")
                    .HasComment("M:表示是视图中的主表   F：表示是视图中的附表");

                entity.Property(e => e.Tid)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("tid");

                entity.Property(e => e.Viewid)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("viewid");
            });

            modelBuilder.Entity<MdViewtablecolumn>(entity =>
            {
                entity.HasKey(e => e.Vtcid)
                    .HasName("PRIMARY");

                entity.ToTable("md_viewtablecolumn");

                entity.Property(e => e.Vtcid)
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever()
                    .HasColumnName("vtcid");

                entity.Property(e => e.Canconditionshow)
                    .HasColumnType("int(11)")
                    .HasColumnName("canconditionshow")
                    .HasComment("可做条件");

                entity.Property(e => e.Canmodify)
                    .HasColumnType("int(11)")
                    .HasColumnName("canmodify")
                    .HasComment("是否可以修改（只用于审核类型的VIEW）");

                entity.Property(e => e.Canresultshow)
                    .HasColumnType("int(11)")
                    .HasColumnName("canresultshow")
                    .HasComment("可做结果");

                entity.Property(e => e.Defaultshow)
                    .HasColumnType("int(11)")
                    .HasColumnName("defaultshow")
                    .HasComment("默认显示字段");

                entity.Property(e => e.Displayorder)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayorder")
                    .HasDefaultValueSql("'0'")
                    .HasComment("显示顺序");

                entity.Property(e => e.Dwdm)
                    .HasMaxLength(12)
                    .HasColumnName("dwdm")
                    .HasComment("单位代码");

                entity.Property(e => e.Fixqueryitem)
                    .HasColumnType("int(11)")
                    .HasColumnName("fixqueryitem")
                    .HasComment("是否固定查询项");

                entity.Property(e => e.Priority)
                    .HasColumnType("int(11)")
                    .HasColumnName("priority")
                    .HasComment("优先级");

                entity.Property(e => e.Tcid)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("tcid");

                entity.Property(e => e.Vtid)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("vtid");
            });

            modelBuilder.Entity<MdZbdisplayitem>(entity =>
            {
                entity.ToTable("md_zbdisplayitem");

                entity.HasComment("指标展示项目表");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Displayname)
                    .HasMaxLength(50)
                    .HasColumnName("displayname")
                    .HasComment("显示名称");

                entity.Property(e => e.Dwdm)
                    .HasMaxLength(12)
                    .HasColumnName("dwdm")
                    .HasComment("单位代码");

                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .HasColumnName("type")
                    .HasComment("类型 0复合型 1分组型");
            });

            modelBuilder.Entity<TjZbztmcdyb>(entity =>
            {
                entity.HasKey(e => e.Zbztmc)
                    .HasName("PRIMARY");

                entity.ToTable("tj_zbztmcdyb");

                entity.HasComment("指标主题名称定义表");

                entity.Property(e => e.Zbztmc)
                    .HasMaxLength(200)
                    .HasColumnName("zbztmc")
                    .HasComment("指标所属主题名称");

                entity.Property(e => e.Lx)
                    .HasColumnType("int(11)")
                    .HasColumnName("lx")
                    .HasComment("类型1-报表指标 3-统计指标");

                entity.Property(e => e.Namespace)
                    .HasMaxLength(50)
                    .HasColumnName("namespace")
                    .HasComment("命名空间");

                entity.Property(e => e.Qxlx)
                    .HasColumnType("int(11)")
                    .HasColumnName("qxlx")
                    .HasComment("权限类型1-允许下级使用 2-只有本单位使用");

                entity.Property(e => e.Ssdw)
                    .HasMaxLength(12)
                    .HasColumnName("ssdw")
                    .HasComment("所属单位");

                entity.Property(e => e.Zbztsm)
                    .HasMaxLength(200)
                    .HasColumnName("zbztsm")
                    .HasComment("指标主题说明");
            });

            modelBuilder.Entity<TjZdyzbdyb>(entity =>
            {
                entity.ToTable("tj_zdyzbdyb");

                entity.HasComment("自定义指标定义表");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever()
                    .HasColumnName("id")
                    .HasComment("ID");

                entity.Property(e => e.Fid)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("fid")
                    .HasComment("父指标ID");

                entity.Property(e => e.JsmxZbmeta)
                    .HasMaxLength(4000)
                    .HasColumnName("jsmx_zbmeta")
                    .HasComment("明细_指标元数据")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Xsxh)
                    .HasColumnType("int(11)")
                    .HasColumnName("xsxh")
                    .HasComment("显示顺序");

                entity.Property(e => e.Zbcxsf)
                    .HasMaxLength(4000)
                    .HasColumnName("zbcxsf")
                    .HasComment("指标查询算法 指标查询的SELECT语句")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Zbmc)
                    .HasMaxLength(200)
                    .HasColumnName("zbmc")
                    .HasComment("指标名称")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Zbmeta)
                    .HasColumnType("text")
                    .HasColumnName("zbmeta")
                    .HasComment("指标元数据")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Zbsf)
                    .HasMaxLength(4000)
                    .HasColumnName("zbsf")
                    .HasComment("指标算法")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Zbsm)
                    .HasMaxLength(4000)
                    .HasColumnName("zbsm")
                    .HasComment("指标说明")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Zbzt)
                    .HasMaxLength(200)
                    .HasColumnName("zbzt")
                    .HasComment("指标主题")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<TjZdyzbdybC>(entity =>
            {
                entity.HasKey(e => e.Csid)
                    .HasName("PRIMARY");

                entity.ToTable("tj_zdyzbdyb_cs");

                entity.HasComment("自定义指标定义表_参数值");

                entity.Property(e => e.Csid)
                    .HasColumnType("bigint(20)")
                    .ValueGeneratedNever()
                    .HasColumnName("csid")
                    .HasComment("参数ID");

                entity.Property(e => e.Cs)
                    .HasMaxLength(1000)
                    .HasColumnName("cs")
                    .HasComment("参数存放已经保存的条件值");

                entity.Property(e => e.Dwid)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("dwid")
                    .HasComment("单位ID");

                entity.Property(e => e.Querysql)
                    .HasMaxLength(4000)
                    .HasColumnName("querysql")
                    .HasComment("按照参数值生成的自定义指标SQL");

                entity.Property(e => e.Zbid)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("zbid")
                    .HasComment("自定义指标ID");
            });

            modelBuilder.Entity<XtSystemlog>(entity =>
            {
                entity.ToTable("xt_systemlog");

                entity.HasComment("系统日志");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("id")
                    .HasComment("ID");

                entity.Property(e => e.Czsj)
                    .HasColumnType("datetime")
                    .HasColumnName("czsj")
                    .HasComment("日志记录时间");

                entity.Property(e => e.Logtext)
                    .HasMaxLength(4000)
                    .HasColumnName("logtext")
                    .HasComment("日志内容");

                entity.Property(e => e.Logtype)
                    .HasMaxLength(20)
                    .HasColumnName("logtype")
                    .HasComment("类型　　INFO:信息　ERROR:错误");

                entity.Property(e => e.Mark)
                    .HasMaxLength(50)
                    .HasColumnName("mark")
                    .HasComment("备注其他");
            });

            modelBuilder.Entity<XtUserlog>(entity =>
            {
                entity.ToTable("xt_userlog");

                entity.HasComment("用户操作日志");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("id")
                    .HasComment("主键");

                entity.Property(e => e.Czlx)
                    .HasMaxLength(100)
                    .HasColumnName("czlx")
                    .HasComment("操作类型");

                entity.Property(e => e.Czsj)
                    .HasColumnType("datetime")
                    .HasColumnName("czsj")
                    .HasComment("操作时间");

                entity.Property(e => e.Czxxnr)
                    .HasMaxLength(2000)
                    .HasColumnName("czxxnr")
                    .HasComment("操作详细内容");

                entity.Property(e => e.Fromhost)
                    .HasMaxLength(200)
                    .HasColumnName("fromhost")
                    .HasComment("使用主机名");

                entity.Property(e => e.Fromip)
                    .HasMaxLength(50)
                    .HasColumnName("fromip")
                    .HasComment("操作来自IP地址");

                entity.Property(e => e.Gwid)
                    .HasMaxLength(50)
                    .HasColumnName("gwid")
                    .HasComment("岗位id");

                entity.Property(e => e.Resulttype)
                    .HasColumnType("int(11)")
                    .HasColumnName("resulttype")
                    .HasComment("操作结果 0.未知 1.成功 2.失败");

                entity.Property(e => e.Systemid)
                    .HasMaxLength(50)
                    .HasColumnName("systemid")
                    .HasComment("客户端系统ID");

                entity.Property(e => e.Yhid)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("yhid")
                    .HasComment("操作用户ID");
            });

            modelBuilder.Entity<XtUsertoken>(entity =>
            {
                entity.ToTable("xt_usertoken");

                entity.HasComment("用户TOKEN验证表");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .HasColumnName("id");

                entity.Property(e => e.Dwdm)
                    .HasMaxLength(100)
                    .HasColumnName("dwdm")
                    .HasComment("单位代码");

                entity.Property(e => e.Token)
                    .HasMaxLength(1000)
                    .HasColumnName("token")
                    .HasComment("TOKEN");

                entity.Property(e => e.Updatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("updatetime")
                    .HasComment("更新时间");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username")
                    .HasComment("用户名");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

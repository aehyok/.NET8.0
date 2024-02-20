## 已经使用的技术
- 依赖注入和控制反转
  - https://furion.net/docs/dependency-injection
  - https://www.cnblogs.com/laozhang-is-phi/p/9541414.html
    - 
## 待处理的问题

- 上传视频以及上传视频的缩略图生成方案
- 上传数据和下载数据  以及Excel文档等的处理
- 手机号登录 微信扫码登录
- 模版文件的处理
- 上传所有文件的查看预览(√)
- 任务调度表的维护(√)列表已有、 修改表达式和启用禁用待完善
- 数据初始化的维护
- 系统后端服务的维护
- 系统前端的部署以及小程序的发布部署 ()
- 系统redis所有缓存的查看维护 （Keys * 来获取数据）
- 登录日志和操作日志的查看(已有接口)

## .net 8
```
- 1、初始化了EntityFrameworkCore
- 2、初始化了消息队列RabbitMQ
- 3、初始化了持久化缓存Redis
- 4、初始化了记录日志Serilog
- 5、初始化了接口文档Swagger
```

## 服务器上所使用的外部套件
```
- 1、docker和docker-compose（✓）
- 2、zabbix docker 分布式监控系统，监控服务器的CPU、内存、硬盘、网络等使用情况
- 3、Serilog docker 日志记录系统（✓）
    - 可通过端口12000进行访问 
    - 用户名和密码 admin sunlight2023 
- 4、goploy CI CD 部署系统（✓）
- 5、redis 可使用docker部署（✓）
- 6、rabbitmq 可使用docker部署（✓）
- 7、mysql 可使用docker部署（✓）
- 8、nginx 可使用docker部署（✓）
- 9、net8.0 可使用docker部署（✓）
- 10、SkyWalking 监测  链路追踪（×）  
    - https://www.cnblogs.com/wei325/p/16412775.html
```


##  操作日志记录通过后端中间件实现OperationLogActionFilter
```
    操作日志通过Action上标注特性
    [OperationLogAction("修改菜单，菜单Id为:{id}，菜单Code为{model.Code}")]
    同时需要前端传递menuCode，如果不传递menuCode则不进行记录操作日志的行为
```

##  ApiAsyncExceptionFilter

## EFCore 开发环境更新数据库
```
开发环境下在sun.SystemService项目下

dotnet-ef migrations add InitTask -c DvsContext --framework net8.0 -v
   
dotnet-ef database update -c DvsContext --framework net8.0 -v

执行完记得将代码进行push，以免跟其他开发人员的更新数据库冲突

生产环境待确认生成脚本进行执行（？？？）https://www.cnblogs.com/chenxinblogs/p/15208251.html
```

## EFCore新增更新删除时的操作
```
新增时会通过拦截器DvsSaveChangeInterceptor拦截器，将CreateBy CreateAt UpdateBy UpdateAt写入到数据库中
修改时，只会将UpdateBy UpdateAt写入到数据库中
删除的时候，会在修改的基础上将IsDeleted置为true

查询数据的时候统一过滤IsDeleted为false的数据（modelBuilder.ApplyGlobalFilterAsDeleted）
```

## EFCore查询优化操作
- 通过LinqKit(https://github.com/scottksmith95/LINQKit) 进行优化查询条件表达式


- 通过Specification(https://github.com/ardalis/Specification) 进行查询表达式的优化
```
    // 关联查询
    if (model.AuthStatus == 1)
        spec.Query.Where(a => villagePopulationService.GetDbContext.Set<User>().Any(c => c.PopulationId == a.PopulationId));
    else if (model.AuthStatus == 2)
        spec.Query.Where(a => !villagePopulationService.GetDbContext.Set<User>().Any(c => c.PopulationId == a.PopulationId));
```



- **2、接口安全性**
  ```
   Menu 菜单定义表（手动维护）
   
   ApiResource 接口列表（相当于通过反射将所有控制器和方法都提取出来进行保存）
   
   MenuResource 菜单和接口关联表
   
   每个操作
  ```

- **3、操作日志**
  ```
  https://www.cnblogs.com/hooray/archive/2012/09/05/2672133.html
  // 在insert update delete 时同时写入操作日志
  // 根据映射获取操作的表名也就是业务表 即可写入什么操作

  // 通过action方法上添加Attribute进行日志声明
  // 类似于EFCore封装了统一的事务（TransactionAsyncInterceptor）
  https://juejin.cn/post/7009116644031070244?searchId=2023121513462817BCA1D45D34D690B014

  ```
 - **4、用户名密码登录
 ```
 首先校验验证码
 根据用户名查找用户信息
 如果为空看看用户名是否为手机号，然后再根据手机号查找用户信息
 如果还是为空，则提示账号或密码错误
 如果不为空，判断PasswordSalt是否为空
 为空则提示密码没有设置
 有密码的话，则开始校验密码(密码混淆)

 判断用户是否启用，没启用则报错
 更新最后登录时间

 然后获取一堆信息
 写入usertoken
 并添加用户信息 角色信息等写入缓存

 ```

## RabbitMQ 调试
- 暂时开两个解决方案调试
- sun.NET8: 一个是为了打开 sun.Basic.Api 发布消息的或者其他API服务
- sun.NET8.SystemService: 另外一个为了打开sun.SystemService 开启订阅消息


## redis 单独封装保持与指令一致的操作

- sun.Redis项目中封装了RedisService类，保持与redis指令一致的操作
- 使用时只需要在构造函数进行注入（IRedisService redisService）
- 然后就可以使用了
```
    // 构造函数注入服务
    public class RedisController(IRedisService redisService) 

    // 获取key数据
    redisService.GetAsync<T>(key);

    // 设置key数据
    redisService.SetAsync<T>(key, value);
```

## 使用的开源库

```
- DDD 
    - https://github.com/ntxinh/AspNetCore-DDD
- EFCore查询 
    - https://github.com/ardalis/Specification
    - https://specification.ardalis.com/getting-started
- EFCore QueryRepository RepositoryBase
    - https://github.com/TanvirArjel/EFCore.GenericRepository
- EFCore批量操作
 - Z.EntityFramework.Extensions.EFCore不开源
 - https://entityframework-extensions.net
 - https://github.com/zzzprojects/EntityFramework-Extensions
- PaginatedList 数据分页
    - https://github.com/dncuug/X.PagedList    
- LinqKit 表达式查询
    - https://github.com/scottksmith95/LINQKit
- AutoMapper dto映射
    - https://automapper.org
    - https://github.com/AutoMapper/AutoMapper    
- RabbitMQ 消息队列
  - 可使用docker搭建环境
  - https://github.com/rabbitmq/rabbitmq-dotnet-client
- Redis持久化缓存
  - redis官网 https://redis.io/docs/
  - 中文官网 https://www.redis.net.cn/order/3552.html

  - 可使用docker搭建环境
  - https://github.com/dotnetcore/EasyCaching
  - https://easycaching.readthedocs.io/en/latest/
  - https://github.com/StackExchange/StackExchange.Redis
  - https://stackexchange.github.io/StackExchange.Redis/
  - https://github.com/2881099/csredis
- 记录日志
  - 可使用docker搭建环境
  - https://github.com/serilog/serilog
- 定时任务表达式
  - Cronos
  - https://github.com/HangfireIO/Cronos
  - 可参考 https://github.com/HangfireIO/Hangfire
- Excel 操作
  - https://github.com/EPPlusSoftware/EPPlus
  - https://github.com/nissl-lab/npoi
- 二维码
  - https://github.com/manuelbl/QrCodeGenerator
  - https://github.com/mono/SkiaSharp
- SSE
  - https://github.com/tpeczek/Lib.AspNetCore.ServerSentEvents
- 请求接口
  - https://github.com/tmenier/Flurl
- 编辑图像中的exif数据
  - https://github.com/oozcitak/exiflibrary
- itext
  - https://github.com/itext/itext7-dotnet
- 工作流
  - https://github.com/elsa-workflows/elsa-core 
- swagger 
  - https://github.com/domaindrivendev/Swashbuckle.AspNetCore
```

- 待对接的第三方组件库
```
- 发送邮件 https://github.com/jstedfast/MimeKit
```



## 前端

- 前端UI https://github.com/tusen-ai/naive-ui
- 微前端 https://github.com/Tencent/wujie

## swagger

开启sun.SystemService项目，然后访问http://localhost:12000/swagger/index.html
然后在配置文件中配置了Endpoints数组，每个数组中的节点配置开启的微服务，
然后就可以通过http://localhost:12000/swagger/index.html访问到其他微服务的接口文档了

以及在swagger中配置了接口的安全性，需要在请求头中添加token才能访问接口，接口也可以设置无需token访问

## RabbitMQ 
- https://www.cnblogs.com/ysocean/p/9251884.html#_label2
## Serilog日志

## 初始化数据考量

会在sun.SystemService系统初始化的时候，进行种子数据的初始化，并将种子数据任务写入到数据库（SeedDataTask）方便后台查看
种子数据初始化的时候可以设置执行顺序，通过设置Order进行排序，越小的Order值越先执行

同时可以在后台进行设置是否启用，如果不启用则不会执行

几种类型
- 1、一次性数据更新（只会执行一次后续将不会更新）
    - 区域初始化根节点
    - 角色初始化（运维管理员角色）系统角色（已处理不允许删除和编辑）
    - 用户初始化（运维管理员）root用户不会出现在用户列表进行了过滤
- 2、每次更新版本时的数据更新
    - 菜单需要每次更新程序要先初始化
    - 然后运维角色的权限将所有菜单写入
- 3、根据需要全量拷贝的初始化数据待处理（???）

## token无感刷新

- 比如用户名和密码登录后，会生成一个token，有效期为两个小时，同时会生成一个RefreshToken，有效期为一个月
- 每次请求接口时，都会带上token，如果token过期了，就会使用RefreshToken去刷新token，如果RefreshToken也过期了，那么就需要重新登录了

这算是无感刷新token的一个比较好的方案了

## 定时任务
- 使用.net内置 BackgroundService后台异步执行任务
- 使用Cronos表达式进行定时任务的配置，可支持到秒级执行
- 封装统一处理定时任务基类CronScheduleService，会在sun.SystemService系统服务开启后将服务本身同步到Mysql和Redis（ScheduleTask)
- 会对定时任务的执行过程进行记录，记录到数据库中(ScheduleTaskRecord) 记录开始执行时间，结束执行时间，执行是否成功，以及表达式的转换时间等
- 通过后台可以对定时任务进行启用禁用，修改表达式等操作




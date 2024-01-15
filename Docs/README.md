## 待处理的问题
- 上传视频的缩略图生成方案

## .net 8
```
- 1、初始化了EntityFrameworkCore
- 2、初始化了定时任务CronTask
- 3、初始化了消息队列RabbitMQ
- 4、初始化了持久化缓存Redis
- 5、初始化了记录日志Serilog
- 6、初始化了接口文档Swagger
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
```

## 完成的中间件
```
- 1、操作日志通过Action上标注特性
    [OperationLogAction("修改菜单，菜单Id为:{id}，菜单Code为{model.Code}")]
```

- **1、EFCore更新数据库**
```
在aehyok.Schedules项目下

dotnet-ef migrations add InitTask -c DvsContext --framework net8.0 -v
   
dotnet-ef database update -c DvsContext --framework net8.0 -v
```
-**1.1、EFCore 新增更新删除时的操作**
```
新增时会通过拦截器DvsSaveChangeInterceptor拦截器，将CreateBy CreateAt UpdateBy UpdateAt写入到数据库中
修改时，只会将UpdateBy UpdateAt写入到数据库中
删除的时候，会在修改的基础上将IsDeleted置为true
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



## UI

- https://github.com/tusen-ai/naive-ui


## 初始化数据考量

- 1、一次性数据更新（只会执行一次后续将不会更新）
    - 区域初始化根节点
    - 角色初始化（运维管理员角色）
    - 用户初始化（运维管理员）
- 2、每次更新版本时的数据更新
    - 菜单需要每次更新程序要先初始化
    - 然后运维角色的权限将所有菜单写入




## 从登录开始说起

- 比如用户名和密码登录后，会生成一个token，有效期为两个小时，同时会生成一个RefreshToken，有效期为一个月
- 每次请求接口时，都会带上token，如果token过期了，就会使用RefreshToken去刷新token，如果RefreshToken也过期了，就需要重新登录

## 登录成功后获取用户信息


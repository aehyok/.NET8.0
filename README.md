## .net 8

- **1、EFCore更新数据库**
  ```
   在aehyok.Schedules项目下

   dotnet-ef migrations add InitTask -c DvsContext --framework net8.0 -v
   
   dotnet-ef database update -c DvsContext --framework net8.0 -v
  ```

- **2、接口安全性**
  ```
  IActionDescriptorCollectionProvider 
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
```
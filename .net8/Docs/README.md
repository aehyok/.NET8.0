## .net 8

- **1��EFCore�������ݿ�**
  ```
   ��aehyok.Schedules��Ŀ��

   dotnet-ef migrations add InitTask -c DvsContext --framework net8.0 -v
   
   dotnet-ef database update -c DvsContext --framework net8.0 -v
  ```

- **2���ӿڰ�ȫ��**
  ```
  IActionDescriptorCollectionProvider 
  ```

- **3��������־**
  ```
  https://www.cnblogs.com/hooray/archive/2012/09/05/2672133.html
  // ��insert update delete ʱͬʱд�������־
  // ����ӳ���ȡ�����ı���Ҳ����ҵ��� ����д��ʲô����

  // ͨ��action���������Attribute������־����
  // ������EFCore��װ��ͳһ������TransactionAsyncInterceptor��
  https://juejin.cn/post/7009116644031070244?searchId=2023121513462817BCA1D45D34D690B014

  ```
## ʹ�õĿ�Դ��

```
- DDD 
    - https://github.com/ntxinh/AspNetCore-DDD
- EFCore��ѯ 
    - https://github.com/ardalis/Specification
    - https://specification.ardalis.com/getting-started
- EFCore QueryRepository RepositoryBase
    - https://github.com/TanvirArjel/EFCore.GenericRepository
- EFCore��������
 - Z.EntityFramework.Extensions.EFCore����Դ
 - https://entityframework-extensions.net
 - https://github.com/zzzprojects/EntityFramework-Extensions
- PaginatedList ���ݷ�ҳ
    - https://github.com/dncuug/X.PagedList    
- LinqKit ���ʽ��ѯ
    - https://github.com/scottksmith95/LINQKit
- AutoMapper dtoӳ��
    - https://automapper.org
    - https://github.com/AutoMapper/AutoMapper    
- RabbitMQ ��Ϣ����
  - ��ʹ��docker�����
  - https://github.com/rabbitmq/rabbitmq-dotnet-client
- Redis�־û�����
  - redis���� https://redis.io/docs/
  - ���Ĺ��� https://www.redis.net.cn/order/3552.html

  - ��ʹ��docker�����
  - https://github.com/dotnetcore/EasyCaching
  - https://easycaching.readthedocs.io/en/latest/
  - https://github.com/StackExchange/StackExchange.Redis
  - https://stackexchange.github.io/StackExchange.Redis/
  - https://github.com/2881099/csredis
- ��¼��־
  - ��ʹ��docker�����
  - https://github.com/serilog/serilog
- ��ʱ������ʽ
  - Cronos
  - https://github.com/HangfireIO/Cronos
  - �ɲο� https://github.com/HangfireIO/Hangfire
- Excel ����
  - https://github.com/EPPlusSoftware/EPPlus
  - https://github.com/nissl-lab/npoi
- ��ά��
  - https://github.com/manuelbl/QrCodeGenerator
  - https://github.com/mono/SkiaSharp
- SSE
  - https://github.com/tpeczek/Lib.AspNetCore.ServerSentEvents
- ����ӿ�
  - https://github.com/tmenier/Flurl
- �༭ͼ���е�exif����
  - https://github.com/oozcitak/exiflibrary
- itext
  - https://github.com/itext/itext7-dotnet
- ������
  - https://github.com/elsa-workflows/elsa-core 
- swagger 
  - https://github.com/domaindrivendev/Swashbuckle.AspNetCore
```



## UI

- https://github.com/tusen-ai/naive-ui
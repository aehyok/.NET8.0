using sun.Core;var moduleKey = "sun-ncdp";
var moduleTitle = "无代码开放平台";

var builder = WebApplication.CreateBuilder(args);

await builder.InitAppliation(moduleKey, moduleTitle);

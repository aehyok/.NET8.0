using sun.Core;

var moduleKey = "sun-basic";
var moduleTitle = "基础服务";

var builder = WebApplication.CreateBuilder(args);

await builder.InitAppliation(moduleKey, moduleTitle);

using aehyok.Core;

var moduleKey = "aehyok-basic";
var moduleTitle = "��������";

var builder = WebApplication.CreateBuilder(args);

await builder.InitAppliation(moduleKey, moduleTitle);

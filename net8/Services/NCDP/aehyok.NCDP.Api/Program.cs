using aehyok.Core;

var moduleKey = "aehyok-ncdp";
var moduleTitle = "�޴��뿪��ƽ̨";

var builder = WebApplication.CreateBuilder(args);

await builder.InitAppliation(moduleKey, moduleTitle);

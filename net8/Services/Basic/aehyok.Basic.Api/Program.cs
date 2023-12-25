using aehyok.Core;

var moduleKey = "aehyok-basic";
var moduleTitle = "»ù´¡·þÎñ";

var builder = WebApplication.CreateBuilder(args);

await builder.InitAppliation(moduleKey, moduleTitle);

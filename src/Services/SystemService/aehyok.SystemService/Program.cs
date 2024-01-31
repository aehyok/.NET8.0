// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.Builder;
using aehyok.Core;

var moduleKey = "aehyok-systemservice";

var moduleTitle = "系统服务";

var builder = WebApplication.CreateBuilder(args);

await builder.InitAppliation(moduleKey, moduleTitle, true);

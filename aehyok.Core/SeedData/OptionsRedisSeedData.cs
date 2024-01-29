using aehyok.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.SeedData
{
    public class OptionsRedisSeedData : ISeedData
    {
        public int Order => 12;

        public string ConfigPath { get; set; } = null;

        public Task ApplyAsync(SeedDataTask model)
        {
            // TODO 初始化配置参数到Options表，并同步到Redis
            Console.WriteLine("初始化配置参数到Options表，并同步到Redis");
            return Task.CompletedTask;
        }
    }
}

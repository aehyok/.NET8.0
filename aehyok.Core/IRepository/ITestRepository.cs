using aehyok.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Core.IRepository
{
    public interface ITestRepository: IDependency
    {
        /// <summary>
        ///  获取测试数据
        /// </summary>
        /// <returns></returns>
        string GetTest();
    }
}

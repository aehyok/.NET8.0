using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Core.Repository
{
    public interface ITestRepository
    {
        bool CheckLogin(string userName, string password);
    }
}

using aehyok.Core.Data;
using aehyok.Core.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.IRepository
{
    public interface IFormRepository
    {
        OperationResult SaveEntity(MD_InputEntity inputEntity, TransmitUserInfo sinoRequestUser);
    }
}

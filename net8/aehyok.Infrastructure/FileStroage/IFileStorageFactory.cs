using aehyok.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Infrastructure.FileStroage
{
    public interface IFileStorageFactory
    {
        IFileStorage GetStorage(FileStorageType storageType);

        IFileStorage GetStorage();
    }
}

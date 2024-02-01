using sun.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Infrastructure.FileStroage
{
    public interface IFileStorageFactory
    {
        IFileStorage GetStorage(FileStorageType storageType);

        IFileStorage GetStorage();
    }
}

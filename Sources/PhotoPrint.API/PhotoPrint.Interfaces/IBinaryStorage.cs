using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPT.Interfaces
{
    public interface IBinaryStorage : IInitializable
    {
        string Upload(string blobName, Stream data);

        bool Delete(string blobName);
    }
}

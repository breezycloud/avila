using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvilaNaturalle.Shared.Util;

public interface IConverter
{
    public ValueTask<byte[]> GenerateQR(Guid id);
}

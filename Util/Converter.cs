using QRCoder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AvilaNaturalle.Shared.Util;

public class Converter : IConverter
{
    public async ValueTask<byte[]> GenerateQR(Guid id)
    {
        var payload = Convert.ToBase64String(id.ToByteArray());
        QRCodeGenerator qr = new();
        QRCodeData codeData = qr.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
        PngByteQRCode qrCode = new(codeData);
        byte[] qrCodeAsBitmapByteArr = qrCode.GetGraphic(50);
        await Task.Delay(0);
        using var ms = new MemoryStream(qrCodeAsBitmapByteArr);
        return ms.ToArray();
    }
}

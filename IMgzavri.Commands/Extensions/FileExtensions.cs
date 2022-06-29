using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Commands.Extensions
{
    public static class FileExtensions
    {
        public static byte[] ByteArrayFromBase64(this string base64)
        {
            if (string.IsNullOrWhiteSpace(base64))
                throw new ArgumentNullException(nameof(base64));

            return Convert.FromBase64String(base64);
        }
    }
}

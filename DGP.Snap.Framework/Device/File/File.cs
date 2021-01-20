using System.IO;
using System.Threading.Tasks;

namespace DGP.Snap.Framework.Device.File
{
    internal class File
    {
        private readonly string path;
        public async Task<string> ReadToEndAsync()
        {
            string line;
            using (StreamReader sr = new StreamReader(this.path))
            {
                line = await sr.ReadToEndAsync();
            }
            return line;
        }

        public async Task WriteAsync(string str)
        {
            using (StreamWriter sw = new StreamWriter(this.path))
            {               
                await sw.WriteAsync(str);
            }
        }
    }
}

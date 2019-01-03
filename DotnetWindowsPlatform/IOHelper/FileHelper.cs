using System.IO;
using System.Text;

namespace DotnetWindowsPlatform.IOHelper
{
    public static class FileHelper
    {
        public static void WriteText(string saveTextDirectory, string saveTextName, string contents)
        {
            if (!Directory.Exists(saveTextDirectory))
            {
                Directory.CreateDirectory(saveTextDirectory);
            }

            var finalPath = Path.Combine(saveTextDirectory, saveTextName);

            using (var fileStream = new FileStream(finalPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                var bytes = Encoding.Default.GetBytes(contents);

                fileStream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}

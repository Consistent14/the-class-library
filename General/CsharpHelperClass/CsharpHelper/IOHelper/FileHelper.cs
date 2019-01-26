using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpHelper.IOHelper
{
    /// <summary>
    /// 1.文件操作帮助类
    /// 2.2019.1.26创建
    /// </summary>
    static public class FileHelper
    {
        /// <summary>
        /// 写入内容到磁盘指定路径下的txt文件内
        /// </summary>
        /// <param name="txtPath"></param>
        /// <param name="contents"></param>
        public static void WriteToTxt(this string txtPath, string contents)
        {
            using (var fileStream = new FileStream(txtPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite
                , int.MaxValue, FileOptions.Asynchronous))
            {
                var bytes = Encoding.Default.GetBytes(contents);

                fileStream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}

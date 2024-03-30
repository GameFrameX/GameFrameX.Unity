using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public static class StringExtension
{
    /// <summary>
    /// 快速比较两个字符串内容是否一致
    /// </summary>
    /// <param name="self">当前字符串</param>
    /// <param name="target">对比的目标字符串</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">当前对象为空</exception>
    public static bool EqualsFast(this string self, string target)
    {
        if (self == null)
        {
            return target == null;
        }

        if (target == null)
        {
            return false;
        }

        if (self.Length != target.Length)
        {
            return false;
        }

        int ap = self.Length - 1;
        int bp = target.Length - 1;

        while (ap >= 0 && bp >= 0 && self[ap] == target[bp])
        {
            ap--;
            bp--;
        }

        return (bp < 0);
    }

    /// <summary>
    /// 判断字符串是否以目标字符串结尾
    /// </summary>
    /// <param name="self"></param>
    /// <param name="target">目标字符串</param>
    /// <returns></returns>
    public static bool EndsWithFast(this string self, string target)
    {
        int ap = self.Length - 1;
        int bp = target.Length - 1;

        while (ap >= 0 && bp >= 0 && self[ap] == target[bp])
        {
            ap--;
            bp--;
        }

        return (bp < 0);
    }

    /// <summary>
    /// 判断字符串是否以目标字符串开始
    /// </summary>
    /// <param name="self"></param>
    /// <param name="target">目标字符串</param>
    /// <returns></returns>
    public static bool StartsWithFast(this string self, string target)
    {
        int aLen = self.Length;
        int bLen = target.Length;

        int ap = 0;
        int bp = 0;

        while (ap < aLen && bp < bLen && self[ap] == target[bp])
        {
            ap++;
            bp++;
        }

        return (bp == bLen);
    }

    /// <summary>
    /// 字符串转字符数组
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static IEnumerable<byte> ToBytes(this string self)
    {
        byte[] byteArray = Encoding.Default.GetBytes(self);
        return byteArray;
    }

    /// <summary>
    /// 字符串转字符数组
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static byte[] ToByteArray(this string self)
    {
        byte[] byteArray = Encoding.Default.GetBytes(self);
        return byteArray;
    }

    /// <summary>
    /// 字符串转UTF8字符数组
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static byte[] ToUtf8(this string self)
    {
        byte[] byteArray = Encoding.UTF8.GetBytes(self);
        return byteArray;
    }

    /// <summary>
    /// 16进制字符串转字节数组
    /// </summary>
    /// <param name="hexString">字符串</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">字符串字符数不是偶数引发异常</exception>
    public static byte[] HexToBytes(this string hexString)
    {
        if (hexString.Length % 2 != 0)
        {
            throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}",
                hexString));
        }

        var hexAsBytes = new byte[hexString.Length / 2];
        for (int index = 0; index < hexAsBytes.Length; index++)
        {
            string byteValue = "";
            byteValue += hexString[index * 2];
            byteValue += hexString[index * 2 + 1];
            hexAsBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        }

        return hexAsBytes;
    }

    /// <summary>
    /// 指定的字符串是 null、空还是仅由空白字符组成。
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static bool IsNullOrWhiteSpace(this string self)
    {
        const string nullString = "null";
        return self.EqualsFast(nullString) || string.IsNullOrWhiteSpace(self);
    }

    /// <summary>
    /// 指定的字符串是 null 还是 Empty 字符串。
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty(this string self)
    {
        return string.IsNullOrEmpty(self);
    }

    /// <summary>
    /// 指定的字符串[不]是 null、空还是仅由空白字符组成。
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static bool IsNotNullOrWhiteSpace(this string self)
    {
        return !self.IsNullOrWhiteSpace();
    }

    /// <summary>
    /// 指定的字符串[不]是 null 还是 Empty 字符串。
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static bool IsNotNullOrEmpty(this string self)
    {
        return !self.IsNullOrEmpty();
    }

    /// <summary>
    /// 格式化
    /// </summary>
    /// <param name="text"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static string Format(this string text, params object[] args)
    {
        return string.Format(text, args);
    }

    /// <summary>
    /// 将[\n、\t、\r、空格]替换为空,并返回
    /// </summary>
    /// <param name="self">原始字符串</param>
    /// <returns></returns>
    public static string TrimEmpty(this string self)
    {
        self = self.Replace("\n", string.Empty).Replace(" ", string.Empty).Replace("\t", string.Empty).Replace("\r", string.Empty);
        return self;
    }


    /// <summary>
    /// 匹配中文正则表达式
    /// </summary>
    private static readonly Regex CnReg = new Regex(@"[\u4e00-\u9fa5]");

    /// <summary>
    /// 替换中文为空字符串
    /// </summary>
    /// <param name="self">原始字符串</param>
    /// <returns></returns>
    public static string TrimZhCn(this string self)
    {
        self = CnReg.Replace(self, string.Empty);
        return self;
    }

    public static int[] SplitToIntArray(this string str, char sep = '+')
    {
        if (string.IsNullOrEmpty(str))
            return Array.Empty<int>();

        var arr = str.Split(sep);
        int[] ret = new int[arr.Length];
        for (int i = 0; i < arr.Length; ++i)
        {
            if (int.TryParse(arr[i], out var t))
                ret[i] = t;
        }

        return ret;
    }

    public static int[][] SplitTo2IntArray(this string str, char sep1 = ';', char sep2 = '+')
    {
        if (string.IsNullOrEmpty(str))
            return Array.Empty<int[]>();

        var arr = str.Split(sep1);
        if (arr.Length <= 0)
            return Array.Empty<int[]>();

        int[][] ret = new int[arr.Length][];

        for (int i = 0; i < arr.Length; ++i)
            ret[i] = arr[i].SplitToIntArray(sep2);
        return ret;
    }

    /// <summary>
    /// 根据字符串创建目录,递归
    /// </summary>
    public static void CreateAsDirectory(this string path, bool isFile = false)
    {
        if (isFile)
            path = Path.GetDirectoryName(path);
        if (!Directory.Exists(path))
        {
            CreateAsDirectory(path, true);
            Directory.CreateDirectory(path);
        }
    }

    /// <summary>
    /// 从指定字符串中的指定位置处开始读取一行。
    /// </summary>
    /// <param name="rawString">指定的字符串。</param>
    /// <param name="position">从指定位置处开始读取一行，读取后将返回下一行开始的位置。</param>
    /// <returns>读取的一行字符串。</returns>
    public static string ReadLine(this string rawString, ref int position)
    {
        if (position < 0)
        {
            return null;
        }

        int length = rawString.Length;
        int offset = position;
        while (offset < length)
        {
            char ch = rawString[offset];
            switch (ch)
            {
                case '\r':
                case '\n':
                    if (offset > position)
                    {
                        string line = rawString.Substring(position, offset - position);
                        position = offset + 1;
                        if ((ch == '\r') && (position < length) && (rawString[position] == '\n'))
                        {
                            position++;
                        }

                        return line;
                    }

                    offset++;
                    position++;
                    break;

                default:
                    offset++;
                    break;
            }
        }

        if (offset > position)
        {
            string line = rawString.Substring(position, offset - position);
            position = offset;
            return line;
        }

        return null;
    }
}
using System;

namespace LAP.Common
{
    /// <summary>
    /// 常用库
    /// </summary>
    public class Tools
    {
        /// <summary>
        /// 获取Guid
        /// </summary>
        public static string NewGuid => Guid.NewGuid().ToString().ToLower();

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string TimeStamp() => new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds().ToString();

        /// <summary>
        /// 检测客户输入的字符串是否有效,并将原始字符串修改为有效字符串或空字符串。
        /// 当检测到客户的输入中有攻击性危险字符串,则返回false,有效返回true。
        /// </summary>
        /// <param name="input">要检测的字符串</param>
        public static bool IsValidInput(string input)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    //如果是空值,则跳出
                    return true;
                }

                //替换单引号
                input = input.Replace("'", "''").Trim();

                //检测攻击性危险字符串
                string testString = "exec|exec+|insert|insert+|delete|delete+|update|update+|chr|chr+|master+|truncate|truncate+|declare|drop+|drop+table|creat+|iframe|script";
                string[] testArray = testString.Split('|');
                foreach (var testStr in testArray)
                {
                    if (input.ToLower().IndexOf(testStr, StringComparison.Ordinal) == -1) continue;
                    //检测到攻击字符串,清空传入的值
                    return false;
                }

                //未检测到攻击字符串
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Experience.Framework
{
    /// <summary>
    /// 密码强度
    /// </summary>
    public enum StrengthState
    {
        //初始值
        Initial = 0,
        //最差状态
        Worst = 1,
        //中等的
        Moderate = 2,
        //强
        Strong = 3,
        //极佳
        Excellent = 4
    }


    /// <summary>
    /// 测试密码强度工具类
    /// 请参考http://www.refly.net/passwordchecker/
    /// </summary>
    public class PasswordLevelKit
    {
        #region == Regex ==

        // 只有数字
        private static Regex rDigitOnly = new Regex(@"^\d{1,}$");
        // 只有字母
        private static Regex rCharOnly = new Regex(@"^[a-zA-Z]{1,}$");
        // 只有特殊字符
        private static Regex rSpecialCharOnly =
            new Regex(@"^[~`!@#\$%\^\&\*\(\)\-_\+=\[\]\{\}\|\\:;""'<,>\.\?/\s]{1,}$");

        // 有数字和字母
        private static Regex rDigitAndChar = new Regex(@"^[A-Za-z0-9]+$");
        // 有数字和特殊字符
        private static Regex rDigitAndSpecialChar =
            new Regex(@"^[0-9~`!@#\$%\^\&\*\(\)\-_\+=\[\]\{\}\|\\:;""'<,>\.\?/\s]+$");
        // 有字母和特殊字符
        private static Regex rCharAndSpecialChar =
            new Regex(@"^[A-Za-z~`!@#\$%\^\&\*\(\)\-_\+=\[\]\{\}\|\\:;""'<,>\.\?/\s]+$");

        // 数字、有字母和特殊字符
        private static Regex rDigitAndCharAndSpecialChar =
            new Regex(@"^[A-Za-z0-9~`!@#\$%\^\&\*\(\)\-_\+=\[\]\{\}\|\\:;""'<,>\.\?/\s]+$");

        private static int minPasswordLength = 6;
        #endregion == Regex ==
        /// <summary>
        /// 更改强度状态
        /// </summary>
        public static StrengthState ChangeStrengthState(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return StrengthState.Initial;
            }
            else if (password.Length < minPasswordLength)
            {
                return StrengthState.Worst;
            }
            else if (rDigitOnly.IsMatch(password) || rCharOnly.IsMatch(password) || rSpecialCharOnly.IsMatch(password))
            {
                return StrengthState.Moderate;
            }
            else if (rDigitAndChar.IsMatch(password) || rDigitAndSpecialChar.IsMatch(password) || rCharAndSpecialChar.IsMatch(password))
            {
                return StrengthState.Strong;
            }
            else if (rDigitAndCharAndSpecialChar.IsMatch(password))
            {
                return StrengthState.Excellent;
            }
            else
            {
                return StrengthState.Worst;
            }
        }
    }
}

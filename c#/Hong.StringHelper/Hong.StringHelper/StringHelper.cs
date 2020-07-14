﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hong.StringHelper
{
    public static class StringHelper
    {
        #region  因為 Substring 都會爆錯 => 所以加上 try catch ，就不用再寫那麼多行了
        /// <summary>
        ///  同 原本的 Substring ， 但是有加上 try catch => 會回傳 空
        /// </summary>
        /// <param name="source"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubString2(this string source, int startIndex, int length)
        {
            try
            {
                // 錯誤判斷，不要讓他 猛進到 try catch 脫慢速度
                if (string.IsNullOrEmpty(source)) { return string.Empty; }
                int len = source.Length;
                if (startIndex >= len) { return string.Empty; }
                if ((startIndex + length) > len) { return source.SubString2(startIndex); }

                source = string.IsNullOrEmpty(source) ? string.Empty : source;
                string result = source.Substring(startIndex, length);
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " --> " + ex.Message);

                return source.SubString2(startIndex);
            }
        }

        /// <summary>
        ///  同 原本的 Substring ， 但是有加上 try catch => 會回傳 空
        /// </summary>
        /// <param name="source"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static string SubString2(this string source, int startIndex)
        {
            try
            {
                // 錯誤判斷，不要讓他 猛進到 try catch 脫慢速度
                if (string.IsNullOrEmpty(source)) { return string.Empty; }
                if (startIndex >= source.Length) { return string.Empty; }

                source = string.IsNullOrEmpty(source) ? string.Empty : source;
                string result = source.Substring(startIndex);
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " --> " + ex.Message);
                return string.Empty;
            }
        }

        #endregion

        #region  重複字串

         //回傳重複的 "字元" 所組成的字串
        public static string Repeat(this char source, int repeatNum = 2)
        {
            repeatNum = repeatNum < 1 ? 1 : repeatNum;
            return new string(source, repeatNum);
        }

        //回傳重複的 "字串" 所組成的字串
        public static string Repeat(this string source, int repeatNum = 2)
        {
            if (string.IsNullOrEmpty(source)) { return string.Empty; }
            if (repeatNum <= 1)
            {
                return source;
            }

            var builder = new StringBuilder(repeatNum * source.Length);
            for (int i = 0; i < repeatNum; i++)
            {
                builder.Append(source);
            }
            return builder.ToString();
        }

        #endregion

        /// <summary>
        ///  維持固定的長度回傳
        ///  isRightAddSpace = 如果不足長度是否是向右補空白 (預設是 true, 如果是 false 就是像左補空白)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static string GetFixedLength(this string source, int stringLen, bool isRightAddSpace = true)
        {
            // 長度
            stringLen = stringLen <= 0 ? 0 : stringLen;

            // String.Format("{0, -3}", ) => 不足3，向右補空白
            // String.Format("{0, 3}", ) => 不足3，向左補空白
            string addSpace = isRightAddSpace ? "-" : "";
            return string.Format("{0, " + addSpace + stringLen.ToString() + "}", source.SubString2(0, stringLen));
        }

        #region 字串比大小，寫的比較直覺

        /// <summary>
        ///  字串 source 是否 比 matchStr 小
        /// </summary>
        /// <param name="source"></param>
        /// <param name="matchStr"></param>
        /// <returns></returns>
        public static bool SmallTo(this string source, string matchStr)
        {
            /*
              string.Compare(A, B) => 會得出數字
              < 0 => A < B
              = 0 => A = B
              > 0 => A > B
             */
            return string.Compare(source, matchStr) < 0;
        }

        /// <summary>
        ///  字串 source 是否 比 matchStr 大
        /// </summary>
        /// <param name="source"></param>
        /// <param name="matchStr"></param>
        /// <returns></returns>
        public static bool LargeTo(this string source, string matchStr)
        {
            return string.Compare(source, matchStr) > 0;
        }

        /// <summary>
        ///  把 用 字串 分割字串 寫成精簡的 (沒測過，所有可能有小問題，但主要的都正常)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="splitStr"></param>
        /// <returns></returns>
        public static string[] Split(this string source, string splitStr){
            if (string.IsNullOrEmpty(source)){return new string[];}
            splitStr = string.IsNullOrEmpty(splitStr) ? string.Empty : splitStr;
            return Regex.Split(source, splitStr, RegexOptions.IgnoreCase)
        }

        #endregion

        #region 相關 TryParse 平常要寫2行 => 把他精簡掉

        /// <summary>
        ///  把用 try parse 寫成2行的部份 轉成一行
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int IntTryParse(this char source)
        {
            int result = 0;

            try
            {
                result = Convert.ToInt32(source);
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return 0;
            }
        }

        /// <summary>
        ///  把用 try parse 寫成2行的部份 轉成一行
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int IntTryParse(this string source)
        {
            int result = 0;
            int.TryParse(source, out result);
            return result;
        }

        /// <summary>
        ///  把用 try parse 寫成2行的部份 轉成一行
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static decimal DecimalTryParse(this string source)
        {
            decimal result = 0;
            decimal.TryParse(source, out result);
            return result;
        }

        /// <summary>
        /// 轉成 Decimal ， 後面可以輸入有幾個小數位
        /// 例如： source = "12345", floatNum = 2 => 結果 = 123.45
        /// </summary>
        /// <param name="source"></param>
        /// <param name="floatNum"></param>
        /// <returns></returns>
        public static decimal DecimalTryParse(string source, int floatNum = 0)
        {
            decimal result = 0;
            if (string.IsNullOrEmpty(source)) { source = "0"; }
            floatNum = floatNum <= 0 ? 0 : floatNum;

            string before = string.Empty;
            string after = string.Empty;
            string haveFloat = (floatNum <= 0) ? "" : ".";  // 是否要出現小數點 (如果 float是 <= 0 的話)

            if (floatNum >= source.Length)
            {
                before = "0";

                int diff = floatNum - source.Length;
                for (var i = 1; i <= diff; i++)
                {
                    after += "0";
                }
                after += source;
            }
            else
            {
                int diff = source.Length - floatNum;
                before = source.SubString2(0, diff);
                after = source.SubString2(diff);
            }

            source = $"{before}{haveFloat}{after}";

            decimal.TryParse(source, out result);
            return result;
        }

        /// <summary>
        ///  把用 try parse 寫成2行的部份 轉成一行
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static double DoubleTryParse(this string source)
        {
            double result = 0;
            double.TryParse(source, out result);
            return result;
        }

        /// <summary>
        ///  這個 字串 是不是 數字型 (能用下面的 驗證全數字就行)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNumeric(string source)
        {
            if (string.IsNullOrEmpty(source)) { return false; }

            try
            {
                var i = Convert.ToDecimal(source);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " --> " + ex.Message);

                try
                {
                    var i = Convert.ToDouble(source);
                    return true;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " --> " + e.Message);

                    /*
                        原本在這邊有寫 ToInt32 => 但拿掉
                        原因： 連 Double 和 Decimal 都不行了 ，那 Int 絕對就不可能
                        不要再浪費時間 做 Convert (花的時間很久)
                     */

                    return false;
                }
            }
        }

        /// <summary>
        ///  無條件捨去至 小數點第幾位
        /// </summary>
        /// <param name="source"></param>
        /// <param name="floatNum"></param>
        /// <returns></returns>
        public static decimal RoundDown(string source, int floatNum = 0)
        {
            decimal result = 0;
            if (string.IsNullOrEmpty(source)) { source = "0"; }
            floatNum = floatNum <= 0 ? 0 : floatNum;

            if (source.Contains("."))
            {
                // 只取到第一個，後面不管
                List<string> data = source.Split('.').ToList();
                string item = data[0];

                if (floatNum > 0)
                {
                    // 先幫他補0 (免得其實他的長度不夠)
                    item += ".";
                    string item1 = data[1];
                    int len = item1.Length;
                    for (var i = len; i < floatNum; i++)
                    {
                        item1 += "0";
                    }

                    // 再用 Substring
                    item += item1.SubString2(0, floatNum);
                }

                result = DecimalTryParse(item);
            }
            else
            {
                if (floatNum > 0)
                {
                    source += ".";
                    for (var i = 0; i < floatNum; i++)
                    {
                        source += "0";
                    }
                }

                result = DecimalTryParse(source);
            }

            return result;
        }

        #endregion

        #region 相關 數字靠右前補0 + 數字靠左後補0 + 小數格式

        /// <summary>
        ///  像是 "0   " => "0000"
        ///  數字靠右補空白
        ///  特例： 如果是 "1234" ， len 給2 => 結果 "34"
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string NumericStringNumberMoveRight(string source, int len)
        {
            string result = string.Empty;
            len = len < 0 ? 0 : len;

            if (!string.IsNullOrEmpty(source))
            {
                result = source.Replace(" ", string.Empty);

                // 必要是全數字
                if (IsAllNumeric(result))
                {
                    int diff = len - result.Length;
                    if (diff <= 0)
                    {
                        // 拿後面的碼
                        return result.SubString2(result.Length - len , len);
                    }
                    else
                    {
                        string space = string.Empty;
                        for(var i = 0; i < diff; i++)
                        {
                            space += "0";
                        }

                        return space + result;
                    }
                }
            }

            return result;
        }

        /// <summary>
        ///  像是 "0   " => "0000"
        ///  數字靠右補空白
        ///  特例： 如果是 "1234" ， len 給2 => 結果 "34"
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string NumericStringNumberMoveRight(int source, int len)
        {
            string item = source.ToString();
            return NumericStringNumberMoveRight(item, len);
        }

        /// <summary>
        ///  像是 "   0" => "0000"
        ///  數字靠左補空白 (在小數點後的數用的)
        ///  特例： 如果是 "1234" ， len 給2 => 結果 "12"
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string NumericStringNumberMoveLeft(string source, int len)
        {
            string result = string.Empty;
            len = len < 0 ? 0 : len;

            if (!string.IsNullOrEmpty(source))
            {
                result = source.Replace(" ", string.Empty);

                // 必要是全數字
                if (IsAllNumeric(result))
                {
                    int diff = len - result.Length;
                    if (diff <= 0)
                    {
                        // 拿前面的碼
                        return result.SubString2(0, len);
                    }
                    else
                    {
                        string space = string.Empty;
                        for (var i = 0; i < diff; i++)
                        {
                            space += "0";
                        }

                        return result + space;
                    }
                }
            }

            return result;
        }

        /// <summary>
        ///  像是 "   0" => "0000"
        ///  數字靠左補空白 (在小數點後的數用的)
        ///  特例： 如果是 "1234" ， len 給2 => 結果 "12"
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string NumericStringNumberMoveLeft(int source, int len)
        {
            string item = source.ToString();
            return NumericStringNumberMoveLeft(item, len);
        }

        /// <summary>
        ///  將 deciaml 轉換為 格式 
        ///  例： YYY PIC 9(3)V9
        ///  給的值是 22
        ///  出來 的要是 0220
        ///  給的值是 12.33
        ///  出來的要是 012.3
        /// </summary>
        /// <param name="source"></param>
        /// <param name="numLen"></param>
        /// <param name="floatLen"></param>
        /// <returns></returns>
        public static string NumericStringDecimalFormat(string source, int numLen, int floatLen)
        {
            string result = string.Empty;
            numLen = numLen < 0 ? 0 : numLen;
            floatLen = floatLen < 0 ? 0 : floatLen;

            if (!string.IsNullOrEmpty(source))
            {
                // 因為要直接拿小數點，所以給他加個 . 就絕對不會爆錯了
                source += ".0";
                List<string> datas = source.Split('.').ToList();

                // 數字靠右 前補0，小數靠左 後補0
                result = NumericStringNumberMoveRight(datas[0], numLen) +
                    "." +
                    NumericStringNumberMoveLeft(datas[1], floatLen);
            }

            return result;
        }

        /// <summary>
        ///  將 deciaml 轉換為 格式 
        ///  例： YYY PIC 9(3)V9
        ///  給的值是 22
        ///  出來 的要是 0220
        ///  給的值是 12.33
        ///  出來的要是 012.3
        /// </summary>
        /// <param name="source"></param>
        /// <param name="numLen"></param>
        /// <param name="floatLen"></param>
        /// <returns></returns>
        public static string NumericStringDecimalFormat(decimal source, int numLen, int floatLen)
        {
            string item = source.ToString();
            return NumericStringDecimalFormat(item, numLen, floatLen);
        }

        #endregion

        #region 相關驗證(是否全數字、是否全英文、是否全英數字、是否全是中文) => 用 StringHelper.xxx() 的方式來用

        /// <summary>
        ///  是否是全數字
        /// </summary>
        public static bool IsAllNumeric(string source)
        {
            bool result = false;

            try
            {
                if (string.IsNullOrEmpty(source)) { return false; }
                source = source.Trim();
                result = Regex.IsMatch(source, @"^[0-9]+$");
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " --> " + ex.Message);
                return false;
            }
        }

        /// <summary>
        ///  是否是全英文
        /// </summary>
        public static bool IsAllEnglish(string source)
        {
            bool result = false;

            try
            {
                if (string.IsNullOrEmpty(source)) { return false; }
                source = source.Trim();
                result = Regex.IsMatch(source, @"^[a-zA-Z]+$");
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " --> " + ex.Message);
                return false;
            }
        }

        /// <summary>
        ///  是否是全英文或數字
        /// </summary>
        public static bool IsAllEnglishOrNumeric(string source)
        {
            bool result = false;

            try
            {
                if (string.IsNullOrEmpty(source)) { return false; }
                source = source.Trim();
                result = Regex.IsMatch(source, @"^[a-zA-Z0-9]+$");
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " --> " + ex.Message);
                return false;
            }
        }

        /// <summary>
        ///  是否 包含 中文 (符號不算中文 => 用 ASCII > 126 來判斷)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsContainChinese(string source)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(source))
            {
                foreach (var currentChar in source)
                {
                    // 這邊不管符號的 => 所以查 ASCII碼時發現 126 是底
                    // => 所以如果 ASCII > 126 就都視為中文
                    int currentCharNum = currentChar.IntTryParse();
                    if ((currentCharNum <= 0) || (currentCharNum > 126))
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        /// <summary>
        ///  是否 全部 中文 (符號不算中文 => 用 ASCII > 126 來判斷)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsAllChinese(string source)
        {
            bool result = true;

            if (!string.IsNullOrEmpty(source))
            {
                foreach (var currentChar in source)
                {
                    // 這邊不管符號的 => 所以查 ASCII碼時發現 126 是底
                    // => 所以如果 ASCII > 126 就都視為中文
                    int currentCharNum = currentChar.IntTryParse();
                    if ((currentCharNum > 0) && (currentCharNum <= 126))
                    {
                        result = false;
                    }
                }

                return result;
            }
            else
            {
                return false;
            }
        }



        #endregion
    }
}

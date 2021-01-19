using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Web_Scraper
{
    public class StripDataLogic : SubstringData
    {
        protected virtual string StripData(string data, string firstParam, string secondParam)
        {
            string fixRes = "";
            var temp = data.Split(firstParam);

            if (temp.Length > 1)
            {
                var resultData = temp[1].Split(secondParam);
                if (resultData.Length > 0)
                {
                    fixRes = resultData[0];
                }
            }

            fixRes = this.SplitByImg(fixRes);

            //var fixRes = this.FixResultData(res);
            //fixRes = this.SplitByType(fixRes);

            return fixRes;
        }

        protected virtual string SplitByImg(string data)
        {
            var pattern = @"<img[^>]*>";
            var x = Regex.Replace(data, pattern, "");
            return x.Trim();
        }

        private string FixResultData(string res)
        {
            var strArray = res.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Where(x => x.Length > 2)
                .ToArray();

            var resData = string.Empty;
            foreach (var word in strArray)
            {
                if (word.Contains("\n"))
                {
                    resData += base.SubString(word, "\n");
                }
                else if (word.Contains("/n"))
                {
                    resData += base.SubString(word, "/n");
                }
                else if (word.Contains("\t"))
                {
                    resData += base.SubString(word, "\t");
                }
                else if (word.Contains("\\,t"))
                {
                    resData += base.SubString(word, "\\,t");
                }
                else if (word.Contains("\\t"))
                {
                    resData += base.SubString(word, "\\t");
                }
                else if (word.Contains("\t\t"))
                {
                    resData += base.SubString(word, "\t\t");
                }
                else
                {
                    if (word.Length > 2)
                    {
                        resData += word + " ";
                    }
                }
            }

            return resData;
        }
    }
}

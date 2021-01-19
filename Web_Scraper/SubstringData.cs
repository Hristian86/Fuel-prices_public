namespace Web_Scraper
{
    public class SubstringData
    {
        protected virtual string SubString(string word, string regexStr)
        {
            int startIndex = word.IndexOf(regexStr);
            var splitLine = word.Substring(0, word.Length - (word.Length - startIndex));
            if (splitLine.Length > 1)
            {
                return splitLine + " ";
            }

            return splitLine;
        }
    }
}
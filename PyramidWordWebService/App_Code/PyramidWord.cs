using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Services;

[WebService(Namespace = "http://Sampath/PyramidWordWebService/PyramidWord.asmx/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class PyramidWordService : WebService
{
    [WebMethod]
    public bool IsPyramidWord(string word)
    {
        bool onlyLetters = Regex.IsMatch(word, @"^[a-zA-Z]+$");
        if (!onlyLetters)
            return false;


        char[] wordChars = word.ToCharArray();
        Dictionary<char, int> wordFrequency = new Dictionary<char, int>();
        foreach (char letter in wordChars)
        {
            if (wordFrequency.ContainsKey(letter))
                wordFrequency[letter]++;
            else
                wordFrequency[letter] = 1;
        }

        int[] sortedFrequency = wordFrequency.Values.ToArray();
        Array.Sort(sortedFrequency);

        return sortedFrequency[0] == 1 && IsArraySequential(sortedFrequency);
    }

    [WebMethod]
    private bool IsArraySequential(int[] inputArray)
    {
        for (int i = 0; i < inputArray.Length - 1; i++)
        {
            int currentCount = inputArray[i];
            int nextCount = inputArray[i + 1];
            if (nextCount - currentCount != 1)
            {
                return false;
            }
        }
        return true;
    }
}

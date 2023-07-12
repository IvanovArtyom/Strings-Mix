using System.Collections.Generic;
using System.Linq;

public class Mixing
{
    public static void Main()
    {
        // Test
        string s1 = "my&friend&Paul has heavy hats! &";
        string s2 = "my friend John has many many friends &";
        var t = Mix(s1, s2);
        // ...should return "2:nnnnn/1:aaaa/1:hhh/2:mmm/2:yyy/2:dd/2:ff/2:ii/2:rr/=:ee/=:ss"
    }

    public static string Mix(string s1, string s2)
    {
        var arr1 = CountLetters(s1);
        var arr2 = CountLetters(s2);
        var result = new List<string>();

        for (int i = 0; i < 26; i++)
        {
            if (arr1[i] < 2 && arr2[i] < 2)
                continue;

            if (arr1[i] > arr2[i])
                result.Add("1:" + new string((char)(i + 97), arr1[i]));

            else if (arr1[i] < arr2[i])
                result.Add("2:" + new string((char)(i + 97), arr2[i]));

            else result.Add("3:" + new string((char)(i + 97), arr1[i]));
        }

        result = result.OrderByDescending(s => s.Length).ThenBy(s => s).ToList();

        for (int i = 0; i < result.Count; i++)
        {
            if (result[i][0] == '3')
                result[i] = "=" + result[i][1..];
        }

        return string.Join("/", result);
    }

    public static int[] CountLetters(string s)
    {
        int[] arr = new int[26];

        foreach (char c in s)
        {
            if (char.IsLetter(c) && char.IsLower(c))
                arr[c - '0' - 49]++;
        }

        return arr;
    }
}
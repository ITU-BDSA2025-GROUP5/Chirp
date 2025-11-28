using System.Text;

namespace Chirp.Tests.Tools_to_Test;

public class InputFuzzers
{
    private static readonly Random _rand = new Random();

    // Generate a random string of given length
    public static string RandomString(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!#$%^&*";
        var sb = new StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            sb.Append(chars[_rand.Next(chars.Length)]);
        }
        return sb.ToString();
    }
    // String mutation device  randomly moves chars in String to random pos.
    public static string RandomMutation(string input)
    {
        var chars = input.ToCharArray();
        int idx = _rand.Next(chars.Length);
        chars[idx] = (char)_rand.Next(32, 126); 
        return new string(chars);
    }

    public static string RandomSentence()
    {
        int randomLength = _rand.Next(50, 121);
        string input = RandomString(randomLength);
        
        var sb = new StringBuilder();
        int i = 0;

        while (i < input.Length && sb.Length < 160)
        {
            // Random segment length between 3–10 characters
            int segmentLength = _rand.Next(3, 10);
            int take = Math.Min(segmentLength, input.Length - i);
            sb.Append(input.Substring(i, take));
            i += take;

            // 80% chance to insert a space
            if (i < input.Length && _rand.NextDouble() < 0.8)
                sb.Append(' ');
        }

        // Trim and cap at 160 chars
        var result = sb.ToString().Trim();
        if (result.Length > 160)
            result = result.Substring(0, 160);
        
        return result;
    }
    
}
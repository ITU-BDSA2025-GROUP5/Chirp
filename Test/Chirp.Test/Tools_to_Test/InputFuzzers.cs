using System.Text;

namespace Chirp.Tests.Tools_to_Test;

public class InputFuzzers
{
    private static readonly Random _rand = new Random();

    // Generate a random string of given length
    public static string RandomString(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*";
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

}
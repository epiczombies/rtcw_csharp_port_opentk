using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class game
{
    public static void strcpy(string a, string b)
    {
        a = b;
    }
    public static int strlen(string a)
    {
        return a.Length;
    }
    public static string strcat(string a, string b)
    {
        a = a + b;
        return a;
    }
    public static bool strcmp(string a, string b)
    {
        return a == b;
    }
    public static string va(string a, params object[] args)
    {
        throw new Exception("Copy my va function for c# (Drive E -> Visual Studio -> MemoryClass)");
        return a;
    }

    /// <summary>
    /// returns -1 if the char b where not found in string a.
    /// </summary>
    public static int strchr(string a, char b)
    {
        return a.IndexOf(b);
    }
}
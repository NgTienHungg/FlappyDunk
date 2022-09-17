using UnityEngine;

public class Logger
{
    private static bool enable = false;

    public static void Log(string message)
    {
        if (enable)
        {
            Debug.Log(message);
        }
    }

    public static void Warning(string message)
    {
        if (enable)
        {
            Debug.LogWarning(message);
        }
    }

    public static void Error(string message)
    {
        if (enable)
        {
            Debug.LogError(message);
        }
    }
}

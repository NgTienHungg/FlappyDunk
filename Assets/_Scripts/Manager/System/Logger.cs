using UnityEngine;

public class Logger
{
    private static bool enable = true;

    public static void Log<T>(T message)
    {
        if (enable)
        {
            Debug.Log(message);
        }
    }

    public static void Warning<T>(T message)
    {
        if (enable)
        {
            Debug.LogWarning(message);
        }
    }

    public static void Error<T>(T message)
    {
        if (enable)
        {
            Debug.LogError(message);
        }
    }
}
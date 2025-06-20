using System;

namespace Utils
{
    public static class Logger
    {
        public static void Info(string message) => Print(message, ConsoleColor.Cyan);
        public static void Success(string message) => Print(message, ConsoleColor.Green);
        public static void Error(string message) => Print(message, ConsoleColor.Red);
        public static void Warn(string message) => Print(message, ConsoleColor.Yellow);

        private static void Print(string msg, ConsoleColor color)
        {
            var now = DateTime.Now.ToString("HH:mm:ss");
            Console.ForegroundColor = color;
            Console.WriteLine($"[{now}] {msg}");
            Console.ResetColor();
        }
    }
}

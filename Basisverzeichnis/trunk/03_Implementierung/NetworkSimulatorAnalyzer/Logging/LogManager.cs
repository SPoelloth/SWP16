using System;
using System.Collections.Generic;
using System.IO;

namespace NSA.Logging
{
  public class LogManager
  {
    public static string LogfilePath { get; }
    static object fileLock = new object();

    static LogManager()
    {
      LogfilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", $@"Session_{DateTime.Now:dd.MM.yy_HH.mm.ss}.log");
      Directory.CreateDirectory("Logs");
      WriteToLog("Hi, I'm your logfile!");
    }

    public static void WriteToLog(string text)
    {
      lock (fileLock)
      {
        File.AppendAllText(LogfilePath, $"{DateTime.Now:HH:mm:ss} {text}{Environment.NewLine}");
      }
    }

    public static void WriteToLog(IEnumerable<string> text)
    {
      foreach (var line in text) WriteToLog(line);
    }
  }
}

using System;
using System.Collections.Generic;
using System.IO;

namespace NetworkSimulatorAnalyzer.Logging
{
  public class LogManager
  {
    public static string LogfilePath { get; private set; }
    static object fileLock = new object();

    static LogManager()
    {
      LogfilePath = string.Format(@"Logs\Session_{0:dd.MM.yy_HH.mm.ss}.log", DateTime.Now);
      Directory.CreateDirectory("Logs");
      WriteToLog("Hi, I'm your logfile!");
    }

    public static void WriteToLog(string text)
    {
      lock (fileLock)
      {
        File.AppendAllText(LogfilePath, string.Format("{0:HH:mm:ss} {1}{2}", DateTime.Now, text, Environment.NewLine));
      }
    }

    public static void WriteToLog(IEnumerable<string> text)
    {
      foreach (var line in text) WriteToLog(line);
    }
  }
}

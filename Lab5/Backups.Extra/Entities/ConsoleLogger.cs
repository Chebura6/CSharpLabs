using Backups.Extra.Interfaces;
using Newtonsoft.Json;

namespace Backups.Extra.Entities;
public class ConsoleLogger : ILogger
{
    public void MakeLog(string logMessage)
    {
        Console.WriteLine(logMessage);
    }
}

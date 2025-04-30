using System.Text.Json;

namespace Cv3;

public class JsonLogger : IMyLogger
{
    public void Log(string txt)
    {
        const string file = "log.json";
        List<string>? logs;
        if (File.Exists(file))
        {
            logs = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(file));
        }
        else
        {
            logs = [];
        }
        logs!.Add(txt);
        
        File.WriteAllText(file, JsonSerializer.Serialize(logs));
    }
}
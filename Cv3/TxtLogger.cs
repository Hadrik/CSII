namespace Cv3;

public class TxtLogger : IMyLogger
{
    public void Log(string txt)
    {
        File.AppendAllText("log.txt", $"{txt}\n");
    }
}
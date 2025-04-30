namespace Cv3;

public class ExceptionHandler(IMyLogger logger)
{
    public void Handle(Exception e)
    {
        File.AppendAllText("errors.txt", $"{e.Message}\n{e.StackTrace}\n\n");
    }
}
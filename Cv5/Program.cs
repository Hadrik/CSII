namespace Cv5;

class Program
{
    private static async Task Main(string[] args)
    {
        await Experiment();



        /*
        var stack = new SimpleStack<int>();
        var rand = new Random();
        var lockObject = new object();

        var adder = new Thread(() =>
        {
            while (true)
            {
                stack.Push(rand.Next());
                lock (lockObject)
                {
                    Monitor.Pulse(lockObject);
                }
                Thread.Sleep(100);
            }
        });

        ThreadStart fn = () =>
        {
            while (true)
            {
                if (stack.TryPop(out var num))
                {
                    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {num}");
                    Thread.Sleep(rand.Next(40, 1001));
                }
                else
                {
                    Console.WriteLine("Sleep");
                    lock (lockObject)
                    {
                        Monitor.Wait(lockObject);
                    }
                }
            }
        };

        adder.Start();
        List<Thread> threads = [];

        for (var i = 0; i < 5; i++)
        {
            threads.Add(new Thread(fn));
        }

        foreach (var thread in threads)
        {
            thread.Start();
        }
        */
        /*
        ThreadStart fn = () =>
        {
            while (true)
            {
                if (rand.NextDouble() < 0.3)
                {
                    stack.Push(rand.Next());
                }
                else
                {
                    if (stack.TryPop(out var num))
                    {
                        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {num}");
                    }
                }
            }
        };

        List<Thread> threads = [];

        for (var i = 0; i < 5; i++)
        {
            threads.Add(new Thread(fn));
        }

        foreach (var thread in threads)
        {
            thread.Start();
        }
        */
    }

    private static async Task Experiment()
    {
        Console.WriteLine("Start");
        await Task.Delay(1000);
        using (var sw = new StreamWriter("abc.txt"))
        {
            await sw.WriteLineAsync("abcdefgh");
        }
        await Task.Delay(1000);
        Console.WriteLine("End");
    }
}

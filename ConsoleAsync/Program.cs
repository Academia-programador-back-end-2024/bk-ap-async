namespace ConsoleAsync
{
    internal class Program
    {
        static object Objeto = new object();

        static CancellationTokenSource tokenSource = new CancellationTokenSource();
        static CancellationToken token = tokenSource.Token;
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            List<Task> tarefas = new List<Task>();

            Task tarefa1 = Task.Run(ThreadNumerosAsync, token);


            tarefas.Add(tarefa1);
            Task tarefa2 = new Task(ThreadNumerosAsync, token);
            tarefa2.Start();
            tarefas.Add(tarefa2);
            Task tarefa3 = Task.Run(ThreadNumerosAsync);

            tarefas.Add(tarefa3);

            Task.WhenAny(tarefas.ToArray());


            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"Iteração sincrona {i}");
                if (i == 10)
                {
                    tokenSource.Cancel();
                }
            }


        }

        private static void ThreadNumerosAsync()
        {
            //while (token.IsCancellationRequested is false)
            var conteudoArquivo = String.Empty;
            //lock (Objeto)
            {
                conteudoArquivo = ConteudoArquivo();

                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine($"Iteração thread {i}");
                    conteudoArquivo += $"\r\nIteração thread {i}";
                    if (i == 50)
                    {
                        //Thread.Sleep(TimeSpan.FromSeconds(1));
                    }
                }
                File.WriteAllText("teste.txt", conteudoArquivo);
            }
        }

        private static string ConteudoArquivo()
        {
            string conteudoArquivo = File.ReadAllText("teste.txt");
            return conteudoArquivo;
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;

namespace lab2
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				if (args.Length == 0)
					throw new Exception("Отсутствует входной аргумент!");
				int timeout = int.Parse(args[0]);
				CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
				CancellationToken token = cancelTokenSource.Token;
				Task task = Task.Run(() => Processing(token));
				Thread.Sleep(timeout);
				cancelTokenSource.Cancel();
				task.Wait();
			}
			catch (FormatException)
			{
				Console.WriteLine("Входной аргумент имеет неправильный формат!");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
		static void Processing(CancellationToken token)
		{
			Thread.Sleep(100);
			if (token.IsCancellationRequested)
			{
				Console.WriteLine("Задача завершена");
				return;
			}
			Console.WriteLine("Задача выполняется");
			Thread.Sleep(100);
			Console.WriteLine("Задача завершена");
		}
	}
}

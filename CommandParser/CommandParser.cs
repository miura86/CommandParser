using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandParser
{
	class CommandParser
	{
		private static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				// No arguments, show help
				CommandsHelper.ShowHelp();
			}
			else
			{
				try
				{
					// Get commands and their parameters
					var commands = CommandsHelper.GetCommands(args);
					// Execute commands
					CommandsHelper.ExecuteCommands(commands);
				}
				catch (ArgumentException)
				{
					Console.WriteLine("Application does not support reccuring commands.");
				}
				catch (Exception ex)
				{
					Console.WriteLine("Unknown error occured in application. Error details: " + ex.ToString());
				}
            }
		}
	}
}

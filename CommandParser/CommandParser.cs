﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandParser
{
	class Program
	{
		private static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				// No arguments, show help
				ShowHelp();
			}
			else
			{
				// Get commands and their parameters
				var commands = GetCommands(args);

				var firstHelp = true; // flag, shows whether help was shown or not

				// Execute each command
				foreach (var command in commands)
				{
					switch (command.Key.ToUpper())
					{
						case "?":
						case "HELP":
						{
							// Check whether help was not executed earlier
							if (firstHelp)
							{
								ShowHelp();
								firstHelp = false;
							}
							break;
						}
						case "K":
						{
							// Get key-value pairs in new format
							var output = FormatKeyValuePairs(command.Value);
							Console.WriteLine(output);
							break;
						}
						case "PING":
						{
							Console.WriteLine("Pinging...");
							Console.Beep();
							break;
						}
						case "PRINT":
						{
							// Create message from all parameters
							var builder = new StringBuilder();
							foreach (var item in command.Value)
							{
								builder.Append(item + " ");
							}
							// Delete last whitespace
							builder.Length--;

							Console.WriteLine(builder.ToString());
							break;
						}
						default:
						{
							// Inform user
							Console.WriteLine(
								string.Format("Command {0} is not supported, use CommandParser.exe /? to see set of allowed commands.", command.Key));
							break;
						}
					}
				}
			}
		}

		/// <summary>
		/// Shows instructions for all supported commands
		/// </summary>
		static void ShowHelp()
		{
			Console.WriteLine("?      Provides help information.");
			Console.WriteLine("HELP   Provides help information.");
			Console.WriteLine("K      Accepts key-value pairs, returns them as a list. Parameter - key-value pairs.");
			Console.WriteLine("PING   Pinging...");
			Console.WriteLine("PRINT  Prints entered text. Parameter - message.");
		}

		/// <summary>
		/// Creates list of commands with their parameters (also list).
		/// </summary>
		/// <param name="args"></param>
		/// <returns>List of commands with paremeters.</returns>
		static IEnumerable<KeyValuePair<string, List<string>>> GetCommands(IEnumerable<string> args)
		{
			var commands = new List<KeyValuePair<string, List<string>>>();
			var command = "";
			var parameters = new List<string>();
            foreach (var arg in args)
			{
				// Identify commands between args
                if (arg[0] == '/' || arg[0] == '-')
                {
					// Check whether previos command exist
	                if (!string.IsNullOrEmpty(command))
	                {
		                commands.Add(new KeyValuePair<string, List<string>>(command, new List<string>(parameters)));
		                parameters.Clear();
	                }
					// Save command until all parameters will be read
	                command = arg.Substring(1, arg.Length - 1);
                }
                else
                {
					// Collect all parameters
	                parameters.Add(arg);
                }
			}
			// Add last command
			commands.Add(new KeyValuePair<string, List<string>>(command, parameters));

			return commands;
		}

		/// <summary>
		/// Gets one line string of key-value pairs and transforms it in multiline format.
		/// </summary>
		/// <param name="input"></param>
		/// <returns>Key-value pairs in multiline format.</returns>
		static string FormatKeyValuePairs(IReadOnlyList<string> input)
		{
			var builder = new StringBuilder();
			for (int k = 0; k < input.Count; k = k + 2)
			{
				builder.Append(input[k]).Append(" - ");
				builder.Append(k + 1 < input.Count ? input[k + 1] : "<null>");
				builder.AppendLine();
			}
			// Exclude last "new line" character
			builder.Length--;

			return builder.ToString();
		}
	}
}

using Output = System.Console;

namespace VirtualFileSystem.Console
{
	internal class Program
	{
		private static VirtualFileSystem fileSystem = new VirtualFileSystem();

		private static void Main(string[] args)
		{
			var userInput = "";
			Output.WriteLine("\n\nWelcome to out virtual file system:");
			Output.WriteLine("Type help for help menu:");
			userInput = Output.ReadLine();
			while (userInput.ToLower() != "exit")
			{
				switch (userInput)
				{
					case "help":
						Output.WriteLine("--> help Prints a help menu, with short description of all the commands ");
						Output.WriteLine("--> md[directory name] Creates a directory, For example: md dir1");
						Output.WriteLine("--> cd[directory name] Changes the current directory, for example: cd dir1");
						Output.WriteLine("--> cd..Changes the current directory to parent directory");
						Output.WriteLine("--> mf[file name] Creates a file, for example: mf file.txt");
						Output.WriteLine("--> dir Displays list of files and subdirectories in current directory");
						Output.WriteLine("--> dir / s Displays files in specified directory and all subdirectories");
						Output.WriteLine("--> exit Quits the program");
						break;

					case "cd..":
						if (fileSystem.Current.Parent != null)
							fileSystem.ChangeDirectory(fileSystem.Current.Parent);
						else
							Output.WriteLine("Already at root level");
						break;

					case "dir":
						foreach (var dir in fileSystem.Current.SubDirectories)
						{
							Output.WriteLine($"{dir.Name}");
						}
						foreach (var file in fileSystem.Current.Files)
						{
							Output.WriteLine($"{file.Name}");
						}
						break;

					case "dir /s":
						foreach (var fileName in fileSystem.Current.ListFiles())
						{
							Output.WriteLine($"{fileName}");
						}
						break;

					default:
						var directory = fileSystem.Current.ExecuteUserCommand(userInput);
						fileSystem.ChangeDirectory(directory);
						Main(args);
						return;
				}

				Main(args);
			}
		}
	}
}
using System.Linq;
using Output = System.Console;

namespace VirtualFileSystem.Console
{
	/// <summary>
	/// Class representing a virtual directory
	/// </summary>
	public class VirtualDirectory
	{
		public string Name { get; private set; }
		public string FullPath { get; private set; }
		public VirtualDirectory Parent { get; }

		public VirtualFile[] Files { get; set; }
		public VirtualDirectory[] SubDirectories { get; set; }

		/// <summary>
		/// Creates a new directory with name and path
		/// </summary>
		/// <param name="name"></param>
		/// <param name="parentDirectory"></param>
		public VirtualDirectory(string name, VirtualDirectory parentDirectory)
		{
			Name = name;
			FullPath = $@"{parentDirectory?.FullPath }\{Name}";
			Parent = parentDirectory;

			Files = new VirtualFile[] { };
			SubDirectories = new VirtualDirectory[] { };
		}

		public string CreateDirectory(string name)
		{
			var directories = new VirtualDirectory[SubDirectories.Length + 1];
			for (int i = 0; i < SubDirectories.Length; i++)
			{
				directories[i] = SubDirectories[i];
			}
			var newDirectory = new VirtualDirectory(name, this);
			directories[SubDirectories.Length] = newDirectory;

			SubDirectories = directories;
			Output.WriteLine($"Created new directory: {newDirectory.FullPath}");
			return newDirectory.Name;
		}

		public string CreateFile(string name)
		{
			var files = new VirtualFile[Files.Length + 1];
			for (int i = 0; i < Files.Length; i++)
			{
				files[i] = Files[i];
			}
			var newFile = new VirtualFile(name, FullPath);
			files[Files.Length] = newFile;

			Files = files;
			Output.WriteLine($"Created new file: {newFile.Path}");
			return newFile.Name;
		}

		public string[] ListFiles(bool recurse = true)
		{
			var allFiles = new string[Files.Length];
			foreach (var file in Files)
			{
				allFiles.Append(file.Name);
			}

			foreach (var dir in SubDirectories)
			{
				var files = new string[allFiles.Length + dir.Files.Length];
				for (int i = 0; i < dir.Files.Length; i++)
				{
					allFiles.Append(dir.Files[i].Name);
				}
			}

			return allFiles;
		}

		public VirtualDirectory ExecuteUserCommand(string userCommand)
		{
			var command = userCommand.ToLower();
			//Create new directory
			if (command.StartsWith("md "))
			{
				var directoryName = userCommand.Split(" ")[1];
				CreateDirectory(directoryName);
				return this;
			}

			if (command.StartsWith("mf "))
			{
				var fileName = userCommand.Split(" ")[1];
				CreateFile(fileName);
				return this;
			}
			if (command.StartsWith("cd "))
			{
				Output.WriteLine("Changing directory..");
				var directoryName = userCommand.Split(" ")[1].ToLower();
				var subDirectory = SubDirectories.FirstOrDefault(x => x.Name.ToLower() == directoryName);
				if (subDirectory != null)
				{
					Output.WriteLine($"Changed directory to {subDirectory.Name}");
					return subDirectory;
				}
			}
			return this;
		}
	}
}
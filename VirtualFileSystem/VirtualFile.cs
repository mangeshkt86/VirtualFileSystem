namespace VirtualFileSystem.Console
{
	public class VirtualFile
	{
		public string Name { get; private set; }
		public string Path { get; private set; }

		public VirtualFile(string name, string parentPath)
		{
			Name = name;
			Path = $@"{parentPath}\{name}";
		}
	}
}

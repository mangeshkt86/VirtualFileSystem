namespace VirtualFileSystem.Console
{
	public class VirtualFileSystem
	{
		public VirtualDirectory Current { get; private set; }

		public VirtualFileSystem()
		{
			Current = new VirtualDirectory("Root", null);
		}

		public void ChangeDirectory(VirtualDirectory directory)
		{
			Current = directory;
		}
	}
}

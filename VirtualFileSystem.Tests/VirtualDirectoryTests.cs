using System;
using System.Linq;
using VirtualFileSystem.Console;
using Xunit;

namespace VirtualFileSystem.Tests
{
	public class VirtualDirectoryTests
	{

		public VirtualDirectoryTests()
		{

		}

		[Fact]
		public void GivenDirectoryName_WhenCreateDirectory_ThenCreateNewDirectory()
		{
			// Arrange
			var name = "newdirectory";

			// Act
			var parentDir = new VirtualDirectory("/", null);
			var newDirectory = parentDir.CreateDirectory(name);

			// Assert
			Assert.True(parentDir.SubDirectories.Count(x => x.Name == name) == 1);
		}

		[Fact]
		public void GivenFileName_WhenCreateFile_ThenCreateNewFile()
		{
			// Arrange
			var name = "newFile";

			// Act
			var parentDir = new VirtualDirectory("/", null);
			var newDirectory = parentDir.CreateFile(name);

			// Assert
			Assert.True(parentDir.Files.Count(x => x.Name == name) == 1);
		}

	}
}

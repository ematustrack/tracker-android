using System;
namespace test2.Droid
{
	public class FileAccessHelper
	{
		public static string GetLocalFilePath(string filename)
		{
			string path = Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			return System.IO.Path.Combine(path, filename);
		}
	}
}

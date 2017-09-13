using TodoGenericRepo.iOS;
using System;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(FileHelper))]
namespace TodoGenericRepo.iOS
{
	public class FileHelper: IFileHelper
    {
		public string GetLocalFilePath(string filename)
		{
			string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

			if (!Directory.Exists(libFolder))
			{
				Directory.CreateDirectory(libFolder);
			}

			return Path.Combine(libFolder, filename);
		}
	}
}

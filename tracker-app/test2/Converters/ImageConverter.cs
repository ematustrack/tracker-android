using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace test2
{
	public class DataConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) Console.WriteLine(" Image path null");
			//var basePath = "/storage/emulated/0/Android/data/com.companyname.test/files";//Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			//var filePath = Path.Combine(basePath, "Pictures/Sample", value as string + ".jpg");
			//var x = ImageSource.FromFile(filePath);
			byte[] data = System.Convert.FromBase64String(value as string);
			return ImageSource.FromStream(() => new MemoryStream(data));
		}
				public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

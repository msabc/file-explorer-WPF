using FileExplorer.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace FileExplorer.Converters
{
    class DirectoryTypeToImageConverter : IValueConverter
    {
        public static DirectoryTypeToImageConverter Instance { get; } = new DirectoryTypeToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string imageName = null;

            if (value is DirectoryType type)
            {
                switch (type)
                {
                    case DirectoryType.Drive:
                        imageName = "disk.jpg";
                        break;
                    case DirectoryType.Folder:
                        imageName = "folder.jpg";
                        break;
                    case DirectoryType.File:
                        imageName = "file.jpg";
                        break;
                    default:
                        break;
                }
            }

            return imageName == null ? null : new Uri($"../Resources/Images/{imageName}", UriKind.Relative);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

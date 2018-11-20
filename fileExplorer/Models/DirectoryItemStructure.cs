using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileExplorer.Models
{
    static class DirectoryItemStructure
    {
        public static List<DirectoryItem> GetAllDrives()
        {
            var drives = Directory.GetLogicalDrives();

            if (drives == null || drives.Length == 0)
            {
                return null;

            }
            return drives.Select(drive => new DirectoryItem() { FullPath = drive,Name = drive, Type = DirectoryType.Drive }).ToList();
        }

        public static List<DirectoryItem> GetSubItems(string fullPath)
        {

            // Create empty list
            var items = new List<DirectoryItem>();

            #region Get Folders

            // Try and get directories from the folder
            // ignoring any issues doing so
            try
            {
                var dirs = Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    items.AddRange(dirs.Select(dir => new DirectoryItem { FullPath = dir, Name = Path.GetFileName(dir), Type = DirectoryType.Folder }));
            }
            catch { }

            #endregion

            #region Get Files

            // Try and get files from the folder
            // ignoring any issues doing so
            try
            {
                var fs = Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                    items.AddRange(fs.Select(file => new DirectoryItem { FullPath = file, Name = Path.GetFileName(file), Type = DirectoryType.File }));
            }
            catch
            {

            }
            

            #endregion

            return items;
        }
    }
}

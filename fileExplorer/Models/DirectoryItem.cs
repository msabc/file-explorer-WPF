namespace FileExplorer.Models
{
    enum DirectoryType
    {
        Drive,
        Folder,
        File
    }

    class DirectoryItem
    {
        public string FullPath { get; set; }
        public string Name { get; set; }
        public DirectoryType Type { get; set; }

        
    }
}

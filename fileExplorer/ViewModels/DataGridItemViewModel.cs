using FileExplorer.Commands;
using FileExplorer.Models;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;

namespace FileExplorer.ViewModels
{
    class DataGridItemViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        
        private void CallPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region Backing Fields

        private string fullPath;
        private string name;
        private DirectoryType type;
        private long size;
        private DateTime lastChangeDate;
        private string typeDescription;
        #endregion
        #region Properties
        public string FullPath { get { return fullPath; }
            set
            {
                if (fullPath==value)
                {
                    return;
                }
                fullPath = value;
                CallPropertyChanged(nameof(FullPath));
            }
        }
        public string Name {
            get { return name; }
            set {
                if (name == value)
                {
                    return;
                }
                name = value;
                CallPropertyChanged(nameof(Name));
            }
        }
        public DirectoryType Type {
            get { return type; }
            set {
                if (type == value)
                {
                    return;
                }
                type = value;
                CallPropertyChanged(nameof(Type));
            }
        }
        public long Size {
            get { return Type == DirectoryType.File ? new FileInfo(FullPath).Length : 0; }
            set
            {
                if (size==value)
                {
                    return;
                }
                size = value;
                CallPropertyChanged(nameof(Size));
            }
        }
        public DateTime LastChangeDate {

            get {
                switch (Type)
                {
                    case DirectoryType.Folder:
                        return Directory.GetLastWriteTime(FullPath);

                    case DirectoryType.File:
                        return File.GetLastWriteTime(FullPath);


                    case DirectoryType.Drive:
                    default:
                        return default(DateTime);
                }
            }
            set {
                if (lastChangeDate == value)
                {
                    return;
                }
                lastChangeDate = value;
                CallPropertyChanged(nameof(LastChangeDate));
            }
        }
        public string TypeDescription {

            get {
                switch (Type)
                {
                    case DirectoryType.Folder:
                        return "Mapa s datotekama";

                    case DirectoryType.File:
                        return $"{Path.GetExtension(FullPath).ToUpper()} datoteka";


                    case DirectoryType.Drive:
                    default:
                        return "Sjebaus";
                }
            }

            set {
                if (typeDescription == value)
                {
                    return;
                }
                typeDescription = value;
                CallPropertyChanged(nameof(TypeDescription));
            }
        }
        
        

        public ICommand Open => new RelayCommand(() => Process.Start(FullPath));

        #endregion
        
    }
}

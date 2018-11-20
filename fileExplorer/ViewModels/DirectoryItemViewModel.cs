using FileExplorer.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace FileExplorer.ViewModels
{
    public delegate void SelectedItemEventHandler(object sender, EventArgs e);

    class DirectoryItemViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static event SelectedItemEventHandler OnSelectedItem;


        private void CallPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DirectoryItemViewModel()
        {
            Children = new ObservableCollection<DirectoryItemViewModel>();

            ResetChildren();
        }

        
        private ObservableCollection<DirectoryItemViewModel> children;
        private string fullPath;
        private string fileinfo;
        private string name;
        private DirectoryType type;
        private bool isExpanded;
        private bool isSelected;



        public ObservableCollection<DirectoryItemViewModel> Children { get { return children; }

            set
            {
                if (children==value)
                {
                    return;
                }
                children = value;
                CallPropertyChanged(nameof(Children));
            }

        }
        public string FullPath { get { return fullPath; }

            set
            {
                if (value == fullPath)
                {
                    return;
                }
                fullPath = value;
                CallPropertyChanged(nameof(FullPath));
            }
        }
        public string FileInfo { get { return fileinfo; }

            set {

                if (value == fileinfo)
                {
                    return;
                }
                fileinfo = value;
                CallPropertyChanged(nameof(FileInfo));
            }
            
        }
        public string Name
        {
            get { return name; }

            set
            {
                if (value == name)
                {
                    return;
                }
                name = value;
                CallPropertyChanged(nameof(Name));
            }
        }
        public DirectoryType Type
        {
            get { return type; }

            set
            {
                if (value == type)
                {
                    return;
                }
                type = value;
                CallPropertyChanged(nameof(Type));

                ResetChildren();
            }
        }
        public bool IsExpanded {

            get { return isExpanded; }

            set
            {
                if (value == isExpanded)
                {
                    return;
                }
                isExpanded = value;
                CallPropertyChanged(nameof(IsExpanded));

                if (isExpanded)
                {
                    Expand();
                }
                else
                {
                    ResetChildren();
                }
            }
        }
        public bool IsSelected
        {
            get { return isSelected; }

            set
            {
                if (value == isSelected)
                {
                    return;
                }
                isSelected = value;
                CallPropertyChanged(nameof(isSelected));

                if (isSelected)
                {
                    OnSelectedItem(this, EventArgs.Empty);
                }
              
            }
        }
     
        

        public void Expand()
        {
            if (Children.Count != 1 || Children[0] != null)
            {
                return;
            }

            //ˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇˇ
            Children = new ObservableCollection<DirectoryItemViewModel>(
                DirectoryItemStructure.GetSubItems(FullPath).Select(item => new DirectoryItemViewModel() { FullPath = item.FullPath, Name = item.Name, Type = item.Type }));

        }

        private void ResetChildren()
        {
            Children.Clear();

            if (Type != DirectoryType.File)
            {
                Children.Add(null);
            }
        }

    }
}

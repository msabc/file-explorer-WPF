using FileExplorer.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FileExplorer.ViewModels
{
    class DirectoryStructureViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<DataGridItemViewModel> origigiDataGridItems;

        private ObservableCollection<DataGridItemViewModel> dataGridItems;
        private string search;

        public ObservableCollection <DirectoryItemViewModel> Items { get; set; }
        public ObservableCollection<DataGridItemViewModel> DataGridItems { get { return dataGridItems; }

            set
            {
                if (dataGridItems==value)
                {
                    return;
                }
                dataGridItems = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(DataGridItems)));
            }
        }

        public string Search
        {
            get { return search; }
            set {
                if (search == value)
                {
                    return;
                }
                search = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Search)));

                Task.Run(() => DataGridItems = new ObservableCollection<DataGridItemViewModel>(origigiDataGridItems.Where(item => Contains(item.Name, search, StringComparison.OrdinalIgnoreCase))));
            }
        }

        private bool Contains(string source, string toCheck, StringComparison comparison) => source.IndexOf(toCheck, comparison) >= 0;

        public DirectoryStructureViewModel()
        {
            //pretvaranje directory itema u directory item view modele
            Items = new ObservableCollection<DirectoryItemViewModel>(
            DirectoryItemStructure.GetAllDrives().Select(
                drive => new DirectoryItemViewModel() { FullPath = drive.FullPath, Name = drive.Name, Type = drive.Type }));

            /*========================================================================================================================*/
            DirectoryItemViewModel.OnSelectedItem += (sender, e) =>
            {
                if(sender is DirectoryItemViewModel treeViewItem)
                {
                    var subItems = DirectoryItemStructure.GetSubItems(treeViewItem.FullPath);
                    var vmItems = subItems.Select(subitem => new DataGridItemViewModel()
                    {
                        FullPath = subitem.FullPath,
                        Name = subitem.Name,
                        Type = subitem.Type
                    });

                    DataGridItems = new ObservableCollection<DataGridItemViewModel>(vmItems);
                    origigiDataGridItems = DataGridItems;
                }
            };

            /*========================================================================================================================*/

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

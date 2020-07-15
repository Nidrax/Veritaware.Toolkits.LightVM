using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veritaware.Toolkits.LightVM.WpfMock.IServices;
using Veritaware.Toolkits.LightVM.WpfMock.MockServices;

namespace Veritaware.Toolkits.LightVM.WpfMock.ViewModels
{
    internal class ObservableListViewModel : ViewModelBase
    {
        private readonly INamesService _namesService;
        private readonly Random _random;

        public ObservableListViewModel() : this(new MockNamesService()) { }

        public ObservableListViewModel(INamesService namesService)
        {
            _namesService = namesService;
            _random = new Random();
            Load();
        }

        private void Load()
        {
            DoGenerate = new RelayCommand(Generate);
            DoAdd = new RelayCommand(Add);
            DoInsert = new RelayCommand(Insert);
            DoRemoveLast = new RelayCommand(RemoveLast);
            DoRemove = new RelayCommand(Remove);
            DoReverse = new RelayCommand(Reverse);
            DoSort = new RelayCommand(Sort);

            AddIndex = 0;
            RemoveIndex = 0;
        }

        private int _addIndex;

        public int AddIndex
        {
            get => _addIndex;
            set => Set(ref _addIndex, value);
        }

        private int _removeIndex;

        public int RemoveIndex
        {
            get => _removeIndex;
            set => Set(ref _removeIndex, value);
        }

        private ObservableList<string> _names;

        public ObservableList<string> Names
        {
            get => _names;
            set => Set(ref _names, value);
        }

        public RelayCommand DoGenerate { get; private set; }

        private void Generate() 
            => Names = new ObservableList<string>(_namesService.Get(_random.Next(5, 10)));

        public RelayCommand DoAdd { get; private set; }

        private void Add()
            => Names.Add(_namesService.Get());

        public RelayCommand DoInsert { get; private set; }

        private void Insert()
            => Names.Insert(AddIndex, _namesService.Get());

        public RelayCommand DoRemoveLast { get; private set; }

        private void RemoveLast()
            => Names.RemoveAt(Names.Count - 1);

        public RelayCommand DoRemove { get; private set; }

        private void Remove()
            => Names.RemoveAt(RemoveIndex);

        public RelayCommand DoReverse { get; private set; }

        private void Reverse()
            => Names.Reverse();

        public RelayCommand DoSort { get; private set; }

        private void Sort()
            => Names.Sort();
    }
}

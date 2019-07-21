using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Veritaware.Toolkits.LightVM.Common
{
    public abstract class NotifyingObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void Set<T>(ref T field, T newValue,
            [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
                return;

            field = newValue;
            OnPropertyChanged(propertyName);
        }

        public void Set<T>(T newValue, ref T field,
            [CallerMemberName] string propertyName = null)
            => Set(ref field, newValue, propertyName);

        //Kept for backwards compatibility
        public void TryAndSet<T>(T newValue, ref T field,
            [CallerMemberName] string propertyName = null)
            => Set(ref field, newValue, propertyName);

        public void TryAndSet<T>(ref T field, T newValue,
            [CallerMemberName] string propertyName = null)
            => Set(ref field, newValue, propertyName);

        public delegate void Del([CallerMemberName] string propertyName = null);

        public Del RaisePropertyChanged;

        protected NotifyingObject() => RaisePropertyChanged = OnPropertyChanged;
    }
}

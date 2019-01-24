using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Veritaware.Toolkits.LightVM.Std.Common
{
    public abstract class NotifyingObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Set<T>(ref T targetField, T propertyValue,
            [CallerMemberName] string propertyName = null)
        {
            if (propertyValue == null && targetField == null)
                return;

            if (propertyValue != null && propertyValue.Equals(targetField))
                return;

            targetField = propertyValue;
            OnPropertyChanged(propertyName);
        }

        public void Set<T>(T propertyValue, ref T targetField,
            [CallerMemberName] string propertyName = null)
        {
            Set(ref targetField, propertyValue, propertyName);
        }

        //Kept for backwards compatibility
        public void TryAndSet<T>(T propertyValue, ref T targetField,
            [CallerMemberName] string propertyName = null)
        {
            Set(ref targetField, propertyValue, propertyName);
        }

        public void TryAndSet<T>(ref T targetField, T propertyValue,
            [CallerMemberName] string propertyName = null)
        {
            Set(ref targetField, propertyValue, propertyName);
        }


        public delegate void Del([CallerMemberName] string propertyName = null);

        public Del RaisePropertyChanged;

        protected NotifyingObject() => RaisePropertyChanged = OnPropertyChanged;
    }
}

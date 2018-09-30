using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;

namespace DataBindingFromScratch
{
    public class ViewModel : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private int age;
        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                OnPropertyChanged();
            }
        }

        public ViewModel()
        {
            Name = "Default name";
            Age = 10;
            var timer = new Timer(1000);
            timer.Elapsed += (s, e) =>
            {
                Age++;
            };
            timer.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

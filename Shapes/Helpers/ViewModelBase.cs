using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes.Helpers
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase()
        { }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        private bool _HasValidationErrors = false;
        public bool HasValidationErrors
        {
            get
            {
                return _HasValidationErrors;
            }

            set
            {
                if (_HasValidationErrors != value)
                {
                    _HasValidationErrors = value;
                    OnPropertyChanged(nameof(HasValidationErrors));
                }
            }
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Cql.Wpf.Validate.ViewModel;

namespace Cql.Wpf.Validate.Tests
{
    public class TestNullsModel : INotifyPropertyChanged
    {
        #region NullField
        private string _fieldName;
        public string NullField
        {
            get { return _fieldName; }
            set
            {
                if (_fieldName != value)
                {
                    _fieldName = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region ViewConfig
        private ViewConfigViewModel _viewConfig;
        public ViewConfigViewModel ViewConfig
        {
            get { return _viewConfig; }
            set
            {
                if (_viewConfig != value)
                {
                    _viewConfig = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

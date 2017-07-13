using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Cql.Wpf.Validate.ViewModel;

namespace Cql.Wpf.Validate.Tests
{
    public class TestMultipleFieldsModel : INotifyPropertyChanged
    {
        #region Field1
        private string _field1;
        public string Field1
        {
            get { return _field1; }
            set
            {
                if (_field1 != value)
                {
                    _field1 = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Field2
        private string _field2;
        public string Field2
        {
            get { return _field2; }
            set
            {
                if (_field2 != value)
                {
                    _field2 = value;
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

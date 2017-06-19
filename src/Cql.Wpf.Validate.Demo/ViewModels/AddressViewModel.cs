using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Cql.Wpf.Validate.ViewModel;

namespace Cql.Wpf.Validate.Demo.ViewModels
{
	public class AddressViewModel : INotifyPropertyChanged
	{
		#region States
		private BindingList<string> _states = new BindingList<string>(new List<string>() { "AL", "CO", "MI", "TX", "WY" });
		public BindingList<string> States
		{
			get { return _states; }
			set
			{
				if (_states != value)
				{
					_states = value;
					OnPropertyChanged();
				}
			}
		}
		#endregion

		#region Address1
		private string _address1;
		public string Address1
		{
			get { return _address1; }
			set
			{
				if (_address1 != value)
				{
					_address1 = value;
					OnPropertyChanged();
				}
			}
		}
		#endregion

		#region Address2
		private string _address2;
		public string Address2
		{
			get { return _address2; }
			set
			{
				if (_address2 != value)
				{
					_address2 = value;
					OnPropertyChanged();
				}
			}
		}
		#endregion

		#region City
		private string _city;
		public string City
		{
			get { return _city; }
			set
			{
				if (_city != value)
				{
					_city = value;
					OnPropertyChanged();
				}
			}
		}
		#endregion

		#region State
		private string _state;
		public string State
		{
			get { return _state; }
			set
			{
				if (_state != value)
				{
					_state = value;
					OnPropertyChanged();
				}
			}
		}
		#endregion

		#region PostalCode
		private string _postalCode;
		public string PostalCode
		{
			get { return _postalCode; }
			set
			{
				if (_postalCode != value)
				{
					_postalCode = value;
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

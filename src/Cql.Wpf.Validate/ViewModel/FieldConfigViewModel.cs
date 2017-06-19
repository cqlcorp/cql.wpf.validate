using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Cql.Wpf.Validate.Config;

namespace Cql.Wpf.Validate.ViewModel
{
	/// <summary>
	/// Wraps a single field configuration and is used by the <see cref="FieldValidatorControl"/> as configuration and to provide feedback when validation fails.
	/// </summary>
	public class FieldConfigViewModel : INotifyPropertyChanged
    {
        private readonly FieldConfig _fieldConfig;
        private bool _isValid;

        public FieldConfigViewModel(FieldConfig fieldConfig)
        {
            _fieldConfig = fieldConfig;
            IsValid = true;
        }

		/// <summary>
		/// Name of the field.  Used by <see cref="ViewValidatorControl"/> to match the Name of a control.
		/// </summary>
        public string Name => _fieldConfig.Name;

		/// <summary>
		/// If a LabelKey is provided, the text label will be overwritten using the <see cref="IMessageProvider"/> instance in context.
		/// This is useful if multiple languages are supported, or you need to provide different labels in different contexts to the same field.
		/// </summary>
        public string LabelKey => _fieldConfig.LabelKey;

		/// <summary>
		/// If false, the control's parent Visibility will be set to Visibility.Collapsed.
		/// </summary>
        public bool IsVisible => _fieldConfig.IsVisible;

		/// <summary>
		/// Sets whether the field is required.
		/// </summary>
        public bool IsRequired => _fieldConfig.IsRequired;

		/// <summary>
		/// Regex used to validate the field.
		/// </summary>
        public string Regex => _fieldConfig.Regex;

		/// <summary>
		/// Lower bound number that is acceptable.
		/// </summary>
        public double? NumberMin => _fieldConfig.NumberMin;

		/// <summary>
		/// Upper bound number that is acceptable.
		/// </summary>
        public double? NumberMax => _fieldConfig.NumberMax;

	    /// <summary>
	    /// Lower bound date that is acceptable.
	    /// </summary>
		public DateTime? DateMin => _fieldConfig.DateMin;

	    /// <summary>
	    /// Upper bound date that is acceptable.
	    /// </summary>
		public DateTime? DateMax => _fieldConfig.DateMax;

	    /// <summary>
	    /// Lower bound text length that is acceptable.
	    /// </summary>
		public int? MinLength => _fieldConfig.MinLength;

		/// <summary>
		/// Upper bound text length that is acceptable. Also sets MaxLength on <see cref="System.Windows.Controls.TextBox"/>.
		/// </summary>
		public int? MaxLength => _fieldConfig.MaxLength;

		/// <summary>
		/// If a ValidationMessageKey is provided, the validation message will be determined using the <see cref="IMessageProvider"/> instance in context.
		/// This is useful if multiple languages are supported, or you need to provide different or more helpful messages than the defaults.
		/// </summary>
		public string ValidationMessageKey => _fieldConfig.ValidationMessageKey;

		/// <summary>
		/// <see cref="FieldConfig"/> object.
		/// </summary>
		public FieldConfig FieldConfig => _fieldConfig;

		/// <summary>
		/// If ShareLabelWith is set, a single label will be used to provide feedback for multiple fields.
		/// For example, if you have a Name label and a FirstName and LastName field, if either fail validation, the Name label will provide feedback.
		/// </summary>
		public string ShareLabelWith => _fieldConfig.ShareLabelWith;

		/// <summary>
		/// Format string to format the value with when displaying validation messages.
		/// For example, you may want to display a date with no time, or a number with a certain number of decimal places.
		/// </summary>
		public string ValueFormat => _fieldConfig.ValueFormat;

		internal string GetLabelKey()
        {
            return !string.IsNullOrWhiteSpace(FieldConfig.LabelKey) ? FieldConfig.LabelKey : FieldConfig.Name;
        }

		/// <summary>
		/// Gets whether the field is currently valid.
		/// </summary>
        public bool IsValid
        {
            get { return _isValid; }
            internal set
            {
                if (_isValid == value)
                    return;
                _isValid = value;
                OnPropertyChanged();
            }
		}

	    public override string ToString()
	    {
		    return Name;
	    }

		#region INotifyPropertyChanged Implementation
		public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
		#endregion
	}
}
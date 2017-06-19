using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Cql.Wpf.Validate.Extensions;
using Cql.Wpf.Validate.ViewModel;

namespace Cql.Wpf.Validate.UI
{
	/// <summary>
	/// WPF control that provides validation feedback. Note, that this control does not do validation on its own.
	/// When validation fails, the label foreground is changed (default is SolidBrush(Colors.Red).
	/// An asterisk is added to each label that is a required field.
	/// The control can be used as a standalone validation field, but is usually automatically created by <see cref="ViewValidatorControl"/> instances.
	/// </summary>
	public class FieldValidatorControl : FrameworkElement
    {
		/// <summary>
		/// Control that contains the data being validated.
		/// </summary>
        public static readonly DependencyProperty ControlProperty = DependencyProperty.Register("Control", typeof(Control), typeof(FieldValidatorControl), new PropertyMetadata(null, LabelOrControlChanged));

		/// <summary>
		/// Label for the control that is being validated.
		/// </summary>
		public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(Label), typeof(FieldValidatorControl), new PropertyMetadata(null, LabelOrControlChanged));

		/// <summary>
		/// Alternative to a label.
		/// </summary>
		public static readonly DependencyProperty TextBlockProperty = DependencyProperty.Register("TextBlock", typeof(TextBlock), typeof(FieldValidatorControl), new PropertyMetadata(null, TextBlockChanged));

		/// <summary>
		/// Name of the field that is being validated. This is used by the <see cref="ViewValidatorControl"/> to match a field in the config to an existing validator.
		/// </summary>
		public static readonly DependencyProperty FieldNameProperty = DependencyProperty.Register("FieldName", typeof(string), typeof(FieldValidatorControl), new PropertyMetadata(null));

		/// <summary>
		/// Viewmodel that should be bound to this control. This field is required in order for the <see cref="FieldValidatorControl"/> to be useful.
		/// </summary>
		public static readonly DependencyProperty FieldProperty = DependencyProperty.Register("Field", typeof(FieldConfigViewModel), typeof(FieldValidatorControl), new PropertyMetadata(null, FieldChanged));

		/// <summary>
		/// Gets or sets whether the value is currently valid. This is set by the <see cref="IValidator"/> instance being used to validate.
		/// </summary>
		public static readonly DependencyProperty IsValidProperty = DependencyProperty.Register("IsValid", typeof(bool), typeof(FieldValidatorControl), new PropertyMetadata(true, IsValidChanged));

		/// <summary>
		/// Gets or sets whether to auto-wireup the control based on the WPF layout. This field will automatically use the first non-label Control in the Parent context as the Control
		/// field, and the first Label in the Parent context as the Label. Given that, this instance should be in a container with just a Label and a Control (Textbox, ComboBox, etc).
		/// </summary>
		public static readonly DependencyProperty AutoWireupProperty = DependencyProperty.Register("AutoWireup", typeof(bool), typeof(FieldValidatorControl), new PropertyMetadata(false, AutoWireupChanged));

	    /// <summary>
	    /// Brush to use on the Label Forground when validation failed.
	    /// </summary>
	    public static readonly DependencyProperty ForegroundErrorBrushProperty = DependencyProperty.Register("ForegroundErrorBrush", typeof(Brush), typeof(FieldValidatorControl), new PropertyMetadata(new SolidColorBrush(Colors.Red)));

		private Brush _originalLabelForeground;
        private Brush _originalControlBorderBrush;
        private Thickness _originalControlBorderThickness;
        private Brush _originalTextBlockForeground;

        public FieldValidatorControl()
        {
			DataContextChanged += FieldValidatorControl_DataContextChanged;
			Unloaded += FieldValidatorControl_Unloaded;
            Visibility = Visibility.Collapsed;
        }

		private void FieldValidatorControl_Unloaded(object sender, RoutedEventArgs e)
		{
			
		}

		private void FieldValidatorControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			Init();
		}

	    private void Init()
	    {
		    SetupControl();
		    SetupLabel();
		    WireupControls();
	    }

	    /// <summary>
	    /// Gets or sets whether to auto-wireup the control based on the WPF layout. This field will automatically use the first non-label Control in the Parent context as the Control
	    /// field, and the first Label in the Parent context as the Label. Given that, this instance should be in a container with just a Label and a Control (Textbox, ComboBox, etc).
	    /// </summary>
		public bool AutoWireup
        {
            get { return (bool)GetValue(AutoWireupProperty); }
            set { SetValue(AutoWireupProperty, value); }
        }

	    /// <summary>
	    /// Gets or sets whether the value is currently valid. This is set by the <see cref="IValidator"/> instance being used to validate.
	    /// </summary>
		public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
        }

	    /// <summary>
	    /// Viewmodel that should be bound to this control. This field is required in order for the <see cref="FieldValidatorControl"/> to be useful.
	    /// </summary>
		public FieldConfigViewModel Field
        {
            get { return (FieldConfigViewModel)GetValue(FieldProperty); }
            set { SetValue(FieldProperty, value); }
        }

	    /// <summary>
	    /// Control that contains the data being validated.
	    /// </summary>
		public Control Control
        {
            get { return (Control)GetValue(ControlProperty); }
            set { SetValue(ControlProperty, value); }
        }

	    /// <summary>
	    /// Label for the control that is being validated.
	    /// </summary>
		public Label Label
        {
            get { return (Label)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

	    /// <summary>
	    /// Alternative to a label.
	    /// </summary>
		public TextBlock TextBlock
        {
            get { return (TextBlock)GetValue(TextBlockProperty); }
            set { SetValue(TextBlockProperty, value); }
        }

	    /// <summary>
	    /// Name of the field that is being validated. This is used by the <see cref="ViewValidatorControl"/> to match a field in the config to an existing validator.
	    /// </summary>
		public string FieldName
        {
            get { return (string)GetValue(FieldNameProperty); }
            set { SetValue(FieldNameProperty, value); }
        }

	    /// <summary>
	    /// Brush to use on the Label Forground when validation failed.
	    /// </summary>
	    public Brush ForegroundErrorBrush
	    {
		    get { return (Brush)GetValue(ForegroundErrorBrushProperty); }
		    set { SetValue(ForegroundErrorBrushProperty, value); }
	    }

		private static void LabelOrControlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var validator = d as FieldValidatorControl;
            if (validator == null) return;

            validator.SetupLabel();
            validator.SetupControl();
            validator.SetValid();
        }

        private static void IsValidChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var validator = d as FieldValidatorControl;
            validator?.SetValid();
        }

        private static void FieldChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var validator = d as FieldValidatorControl;
            if (validator == null) return;

            validator.DataContext = validator.Field;
            validator.SetBinding(IsValidProperty, "IsValid");

            validator.SetupLabel();
            validator.SetupControl();
            validator.SetupTextBlocks();
            validator.SetValid();
        }

        private static void TextBlockChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var validator = d as FieldValidatorControl;
            validator?.SetupTextBlocks();
        }

        private static void AutoWireupChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var validator = d as FieldValidatorControl;
            validator?.WireupControls();
        }

        private void SetValid()
        {
	        if (Label != null && _originalLabelForeground == null)
		        _originalLabelForeground = Label.Foreground;

			if (Control != null)
            {
                if (_originalControlBorderBrush == null)
                    _originalControlBorderBrush = Control.BorderBrush;

				if (_originalControlBorderThickness == default(Thickness))
                    _originalControlBorderThickness = Control.BorderThickness;
            }

			if (TextBlock != null && _originalTextBlockForeground == null)
		        _originalTextBlockForeground = TextBlock.Foreground;

	        if (IsValid)
            {
                if (Label != null)
                    Label.Foreground = _originalLabelForeground;

				if (TextBlock != null)
                    TextBlock.Foreground = _originalTextBlockForeground;
            }
            else
            {
                if (Label != null)
                    Label.Foreground = ForegroundErrorBrush ?? new SolidColorBrush(Colors.Red);

				if (TextBlock != null)
                    TextBlock.Foreground = ForegroundErrorBrush ?? new SolidColorBrush(Colors.Red);
            }
        }

        void SetupLabel()
        {
            if (Label != null && Field != null)
            {
                var labelKey = Field.GetLabelKey();
                var text = ValidationContext.Global.MessageProvider.GetText(labelKey);

				if (!string.IsNullOrWhiteSpace(text))
                    Label.Content = text;

                if (Label.Content != null)
                {
                    Label.Content = Label.Content.ToString().TrimEnd('*').TrimEnd();
                    if (Field.IsRequired)
                        Label.Content = Label.Content + " *";
                }

	            var parent = Label.Parent as UIElement;
	            if (parent != null)
                    parent.Visibility = Field.IsVisible ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        void SetupControl()
        {
            if (Field != null && Control != null)
            {
                if (Field.MaxLength.HasValue && Control is TextBox)
                    ((TextBox)Control).MaxLength = Field.MaxLength.Value;

                if (Control is ToggleButton || Control is Separator)
                {
                    Control.Visibility = Field.IsVisible ? Visibility.Visible : Visibility.Collapsed;
                    return;
                }

	            var parent = Control.Parent as UIElement;
	            if (parent != null)
                    parent.Visibility = Field.IsVisible ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        void SetupTextBlocks()
        {
            if (TextBlock != null && Field != null)
                TextBlock.Visibility = Field.IsVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        private void WireupControls()
        {
            if (AutoWireup)
            {
                Control = Parent.FindVisualChildren<Control>(searchChildren: false).FirstOrDefault(c => !(c is Label));
                Label = Parent.FindVisualChildren<Label>().FirstOrDefault();
				if(string.IsNullOrWhiteSpace(FieldName))
					FieldName = Control?.Name;
            }
        }

    }
}

using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Cql.Wpf.Validate.Extensions;
using Cql.Wpf.Validate.ViewModel;

namespace Cql.Wpf.Validate.UI
{
	/// <summary>
	/// WPF control that wires up UI controls to a validation config.
	/// </summary>
    public class ViewValidatorControl : Panel
    {
		/// <summary>
		/// View configuration view model. This field is required in order for this control to be useful.
		/// </summary>
        public static readonly DependencyProperty ViewConfigProperty = DependencyProperty.Register("ViewConfig", typeof(ViewConfigViewModel), typeof(ViewValidatorControl), new PropertyMetadata(null, ViewConfigChanged));

        private static void ViewConfigChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var validator = d as ViewValidatorControl;
            validator?.WireupValidators();
        }

        public ViewValidatorControl()
        {
            Loaded += ViewValidator_Loaded;
        }

        private void ViewValidator_Loaded(object sender, RoutedEventArgs e)
        {
            WireupValidators();
        }

        private void WireupValidators()
        {
            if (ViewConfig != null)
            {
                var validators = Parent.FindVisualChildren<FieldValidatorControl>().ToList();
                var controls = Parent.FindVisualChildren<Control>().ToList();
                var labels = Parent.FindVisualChildren<Label>().ToList();
                var textblocks = Parent.FindVisualChildren<TextBlock>().ToList();

                foreach (var field in ViewConfig.Fields)
                {
                    var validator = validators.FirstOrDefault(v => v.FieldName == field.Name);
                    if (validator != null)
                    {
                        validator.Field = field;
                        continue;
                    }

                    var control = controls.FirstOrDefault(c => c.Name == field.Name || c.Name == field.ShareLabelWith);
                    if (control != null)
                    {
                        var label = labels.FirstOrDefault(l => Equals(l.Target, control));
                        validator = new FieldValidatorControl
                        {
                            AutoWireup = false,
                            Field = field,
                            Control = control,
                            Label = label
                        };
                        Children.Add(validator);
                        continue;
                    }

                    var textblock = textblocks.FirstOrDefault(t => t.Name == field.Name);
                    if (textblock != null)
                    {
                        validator = new FieldValidatorControl
                        {
                            TextBlock = textblock,
                            Field = field,
                            AutoWireup = false
                        };
                        Children.Add(validator);
                    }

                }

                foreach (var validator in validators.Where(v => v.AutoWireup && v.FieldName != null))
                {
                    var field = ViewConfig.Fields.FirstOrDefault(f => f.Name == validator.FieldName);
                    if (field != null)
                    {
                        validator.Field = field;
                    }
                }

            }
        }

	    /// <summary>
	    /// View configuration view model. This field is required in order for this control to be useful.
	    /// </summary>
		public ViewConfigViewModel ViewConfig
        {
            get { return (ViewConfigViewModel)GetValue(ViewConfigProperty); }
            set { SetValue(ViewConfigProperty, value); }
        }
    }
}
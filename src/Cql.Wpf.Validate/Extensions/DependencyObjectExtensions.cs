using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Cql.Wpf.Validate.Extensions
{
	/// <summary>
	/// Contains extension methods on <see cref="DependencyObject"/>.
	/// </summary>
    public static class DependencyObjectExtensions
    {
		/// <summary>
		/// Find controls of type <see cref="T"/> in the Visual Tree.
		/// </summary>
		/// <typeparam name="T">Type of control to find</typeparam>
		/// <param name="depObj">Context to search within</param>
		/// <param name="searchChildren">True to recursively search children.</param>
		/// <returns>All matching objects found.</returns>
        public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject depObj, bool searchChildren = true) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    var child = VisualTreeHelper.GetChild(depObj, i);
	                var children = child as T;
	                if (children != null)
                    {
                        yield return children;
                    }

                    if (searchChildren)
                    {
                        foreach (var childOfChild in FindVisualChildren<T>(child))
                        {
                            yield return childOfChild;
                        }
                    }
                }
            }
        }

	    /// <summary>
	    /// Find first instance of a control of type <see cref="T"/> in the Visual Tree.
	    /// </summary>
	    /// <typeparam name="T">Type of control to find</typeparam>
	    /// <param name="depObj">Context to search within</param>
	    /// <param name="searchChildren">True to recursively search children.</param>
	    /// <returns>First instance found. Null if none found.</returns>
		public static T FindVisualChild<T>(this DependencyObject depObj, bool searchChildren = true) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    var child = VisualTreeHelper.GetChild(depObj, i);
	                var visualChild = child as T;
	                if (visualChild != null)
                    {
                        return visualChild;
                    }

                    if (searchChildren)
                    {
                        foreach (var childOfChild in FindVisualChildren<T>(child))
                        {
                            return childOfChild;
                        }
                    }
                }
            }
            return null;
		}

		/// <summary>
		/// Sets keyboard focus on a <see cref="UIElement"/>.
		/// </summary>
		/// <param name="element">Element to focus.</param>
		/// <param name="tryAgain">True to make a second attempt if the element did not receive focus. (This happens sometimes!)</param>
		/// <returns></returns>
	    public static async Task<IInputElement> SetKeyboardFocus(this UIElement element, bool tryAgain = true)
	    {
		    if (element != null)
		    {
			    element.Focus();
			    Keyboard.Focus(element);
		    }
		    var focused = Keyboard.FocusedElement;

		    if (tryAgain && !ReferenceEquals(focused, element))
		    {
			    //try again
			    await Task.Delay(100).ContinueWith(t =>
			    {
				    Application.Current.Dispatcher.Invoke(async () =>
				    {
					    await element.SetKeyboardFocus(false);
				    });
			    });
		    }

		    return Keyboard.FocusedElement;
	    }
	}
}

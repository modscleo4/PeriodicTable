using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace PeriodicTable.UWP
{
    public sealed class GridElement : Control
    {
        #region Properties

        public static readonly DependencyProperty AtomicNumberProperty;
        public string AtomicNumber
        {
            get
            {
                return (string)GetValue(AtomicNumberProperty);
            }

            set
            {
                SetValue(AtomicNumberProperty, value);
            }
        }

        public static readonly DependencyProperty SymbolProperty;
        public string Symbol
        {
            get
            {
                return (string)GetValue(SymbolProperty);
            }

            set
            {
                SetValue(SymbolProperty, value);
            }
        }

        #endregion Properties

        public GridElement()
        {
            this.DefaultStyleKey = typeof(GridElement);
        }

        static GridElement()
        {
            AtomicNumberProperty = DependencyProperty.Register("AtomicNumber", typeof(string), typeof(GridElement), new PropertyMetadata("Number"));
            SymbolProperty = DependencyProperty.Register("Symbol", typeof(string), typeof(GridElement), new PropertyMetadata("Sym"));
        }
    }
}

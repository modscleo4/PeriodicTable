using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PeriodicTable.WPF.Controls
{
    public class GridElement : Control
    {
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

        public GridElement() : base()
        {
            MouseDoubleClick += new MouseButtonEventHandler(GridElement_MouseDoubleClick);
        }

        private void GridElement_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new ElementDetails(Convert.ToInt32(AtomicNumber)).ShowDialog();
        }

        static GridElement()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GridElement), new FrameworkPropertyMetadata(typeof(GridElement)));

            AtomicNumberProperty = DependencyProperty.Register("AtomicNumber", typeof(string), typeof(GridElement), new FrameworkPropertyMetadata("Number"));
            SymbolProperty = DependencyProperty.Register("Symbol", typeof(string), typeof(GridElement), new FrameworkPropertyMetadata("Sym"));
        }
    }
}

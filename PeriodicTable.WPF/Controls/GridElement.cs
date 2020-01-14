using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PeriodicTable.WPF.Controls
{
    public class GridElement : Control
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

        public bool Clickable
        {
            get; set;
        }

        #endregion Properties

        #region Events

        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(GridElement));

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        protected virtual void OnClick()
        {
            RoutedEventArgs args = new RoutedEventArgs(ClickEvent, this);

            RaiseEvent(args);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            OnClick();
        }

        #endregion Events

        public GridElement() : base()
        {
            Click += new RoutedEventHandler(GridElement_Click);
        }

        private void GridElement_Click(object sender, RoutedEventArgs e)
        {
            if (Clickable)
            {
                new ElementDetails(Convert.ToInt32(AtomicNumber)).ShowDialog();
            }
        }

        static GridElement()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GridElement), new FrameworkPropertyMetadata(typeof(GridElement)));

            AtomicNumberProperty = DependencyProperty.Register("AtomicNumber", typeof(string), typeof(GridElement), new FrameworkPropertyMetadata("Number"));
            SymbolProperty = DependencyProperty.Register("Symbol", typeof(string), typeof(GridElement), new FrameworkPropertyMetadata("Sym"));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeriodicTable.MobileApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GridElement : ContentView
    {
        public static readonly BindableProperty BorderColorProperty;
        public Color BorderColor
        {
            get
            {
                return (Color)GetValue(BorderColorProperty);
            }

            set
            {
                SetValue(BorderColorProperty, value);
            }
        }

        public static readonly BindableProperty SymbolProperty;
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

        public GridElement()
        {
            InitializeComponent();
        }

        static GridElement()
        {
            BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(GridElement), Color.FromHex("A4CAEC"), BindingMode.OneWay);
            SymbolProperty = BindableProperty.Create(nameof(Symbol), typeof(string), typeof(GridElement), "Sy", BindingMode.OneWay);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == BorderColorProperty.PropertyName)
            {
                Border.BackgroundColor = BorderColor;
            }
            else if (propertyName == SymbolProperty.PropertyName)
            {
                LabelSymbol.Text = Symbol;
            }
        }
    }
}
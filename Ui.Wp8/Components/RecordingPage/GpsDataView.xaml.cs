using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Ui.Wp8.Components.RecordingPage
{
    public partial class GpsDataView : UserControl
    {
        public GpsDataView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(double),
                typeof(GpsDataView),
                new PropertyMetadata(0d, OnValueChanged));
 
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
 
        private static void OnValueChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (GpsDataView)d;
            double oldValue = (double)e.OldValue;
            double newValue = target.Value;
            //target.OnValueChanged(oldValue, newValue);
        }

        //private void OnValueChanged(
        //    double oldValue, double newValue)
        //{
        //    ValueString = newValue.ToString("F1");
        //}
        
        //public static readonly DependencyProperty ValueStringProperty =
        //    DependencyProperty.Register(
        //        "ValueString",
        //        typeof(string),
        //        typeof(GpsDataView),
        //        new PropertyMetadata("0"));
 
        //public string ValueString
        //{
        //    get { return (string)GetValue(ValueStringProperty); }
        //    set { SetValue(ValueStringProperty, value); }
        //}
    }
}
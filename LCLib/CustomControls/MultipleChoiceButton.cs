using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LCLib.CustomControls
{
    public class MultipleChoiceButton : Button
    {
        public static readonly DependencyProperty NumberProperty = DependencyProperty.Register("Number", typeof(string), typeof(MultipleChoiceButton), new UIPropertyMetadata(""));
        public static readonly DependencyProperty ChoiceProperty = DependencyProperty.Register("Choice", typeof(string), typeof(MultipleChoiceButton), new UIPropertyMetadata(""));
        static MultipleChoiceButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultipleChoiceButton), new FrameworkPropertyMetadata(typeof(MultipleChoiceButton)));
        }
        public string Number
        {
            get { return (string)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }
        public string Choice
        {
            get { return (string)GetValue(ChoiceProperty); }
            set { SetValue(ChoiceProperty, value); }
        }
    }
}

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
    public class JavaButton : Button
    {
        static JavaButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(JavaButton), new FrameworkPropertyMetadata(typeof(JavaButton)));
        }
    }
    public class CppButton : Button
    {
        static CppButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CppButton), new FrameworkPropertyMetadata(typeof(CppButton)));
        }

    }
    public class HtmlButton : Button
    {
        static HtmlButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HtmlButton), new FrameworkPropertyMetadata(typeof(HtmlButton)));
        }
    }
    public class JsButton : Button
    {
        static JsButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(JsButton), new FrameworkPropertyMetadata(typeof(JsButton)));
        }
    }
    public class PhpButton : Button
    {
        static PhpButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PhpButton), new FrameworkPropertyMetadata(typeof(PhpButton)));
        }
    }
}

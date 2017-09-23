using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Controls;
namespace LCLib.Functions
{
    public static class StoryboardExtensions
    {
        static Storyboard sb = new Storyboard();
        static DoubleAnimation da = new DoubleAnimation();
        static int trans = 500;
        /// <summary>
        /// Shows the Control
        /// </summary>
        /// <param name="obj"></param>
        public static void ShowObject(this UIElement ctrl)
        {
            sb = new Storyboard();
            da = new DoubleAnimation();

            da.From = 0;
            da.To = 1;
            da.Duration = new Duration(TimeSpan.FromMilliseconds(Transition));
            ctrl.IsEnabled = true;
            ctrl.Visibility = Visibility.Visible;
            sb.Children.Add(da);
            Storyboard.SetTarget(da, ctrl);
            Storyboard.SetTargetProperty(da, new PropertyPath(Control.OpacityProperty));
            sb.Begin();
        }
        /// <summary>
        /// Hides the Control
        /// </summary>
        /// <param name="obj"></param>
        public static void HideObject(this UIElement ctrl)
        {
            sb = new Storyboard();
            da = new DoubleAnimation();

            da.From = 1;
            da.To = 0;
            da.Duration = new Duration(TimeSpan.FromMilliseconds(Transition));
            sb.Children.Add(da);
            Storyboard.SetTarget(da, ctrl);
            Storyboard.SetTargetProperty(da, new PropertyPath(Control.OpacityProperty));
            sb.Completed += (s, a) =>
            {
                ctrl.Visibility = Visibility.Hidden;
                ctrl.IsEnabled = false;
            };
            sb.Begin();
        }
        /// <summary>
        /// Changes the Current Control into the Next Control
        /// </summary>
        /// <param name="cobj"></param>
        /// <param name="nobj"></param>
        public static void NextObject(this UIElement cctrl, UIElement nctrl)
        {
            cctrl.HideObject();
            sb.Completed += (s, a) =>
            {
                nctrl.ShowObject();
            };
        }

        /// <summary>
        /// Shows the Border
        /// </summary>
        /// <param name="brd"></param>
        public static void ShowBorder(this System.Windows.Controls.Border brd)
        {
            sb = new Storyboard();
            da = new DoubleAnimation();

            da.From = 0;
            da.To = 1;
            da.Duration = new Duration(TimeSpan.FromMilliseconds(Transition));
            brd.IsEnabled = true;
            brd.Visibility = Visibility.Visible;
            sb.Children.Add(da);
            Storyboard.SetTarget(da, brd);
            Storyboard.SetTargetProperty(da, new PropertyPath(Control.OpacityProperty));
            sb.Begin();
        }
        /// <summary>
        /// Hides the Border
        /// </summary>
        /// <param name="brd"></param>
        public static void HideBorder(this System.Windows.Controls.Border brd)
        {
            sb = new Storyboard();
            da = new DoubleAnimation();

            da.From = 1;
            da.To = 0;
            da.Duration = new Duration(TimeSpan.FromMilliseconds(Transition));
            sb.Children.Add(da);
            Storyboard.SetTarget(da, brd);
            Storyboard.SetTargetProperty(da, new PropertyPath(Control.OpacityProperty));
            sb.Completed += (s, a) =>
            {
                brd.Visibility = Visibility.Hidden;
                brd.IsEnabled = false;
            };
            sb.Begin();
        }
        public static int Transition
        {
            get { return trans; }
            set { trans = value; }
        }
    }
}

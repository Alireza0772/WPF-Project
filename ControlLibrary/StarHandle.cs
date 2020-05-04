using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ControlLibrary
{
    [TemplatePart(Name = "PART_Body", Type = typeof(Grid))]
    [TemplatePart(Name = "PART_Boundary", Type = typeof(Ellipse))]
    [TemplatePart(Name = "PART_Handle", Type = typeof(Path))]
    public class StarHandle : Control
    {
        public static readonly DependencyProperty XProperty = DependencyProperty.Register("X", typeof(float), typeof(StarHandle), new PropertyMetadata(0.00f));
        public static readonly DependencyProperty YProperty = DependencyProperty.Register("Y", typeof(float), typeof(StarHandle), new PropertyMetadata(0.00f));
        public static readonly DependencyProperty IsHandleGrabbedProperty = DependencyProperty.Register("IsHandleGrabbed", typeof(bool), typeof(StarHandle), new PropertyMetadata(false));

        private Grid partBody;
        private Ellipse partBoundary;
        private Path partHandle;
        private double sin;
        private double cos;

        public bool IsHandleGrabbed
        {
            get { return (bool)GetValue(IsHandleGrabbedProperty); }
            private set { SetValue(IsHandleGrabbedProperty, value); }
        }
        public float X
        {
            get { return (float)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }
        public float Y
        {
            get { return (float)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        static StarHandle()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StarHandle), new FrameworkPropertyMetadata(typeof(StarHandle)));
        }
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            IsHandleGrabbed = true;
            SetCoordination(e.GetPosition(partBody));
            SetHandlePosition();
        }
        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            if (IsHandleGrabbed)
            {
                SetCoordination(e.GetPosition(partBody));
                SetHandlePosition();
            }
        }
        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            IsHandleGrabbed = false;
            X = 0;
            Y = 0;
            SetHandlePosition();
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            partBody = GetTemplateChild("Part_Body") as Grid;
            partBoundary = GetTemplateChild("Part_Boundary") as Ellipse;
            partHandle = GetTemplateChild("Part_Handle") as Path;
        }
        private void SetCoordination(Point point)
        {
            double xprime = (point.X - (partBody.ActualWidth / 2)) / (partBoundary.ActualWidth / 2 - 25);
            double yprime = (point.Y - (partBody.ActualWidth / 2)) / (partBoundary.ActualWidth / 2 - 25);
            sin = yprime / Math.Sqrt(Math.Pow(xprime, 2) + Math.Pow(yprime, 2));
            cos = xprime / Math.Sqrt(Math.Pow(xprime, 2) + Math.Pow(yprime, 2));
            if (Math.Sqrt(Math.Pow(xprime, 2) + Math.Pow(yprime, 2)) > 1)
            {
                xprime = cos;
                yprime = sin;
            }
            X = (float)xprime;
            Y = (-1) * (float)yprime;
        }
        private void SetHandlePosition()
        {
            partHandle.Margin = new Thickness
            {
                Left = X * (partBoundary.ActualWidth / 2 - 25),
                Top = (-1) * Y * (partBoundary.ActualHeight / 2 - 25)
            };
        }
    }
}

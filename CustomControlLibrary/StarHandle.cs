using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomControlLibrary
{
	[TemplatePart(Name = "PART_Body", Type = typeof(Grid))]
	[TemplatePart(Name = "PART_Boundary", Type = typeof(Ellipse))]
	[TemplatePart(Name = "PART_Handle", Type = typeof(Path))]
	public class StarHandle : Control
	{
		public static readonly DependencyProperty XProperty = DependencyProperty.Register("X", typeof(float), typeof(StarHandle), new PropertyMetadata(0.00f));
		public static readonly DependencyProperty YProperty = DependencyProperty.Register("Y", typeof(float), typeof(StarHandle), new PropertyMetadata(0.00f));

		private Grid partBody;
		private Ellipse partBoundary;
		private Path partHandle;
		private bool isHandleGrabbed = false;
		private double bodySize;
		private double boundarySize;
		private double sin;
		private double cos;

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
			isHandleGrabbed = true;
			SetCoordination(e.GetPosition(partBody));
			SetHandlePosition();
		}
		protected override void OnPreviewMouseMove(MouseEventArgs e)
		{
			if (isHandleGrabbed)
			{
				SetCoordination(e.GetPosition(partBody));
				SetHandlePosition();
			}
		}
		protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
		{
			isHandleGrabbed = false;
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
		protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
		{
			base.OnRenderSizeChanged(sizeInfo);
			bodySize = partBody.Width;
			boundarySize = partBoundary.Width;
		}
		private void SetCoordination(Point point)
		{
			double xprime = (point.X - (bodySize / 2)) / (boundarySize / 2 - 25);
			double yprime = (point.Y - (bodySize / 2)) / (boundarySize / 2 - 25);
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
				Left = X * (boundarySize / 2 - 25),
				Top = (-1) * Y * (boundarySize / 2 - 25)
			};
		}
	}
}

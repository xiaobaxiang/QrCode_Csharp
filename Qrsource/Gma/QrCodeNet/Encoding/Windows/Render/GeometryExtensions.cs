namespace Gma.QrCodeNet.Encoding.Windows.Render
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    internal static class GeometryExtensions
    {
        internal static void DrawRectGeometry(this StreamGeometryContext ctx, Int32Rect rect)
        {
            if (!rect.IsEmpty)
            {
                ctx.BeginFigure(new Point((double) rect.X, (double) rect.Y), true, true);
                ctx.LineTo(new Point((double) rect.X, (double) (rect.Y + rect.Height)), false, false);
                ctx.LineTo(new Point((double) (rect.X + rect.Width), (double) (rect.Y + rect.Height)), false, false);
                ctx.LineTo(new Point((double) (rect.X + rect.Width), (double) rect.Y), false, false);
            }
        }
    }
}


using Microsoft.Maui.Controls.Internals;
using Syncfusion.Maui.Charts;

namespace AddVectorImage
{
    public class ColumnSeriesExt : ColumnSeries
    {
        protected override ChartSegment CreateSegment()
        {
            return new ColumnSegmentExt();
        }
    }

    public class ColumnSegmentExt : ColumnSegment
    {
        protected override void Draw(ICanvas canvas)
        {
            base.Draw(canvas);

            if (Series is ColumnSeriesExt && Series.BindingContext is ViewModel viewModel)
            {
                canvas.DrawImage(viewModel.Data[Index].Image, Left, Top, Right - Left, Bottom - Top);
            }
        }
    }
}

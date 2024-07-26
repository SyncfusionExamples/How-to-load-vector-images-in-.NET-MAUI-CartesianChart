# How to load vector images in .NET MAUI CartesianChart

In [.NET MAUI Cartesian Chart](https://www.syncfusion.com/maui-controls/maui-cartesian-charts), you can load and display vector images by customizing the chart series. This guide will walk you through the process to load vector images using Syncfusion’s SfCartesianChart.
Imagine you want to visualize data with custom images representing each data point in the chart. To achieve this, you need to create a custom series that draws vector images on the chart segments.

**Step 1: Create Custom Series and Segment**
To display vector images in the chart, create custom series and segment classes.

**Custom Series**

Create a custom series class by inheriting from the [ColumnSeries](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Charts.ColumnSeries.html?tabs=tabid-1) class. Override the CreateSegment method to return a custom segment.

**[C#]**
```
public class ColumnSeriesExt : ColumnSeries
{
    protected override ChartSegment CreateSegment()
    {
        return new ColumnSegmentExt();
    }
}
```
**Custom Segment**

Create a custom segment class by inheriting from the [ColumnSegment](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Charts.ColumnSegment.html) class. Override the Draw method to draw images on the chart segments.

**[C#]**
```
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
```

**Step 2: Initialize the SfCartesianChart**

Refer to the [documentation](https://help.syncfusion.com/maui/cartesian-charts/getting-started) for detailed steps on initializing the SfCartesianChart.

The following code illustrates how to initialize the SfCartesianChart with custom series `ColumnSeriesExt`.

**[XAML]**
```
<chart:SfCartesianChart Title="Age Vs. Height">

    <chart:SfCartesianChart.XAxes>
        <chart:NumericalAxis ShowMajorGridLines="False" Interval="2">
            <chart:NumericalAxis.Title>
                <chart:ChartAxisTitle Text="Age(Years)" ></chart:ChartAxisTitle>
            </chart:NumericalAxis.Title>
        </chart:NumericalAxis>
    </chart:SfCartesianChart.XAxes>

    <chart:SfCartesianChart.YAxes>
        <chart:NumericalAxis ShowMajorGridLines="False">
            <chart:NumericalAxis.Title>
                <chart:ChartAxisTitle Text="Height(cm)"></chart:ChartAxisTitle>
            </chart:NumericalAxis.Title>
        </chart:NumericalAxis>
    </chart:SfCartesianChart.YAxes>

    <local:ColumnSeriesExt ItemsSource="{Binding Data}" 
                           XBindingPath="Age" 
                           YBindingPath="Height" 
                           Fill="Transparent"
                           Width="0.6"/>

</chart:SfCartesianChart>
```
**[C#]**
```
SfCartesianChart chart = new SfCartesianChart();
chart.Title = "Age Vs. Height";

NumericalAxis primaryAxis = new NumericalAxis();
primaryAxis.Interval = 2;
primaryAxis.ShowMajorGridLines = false;
primaryAxis.Title = new ChartAxisTitle();
primaryAxis.Title.Text = "Age(Years)";
chart.XAxes.Add(primaryAxis);

NumericalAxis secondaryAxis = new NumericalAxis();
secondaryAxis.ShowMajorGridLines= false;
secondaryAxis.Title = new ChartAxisTitle();
secondaryAxis.Title.Text = "Height(cm)";
chart.YAxes.Add(secondaryAxis);

ColumnSeriesExt series = new ColumnSeriesExt();
series.ItemsSource = (new ViewModel()).Data;
series.XBindingPath = "Age";
series.YBindingPath = "Height";
series.Fill = Colors.Transparent;
series.Width = 0.6;

chart.Series.Add(series);
this.Content = chart;
```
**View Model**

Load the data and the vector image to be displayed in the chart.
```
public class ViewModel
{
    public ObservableCollection<Model> Data { get; set; }

    public ViewModel()
    {
        Data = new ObservableCollection<Model>()
        {
            new Model() { Age = 12, Height = 135, Image = GetImage("age12")},
            new Model() { Age = 14, Height = 165, Image = GetImage("age14")},
            new Model() { Age = 16, Height = 175, Image = GetImage("age16")},
            new Model() { Age = 18, Height = 185, Image = GetImage("age18")},
        };
    }

    private Microsoft.Maui.Graphics.IImage? GetImage(string resourcePath)
    {
        Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;

        using Stream? stream = assembly.GetManifestResourceStream("AddVectorImage.Resources.Images." + resourcePath + ".png"); ;

        if (stream != null)
        {
            return PlatformImage.FromStream(stream);
        }

        return null;
    }
}
```
**Output**
 ![AddVectorImage.png](https://support.syncfusion.com/kb/agent/attachment/article/16708/inline?token=eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjI3MDg5Iiwib3JnaWQiOiIzIiwiaXNzIjoic3VwcG9ydC5zeW5jZnVzaW9uLmNvbSJ9.zutyau7kcblji3rkXtNTsWkheDi5KMgPSlrICHFboo4)
using Microsoft.Maui.Graphics.Platform;
using System.Collections.ObjectModel;
using System.Reflection;

namespace AddVectorImage
{
    public class ViewModel
    {
        public ObservableCollection<Model> Data { get; set; }

        public Microsoft.Maui.Graphics.IImage? Image { get; set; }

        public ViewModel()
        {
            Data = new ObservableCollection<Model>()
            {
                new Model() { Age = 12, Height = 135, Image = GetImage("AddVectorImage.Resources.Images.age12.png")},
                new Model() { Age = 14, Height = 165, Image = GetImage("AddVectorImage.Resources.Images.age14.png")},
                new Model() { Age = 16, Height = 175, Image = GetImage("AddVectorImage.Resources.Images.age16.png")},
                new Model() { Age = 18, Height = 185, Image = GetImage("AddVectorImage.Resources.Images.age18.png")},
            };
        }

        private Microsoft.Maui.Graphics.IImage? GetImage(string resourcePath)
        {
            Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;

            using Stream? stream = assembly.GetManifestResourceStream(resourcePath);

            if (stream != null)
            {
                return PlatformImage.FromStream(stream);
            }

            return null;
        }
    }
}

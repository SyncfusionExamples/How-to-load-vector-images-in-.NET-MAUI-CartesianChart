using Microsoft.Maui.Graphics.Platform;
using System.Collections.ObjectModel;
using System.Reflection;

namespace AddVectorImage
{
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
}

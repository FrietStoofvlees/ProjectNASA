using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IImage = Microsoft.Maui.Graphics.IImage;

namespace ProjectNasa.ViewModel
{
    [QueryProperty(nameof(Apod), "Apod")]
    public partial class ImageViewModel : BaseViewModel
    {
        [ObservableProperty]
        Apod apod;

        [RelayCommand]
        void DownloadImage() 
        {
            //IImage image;

            //Assembly assembly = GetType().GetTypeInfo().Assembly;

            //using Stream stream = assembly.GetManifestResourceStream(Apod.Hdurl);
            //image = PlatformImage.FromStream(stream);

            //if (image != null)
            //{
            //    using MemoryStream memStream = new();
            //    image.Save(memStream);
            //}
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RestTMDB
{
    public partial class MainPage : ContentPage
    {
        readonly ObservableCollection<Data.Result> peliculas = new ObservableCollection<Data.Result>();
        readonly Data.PeliculasManager manager = new Data.PeliculasManager();
        public MainPage()
        {
            BindingContext = peliculas;
            InitializeComponent();
        }

        private async void CountriesSearchBar_SearchButtonPressed(object sender, EventArgs e)
        {

            peliculas.Clear();

            if (IsBusy)
                return;

            try
            {

                IsBusy = true;
                var peliculaCollection = await manager.GetAll(CountriesSearchBar.Text);

                imagenLogoPelicula.IsVisible = false;

                imagenCargar.IsVisible = true;

                await imagenCargar.RotateTo(360, 2000);
                imagenCargar.Rotation = 0;

                foreach (Data.Result pelicula in peliculaCollection.results)
                {
                    pelicula.poster_path = "https://image.tmdb.org/t/p/w500" + pelicula.poster_path;
                    
                    pelicula.release_date = "(" + pelicula.release_date + ")";
                    
                    peliculas.Add(pelicula);

                }

            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Error",
                        ex.Message,
                        "OK");
                imagenLogoPelicula.IsVisible = true;
            }
            finally
            {
                IsBusy = false;
                imagenCargar.IsVisible = false;
            }

        }
    }
}

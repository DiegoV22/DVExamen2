using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace DVExamen2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                RadioButton rb = sender as RadioButton;
                DisplayAlert("Monto seleccionado", $"Has seleccionado {rb.Content} unidades.", "OK");
            }
        }

        private async void OnRecargarClicked(object sender, EventArgs e)
        {
            string numero = entDV.Text;
            string operador = pickOperadorDV.SelectedItem?.ToString();
            string monto = null;

            if (rb3DV.IsChecked) monto = "3";
            else if (rb5DV.IsChecked) monto = "5";
            else if (rb10DV.IsChecked) monto = "10";

            bool confirm = await DisplayAlert("Confirmar Recarga", $"Número: {numero}\nOperador: {operador}\nMonto: {monto} unidades\n¿Desea continuar?", "Sí", "No");
            if (confirm)
            {
                await Task.Delay(2000);

                string fecha = DateTime.Now.ToString("dd/MM/yy");
                string contenido = $"Se hizo una recarga de {monto} dólares en la siguiente fecha: {fecha}";
                string rutaArchivo = Path.Combine(FileSystem.AppDataDirectory, $"{numero}.txt");

                File.WriteAllText(rutaArchivo, contenido);

                await DisplayAlert("Recarga Exitosa", "Su recarga ha sido realizada con éxito.", "OK");

                string contenidoArchivo = File.ReadAllText(rutaArchivo);
                await DisplayAlert("Contenido del Archivo", contenidoArchivo, "OK");

                await DisplayAlert("Gracias", "Gracias por usar nuestro servicio de recargas.", "OK");
            }
        }
    }
}

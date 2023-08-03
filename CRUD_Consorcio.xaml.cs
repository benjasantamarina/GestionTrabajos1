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
using System.Windows.Shapes;

namespace GestionTrabajos1
{
    /// <summary>
    /// Lógica de interacción para CRUD_Consorcio.xaml
    /// </summary>
    public partial class CRUD_Consorcio : Window
    {
        private DataClasses1DataContext dataContext;
        public CRUD_Consorcio(DataClasses1DataContext context)
        {
            InitializeComponent();

            dataContext = context;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();

            string cuit = textcuit.Text;
            string nom = textnombre.Text;
            string dir = textdireccion.Text;
            string sut = textsuterh.Text;

            mainWindow.createConsorcio(cuit, nom, dir, sut);

            MessageBox.Show("Registro ingresado con exito");

            textcuit.Text = "";
            textnombre.Text = "";
            textdireccion.Text = "";
            textsuterh.Text = "";
        }


        public void LoadConsorciosNames()
        {
            try
            {
                // Utilizar LINQ para obtener los nombres de la tabla 'consorcios'
                var nombres = from consorcio in dataContext.Consorcio
                              select consorcio.Nombre;

                // Limpiar el ComboBox antes de cargar los nuevos datos
                comboBoxConsorcios.Items.Clear();

                // Cargar los nombres en el ComboBox
                foreach (var nombre in nombres)
                {
                    comboBoxConsorcios.Items.Add(nombre);

                    
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, muestra un mensaje o realiza alguna acción en caso de error
                MessageBox.Show(ex.Message);
            }
        }


        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            LoadConsorciosNames();
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            (Application.Current.MainWindow as MainWindow)?.SetCRUDConsorcioInstance(null);
        }

       /* private void textcuit_LostFocus(object sender, RoutedEventArgs e)
        {
            string cuitText = textcuit.Text;

            while (true)
            {
                // Verificar si el CUIT tiene 11 dígitos y los primeros 2 números cumplen con las condiciones requeridas
                if (cuitText.Length == 11 && (cuitText.StartsWith("30") || cuitText.StartsWith("33")))
                {
                    try
                    {
                        long cuitNum = long.Parse(cuitText);
                        break;
                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show($"{ex.Message}");

                        textcuit.Text = "";

                        break;
                    }
                }
                else
                {
                    MessageBox.Show("Error, debe ingresar un CUIT correcto");

                    textcuit.Text = "";

                    break;
                }

            }

        }*/

        private void comboBoxConsorcios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Obtener el nombre seleccionado en el ComboBox
            string nombreSeleccionado = comboBoxConsorcios.SelectedItem as string;

            if (nombreSeleccionado != null)
            {
                // Realizar la consulta para obtener los datos del consorcio seleccionado
                var consorcioSeleccionado = dataContext.Consorcio.FirstOrDefault(consorcio => consorcio.Nombre == nombreSeleccionado);

                // Mostrar los datos en los TextBox
                if (consorcioSeleccionado != null)
                {
                    textcuit.Text = consorcioSeleccionado.CUIT;
                    textnombre.Text = consorcioSeleccionado.Nombre;
                    textdireccion.Text = consorcioSeleccionado.Direccion;
                    textsuterh.Text = consorcioSeleccionado.ClaveSuterh;
                }
                else
                {
                    // Si el consorcio no se encontró en la base de datos, limpiar los TextBox
                    textcuit.Text = "";
                    textnombre.Text = "";
                    textdireccion.Text = "";
                    textsuterh.Text = "";
                }
            }
        }
        /*
        private void textnombre_GotFocus(object sender, RoutedEventArgs e)
        {
            string cuit = textcuit.Text;
           
            if (cuit == "") { textcuit.Focus(); }
        }*/
    }
}

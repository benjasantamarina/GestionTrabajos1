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
using System.Configuration;

namespace GestionTrabajos1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataClasses1DataContext dataContext;

        CRUD_Consorcio windowCC;

        public MainWindow()
        {
            InitializeComponent();

            string miConexion = ConfigurationManager.ConnectionStrings["GestionTrabajos1.Properties.Settings.AdministracionConnectionString"].ConnectionString;

            dataContext = new DataClasses1DataContext(miConexion);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // Crear una nueva instancia de CRUD_Consorcio solo si no existe o si ya se ha cerrado
            if (windowCC == null)
            {
                windowCC = new CRUD_Consorcio(dataContext);
                // Asignar el evento Closed para establecer la instancia en null cuando se cierre la ventana
                windowCC.Closed += (s, args) => windowCC = null;

                // Mostrar la ventana CRUD_Consorcio
                windowCC.Show();
            }

            else
            {
                // Si ya existe una instancia activa, traerla al frente (focus)
                windowCC.Focus();
            }

        }

        public void SetCRUDConsorcioInstance(CRUD_Consorcio instance)
        {
            windowCC = instance;
        }



        public void createConsorcio(string cuit, string nom, string dir, string suterh)
        {
            Consorcio consorcio = new Consorcio(){ CUIT = cuit, Nombre = nom, Direccion = dir, ClaveSuterh = suterh };

            dataContext.Consorcio.InsertOnSubmit(consorcio);

            dataContext.SubmitChanges();
        }


    }
}

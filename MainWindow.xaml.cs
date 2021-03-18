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

namespace CRUD_Linq
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataClasses1DataContext dataConext;
        public MainWindow()
        {
            InitializeComponent();

            // Conexión con el origen de datos
            string miConexion = ConfigurationManager.ConnectionStrings["CRUD_Linq.Properties.Settings.CrudLinqSql"].ConnectionString;

            //Crear la conexion con el datacontent
            dataConext = new DataClasses1DataContext(miConexion);

            InsertarEmpresa();
            InsertEmpleados();

        }

        public void InsertarEmpresa()
        {
            dataConext.ExecuteCommand("delete from Empresa");//Borra el contenido de la DB por medio del comando delete
            Empresa pildorasInformaticas = new Empresa(); //PildorasInformaticas es el nombre del objeto no de la empresa
            pildorasInformaticas.Nombre = "Pildoras informaticas"; // Se le pone el nombre al atributo de la clase empresa(aun no se guarda en la base de datos)

            dataConext.Empresa.InsertOnSubmit(pildorasInformaticas);//Por medio del dataconext se guarda en la tabla EMEPRESA el objeto pildoras onfromaticas

            Empresa google = new Empresa(); //PildorasInformaticas es el nombre del objeto no de la empresa
            google.Nombre = "Google"; // Se le pone el nombre al atributo de la clase empresa(aun no se guarda en la base de datos)

            dataConext.Empresa.InsertOnSubmit(google);

            dataConext.SubmitChanges();
            Principal.ItemsSource = dataConext.Empresa;
        }

        public void InsertEmpleados()
        {
            // Crea un objeto llamada pildorasInformaticas de tipo Empresa, la cual se llame "Pildoras informaticas" por medio de expressiones lambda
            Empresa pildorasInformaticas = dataConext.Empresa.First(em => em.Nombre.Equals("Pildoras informaticas"));

            Empresa google = dataConext.Empresa.First(em => em.Nombre.Equals("Google"));

            List<Empleado> listaEmpleados = new List<Empleado>();


            listaEmpleados.Add(new Empleado { Nombre = "Juan", Apellido = "Diaz", EmpresaId  = pildorasInformaticas.Id });
            listaEmpleados.Add(new Empleado { Nombre = "Ana", Apellido = "Martin", EmpresaId = google.Id });
            listaEmpleados.Add(new Empleado { Nombre = "Maria", Apellido = "Lopez", EmpresaId = google.Id });
            listaEmpleados.Add(new Empleado { Nombre = "Antonio", Apellido = "Fernandez", EmpresaId = pildorasInformaticas.Id });

            dataConext.Empleado.InsertAllOnSubmit(listaEmpleados);
            dataConext.SubmitChanges();

            Principal.ItemsSource = dataConext.Empleado;


        }
    }
}

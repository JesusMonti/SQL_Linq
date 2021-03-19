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

            //InsertarEmpresa();
            //InsertEmpleados();
            //InsertarCargos();
            InsertarEmpleadoCargo();
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

        public void InsertarEmpleadoCargo()
        {
            // Forma 1 de Agregar los cargos de manera individual sin conocer los ID
            /*Empleado Juan = dataConext.Empleado.First(em => em.Nombre.Equals("Juan"));
            Empleado Ana = dataConext.Empleado.First(em => em.Nombre.Equals("Ana"));

            Cargo cAdminstrador = dataConext.Cargo.First(cg => cg.NombreCargo.Equals("Administrador"));
            Cargo cContador = dataConext.Cargo.First(cg => cg.NombreCargo.Equals("Contador"));

            CargoEmpleado cargoJuan = new CargoEmpleado();
            cargoJuan.CargoId = cAdminstrador.Id;
            cargoJuan.Empleado = Juan;

            CargoEmpleado cargoAna = new CargoEmpleado();
            cargoAna.CargoId = cContador.Id;
            cargoAna.Empleado = Ana;*/

            // Forma de 2, conciendo o son pocos los Id a tratar
            /*
            List<CargoEmpleado> listaCargoEmpleados = new List<CargoEmpleado>();

            listaCargoEmpleados.Add(new CargoEmpleado { CargoId = 1, EmpleadoId = 3});
            listaCargoEmpleados.Add(new CargoEmpleado { CargoId = 2, EmpleadoId = 4});
            */

            //Forma 3, sin conocer los Id y usando listas
            /*
            Empleado Ana = dataConext.Empleado.First(em => em.Nombre.Equals("Ana"));
            Empleado Maria = dataConext.Empleado.First(em => em.Nombre.Equals("Maria"));
            Empleado Antonio = dataConext.Empleado.First(em => em.Nombre.Equals("Antonio"));

            Cargo cAdminstrador = dataConext.Cargo.First(cg => cg.NombreCargo.Equals("Administrador"));
            Cargo cContador = dataConext.Cargo.First(cg => cg.NombreCargo.Equals("Contador"));

            List<CargoEmpleado> listaCargoEmpleados = new List<CargoEmpleado>();

            listaCargoEmpleados.Add(new CargoEmpleado { Empleado = Ana, Cargo= cContador});
            listaCargoEmpleados.Add(new CargoEmpleado { Empleado = Maria, Cargo = cAdminstrador});
            listaCargoEmpleados.Add(new CargoEmpleado { Empleado = Antonio, Cargo = cContador});

            dataConext.CargoEmpleado.InsertAllOnSubmit(listaCargoEmpleados);
            */
            // Forma 4, sin conocer los Id y usando listas pero sin instanciar los objetos
            List<CargoEmpleado> listaCargoEmpleados = new List<CargoEmpleado>();

            listaCargoEmpleados.Add(new CargoEmpleado { Empleado = dataConext.Empleado.First(em => em.Nombre.Equals("Ana")),
                Cargo = dataConext.Cargo.First(cg => cg.NombreCargo.Equals("Contador"))});
            listaCargoEmpleados.Add(new CargoEmpleado
            {
                Empleado = dataConext.Empleado.First(em => em.Nombre.Equals("Maria")),
                Cargo = dataConext.Cargo.First(cg => cg.NombreCargo.Equals("Administrador"))
            });
            listaCargoEmpleados.Add(new CargoEmpleado
            {
                Empleado = dataConext.Empleado.First(em => em.Nombre.Equals("Antonio")),
                Cargo = dataConext.Cargo.First(cg => cg.NombreCargo.Equals("Contador"))
            });


            dataConext.CargoEmpleado.InsertAllOnSubmit(listaCargoEmpleados);

            // dataConext.CargoEmpleado.InsertOnSubmit(cargoJuan);
            // dataConext.CargoEmpleado.InsertOnSubmit(cargoAna);
            dataConext.SubmitChanges();

            Principal.ItemsSource = dataConext.CargoEmpleado;

        }
        public void InsertarCargos()
        {
            
            List<Cargo> listaCargos = new List<Cargo>();

            listaCargos.Add(new Cargo { NombreCargo= "Administrador"});
            listaCargos.Add(new Cargo { NombreCargo = "Contador" });

            dataConext.Cargo.InsertAllOnSubmit(listaCargos);
            dataConext.SubmitChanges();

            Principal.ItemsSource = dataConext.Cargo;
        }
    }
}

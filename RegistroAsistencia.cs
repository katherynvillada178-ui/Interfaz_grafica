namespace interfazgrafica
{
   //Clase padre
    public class Persona
    {
        public string Documento { get; set; } //ger leer , set guardar
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }

        //Constructor vacio
        public Persona() { }

        //Constructor
        public Persona(string documento, string nombre, string apellido, DateTime fechaNacimiento)
        {
            Documento = documento;
            Nombre = nombre;
            Apellido = apellido;
            FechaNacimiento = fechaNacimiento;
        }

        // Polimorfismo
        //"Virtual" Puede ser modificada por una clase hija pero en el padre no funcionara, sera falso
        public virtual bool PuedeEliminar(string usuario, string clave)
        {
            return false;
        }
    }

    // clase que hereda
    public class Empleado : Persona
    {
        public string Cargo { get; set; }
        public bool Activo { get; set; }

        public Empleado()
        {
            Activo = true;
        }

        public Empleado(string documento, string nombre, string apellido, DateTime fechaNacimiento, string cargo)
            : base(documento, nombre, apellido, fechaNacimiento)
        {
            Cargo = cargo;
            Activo = true;
        }
    }

    // Clase que hereda
    public class AdministradorRH : Empleado
    {
        public string Usuario { get; set; }
        public string Clave { get; set; }

        public AdministradorRH(string documento, string nombre, string apellido, DateTime fechaNacimiento, string usuario, string clave)
            : base(documento, nombre, apellido, fechaNacimiento, "Gestión Humana")
        {
            Usuario = usuario;
            Clave = clave;
        }

        // Polimorfismo
        public override bool PuedeEliminar(string usuario, string clave)
        {
            if (Usuario == usuario && Clave == clave)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    // Clase registro asistencia
    public class RegistroAsistencia
    {
        public string Documento { get; set; }
        public string NombreCompleto { get; set; }
        public DateTime HoraEntrada { get; set; }
        public DateTime HoraSalida { get; set; }
        public bool YaSalio { get; set; }
        public double HorasDiurnas { get; set; }
        public double HorasNocturnas { get; set; }

        public RegistroAsistencia(string documento, string nombreCompleto, DateTime horaEntrada)
        {
            Documento = documento;
            NombreCompleto = nombreCompleto;
            HoraEntrada = horaEntrada;
            YaSalio = false;
            HorasDiurnas = 0;
            HorasNocturnas = 0;
        }

        // Calcula horas diurnas (6:00 a. m. a 7:00 p. m.) y nocturnas (7:00 p. m. a 6:00 a. m.)
        public void CalcularHoras()
        {
            DateTime actual = HoraEntrada;
            double segDiurnos = 0;
            double segNocturnos = 0;

            while (actual < HoraSalida)
            {
                if (actual.Hour >= 6 && actual.Hour < 19)
                {
                    segDiurnos = segDiurnos + 1;
                }
                else
                {
                    segNocturnos = segNocturnos + 1;
                }
                actual = actual.AddSeconds(1);
            }

            HorasDiurnas = Math.Round(segDiurnos / 3600, 4);//segundos (3600), 4(decimales)
            HorasNocturnas = Math.Round(segNocturnos / 3600, 4);
        }
    }

    // Clase Sistema Asistencia / diseno
    public class SistemaAsistencia
    {
        // Vector con los cargos permitidos
        public string[] cargos = { "Operario", "Analista", "Supervisor", "Gestión Humana" };

        // Matriz
        public double[,] matrizHorasPorCargo = new double[4, 2];

        // Listas para guardar empleados y asistencias
        public List<Empleado> listaEmpleados = new List<Empleado>();
        public List<RegistroAsistencia> listaRegistros = new List<RegistroAsistencia>();

        // Administrador de prueba (usuario: admin | clave: 1234)
        public AdministradorRH adminRH = new AdministradorRH("1000", "Admin", "RH", new DateTime(1990, 1, 1), "admin", "1234");

        // Contador de intentos rechazados
        public int intentosRechazados = 0;

        public SistemaAsistencia()
        {
            CargarEmpleadosTxt();
            CargarRegistrosTxt();
        }

        public Empleado BuscarEmpleado(string documento)
        {
            foreach (Empleado emp in listaEmpleados)
            {
                if (emp.Documento == documento)
                {
                    return emp;
                }
            }
            return null;
        }

        // Funcionalidad 1: Registrar empleado
        public string RegistrarEmpleado(string documento, string nombre, string apellido, DateTime fechaNac, string cargo)
        {
            if (BuscarEmpleado(documento) != null)
            {
                return "Error: Ya existe un empleado con ese documento.";
            }

            Empleado nuevo = new Empleado(documento, nombre, apellido, fechaNac, cargo);
            listaEmpleados.Add(nuevo);
            GuardarEmpleadosTxt();

            return "Empleado registrado con éxito.";
        }

        // Funcionalidad 2: Inactivar empleado (solo autorizados)
        public string InactivarEmpleado(string documentoAEliminar, Persona quienIntenta, string usuario, string clave)
        {
            if (quienIntenta.PuedeEliminar(usuario, clave) == false)
            {
                return "Acceso denegado: Solo personal autorizado de Gestión Humana puede hacer esto.";
            }

            Empleado emp = BuscarEmpleado(documentoAEliminar);
            if (emp == null)
            {
                return "No se encontró ningún empleado con ese documento.";
            }

            emp.Activo = false;
            GuardarEmpleadosTxt();
            return "El empleado " + emp.Nombre + " " + emp.Apellido + " fue inactivado.";
        }

        // Funcionalidad 3: Registrar llegada automática
        public string RegistrarLlegada(string documento)
        {
            Empleado emp = BuscarEmpleado(documento);

            if (emp == null)
            {
                intentosRechazados = intentosRechazados + 1;
                return "Rechazado: El documento no existe.";
            }

            if (emp.Activo == false)
            {
                intentosRechazados = intentosRechazados + 1;
                return "Rechazado: El empleado está inactivo.";
            }

            foreach (RegistroAsistencia reg in listaRegistros)
            {
                if (reg.Documento == documento && reg.YaSalio == false)
                {
                    intentosRechazados = intentosRechazados + 1;
                    return "Rechazado: Este empleado ya marcó llegada y no ha salido.";
                }
            }

            DateTime horaActual = DateTime.Now;
            string nombreCompleto = emp.Nombre + " " + emp.Apellido;
            RegistroAsistencia nuevoReg = new RegistroAsistencia(documento, nombreCompleto, horaActual);
            listaRegistros.Add(nuevoReg);
            GuardarRegistrosTxt();

            return "Llegada registrada para " + nombreCompleto + " a las " + horaActual.ToShortTimeString();
        }

        // Funcionalidad 4: Registrar salida automática
        public string RegistrarSalida(string documento)
        {
            Empleado emp = BuscarEmpleado(documento);

            if (emp == null || emp.Activo == false)
            {
                intentosRechazados = intentosRechazados + 1;
                return "Rechazado: El empleado no existe o está inactivo.";
            }

            foreach (RegistroAsistencia reg in listaRegistros)
            {
                if (reg.Documento == documento && reg.YaSalio == false)
                {
                    reg.HoraSalida = DateTime.Now;
                    reg.YaSalio = true;
                    reg.CalcularHoras();
                    GuardarRegistrosTxt();

                    return "Salida registrada para " + reg.NombreCompleto +
                           "\nHoras diurnas: " + reg.HorasDiurnas +
                           "\nHoras nocturnas: " + reg.HorasNocturnas;
                }
            }

            intentosRechazados = intentosRechazados + 1;
            return "Rechazado: No tiene una llegada registrada previamente.";
        }

        // Funcionalidad 5: Generar reporte con ciclos, contadores, acumuladores, banderas, switch y matriz
        public string GenerarReporte()
        {
            int totalValidos = 0;
            double sumaDiurnas = 0;
            double sumaNocturnas = 0;
            bool haySinSalida = false;
            bool huboRechazados = false;

            // Reiniciamos la matriz en ceros antes de calcular
            matrizHorasPorCargo = new double[4, 2];

            foreach (RegistroAsistencia reg in listaRegistros)
            {
                totalValidos = totalValidos + 1;

                if (reg.YaSalio == true)
                {
                    sumaDiurnas = sumaDiurnas + reg.HorasDiurnas;
                    sumaNocturnas = sumaNocturnas + reg.HorasNocturnas;

                    // Llenamos la matriz usando switch según el cargo del empleado
                    Empleado emp = BuscarEmpleado(reg.Documento);
                    if (emp != null)
                    {
                        int fila = 0;
                        switch (emp.Cargo)
                        {
                            case "Operario": fila = 0; break;
                            case "Analista": fila = 1; break;
                            case "Supervisor": fila = 2; break;
                            case "Gestión Humana": fila = 3; break;
                        }

                        matrizHorasPorCargo[fila, 0] = matrizHorasPorCargo[fila, 0] + reg.HorasDiurnas;
                        matrizHorasPorCargo[fila, 1] = matrizHorasPorCargo[fila, 1] + reg.HorasNocturnas;
                    }
                }
                else
                {
                    haySinSalida = true;
                }
            }

            if (intentosRechazados > 0)
            {
                huboRechazados = true;
            }

            string reporte = " REPORTE GESTIÓN HUMANA \r\n" +
                             "Registros válidos: " + totalValidos + "\r\n" +
                             "Intentos rechazados: " + intentosRechazados + "\r\n" +
                             "Total horas diurnas: " + Math.Round(sumaDiurnas, 2) + "\r\n" +
                             "Total horas nocturnas: " + Math.Round(sumaNocturnas, 2) + "\r\n";

            // Recorremos la matriz con un ciclo for para mostrar las horas por cada cargo
            reporte = reporte + "\r\n--- HORAS POR CARGO (MATRIZ) ---\r\n";
            for (int i = 0; i < 4; i++)
            {
                reporte = reporte + cargos[i] + " -> Diurnas: " + Math.Round(matrizHorasPorCargo[i, 0], 2) +
                          " | Nocturnas: " + Math.Round(matrizHorasPorCargo[i, 1], 2) + "\r\n";
            }

            if (haySinSalida == true)
            {
                reporte = reporte + "\r\nAVISO: Quedaron empleados sin marcar salida.";
            }

            if (huboRechazados == true)
            {
                reporte = reporte + "\r\nAVISO: Hubo intentos de registro rechazados.";
            }

            return reporte;
        }

        // Guardar y cargar empleados en empleados.txt
        public void GuardarEmpleadosTxt()
        {
            StreamWriter archivo = new StreamWriter("empleados.txt");
            foreach (Empleado emp in listaEmpleados)
            {
                string linea = emp.Documento + "|" + emp.Nombre + "|" + emp.Apellido + "|" +
                               emp.FechaNacimiento.ToShortDateString() + "|" + emp.Cargo + "|" + emp.Activo;
                archivo.WriteLine(linea);
            }
            archivo.Close();
        }

        public void CargarEmpleadosTxt()
        {
            if (File.Exists("empleados.txt") == false)
            {
                return;
            }

            listaEmpleados.Clear();
            string[] lineas = File.ReadAllLines("empleados.txt");

            foreach (string linea in lineas)
            {
                if (linea != "")
                {
                    string[] datos = linea.Split('|');
                    if (datos.Length == 6)
                    {
                        Empleado emp = new Empleado(
                            datos[0],
                            datos[1],
                            datos[2],
                            Convert.ToDateTime(datos[3]),
                            datos[4]
                        );
                        emp.Activo = Convert.ToBoolean(datos[5]);
                        listaEmpleados.Add(emp);
                    }
                }
            }
        }

        // Guardar y cargar registros en registros.txt
        public void GuardarRegistrosTxt()
        {
            StreamWriter archivo = new StreamWriter("registros.txt");
            foreach (RegistroAsistencia reg in listaRegistros)
            {
                string linea = reg.Documento + "|" + reg.NombreCompleto + "|" +
                               reg.HoraEntrada.ToString("yyyy-MM-dd HH:mm:ss") + "|" +
                               reg.HoraSalida.ToString("yyyy-MM-dd HH:mm:ss") + "|" +
                               reg.YaSalio + "|" + reg.HorasDiurnas + "|" + reg.HorasNocturnas;
                archivo.WriteLine(linea);
            }
            archivo.Close();
        }

        public void CargarRegistrosTxt()
        {
            if (File.Exists("registros.txt") == false)
            {
                return;
            }

            listaRegistros.Clear();
            string[] lineas = File.ReadAllLines("registros.txt");

            foreach (string linea in lineas)
            {
                if (linea != "")
                {
                    string[] datos = linea.Split('|');
                    if (datos.Length == 7)
                    {
                        RegistroAsistencia reg = new RegistroAsistencia(
                            datos[0],
                            datos[1],
                            Convert.ToDateTime(datos[2])
                        );
                        reg.HoraSalida = Convert.ToDateTime(datos[3]);
                        reg.YaSalio = Convert.ToBoolean(datos[4]);
                        reg.HorasDiurnas = Convert.ToDouble(datos[5]);
                        reg.HorasNocturnas = Convert.ToDouble(datos[6]);
                        listaRegistros.Add(reg);
                    }
                }
            }
        }
    }
}
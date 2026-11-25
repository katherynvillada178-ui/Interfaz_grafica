namespace interfazgrafica
{   //Guia Unidad 3 Parte_1
    public class Diseno : Form
    {
        private readonly SistemaAsistencia sistema = new();
        private readonly TabControl tabs = new() { Dock = DockStyle.Fill };

        // Pestaña 1
        private readonly Label lblDoc = new() { Text = "Documento:", AutoSize = true, ForeColor = Color.White };
        private readonly TextBox txtDoc = new() { Width = 180 };
        private readonly Label lblNombre = new() { Text = "Nombre:", AutoSize = true, ForeColor = Color.White };
        private readonly TextBox txtNombre = new() { Width = 180 };
        private readonly Label lblApellido = new() { Text = "Apellido:", AutoSize = true, ForeColor = Color.White };
        private readonly TextBox txtApellido = new() { Width = 180 };
        private readonly Label lblFechaNac = new() { Text = "Fecha Nac.:", AutoSize = true, ForeColor = Color.White };
        private readonly DateTimePicker dtpFechaNac = new() { Format = DateTimePickerFormat.Short, Width = 180 };
        private readonly Label lblCargo = new() { Text = "Cargo:", AutoSize = true, ForeColor = Color.White };
        private readonly ComboBox cboCargo = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 180 };
        private readonly Button btnRegistrar = new() { Text = "Registrar Empleado", AutoSize = true, BackColor = Color.DodgerBlue, ForeColor = Color.White };

        private readonly Label lblDocElim = new() { Text = "Doc. a inactivar:", AutoSize = true, ForeColor = Color.White };
        private readonly TextBox txtDocEliminar = new() { Width = 180 };
        private readonly RadioButton rbAdminRH = new() { Text = "Admin RH", Checked = true, AutoSize = true, ForeColor = Color.Cyan };
        private readonly RadioButton rbEmpleado = new() { Text = "Empleado", AutoSize = true, ForeColor = Color.White };
        private readonly Label lblUser = new() { Text = "Usuario RH:", AutoSize = true, ForeColor = Color.White };
        private readonly TextBox txtUsuarioRH = new() { Width = 180 };
        private readonly Label lblClave = new() { Text = "Clave RH:", AutoSize = true, ForeColor = Color.White };
        private readonly TextBox txtClaveRH = new() { PasswordChar = '*', Width = 180 };
        private readonly Label lblHint = new() { Text = "Prueba: admin / 1234", AutoSize = true, ForeColor = Color.Cyan };
        private readonly Button btnInactivar = new() { Text = "Inactivar Empleado", AutoSize = true, BackColor = Color.Crimson, ForeColor = Color.White };
        private readonly DataGridView dgvEmpleados = new() { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, BackgroundColor = Color.DarkSlateGray };

        // Pestaña 2
        private readonly Label lblHora = new() { Text = "Hora:", AutoSize = true, ForeColor = Color.Cyan, Font = new Font("Consolas", 14, FontStyle.Bold) }; // = formato de tipo y tamaño letra
        private readonly Label lblDocMarc = new() { Text = "Documento:", AutoSize = true, ForeColor = Color.White };
        private readonly TextBox txtDocMarcacion = new() { Width = 220 };
        private readonly Label lblNombreConf = new() { Text = "Esperando documento...", AutoSize = true, ForeColor = Color.LightGray };
        private readonly Button btnLlegada = new() { Text = "Registrar Llegada", AutoSize = true, BackColor = Color.SeaGreen, ForeColor = Color.White };
        private readonly Button btnSalida = new() { Text = "Registrar Salida", AutoSize = true, BackColor = Color.DarkOrange, ForeColor = Color.White };

        // Pestaña 3
        private readonly DataGridView dgvRegistros = new() { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, BackgroundColor = Color.DarkSlateGray };
        private readonly Button btnReporte = new() { Text = "Generar Reporte Final", AutoSize = true, BackColor = Color.Indigo, ForeColor = Color.White };
        private readonly TextBox txtReporte = new() { Dock = DockStyle.Fill, Multiline = true, ReadOnly = true, BackColor = Color.Black, ForeColor = Color.Cyan };

        private readonly ErrorProvider errorProvider1 = new();
        //reloj del pc
        private readonly System.Windows.Forms.Timer timer1 = new() { Enabled = true, Interval = 1000 };

        public Diseno()
        {
            Text = "Registro de Asistencia Laboral";
            Width = 900;
            Height = 620;
            Font = new Font("Segoe UI", 10); //tipo y tamaño de letra
            StartPosition = FormStartPosition.CenterScreen;
            errorProvider1.ContainerControl = this;

            // --- PESTAÑA 1 ---
            TabPage tab1 = new("1. Empleados") { BackColor = Color.MidnightBlue };
            TableLayoutPanel tablaIzq = new() { Dock = DockStyle.Fill, ColumnCount = 2 };
            tablaIzq.Controls.Add(lblDoc, 0, 0); tablaIzq.Controls.Add(txtDoc, 1, 0); //documento; ____________
            tablaIzq.Controls.Add(lblNombre, 0, 1); tablaIzq.Controls.Add(txtNombre, 1, 1);
            tablaIzq.Controls.Add(lblApellido, 0, 2); tablaIzq.Controls.Add(txtApellido, 1, 2);
            tablaIzq.Controls.Add(lblFechaNac, 0, 3); tablaIzq.Controls.Add(dtpFechaNac, 1, 3);
            tablaIzq.Controls.Add(lblCargo, 0, 4); tablaIzq.Controls.Add(cboCargo, 1, 4);
            tablaIzq.Controls.Add(btnRegistrar, 1, 5);

            FlowLayoutPanel panelRol = new() { AutoSize = true };
            panelRol.Controls.Add(rbAdminRH);
            panelRol.Controls.Add(rbEmpleado);

            TableLayoutPanel tablaDer = new() { Dock = DockStyle.Fill, ColumnCount = 2 };
            tablaDer.Controls.Add(lblDocElim, 0, 0); tablaDer.Controls.Add(txtDocEliminar, 1, 0);
            tablaDer.Controls.Add(panelRol, 1, 1);
            tablaDer.Controls.Add(lblUser, 0, 2); tablaDer.Controls.Add(txtUsuarioRH, 1, 2);
            tablaDer.Controls.Add(lblClave, 0, 3); tablaDer.Controls.Add(txtClaveRH, 1, 3);
            tablaDer.Controls.Add(lblHint, 0, 4); tablaDer.Controls.Add(btnInactivar, 1, 4);

            TableLayoutPanel arriba = new() { Dock = DockStyle.Fill, ColumnCount = 2 };//diseño
            arriba.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            arriba.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            arriba.Controls.Add(tablaIzq, 0, 0);
            arriba.Controls.Add(tablaDer, 1, 0);

            TableLayoutPanel layout1 = new() { Dock = DockStyle.Fill, RowCount = 2, Padding = new Padding(10) };//diseño
            layout1.RowStyles.Add(new RowStyle(SizeType.Absolute, 250));
            layout1.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout1.Controls.Add(arriba, 0, 0);
            layout1.Controls.Add(dgvEmpleados, 0, 1);
            tab1.Controls.Add(layout1);

            // --- PESTAÑA 2 ---
            TabPage tab2 = new("2. Llegada y Salida") { BackColor = Color.MidnightBlue };
            FlowLayoutPanel botones = new() { AutoSize = true };
            botones.Controls.Add(btnLlegada);
            botones.Controls.Add(btnSalida);

            TableLayoutPanel layout2 = new() { Dock = DockStyle.Fill, Padding = new Padding(25) };
            layout2.Controls.Add(lblHora, 0, 0);
            layout2.Controls.Add(lblDocMarc, 0, 1);
            layout2.Controls.Add(txtDocMarcacion, 0, 2);
            layout2.Controls.Add(lblNombreConf, 0, 3);
            layout2.Controls.Add(botones, 0, 4);
            tab2.Controls.Add(layout2);

            // --- PESTAÑA 3 ---
            TabPage tab3 = new("3. Reporte RH") { BackColor = Color.MidnightBlue };
            TableLayoutPanel layout3 = new() { Dock = DockStyle.Fill, RowCount = 3, Padding = new Padding(10) };
            layout3.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            layout3.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout3.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            layout3.Controls.Add(dgvRegistros, 0, 0);
            layout3.Controls.Add(btnReporte, 0, 1);
            layout3.Controls.Add(txtReporte, 0, 2);
            tab3.Controls.Add(layout3);

            tabs.Controls.Add(tab1);
            tabs.Controls.Add(tab2);
            tabs.Controls.Add(tab3);
            Controls.Add(tabs);

            // Eventos
            Load += (s, e) => { cboCargo.Items.AddRange(sistema.cargos); ActualizarTablas(); };
            timer1.Tick += (s, e) => lblHora.Text = "Hora: " + DateTime.Now.ToString("hh:mm:ss tt");//hora del sistema
            btnRegistrar.Click += BtnRegistrar_Click;
            btnInactivar.Click += BtnInactivar_Click;
            txtDocMarcacion.TextChanged += TxtDocMarcacion_TextChanged;//guardar informacion
            btnLlegada.Click += (s, e) => { MessageBox.Show(sistema.RegistrarLlegada(txtDocMarcacion.Text)); ActualizarTablas(); };
            btnSalida.Click += (s, e) => { MessageBox.Show(sistema.RegistrarSalida(txtDocMarcacion.Text)); ActualizarTablas(); };
            btnReporte.Click += (s, e) => { ActualizarTablas(); txtReporte.Text = sistema.GenerarReporte(); };
        }
            //condicionales de informacion
        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            bool error = false;
            if (txtDoc.Text == "") { errorProvider1.SetError(txtDoc, "Falta documento"); error = true; }
            if (txtNombre.Text == "") { errorProvider1.SetError(txtNombre, "Falta nombre"); error = true; }
            if (txtApellido.Text == "") { errorProvider1.SetError(txtApellido, "Falta apellido"); error = true; }
            if (cboCargo.Text == "") { errorProvider1.SetError(cboCargo, "Elija cargo"); error = true; }
            if (sistema.BuscarEmpleado(txtDoc.Text) != null) { MessageBox.Show("Documento repetido"); error = true; }
            if (error) return;

            //guarda la informacion
            MessageBox.Show(sistema.RegistrarEmpleado(txtDoc.Text, txtNombre.Text, txtApellido.Text, dtpFechaNac.Value, cboCargo.Text));
            ActualizarTablas();//actualiza la informacion en las tablas
            txtDoc.Clear(); txtNombre.Clear(); txtApellido.Clear(); cboCargo.SelectedIndex = -1;//se limpian las casillas para nueva informacion
        }

        private void BtnInactivar_Click(object sender, EventArgs e)
        {
            Persona p = rbAdminRH.Checked ? sistema.adminRH : new Empleado();//condicion ? a =(o) b
            MessageBox.Show(sistema.InactivarEmpleado(txtDocEliminar.Text, p, txtUsuarioRH.Text, txtClaveRH.Text));
            ActualizarTablas();
        }

        private void TxtDocMarcacion_TextChanged(object sender, EventArgs e)//sistema autonomo
        {
            Empleado emp = sistema.BuscarEmpleado(txtDocMarcacion.Text);
            if (emp == null) { lblNombreConf.Text = "No encontrado"; lblNombreConf.ForeColor = Color.LightGray; }
            else if (emp.Activo) { lblNombreConf.Text = emp.Nombre + " " + emp.Apellido + " (Activo)"; lblNombreConf.ForeColor = Color.Lime; }
            else { lblNombreConf.Text = emp.Nombre + " " + emp.Apellido + " (INACTIVO)"; lblNombreConf.ForeColor = Color.Salmon; }
        }

        private void ActualizarTablas()
        {
            dgvEmpleados.DataSource = null; dgvEmpleados.DataSource = sistema.listaEmpleados;
            dgvRegistros.DataSource = null; dgvRegistros.DataSource = sistema.listaRegistros;
        }
    }
}
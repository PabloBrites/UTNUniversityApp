using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;// Incluye las clases generales para manejar datos y tablas en .NET
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //esto accede a una libreria para usar sql server. 


namespace BaseDeDatos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //aca llamamos al metodo
            TraerAlumnos(); 

        }

        //este metodo lo creamos nosotros y pasamos lo de form1 a TraerAlumnos
        private void TraerAlumnos()
        {
            //Proveedor y base de datos variable string
            string proveedorBD = "Data Source=.\\SQLEXPRESS;Initial Catalog=BD_Instituto;Integrated Security = True";
            SqlConnection conexion = new SqlConnection(proveedorBD); //clase que conecta a la base de datos

            //Variable de tipo datos en forma de tabla
            //adentro de esta variable se ponen los datos que viene de la BD
            DataTable tablaMemoria = new DataTable();

            string consulta = "SELECT * FROM ALUMNO";


            try
            {
                conexion.Open();//si esta todo bien abre mi base de datos

                //Manejar los datos
                // permite que yo use comandos como  SELECT, INSERT, UPDATE y DELETE.
                SqlCommand comando = new SqlCommand(consulta, conexion);//paso la consulta y la conexion

                //este lector uso la consulta del comando
                SqlDataReader lector = comando.ExecuteReader();

                //Metodo que carga en memoria lo que tiene el lector 
                tablaMemoria.Load(lector);

                //DataSource es una propiedad que se utiliza para enlazar datos a un control como un DataGridView
                //lo que tenga tablaMemoria lo carga adentro de dgvUsuarios.DataSource que es una grilla 
                //cargamos grilla con datos de memoria
                dgvUsuarios.DataSource = tablaMemoria;
                //minuto 4:20

                //oculta la columna estado que queda fea
                dgvUsuarios.Columns["estado"].Visible = false;
                dgvUsuarios.Columns["id_alumno"].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conexion.Close();
            }
        }

        private void dgvUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            txtApellido.Text = Convert.ToString(dgvUsuarios.CurrentRow.Cells[3].Value);
            txtNombre.Text = Convert.ToString(dgvUsuarios.CurrentRow.Cells[2].Value);
            txtDni.Text = Convert.ToString(dgvUsuarios.Rows[e.RowIndex].Cells["dni"].Value);
            txtEmail.Text = Convert.ToString(dgvUsuarios.Rows[e.RowIndex].Cells["email"].Value);
            txtTelefono.Text = Convert.ToString(dgvUsuarios.Rows[e.RowIndex].Cells["telefono"].Value);
            dtpFechaIngreso.Value = Convert.ToDateTime(dgvUsuarios.Rows[e.RowIndex].Cells["fecha_ingreso"].Value);
        }

        private void btnAgregarAlumno_Click(object sender, EventArgs e)
        {
            agregarAlumno();
        }

        private void agregarAlumno()
        {
            // Obtener los datos de los controles TextBox y DateTimePicker
            string dni = txtDni.Text;
            string nombre = txtNombre.Text;
            string apellido = txtApellido.Text;
            string email = txtEmail.Text;
            string telefono = txtTelefono.Text;
            DateTime fechaIngreso = dtpFechaIngreso.Value;
            int estado = 1;  // Asumiendo que "activo" corresponde a 1 en la base de datos

            // Proveedor de conexión
            string proveedorBD = "Data Source=.\\SQLEXPRESS;Initial Catalog=BD_Instituto;Integrated Security=True";
            SqlConnection conexion = new SqlConnection(proveedorBD);

            // Consulta SQL sin el campo id_alumno, ya que se autogenera en la base de datos
            string consulta = "INSERT INTO alumno (dni, nombre, apellido, email, telefono, fecha_ingreso, estado) " +
                              "VALUES (@dni, @nombre, @apellido, @email, @telefono, @fecha_ingreso, @estado); " +
                              "SELECT SCOPE_IDENTITY();";

            SqlCommand comando = new SqlCommand(consulta, conexion);

            // Asignar parámetros a la consulta
            comando.Parameters.AddWithValue("@dni", dni);
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@apellido", apellido);
            comando.Parameters.AddWithValue("@email", email);
            comando.Parameters.AddWithValue("@telefono", telefono);
            comando.Parameters.AddWithValue("@fecha_ingreso", fechaIngreso);
            comando.Parameters.AddWithValue("@estado", estado);

            try
            {
                // Abrir la conexión y ejecutar la consulta
                conexion.Open();

                // Ejecutar la consulta y obtener el ID generado
                int idGenerado = Convert.ToInt32(comando.ExecuteScalar());

                // Mostrar un mensaje confirmando la adición y el ID generado
                MessageBox.Show("Alumno agregado correctamente");

                // Llamar a un método para actualizar la lista de alumnos si es necesario
                TraerAlumnos();
            }
            catch (Exception ex)
            {
                // Mostrar el error si ocurre uno
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                // Cerrar la conexión
                conexion.Close();
            }
        }

        private void btnEliminarAlumno_Click(object sender, EventArgs e)
        {
            // Mostrar un cuadro de confirmación antes de eliminar
            var result = MessageBox.Show("¿Estas seguro que queres eliminarlo?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                eliminarAlumno();
            }
            else
            {
                MessageBox.Show("Eliminacion cancelada.");
            }
        }

        private void eliminarAlumno()
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                // Usamos el nombre de columna correcto "id_alumno" aquí
                int idEliminar = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells["id_alumno"].Value);

                string proveedorBD = "Data Source=.\\SQLEXPRESS;Initial Catalog=BD_Instituto;Integrated Security=True";
                SqlConnection conexion = new SqlConnection(proveedorBD);
                string consulta = "DELETE FROM alumno WHERE id_alumno = @id";

                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@id", idEliminar);

                try
                {
                    conexion.Open();
                    int filasAfectadas = comando.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Alumno eliminado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("No se encontró un alumno con ese ID.");
                    }

                    TraerAlumnos();  // Actualizamos la lista después de la eliminación
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    conexion.Close();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un alumno de la lista para eliminar.");
            }


        }

        private void btnModificarAlumno_Click(object sender, EventArgs e)
        {
            // Mostrar un cuadro de diálogo de confirmación antes de modificar
            DialogResult resultado = MessageBox.Show("¿Deseas modificar este alumno?", "Confirmación de modificación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Verificar la respuesta del usuario
            if (resultado == DialogResult.Yes)
            {
                modificarAlumno();  // Llamar al método que modifica el alumno si elige "Sí"
            }
            else
            {
                MessageBox.Show("Modificación cancelada.");  // Mensaje si elige "No"
            }
        }

        private void modificarAlumno()
        {
            // Verificar que se haya seleccionado una fila en el DataGridView
            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un alumno de la lista.");
                return;  // Detenemos la ejecución si no se seleccionó un alumno
            }

            // Obtener el ID del alumno desde la fila seleccionada en el DataGridView
            int idAlumno = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["id_alumno"].Value);

            // Obtener los datos de los controles TextBox y DateTimePicker
            string dni = txtDni.Text;
            string nombre = txtNombre.Text;
            string apellido = txtApellido.Text;
            string email = txtEmail.Text;
            string telefono = txtTelefono.Text;
            DateTime fechaIngreso = dtpFechaIngreso.Value;
            int estado = 1;  // Asumiendo que "activo" corresponde a 1 en la base de datos

            // Proveedor de conexión
            string proveedorBD = "Data Source=.\\SQLEXPRESS;Initial Catalog=BD_Instituto;Integrated Security=True";
            SqlConnection conexion = new SqlConnection(proveedorBD);

            // Consulta SQL para actualizar el alumno
            string consulta = "UPDATE alumno SET dni = @dni, nombre = @nombre, apellido = @apellido, " +
                               "email = @email, telefono = @telefono, fecha_ingreso = @fecha_ingreso, estado = @estado " +
                               "WHERE id_alumno = @idAlumno";

            SqlCommand comando = new SqlCommand(consulta, conexion);

            // Asignar parámetros a la consulta
            comando.Parameters.AddWithValue("@dni", dni);
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@apellido", apellido);
            comando.Parameters.AddWithValue("@email", email);
            comando.Parameters.AddWithValue("@telefono", telefono);
            comando.Parameters.AddWithValue("@fecha_ingreso", fechaIngreso);
            comando.Parameters.AddWithValue("@estado", estado);
            comando.Parameters.AddWithValue("@idAlumno", idAlumno);  // Usamos el ID obtenido del DataGridView

            try
            {
                // Abrir la conexión y ejecutar la consulta
                conexion.Open();
                int filasAfectadas = comando.ExecuteNonQuery();

                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Alumno modificado correctamente.");
                    TraerAlumnos();  // Actualizamos la lista de alumnos
                }
                else
                {
                    MessageBox.Show("No se encontró un alumno con ese ID.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }
        }   
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Cerrar el formulario actual
            this.Close();
        }
    }
}



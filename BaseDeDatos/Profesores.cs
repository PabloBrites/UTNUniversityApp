using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseDeDatos
{
    public partial class Profesores : Form
    {
        public Profesores()
        {
            InitializeComponent();
        }

        private void Profesores_Load(object sender, EventArgs e)
        {
            // Llamamos al método TraerProfesores para cargar los datos al cargar el formulario
            TraerProfesores();
        }

        private void TraerProfesores()
        {
            string proveedorBD = "Data Source=.\\SQLEXPRESS;Initial Catalog=BD_Instituto;Integrated Security=True";
            SqlConnection conexion = new SqlConnection(proveedorBD);
            DataTable tablaMemoria = new DataTable();

            string consulta = "SELECT * FROM PROFESOR";

            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand(consulta, conexion);
                SqlDataReader lector = comando.ExecuteReader();
                tablaMemoria.Load(lector);
                dgvProfesores.DataSource = tablaMemoria;  // Asignamos los datos al DataGridView
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Obtener los datos de los TextBox
            int idProfesor;
            if (int.TryParse(txtProfesor.Text, out idProfesor))
            {
                string nombre = txtNombre.Text;
                string apellido = txtApellido.Text;
                string email = txtEmail.Text;
                string telefono = txtTelefono.Text;
                string estado = txtEstado.Text;

                string proveedorBD = "Data Source=.\\SQLEXPRESS;Initial Catalog=BD_Instituto;Integrated Security=True";

                using (SqlConnection conexion = new SqlConnection(proveedorBD))
                {
                    // Activar IDENTITY_INSERT
                    using (SqlCommand comando = new SqlCommand("SET IDENTITY_INSERT PROFESOR ON", conexion))
                    {
                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }

                    // Insertar los datos
                    string consulta = "INSERT INTO PROFESOR (id_profesor, nombre, apellido, email, telefono, estado) VALUES (@id_profesor, @nombre, @apellido, @email, @telefono, @estado)";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        // Asignar los valores a los parámetros de la consulta
                        comando.Parameters.AddWithValue("@id_profesor", idProfesor);
                        comando.Parameters.AddWithValue("@nombre", nombre);
                        comando.Parameters.AddWithValue("@apellido", apellido);
                        comando.Parameters.AddWithValue("@email", email);
                        comando.Parameters.AddWithValue("@telefono", telefono);
                        comando.Parameters.AddWithValue("@estado", estado);

                        try
                        {
                            int filasAfectadas = comando.ExecuteNonQuery();

                            if (filasAfectadas > 0)
                            {
                                MessageBox.Show("Profesor agregado correctamente.");
                                TraerProfesores(); // Actualiza el DataGridView
                                                   // Limpiar los TextBox
                                txtProfesor.Clear();
                                txtNombre.Clear();
                                txtApellido.Clear();
                                txtEmail.Clear();
                                txtTelefono.Clear();
                                txtEstado.Clear();
                            }
                            else
                            {
                                MessageBox.Show("No se pudo agregar el profesor.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al agregar profesor: " + ex.Message);
                        }
                        finally
                        {
                            using (SqlCommand comandoDesactivado = new SqlCommand("SET IDENTITY_INSERT PROFESOR OFF", conexion))
                            {
                                comandoDesactivado.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un número válido para el ID del profesor.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProfesores.SelectedRows.Count > 0)
            {
                DialogResult resultado = MessageBox.Show("¿Estás seguro de que quieres eliminar este profesor?", "Confirmar eliminación", MessageBoxButtons.YesNo);

                if (resultado == DialogResult.Yes)
                {
                    int idProfesor = Convert.ToInt32(dgvProfesores.SelectedRows[0].Cells["id_profesor"].Value);

                    string proveedorBD = "Data Source=.\\SQLEXPRESS;Initial Catalog=BD_Instituto;Integrated Security=True";

                    using (SqlConnection conexion = new SqlConnection(proveedorBD))
                    {
                        string consulta = "DELETE FROM PROFESOR WHERE id_profesor = @id_profesor";

                        using (SqlCommand comando = new SqlCommand(consulta, conexion))
                        {
                            comando.Parameters.AddWithValue("@id_profesor", idProfesor);

                            try
                            {
                                conexion.Open();
                                int filasAfectadas = comando.ExecuteNonQuery();

                                if (filasAfectadas > 0)
                                {
                                    MessageBox.Show("Profesor eliminado correctamente.");
                                    TraerProfesores(); // Actualiza el DataGridView
                                }
                                else
                                {
                                    MessageBox.Show("No se pudo eliminar el profesor.");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error al eliminar profesor: " + ex.Message);
                            }
                        }
                    }
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvProfesores.SelectedRows.Count > 0)
            {
                int idProfesor = Convert.ToInt32(dgvProfesores.SelectedRows[0].Cells["id_profesor"].Value);

                string nombre = txtNombre.Text;
                string apellido = txtApellido.Text;
                string email = txtEmail.Text;
                string telefono = txtTelefono.Text;
                string estado = txtEstado.Text;

                string proveedorBD = "Data Source=.\\SQLEXPRESS;Initial Catalog=BD_Instituto;Integrated Security=True";

                using (SqlConnection conexion = new SqlConnection(proveedorBD))
                {
                    string consulta = "UPDATE PROFESOR SET nombre = @nombre, apellido = @apellido, email = @email, telefono = @telefono, estado = @estado WHERE id_profesor = @id_profesor";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@id_profesor", idProfesor);
                        comando.Parameters.AddWithValue("@nombre", nombre);
                        comando.Parameters.AddWithValue("@apellido", apellido);
                        comando.Parameters.AddWithValue("@email", email);
                        comando.Parameters.AddWithValue("@telefono", telefono);
                        comando.Parameters.AddWithValue("@estado", estado);

                        try
                        {
                            conexion.Open();
                            int filasAfectadas = comando.ExecuteNonQuery();

                            if (filasAfectadas > 0)
                            {
                                MessageBox.Show("Profesor modificado correctamente.");
                                TraerProfesores(); // Actualiza el DataGridView
                            }
                            else
                            {
                                MessageBox.Show("No se pudo modificar el profesor.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al modificar profesor: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un profesor para modificar.");
            }
        }
    }     
}


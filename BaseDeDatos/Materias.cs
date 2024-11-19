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
    public partial class Materias : Form
    {
        public Materias()
        {
            InitializeComponent();
        }

        private void Materias_Load(object sender, EventArgs e)
        {
            TraerMaterias();
        }

        private void TraerMaterias()
        {
            string proveedorBD = "Data Source=.\\SQLEXPRESS;Initial Catalog=BD_Instituto;Integrated Security=True";
            SqlConnection conexion = new SqlConnection(proveedorBD);
            DataTable tablaMemoria = new DataTable();

            string consulta = "SELECT * FROM MATERIA";

            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand(consulta, conexion);
                SqlDataReader lector = comando.ExecuteReader();
                tablaMemoria.Load(lector);
                dgvMaterias.DataSource = tablaMemoria;  // Asignamos los datos al DataGridView
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
            int idMateria;
            if (int.TryParse(txtMateria.Text, out idMateria))
            {
                // El valor del TextBox es un entero válido
                int idProfesor = Convert.ToInt32(txtProfesor.Text);
                int idCarrera = Convert.ToInt32(txtCarrera.Text);
                string nombre = txtNombre.Text;
                string estado = txtEstado.Text;

                string proveedorBD = "Data Source=.\\SQLEXPRESS;Initial Catalog=BD_Instituto;Integrated Security=True";

                // Usando la instrucción using para asegurar que la conexión se cierre correctamente
                using (SqlConnection conexion = new SqlConnection(proveedorBD))
                {
                    // Activar IDENTITY_INSERT
                    using (SqlCommand comando = new SqlCommand("SET IDENTITY_INSERT MATERIA ON", conexion))
                    {
                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }

                    // Insertar los datos
                    string consulta = "INSERT INTO MATERIA (id_materia, id_profesor, id_carrera, nombre, estado) VALUES (@id_materia, @id_profesor, @id_carrera, @nombre, @estado)";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        // Asignar los valores a los parámetros de la consulta
                        comando.Parameters.AddWithValue("@id_materia", idMateria);
                        comando.Parameters.AddWithValue("@id_profesor", idProfesor);
                        comando.Parameters.AddWithValue("@id_carrera", idCarrera);
                        comando.Parameters.AddWithValue("@nombre", nombre);
                        comando.Parameters.AddWithValue("@estado", estado);

                        try
                        {
                            //conexion.Open(); // Abre la conexión dentro del bloque try
                            int filasAfectadas = comando.ExecuteNonQuery();

                            if (filasAfectadas > 0)
                            {
                                MessageBox.Show("Materia agregada correctamente.");
                                TraerMaterias();
                                // Limpiar los TextBox
                                txtMateria.Clear();
                                txtProfesor.Clear();
                                txtCarrera.Clear();
                                txtNombre.Clear();
                                txtEstado.Clear();
                            }
                            else
                            {
                                MessageBox.Show("No se pudo agregar la materia.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al agregar materia: " + ex.Message);
                        }
                        finally
                        {
                            // Desactivar IDENTITY_INSERT
                            using (SqlCommand comandoDesactivado = new SqlCommand("SET IDENTITY_INSERT MATERIA OFF", conexion))
                            {
                                comandoDesactivado.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            else
            {
                // El valor del TextBox no es un entero válido
                MessageBox.Show("Por favor, ingresa un número válido para el ID de la materia.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verificar si hay una fila seleccionada en el DataGridView
            if (dgvMaterias.SelectedRows.Count > 0)
            {
                // Confirmar la eliminación
                DialogResult resultado = MessageBox.Show("¿Estás seguro de que quieres eliminar esta materia?",
                                                          "Confirmar eliminación", MessageBoxButtons.YesNo);

                if (resultado == DialogResult.Yes)
                {
                    // Obtener el id_materia de la fila seleccionada
                    int idMateria = Convert.ToInt32(dgvMaterias.SelectedRows[0].Cells["id_materia"].Value);

                    string proveedorBD = "Data Source=.\\SQLEXPRESS;Initial Catalog=BD_Instituto;Integrated Security=True";

                    using (SqlConnection conexion = new SqlConnection(proveedorBD))
                    {
                        string consulta = "DELETE FROM MATERIA WHERE id_materia = @id_materia";

                        using (SqlCommand comando = new SqlCommand(consulta, conexion))
                        {
                            comando.Parameters.AddWithValue("@id_materia", idMateria);

                            try
                            {
                                conexion.Open();
                                int filasAfectadas = comando.ExecuteNonQuery();

                                if (filasAfectadas > 0)
                                {
                                    MessageBox.Show("Materia eliminada correctamente.");
                                    TraerMaterias(); // Actualiza el DataGridView
                                }
                                else
                                {
                                    MessageBox.Show("No se pudo eliminar la materia.");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error al eliminar materia: " + ex.Message);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una materia para eliminar.");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Verificar si hay una fila seleccionada en el DataGridView
            if (dgvMaterias.SelectedRows.Count > 0)
            {
                // Obtener el id_materia de la fila seleccionada
                int idMateria = Convert.ToInt32(dgvMaterias.SelectedRows[0].Cells["id_materia"].Value);

                // Validar el TextBox txtProfesor
                int idProfesor;
                if (int.TryParse(txtProfesor.Text, out idProfesor))
                {
                    // El valor del TextBox es un entero válido
                    // Obtener los datos modificados desde los TextBox
                    int idCarrera = Convert.ToInt32(txtCarrera.Text);
                    string nombre = txtNombre.Text;
                    string estado = txtEstado.Text;

                    string proveedorBD = "Data Source=.\\SQLEXPRESS;Initial Catalog=BD_Instituto;Integrated Security=True";

                    using (SqlConnection conexion = new SqlConnection(proveedorBD))
                    {
                        string consulta = "UPDATE MATERIA SET id_profesor = @id_profesor, id_carrera = @id_carrera, nombre = @nombre, estado = @estado WHERE id_materia = @id_materia";

                        using (SqlCommand comando = new SqlCommand(consulta, conexion))
                        {
                            // Asignar los valores a los parámetros de la consulta
                            comando.Parameters.AddWithValue("@id_materia", idMateria);
                            comando.Parameters.AddWithValue("@id_profesor", idProfesor);
                            comando.Parameters.AddWithValue("@id_carrera", idCarrera);
                            comando.Parameters.AddWithValue("@nombre", nombre);
                            comando.Parameters.AddWithValue("@estado", estado);

                            try
                            {
                                conexion.Open();
                                int filasAfectadas = comando.ExecuteNonQuery();

                                if (filasAfectadas > 0)
                                {
                                    MessageBox.Show("Materia modificada correctamente.");
                                    TraerMaterias(); // Actualiza el DataGridView
                                }
                                else
                                {
                                    MessageBox.Show("No se pudo modificar la materia.");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error al modificar materia: " + ex.Message);
                            }
                        }
                    }
                }
                else
                {
                    // El valor del TextBox no es un entero válido
                    MessageBox.Show("Por favor, ingresa un número válido para el ID del profesor.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una materia para modificar.");
            }
        }
    } 
}
            

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
    public partial class Carreras : Form
    {
        public Carreras()
        {
            InitializeComponent();
        }

        private void Carreras_Load(object sender, EventArgs e)
        {
            TraerCarreras();
        }

        private void TraerCarreras()
        {
            string proveedorBD = "Data Source=.\\SQLEXPRESS;Initial Catalog=BD_Instituto;Integrated Security=True";
            SqlConnection conexion = new SqlConnection(proveedorBD);
            DataTable tablaMemoria = new DataTable();

            string consulta = "SELECT * FROM CARRERA";

            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand(consulta, conexion);
                SqlDataReader lector = comando.ExecuteReader();
                tablaMemoria.Load(lector);
                dgvCarreras.DataSource = tablaMemoria;  // Asignamos los datos al DataGridView
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

            string nombre = txtNombre.Text;
            string sede = txtSede.Text;
            string estado = txtEstado.Text;

            // Proveedor de conexión
            string proveedorBD = "Data Source=.\\SQLEXPRESS;Initial Catalog=BD_Instituto;Integrated Security=True";

            using (SqlConnection conexion = new SqlConnection(proveedorBD))
            {
                // Consulta SQL para insertar los datos (excluyendo la columna de identidad id_carrera)
                string consulta = "INSERT INTO CARRERA (nombre, sede, estado) VALUES (@nombre, @sede, @estado)";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    // Asignar los valores a los parámetros de la consulta
                    comando.Parameters.AddWithValue("@nombre", nombre);
                    comando.Parameters.AddWithValue("@sede", sede);
                    comando.Parameters.AddWithValue("@estado", estado);

                    try
                    {
                        // Abrir la conexión y ejecutar la consulta
                        conexion.Open();
                        int filasAfectadas = comando.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Datos agregados correctamente.");

                            // Actualizar el DataGridView para mostrar los nuevos datos
                            TraerCarreras();

                            // Limpiar los TextBox después de agregar
                            txtNombre.Clear();
                            txtSede.Clear();
                            txtEstado.Clear();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo agregar el registro.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al agregar datos: " + ex.Message);
                    }
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {


            // Verificamos si hay una fila seleccionada en el DataGridView
            if (dgvCarreras.SelectedRows.Count > 0)
            {
                // Confirmar si el usuario realmente quiere eliminar los datos
                DialogResult resultado = MessageBox.Show("¿Estás seguro de que quieres eliminar los datos de esta fila?",
                                                          "Confirmar eliminación", MessageBoxButtons.YesNo);

                // Si la respuesta es sí, proceder con la actualización
                if (resultado == DialogResult.Yes)
                {
                    // Obtener el id_carrera de la fila seleccionada
                    int idCarrera = Convert.ToInt32(dgvCarreras.SelectedRows[0].Cells["id_carrera"].Value);

                    // Proveedor de conexión
                    string proveedorBD = "Data Source=.\\SQLEXPRESS;Initial Catalog=BD_Instituto;Integrated Security=True";
                    using (SqlConnection conexion = new SqlConnection(proveedorBD))
                    {
                        // Consulta SQL para actualizar los valores de nombre, sede y estado a NULL o un valor predeterminado
                        string consulta = "UPDATE CARRERA SET nombre = '', sede = '', estado = '' WHERE id_carrera = @id_carrera";

                        using (SqlCommand comando = new SqlCommand(consulta, conexion))
                        {
                            // Asignar el id_carrera a la consulta
                            comando.Parameters.AddWithValue("@id_carrera", idCarrera);

                            try
                            {
                                // Abrir la conexión y ejecutar la consulta
                                conexion.Open();
                                int filasAfectadas = comando.ExecuteNonQuery();

                                if (filasAfectadas > 0)
                                {
                                    MessageBox.Show("Datos eliminados correctamente.");

                                    // Actualizar el DataGridView para reflejar los cambios
                                    TraerCarreras();
                                }
                                else
                                {
                                    MessageBox.Show("No se pudo eliminar la fila.");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error al eliminar los datos: " + ex.Message);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Eliminación cancelada.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila para eliminar.");
            }

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Verificamos si hay una fila seleccionada en el DataGridView
            if (dgvCarreras.SelectedRows.Count > 0)
            {
                // Confirmar si el usuario realmente quiere modificar los datos
                DialogResult resultado = MessageBox.Show("¿Estás seguro de que quieres modificar los datos de esta carrera?",
                                                          "Confirmar modificación", MessageBoxButtons.YesNo);

                // Si la respuesta es sí, proceder con la actualización
                if (resultado == DialogResult.Yes)
                {
                    // Obtener el id_carrera de la fila seleccionada
                    int idCarrera = Convert.ToInt32(dgvCarreras.SelectedRows[0].Cells["id_carrera"].Value);

                    // Obtener los datos modificados desde los TextBox
                    string nombre = txtNombre.Text;
                    string sede = txtSede.Text;
                    string estado = txtEstado.Text;

                    // Proveedor de conexión
                    string proveedorBD = "Data Source=.\\SQLEXPRESS;Initial Catalog=BD_Instituto;Integrated Security=True";
                    using (SqlConnection conexion = new SqlConnection(proveedorBD))
                    {
                        // Consulta SQL para actualizar los valores
                        string consulta = "UPDATE CARRERA SET nombre = @nombre, sede = @sede, estado = @estado WHERE id_carrera = @id_carrera";

                        using (SqlCommand comando = new SqlCommand(consulta, conexion))
                        {
                            // Asignar los valores a los parámetros de la consulta
                            comando.Parameters.AddWithValue("@nombre", nombre);
                            comando.Parameters.AddWithValue("@sede", sede);
                            comando.Parameters.AddWithValue("@estado", estado);
                            comando.Parameters.AddWithValue("@id_carrera", idCarrera);

                            try
                            {
                                // Abrir la conexión y ejecutar la consulta
                                conexion.Open();
                                int filasAfectadas = comando.ExecuteNonQuery();

                                if (filasAfectadas > 0)
                                {
                                    MessageBox.Show("Datos modificados correctamente.");

                                    // Actualizar el DataGridView para reflejar los cambios
                                    TraerCarreras();
                                }
                                else
                                {
                                    MessageBox.Show("No se pudo modificar la fila.");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error al modificar los datos: " + ex.Message);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Modificación cancelada.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila para modificar.");
            }
        }

        
    }
}   

 

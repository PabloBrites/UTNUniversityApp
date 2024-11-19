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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BaseDeDatos
{
    public partial class Cursos : Form
    {
        private BindingList<string> listaComboBox = new BindingList<string>();
        private BindingList<string> listaMateriaCarrera = new BindingList<string>();
        private DataTable tablaMemoria; // DataTable compartido para almacenar las filas

        public Cursos()
        {
            InitializeComponent();
            this.Load += Cursos_Load;
            tablaMemoria = new DataTable(); // Inicializar la tabla
        }

        private void Cursos_Load(object sender, EventArgs e)
        {
            TraerCursos();
            LlenarComboBoxConNombresAleatorios();
            LlenarComboBoxMateria(); // Llamar al método para llenar cbmMateria

            // Asociar eventos para el filtro
            tbxFiltro.TextChanged += tbxFiltro_TextChanged;
            rbtnAlumno.CheckedChanged += rbtnAlumno_CheckedChanged;
            rbtnMateria.CheckedChanged += rbtnMateria_CheckedChanged;
        }

        private void TraerCursos()
        {
            string proveedorBD = "Data Source=.\\SQLEXPRESS;Initial Catalog=BD_Instituto;Integrated Security=True";
            using (SqlConnection conexion = new SqlConnection(proveedorBD))
            {
                tablaMemoria.Clear(); // Limpiar al cargar todos los cursos
                string consulta = "SELECT id_orden, Apellido, DNI, Carrera, Profesor, Materia FROM CURSOS";

                try
                {
                    conexion.Open();
                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    SqlDataReader lector = comando.ExecuteReader();
                    tablaMemoria.Load(lector);
                    dgvCursos.DataSource = tablaMemoria;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los cursos: " + ex.Message);
                }
            }
        }

        private void LlenarComboBoxConNombresAleatorios()
        {
            List<string> nombresAleatorios = new List<string>
            {
                "Juan", "María", "Carlos", "Ana", "Pablo",
                "Lucía", "Miguel", "Laura", "Jorge", "Elena"
            };

            string proveedorBD = "Data Source=.\\SQLEXPRESS;Initial Catalog=BD_Instituto;Integrated Security=True";
            listaComboBox.Clear(); // Limpiar elementos previos

            using (SqlConnection conexion = new SqlConnection(proveedorBD))
            {
                try
                {
                    conexion.Open();
                    string consulta = "SELECT Apellido, DNI FROM CURSOS";
                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    SqlDataReader lector = comando.ExecuteReader();
                    Random random = new Random();

                    while (lector.Read())
                    {
                        string nombreAleatorio = nombresAleatorios[random.Next(nombresAleatorios.Count)];
                        string displayText = $"{nombreAleatorio} {lector["Apellido"]} {lector["DNI"]}";
                        listaComboBox.Add(displayText);  // Agregar a la lista
                    }
                    lector.Close();

                    cmbAlumno.DataSource = listaComboBox;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al llenar el ComboBox: " + ex.Message);
                }
            }
        }

        private void LlenarComboBoxMateria()
        {
            string proveedorBD = "Data Source=.\\SQLEXPRESS;Initial Catalog=BD_Instituto;Integrated Security=True";

            using (SqlConnection conexion = new SqlConnection(proveedorBD))
            {
                // Modificamos la consulta para traer también la carrera asociada a cada materia
                string consulta = "SELECT DISTINCT Materia, Carrera FROM CURSOS";

                try
                {
                    conexion.Open();
                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    SqlDataReader lector = comando.ExecuteReader();

                    // Limpiar el ComboBox antes de cargar los elementos
                    cmbMateria.Items.Clear();

                    // Agregar las materias junto con las carreras al ComboBox
                    while (lector.Read())
                    {
                        string materia = lector["Materia"].ToString();
                        string carrera = lector["Carrera"].ToString();
                        string displayText = $"{materia} - {carrera}";

                        cmbMateria.Items.Add(displayText); // Añadir al ComboBox con ambos valores
                    }

                    // Si hay elementos en el ComboBox, seleccionamos el primero
                    if (cmbMateria.Items.Count > 0)
                    {
                        cmbMateria.SelectedIndex = 0;  // Selecciona el primer elemento
                    }

                    lector.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar las materias y carreras: " + ex.Message);
                }
            }
        }

        private string ObtenerProfesorParaMateria(string materia)
        {
            materia = materia.ToLower().Trim();
            if (materia == "álgebra")
            {
                return "Esteban Almeida";
            }
            else if (materia == "programación")
            {
                return "Demichelis Ruben";
            }
            else if (materia == "base de datos")
            {
                return "Guerrero Walter";
            }
            else
            {
                return "Profesor no asignado";
            }
        }

        private void cmbMateria_SelectedIndexChanged(object sender, EventArgs e)
        {
            string materiaSeleccionada = cmbMateria.SelectedItem.ToString().Trim().ToLower();
            string profesorAsignado = "Profesor no asignado";

            switch (materiaSeleccionada)
            {
                case "base de datos":
                    profesorAsignado = "Guerrero Walter";
                    break;
                case "álgebra":
                    profesorAsignado = "Esteban Almeida";
                    break;
                case "programación":
                    profesorAsignado = "Demichelis Ruben";
                    break;
                default:
                    profesorAsignado = "Elige una materia y un docente";
                    break;
            }

            cmbMateria.Text = profesorAsignado;
        }

        private void btnCargarMateria_Click(object sender, EventArgs e)
        {
            if (cmbAlumno.SelectedItem == null || cmbMateria.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona un alumno y una materia.");
                return;
            }

            string alumnoSeleccionado = cmbAlumno.SelectedItem.ToString();
            string materiaCarreraSeleccionada = cmbMateria.SelectedItem.ToString();

            string[] alumnoParts = alumnoSeleccionado.Split(' ');
            string apellidoAlumno = alumnoParts[1];
            string dniAlumno = alumnoParts[2];

            string[] materiaCarreraParts = materiaCarreraSeleccionada.Split('-');
            string materiaSeleccionada = materiaCarreraParts[0].Trim();
            string carreraSeleccionada = materiaCarreraParts[1].Trim();

            string proveedorBD = "Data Source=.\\SQLEXPRESS;Initial Catalog=BD_Instituto;Integrated Security=True";

            using (SqlConnection conexion = new SqlConnection(proveedorBD))
            {
                string consulta = "UPDATE CURSOS " +
                                  "SET Materia = @NuevaMateria, Profesor = @NuevoProfesor " +
                                  "WHERE Apellido = @Apellido AND DNI = @DNI AND Carrera = @Carrera";

                try
                {
                    conexion.Open();
                    string nuevoProfesor = ObtenerProfesorParaMateria(materiaSeleccionada);

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@NuevaMateria", materiaSeleccionada);
                    comando.Parameters.AddWithValue("@NuevoProfesor", nuevoProfesor);
                    comando.Parameters.AddWithValue("@Apellido", apellidoAlumno);
                    comando.Parameters.AddWithValue("@DNI", dniAlumno);
                    comando.Parameters.AddWithValue("@Carrera", carreraSeleccionada);

                    int filasAfectadas = comando.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Materia y profesor actualizados exitosamente.");
                        TraerCursos();
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ninguna fila para actualizar. Verifica los datos.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar la materia y profesor: " + ex.Message);
                }
            }
        }

        private void FiltrarDatos()
        {
            if (tablaMemoria == null || tablaMemoria.Rows.Count == 0)
            {
                return;
            }

            string filtro = tbxFiltro.Text.Trim();
            string columnaFiltro = "";

            if (rbtnAlumno.Checked)
            {
                columnaFiltro = "Apellido";
            }
            else if (rbtnMateria.Checked)
            {
                columnaFiltro = "Materia";
            }

            if (!string.IsNullOrEmpty(filtro) && !string.IsNullOrEmpty(columnaFiltro))
            {
                try
                {
                    DataView vistaFiltrada = new DataView(tablaMemoria);
                    vistaFiltrada.RowFilter = $"{columnaFiltro} LIKE '%{filtro}%'";
                    dgvCursos.DataSource = vistaFiltrada;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al filtrar datos: " + ex.Message);
                }
            }
            else
            {
                dgvCursos.DataSource = tablaMemoria;
            }
        }

        private void tbxFiltro_TextChanged(object sender, EventArgs e)
        {
            FiltrarDatos();
        }

        private void rbtnAlumno_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarDatos();
        }

        private void rbtnMateria_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarDatos();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
     
    




















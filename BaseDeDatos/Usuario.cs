using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseDeDatos
{
    public partial class FormLogin : Form
    {
        private Image fondo;
        string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=BD_Instituto;Integrated Security=True";

        public FormLogin()
        {
            InitializeComponent();

            // Cargar y oscurecer la imagen en el constructor
            Bitmap imgOriginal = new Bitmap(Application.StartupPath + @"\img\Gloria.jpg");
            Bitmap imgOscurecida = AplicarOscurecimiento(imgOriginal);

            // Asignar la imagen oscurecida al fondo del formulario
            this.BackgroundImage = imgOscurecida;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        // Método para aplicar el filtro de oscurecimiento a la imagen
        private Bitmap AplicarOscurecimiento(Bitmap imagenOriginal)
        {
            Bitmap imgOscurecida = new Bitmap(imagenOriginal.Width, imagenOriginal.Height);

            using (Graphics g = Graphics.FromImage(imgOscurecida))
            {
                // Crear un ColorMatrix para oscurecer la imagen
                float[][] matriz = {
                new float[] { 0.5f, 0, 0, 0, 0 }, // Rojo
                new float[] { 0, 0.5f, 0, 0, 0 }, // Verde
                new float[] { 0, 0, 0.5f, 0, 0 }, // Azul
                new float[] { 0, 0, 0, 1, 0 },    // Alpha (transparencia)
                new float[] { 0, 0, 0, 0, 1 }     // Desplazamiento
            };

                ColorMatrix colorMatrix = new ColorMatrix(matriz);

                // Crear un objeto ImageAttributes para aplicar el filtro
                using (ImageAttributes atributos = new ImageAttributes())
                {
                    atributos.SetColorMatrix(colorMatrix);

                    // Dibujar la imagen oscurecida
                    g.DrawImage(imagenOriginal, new Rectangle(0, 0, imagenOriginal.Width, imagenOriginal.Height),
                                0, 0, imagenOriginal.Width, imagenOriginal.Height, GraphicsUnit.Pixel, atributos);
                }
            }

            return imgOscurecida;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtUsuario.Text;
            string contrasena = txtContrasena.Text;

            // Verificar que los campos no estén vacíos
            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Por favor, ingresa usuario y contraseña.");
                return;
            }

            // Consulta SQL para verificar si el usuario y la contraseña coinciden
            string query = "SELECT COUNT(*) FROM USUARIO WHERE usuario = @usuario AND clave = @clave";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@usuario", nombreUsuario);
                    command.Parameters.AddWithValue("@clave", contrasena);

                    connection.Open();
                    int result = (int)command.ExecuteScalar();

                    if (result > 0)
                    {
                        MessageBox.Show("Login exitoso.");

                        // Abre el formulario principal
                        Principal formPrincipal = new Principal();
                        formPrincipal.Show();

                        // Ocultar el formulario de login
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
                }
            }
        }

        
    }
}
   















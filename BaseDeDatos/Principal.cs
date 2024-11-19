using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseDeDatos
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();

            // Cargar y oscurecer la imagen en el constructor
            Bitmap imgOriginal = new Bitmap(Application.StartupPath + @"\img\Gloria3.jpg");
            Bitmap imgOscurecida = AplicarOscurecimiento(imgOriginal);

            // Configurar la imagen de fondo con la imagen ya oscurecida
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
                new float[] { 0, 0, 0, 1, 0 },   // Alpha (transparencia)
                new float[] { 0, 0, 0, 0, 1 }    // Desplazamiento
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

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void btnAbrirProfesores_Click(object sender, EventArgs e)
        {
            Profesores formularioProfesores = new Profesores();
            formularioProfesores.Show();
        }

        private void btnAbrirCarreras_Click(object sender, EventArgs e)
        {
            Carreras formulariCarreras = new Carreras();
            formulariCarreras.Show();
        }

        private void btnAbrirMaterias_Click(object sender, EventArgs e)
        {
            Materias formularioMaterias = new Materias();
            formularioMaterias.Show();
        }

        private void btnAbrirCursos_Click(object sender, EventArgs e)
        {
            Cursos formularioCursos = new Cursos();
            formularioCursos.Show();
        }
    }
}


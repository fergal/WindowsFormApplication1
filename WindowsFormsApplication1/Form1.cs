using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadImage(this.pictureBox1);
        }
        // Load an image into a PictureBox from a SQL Server image datatype

        public void LoadImage(PictureBox pic)
        {

            // TODO: change connection string

            SqlConnection con = new SqlConnection("Data Source=FERGAL-PC\\LOCALHOST; Initial Catalog=MarketDogg; Integrated Security=SSPI;");

            // TODO: change query to pull form your own tables

            SqlCommand cmd = new SqlCommand("select Data from MDImage where id = 32 ", con);

            SqlDataReader rdr = null;

            byte[] imgData = null;

            try
            {




                con.Open();

                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (rdr.Read())
                {

                    // make sure our byte array is big enough to store the image

                    imgData = new byte[rdr.GetBytes(0, 0, null, 0, int.MaxValue)];

                    // retrieve the image from the datareader and store it in a byte array

                    rdr.GetBytes(0, 0, imgData, 0, imgData.Length);

                    // use a memorystream to read the image

                    System.IO.MemoryStream ms = new System.IO.MemoryStream(imgData);

                    // set the image to the picturebox

                    pic.Image = Image.FromStream(ms);

                    System.Drawing.Image imgSave = System.Drawing.Image.FromStream(ms);
                    Bitmap bmSave = new Bitmap(imgSave);
                    Bitmap bmTemp = new Bitmap(bmSave);

                    Graphics grSave = Graphics.FromImage(bmTemp);
                    grSave.DrawImage(imgSave, 0, 0, imgSave.Width, imgSave.Height);

                    bmTemp.Save("C:\\Users\\Fergal\\Desktop\\Files" + "\\" + "test.jpg");


                }

                else
                {

                    MessageBox.Show("No records found in the database.", "Warning");

                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");

            }

            finally
            {

                if (con.State == ConnectionState.Open) con.Close();

            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilmStudio
{
    public partial class Create_Film_More : Form
    {
        int id;
        int f;
        SqlConnection _con;
        public Create_Film_More(int id, int f, SqlConnection _con)
        {
            InitializeComponent();
            this.id = id;
            this.f = f;
            this._con = _con;
        }

        private void Create_Film_More_Load(object sender, EventArgs e)
        {
            //
            var queryStatement = $"SELECT Name, Year, [Image] FROM Films WHERE IdFilm = {f};";

            using (SqlCommand cmd = new SqlCommand(queryStatement, _con))
            {
                _con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        label5.Text = reader.GetString(0);
                        label6.Text = reader.GetInt32(1).ToString();
                        byte[] picData = reader["Image"] as byte[] ?? null;
                        if (picData != null)
                        {
                            using (MemoryStream ms = new MemoryStream(picData))
                            {
                                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ms);
                                pictureBox1.Image = bmp;
                            }
                        }
                    }
                }

                _con.Close();
            }
            //
            var queryStatement2 = $"SELECT Surname FROM Directors JOIN Films ON Films.IdDirector = Directors.IdDirector WHERE Films.IdDirector = {id};";

            using (SqlCommand cmd = new SqlCommand(queryStatement2, _con))
            {
                _con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        label7.Text = reader.GetString(0);
                    }
                }

                _con.Close();
            }
            //
            var queryStatement3 = $"SELECT CONCAT ([Name], ' ', Surname) FROM Actors JOIN ActorFilm ON ActorFilm.IdActor = Actors.IdActor WHERE IdFilm = {f};";

            using (SqlCommand cmd = new SqlCommand(queryStatement3, _con))
            {
                _con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    listBox1.BeginUpdate();

                    listBox1.Items.Clear();

                    while (reader.Read())

                    {
                        {
                            listBox1.Items.Add(reader.GetString(0));
                        }
                    }

                    _con.Close();
                    listBox1.EndUpdate();
                }
                //
            }
        }
    }
}

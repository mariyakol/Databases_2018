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

namespace FilmStudio
{
    public partial class Create_Film_Add : Form
    {
        int id;
        //int f;
        SqlConnection _con;
        public Create_Film_Add(int id, SqlConnection _con)
        {
            this.id = id;
            //this.f = f;
            this._con = _con;
            InitializeComponent();
        }

        private void Create_Film_Add_Load(object sender, EventArgs e)
        {
            var queryStatement = $"SELECT CONCAT(IdActor, ' - ', Name, ' ', Surname) FROM Actors";
            using (SqlCommand cmd = new SqlCommand(queryStatement, _con))
            {
                _con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    checkedListBox1.BeginUpdate();

                    checkedListBox1.Items.Clear();

                    while (reader.Read())

                    {

                        checkedListBox1.Items.Add(reader.GetString(0));

                    }

                    _con.Close();
                    checkedListBox1.EndUpdate();
                }
            }
            //
            var queryStatement2 = $"SELECT CONCAT(IdProducer, ' - ', Name, ' ', Surname) FROM Producer";
            using (SqlCommand cmd = new SqlCommand(queryStatement2, _con))
            {
                _con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    checkedListBox2.BeginUpdate();

                    checkedListBox2.Items.Clear();

                    while (reader.Read())
                    {
                        checkedListBox2.Items.Add(reader.GetString(0));
                    }

                    _con.Close();
                    checkedListBox2.EndUpdate();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var name = textBox1.Text;
            var idProducer = checkedListBox2.Items[0].ToString().Split(' ')[0];
            int year = 0;
            var b = Int32.TryParse(textBox2.Text, out year);

            var queryStatement3 = $"INSERT INTO Films (Name, Year, IdDirector, IdProducer) VALUES ('{name}', {year}, {id}, {idProducer})";
            using (SqlCommand cmd = new SqlCommand(queryStatement3, _con))
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            int filmId = 0;
            var queryStatementID = $"SELECT IdFilm From Films WHERE [Name] = '{name}'";
            using (SqlCommand cmd = new SqlCommand(queryStatementID, _con))
            {
                _con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                   if (reader.Read())
                   {
                       filmId = reader.GetInt32(0);
                   }
                    _con.Close();
                }
            }

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    string idActor = checkedListBox1.Items[i].ToString().Split(' ')[0];
                    var queryStatement4 = $"INSERT INTO ActorFilm (IdActor, IdFilm) VALUES ({idActor}, {filmId})";
                    using (SqlCommand cmd = new SqlCommand(queryStatement4, _con))
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                    }
                }
            }

            this.Close();
        }
    }
}

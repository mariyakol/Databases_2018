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
    public partial class Actor_Form : Form
    {
        int id;
        SqlConnection _con;
        public Actor_Form(int id, SqlConnection _con)
        {
            InitializeComponent();
            this.id = id;
            this._con = _con;
        }

        void Update_First_Tab()
        {
            var queryStatement = $"SELECT Name, Surname, DOB FROM Actors WHERE IdActor = {id};";

            using (SqlCommand cmd = new SqlCommand(queryStatement, _con))
            {
                _con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        label5.Text = reader.GetString(0);
                        label6.Text = reader.GetString(1);
                        label7.Text = (DateTime.Now.Year - reader.GetDateTime(2).Year).ToString();
                    }
                }

                _con.Close();
            }
        }

        private void Actor_Form_Load(object sender, EventArgs e)
        {
            Update_First_Tab();
        }

        void Update_Second_Tab()
        {
            var queryStatement2 = $"SELECT CONCAT(IdShift, ': ', f.[Name], ' ', s.[Date]), s.[Date] FROM Shifts AS s  " +
            "JOIN Films AS f ON s.IdFilm = f.IdFilm " +
            $"WHERE s.IdActor = {id}";

            var shiftList = new List<string>();
            var dateList = new List<DateTime>();
            using (SqlCommand cmd = new SqlCommand(queryStatement2, _con))
            {
                _con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        shiftList.Add(reader.GetString(0));
                        dateList.Add(reader.GetDateTime(1));
                    }
                }
                _con.Close();
            }

            var d = dateList.ToArray();
            var s = shiftList.ToArray();
            Array.Sort(d, s);

            checkedListBox2.DataSource = s;

            monthCalendar1.BoldedDates = d;
        }

        void Update_Third_Tab()
        {
            var queryStatement2 = $"SELECT CONCAT(IdCasting, ': ', f.[Name], ' ', c.[Date]), c.[Date] FROM Castings AS c  " +
                                  "JOIN Films AS f ON c.IdFilm = f.IdFilm " +
                                  $"WHERE c.IdActor = {id}";

            var shiftList = new List<string>();
            var dateList = new List<DateTime>();
            using (SqlCommand cmd = new SqlCommand(queryStatement2, _con))
            {
                _con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        shiftList.Add(reader.GetString(0));
                        dateList.Add(reader.GetDateTime(1));
                    }
                }
                _con.Close();
            }

            var d = dateList.ToArray();
            var s = shiftList.ToArray();
            Array.Sort(d, s);

            checkedListBox1.DataSource = s;

            monthCalendar2.BoldedDates = d;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    Update_First_Tab();
                    break;
                case 1:
                    Update_Second_Tab();
                    break;
                case 2:
                    Update_Third_Tab();
                    break;

                default:

                    break;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if (checkedListBox2.GetItemChecked(i))
                {
                    var idShift = checkedListBox2.Items[i].ToString().Split(':')[0];
                    var queryStatement3 = $"DELETE FROM Shifts WHERE IdShift = {idShift};";
                    using (SqlCommand cmd = new SqlCommand(queryStatement3, _con))
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                    }
                }
            }

            Update_Second_Tab();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    var idCasting = checkedListBox1.Items[i].ToString().Split(':')[0];
                    var queryStatement3 = $"DELETE FROM Castings WHERE IdCasting = {idCasting};";
                    using (SqlCommand cmd = new SqlCommand(queryStatement3, _con))
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                    }
                }
            }

            Update_Third_Tab();
        }
    }
}

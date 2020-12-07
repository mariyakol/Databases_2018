using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Autopark
{
    public partial class Operator : Form
    {
        OracleConnection _con;
        String login;
        public Operator(OracleConnection _con, string login)
        {
            this._con = _con;
            InitializeComponent();
            get();
            get_person();
            get_routes();
            get_auto();
            get_number_of_car();
            get_roude_name();
            label33.Text = "";
            label34.Text = "";
            label2.Text = login;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var auto_id = comboBox1.SelectedItem.ToString().Split(' ')[0];
            var route_id = comboBox2.SelectedItem.ToString().Split(' ')[0];


            var queryStatement1 = $"INSERT INTO journal (journal_id, time_out, auto_id, routes_id) VALUES (MYSEQ_JOURNAL.NEXTVAL, CURRENT_TIMESTAMP, '{auto_id}', '{route_id}')";
            using (OracleCommand cmd = new OracleCommand(queryStatement1, _con))
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }
            get();

        }
        public void get()
        {
            var queryStatement2 = "SELECT journal_id FROM journal WHERE time_in is null";

            var ListBox = new List<int>();
            using (OracleCommand cmd = new OracleCommand(queryStatement2, _con))
            {
                _con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        ListBox.Add(reader.GetInt32(0));
                }
                _con.Close();
            }

            listBox1.DataSource = ListBox;
        }

        public void get_person()
        {
            var queryStatement6 = "SELECT CONCAT(AUTO_PERSONNEL_ID, CONCAT(' ', CONCAT(first_name, CONCAT(' ', CONCAT(last_name, CONCAT(' ', pather_name)))))) FROM auto_personnel";

            var ListBox = new List<string>();
            using (OracleCommand cmd = new OracleCommand(queryStatement6, _con))
            {
                _con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        ListBox.Add(reader.GetString(0));
                }
                _con.Close();
            }

            listBox2.DataSource = ListBox;
            listBox4.DataSource = ListBox;
            //get_person();
        }

        public void get_routes()
        {
            var queryStatement8 = "SELECT CONCAT(routes_id, CONCAT(' ', \"name\")) FROM routes";

            var ListBox = new List<string>();
            using (OracleCommand cmd = new OracleCommand(queryStatement8, _con))
            {
                _con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        ListBox.Add(reader.GetString(0));
                }
                _con.Close();
            }

            listBox3.DataSource = ListBox;
        }

        public void get_auto()
        {
            var queryStatement11 = "SELECT CONCAT (auto_id, CONCAT( ' ', CONCAT (brand, CONCAT( ' ', CONCAT (\"num\", CONCAT( ' ', CONCAT (color, CONCAT( ' ', auto_personnel_id)))))))) FROM \"auto\"";

            var ListBox = new List<string>();
            using (OracleCommand cmd = new OracleCommand(queryStatement11, _con))
            {
                _con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        ListBox.Add(reader.GetString(0));
                }
                _con.Close();
            }

            listBox5.DataSource = ListBox;
        }

        public void get_number_of_car()
        {
            var queryStatement17= "SELECT CONCAT (auto_id, CONCAT(' ',  \"num\" ))FROM \"auto\"";

            var list = new List<string>();
            using (OracleCommand cmd = new OracleCommand(queryStatement17, _con))
            {
                _con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        list.Add(reader.GetString(0));
                }
                _con.Close();
            }

            comboBox1.DataSource = list;
        }

        public void get_roude_name()
        {
            var queryStatement18 = "SELECT CONCAT (routes_id, CONCAT(' ', \"name\")) FROM ROUTES";

            var list = new List<string>();
            using (OracleCommand cmd = new OracleCommand(queryStatement18, _con))
            {
                _con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        list.Add(reader.GetString(0));
                }
                _con.Close();
            }

            comboBox2.DataSource = list;
        }


        private void button12_Click(object sender, EventArgs e)
        {
            var journal_id = listBox1.SelectedItem.ToString();
            var queryStatement3 = $"UPDATE journal SET time_in = CURRENT_TIMESTAMP WHERE journal_id = '{journal_id}'";
            using (OracleCommand cmd = new OracleCommand(queryStatement3, _con))
            {
               _con.Open();
                cmd.ExecuteNonQuery();
               _con.Close();
            }
            get();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var name = textBox10.Text;
            var surname = textBox9.Text;
            var pather = textBox8.Text;

            var queryStatement4 = $"INSERT INTO auto_personnel (auto_personnel_id, first_name, last_name, pather_name) VALUES (MYSEQ_PERSON.NEXTVAL, '{name}', '{surname}', '{pather}')";
            using (OracleCommand cmd = new OracleCommand(queryStatement4, _con))
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }
            textBox10.Text = "";
            textBox9.Text = "";
            textBox8.Text = "";
            get_person();
        }

        private void button5_Click(object sender, EventArgs e)
        { 
            var person_id = listBox2.SelectedItem.ToString().Split(' ')[0];
            var queryStatement5 = $"DELETE FROM AUTO_PERSONNEL WHERE auto_personnel_id = '{person_id}'";
            using (OracleCommand cmd = new OracleCommand(queryStatement5, _con))
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }
            get_person();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var name = textBox19.Text;

            var queryStatement6 = $"INSERT INTO routes (routes_id, \"name\") VALUES (MYSEQ_ROUTES.NEXTVAL, '{name}')";
            using (OracleCommand cmd = new OracleCommand(queryStatement6, _con))
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }
            textBox19.Text = "";
            get_routes();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var route_id = textBox5.Text;
            var name = textBox19.Text;
            var queryStatement7 = $"UPDATE routes SET \"name\" = '{name}' WHERE routes_id = '{route_id}'";
            using (OracleCommand cmd = new OracleCommand(queryStatement7, _con))
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }
            textBox19.Text = "";
            textBox5.Text = "";
            get_routes();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var route_id = listBox3.SelectedItem.ToString().Split(' ')[0];
            var queryStatement9 = $"DELETE FROM routes WHERE routes_id = '{route_id}'";
            using (OracleCommand cmd = new OracleCommand(queryStatement9, _con))
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }
            get_routes();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var number = textBox11.Text;
            var color = textBox15.Text;
            var brand = textBox14.Text;
            var person_id = listBox4.SelectedItem.ToString().Split(' ')[0];

            var queryStatement10 = $"INSERT INTO \"auto\" (auto_id, \"num\", color, brand, auto_personnel_id) VALUES (MYSEQ_AUTO.NEXTVAL, '{number}', '{color}', '{brand}', '{person_id}')";
            using (OracleCommand cmd = new OracleCommand(queryStatement10, _con))
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }
            textBox11.Text = "";
            textBox15.Text = "";
            textBox14.Text = "";
            get_auto();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var auto_id = listBox5.SelectedItem.ToString().Split(' ')[0];
            var number = textBox11.Text;
            var color = textBox15.Text;
            var brand = textBox14.Text;
            var person_id = textBox6.Text;
            var queryStatement14 = $"UPDATE \"auto\" SET \"num\" = '{number}', color = '{color}', brand = '{brand}', auto_personnel_id = '{person_id}' WHERE auto_id = '{auto_id}'";
            using (OracleCommand cmd = new OracleCommand(queryStatement14, _con))
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }
            textBox11.Text = "";
            textBox15.Text = "";
            textBox14.Text = "";
            textBox6.Text = "";
            get_auto();
        }

        private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            var auto_id = listBox5.SelectedItem.ToString().Split(' ')[0];
            var queryStatement12 = $"SELECT \"num\", color, brand, auto_personnel_id FROM \"auto\" WHERE auto_id = '{auto_id}'";

            var ListBox = new List<int>();
            using (OracleCommand cmd = new OracleCommand(queryStatement12, _con))
            {
                _con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        textBox11.Text = reader.GetString(0);
                        textBox15.Text = reader.GetString(1);
                        textBox14.Text = reader.GetString(2);
                        textBox6.Text = reader.GetInt32(3).ToString();
                    }
                }
                _con.Close();
            }

        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            var person_id= listBox4.SelectedItem.ToString().Split(' ')[0];
            textBox6.Text = person_id.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var auto_id = listBox5.SelectedItem.ToString().Split(' ')[0];
            var queryStatement15 = $"DELETE FROM \"auto\" WHERE auto_id = '{auto_id}'";
            using (OracleCommand cmd = new OracleCommand(queryStatement15, _con))
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }
            get_auto();

        }

        private void button14_Click(object sender, EventArgs e)
        {
            var route_id = textBox4.Text;
            var queryStatement16 = $"SELECT time_in-time_out, auto_id FROM journal WHERE routes_id = '{route_id}' and time_in-time_out =(SELECT MIN(time_in - time_out) mintime FROM journal WHERE routes_id = '{route_id}')";

            using (OracleCommand cmd = new OracleCommand(queryStatement16, _con))
            {
                _con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        label33.Text = reader.GetOracleIntervalDS(0).ToString() +" "+ reader.GetInt32(1).ToString();
                    }
                }
                _con.Close();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var route_id = textBox3.Text;
            var queryStatement17 = $"SELECT COUNT(auto_id) FROM journal WHERE(time_in is null) and(routes_id = '{route_id}')";
            using (OracleCommand cmd = new OracleCommand(queryStatement17, _con))
            {
                _con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        label34.Text = reader.GetInt32(0).ToString();
                    }
                }
                _con.Close();
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var person_id = listBox2.SelectedItem.ToString().Split(' ')[0];
            var queryStatement12 = $"SELECT auto_personnel_id, first_name, last_name, pather_name FROM AUTO_PERSONNEL WHERE auto_personnel_id = '{person_id}'";

            var ListBox = new List<int>();
            using (OracleCommand cmd = new OracleCommand(queryStatement12, _con))
            {
                _con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        textBox10.Text = reader.GetString(1);
                        textBox9.Text = reader.GetString(2);
                        textBox8.Text = reader.GetString(3);
                    }
                }
                _con.Close();
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            var route_id = listBox3.SelectedItem.ToString().Split(' ')[0];
            var queryStatement12 = $"SELECT routes_id, \"name\" FROM ROUTES WHERE routes_id = '{route_id}'";

            var ListBox = new List<int>();
            using (OracleCommand cmd = new OracleCommand(queryStatement12, _con))
            {
                _con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        textBox5.Text = reader.GetInt32(0).ToString();
                        textBox19.Text = reader.GetString(1);
                    }
                }
                _con.Close();
            }
        }
    }
}
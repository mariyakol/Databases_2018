using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilmStudio
{
    public partial class Producer_Form : Form
    {
        SqlConnection _con;
        public Producer_Form(SqlConnection _con)
        {
            InitializeComponent();
            this._con = _con;
        }

        void UpdateList()
        {
            var list = new List<string>();

            var queryStatement2 =
                $"SELECT CONCAT(IdNeeds, ': ', Name, ' (', HowMany, ') - ', Status) FROM Needs"; //sp

            using (SqlCommand cmd = new SqlCommand(queryStatement2, _con))
            {
                _con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        list.Add(reader.GetString(0));
                }

                _con.Close();
            }

            listBox1.DataSource = list;
        }

        void UpdateInfo()
        {
            var queryStatement = $"SELECT Name, Surname FROM Producer;"; //sp

            using (SqlCommand cmd = new SqlCommand(queryStatement, _con))
            {
                _con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        label3.Text = reader.GetString(0);
                        label4.Text = reader.GetString(1);
                    }
                }
                _con.Close();
            }

            UpdateList();
        }

        private void Producer_Form_Load(object sender, EventArgs e)
        {
           UpdateInfo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.GetItemChecked(i))
                {
                    var idNeed = listBox1.Items[i].ToString().Split(':')[0];
                    var queryStatement3 = $"DELETE FROM Needs WHERE IdNeeds = {idNeed};"; //sp
                    using (SqlCommand cmd = new SqlCommand(queryStatement3, _con))
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                    }
                }
            }

            UpdateList();
        }
    }
}

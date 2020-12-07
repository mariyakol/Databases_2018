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
    public partial class Login : Form
    {
        string connectionString = "Data Source=localhost:1521;User Id=C##test_user;Password=oracle;";
        OracleConnection _con;

        public Login()
        {
            InitializeComponent();
            _con = new OracleConnection(connectionString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var login = textBox1.Text;
            var password = textBox2.Text;

            var query = $"select case when (select standard_hash('{password}', 'MD5') from dual) " +
                "= (SELECT H.\"HASH\" FROM \"HASH\" H " +
                $"WHERE H.LOGIN = '{login}') then 'true' else 'false' end res from dual";
            string result = "";
            using ( OracleCommand cmd = new OracleCommand(query, _con))
            {
                _con.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = reader.GetString(0);
                    }
                }
                _con.Close();
            }

            if (result.Equals("true"))
            {
                this.Hide();
                var operatorForm = new Operator(_con, login);
                operatorForm.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var login = textBox1.Text;
            var password = textBox2.Text;
            var hash = "";

            var queryStatement1 = $"INSERT INTO \"HASH\"(LOGIN, \"HASH\") VALUES('{login}', (select standard_hash('{password}', 'MD5') from dual))";
            using (OracleCommand cmd = new OracleCommand(queryStatement1, _con))
            {
                
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
                
            }

            textBox1.Text = "";
            textBox2.Text = "";
        }
    }
}

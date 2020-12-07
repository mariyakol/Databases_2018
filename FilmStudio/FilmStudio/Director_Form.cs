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
    public partial class Director_Form : Form
    {
        int id; //IdDirector
        SqlConnection _con;
        int[] f = new int[4]; //IdFilm
        public Director_Form(int id, SqlConnection _con)
        {
            InitializeComponent();
            this.id = id;
            this._con = _con;
        }
        //
        void Update_First_Tab()
        {
            var queryStatement = $"SELECT Name, Surname FROM Directors WHERE IdDirector = {id};";

            using (SqlCommand cmd = new SqlCommand(queryStatement, _con))
            {
                _con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        label3.Text = reader.GetString(0);
                        label5.Text = reader.GetString(1);
                    }
                }

                _con.Close();
            }

            var list = new List<string>();
            var queryStatement3 =
                $"SELECT CONCAT('Castings ', c.IdCasting, ' ', c.Date,' - ',  a.name, ' ', a.Surname) FROM Castings as c " +

                "JOIN Actors as a ON c.IdActor = a.IdActor " +

                $"WHERE IdDirector = {id}";

            using (SqlCommand cmd = new SqlCommand(queryStatement3, _con))
            {
                _con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        list.Add(reader.GetString(0));
                }

                _con.Close();
            }

            var queryStatement2 =
                $"SELECT CONCAT('Shifts ', s.IdShift, ' ', s.Date,' - ',  a.name, ' ', a.Surname) FROM Shifts as s " +

                "JOIN Actors as a ON s.IdActor = a.IdActor " +

                "JOIN Films as f on f.IdFilm = s.IdFilm " +

                $"WHERE IdDirector = {id}";

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
        private void Director_Form_Load(object sender, EventArgs e)
        {
            Update_First_Tab();
        }
        //
        private void Create_Film()
        {
            var queryStatementCount = $"SELECT COUNT (IdFilm) FROM Films WHERE IdDirector = {id}";//get out count 
            int count = 0;
            using (SqlCommand cmd = new SqlCommand(queryStatementCount, _con))
            {
                _con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        count = reader.GetInt32(0);
                    }
                }
                _con.Close();
            }
            //
            TopLeftAdd.Visible = true;
            TopLeftAdd.Enabled = true;
            TopRightAdd.Visible = true;
            TopRightAdd.Enabled = true;
            LowLeftAdd.Visible = true;
            LowLeftAdd.Enabled = true;
            TopRightAdd.Visible = true;
            TopRightAdd.Enabled = true;
            var queryStatement = $"SELECT Name, Year, [Image], IdFilm FROM Films WHERE IdDirector = {id};";
            using (SqlCommand cmd1 = new SqlCommand(queryStatement, _con))
            {
                _con.Open();
                using (SqlDataReader reader = cmd1.ExecuteReader())
                {
                    
                    for (int i = 0; i < count; i++)
                    {
                        if (reader.Read())
                        {
                            switch (i)
                            {
                                case 0:
                                    TopLeftAdd.Visible = false;
                                    TopLeftAdd.Enabled = false;
                                    TopLeft1.Text = reader.GetString(0);
                                    TopLeft2.Text = reader.GetInt32(1).ToString();

                                    byte[] picData1 = reader["Image"] as byte[] ?? null;
                                    if (picData1 != null)
                                    {
                                        using (MemoryStream ms = new MemoryStream(picData1))
                                        {
                                            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ms);
                                            TopLeftPic.Image = bmp;
                                        }
                                    }
                                    f[0] = reader.GetInt32(3);//get out f!!!!!

                                    break;

                                case 1:
                                    TopRightAdd.Visible = false;
                                    TopRightAdd.Enabled = false;
                                    TopRight1.Text = reader.GetString(0);
                                    TopRight2.Text = reader.GetInt32(1).ToString();

                                    byte[] picData2 = reader["Image"] as byte[] ?? null;
                                    if (picData2 != null)
                                    {
                                        using (MemoryStream ms = new MemoryStream(picData2))
                                        {
                                            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ms);
                                            TopRightPic.Image = bmp;
                                        }
                                    }
                                    f[1] = reader.GetInt32(3);//get out f!!!!!
                                    break;

                                case 2:
                                    LowLeftAdd.Visible = false;
                                    LowLeftAdd.Enabled = false;
                                    LowLeft1.Text = reader.GetString(0);
                                    LowLeft2.Text = reader.GetInt32(1).ToString();

                                    byte[] picData3 = reader["Image"] as byte[] ?? null;
                                    if (picData3 != null)
                                    {
                                        using (MemoryStream ms = new MemoryStream(picData3))
                                        {
                                            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ms);
                                            LowLeftPic.Image = bmp;
                                        }
                                    }
                                    f[2] = reader.GetInt32(3);//get out f!!!!!

                                    break;


                                case 3:
                                    LowRightAdd.Visible = false;
                                    LowRightAdd.Enabled = false;
                                    LowRight1.Text = reader.GetString(0);
                                    LowRight2.Text = reader.GetInt32(1).ToString();

                                    byte[] picData4 = reader["Image"] as byte[] ?? null;
                                    if (picData4 != null)
                                    {
                                        using (MemoryStream ms = new MemoryStream(picData4))
                                        {
                                            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ms);
                                            LowRightPic.Image = bmp;
                                        }
                                    }
                                    f[3] = reader.GetInt32(3);//get out f!!!!!

                                    break;

                            }
                        }
                    }
                    _con.Close();
                }
            }
        }


        private void TopLeftBut_Click(object sender, EventArgs e)
        {
            Create_Film_More form = new Create_Film_More(id, f[0], _con);
            form.Show();
        }
        private void TopRightBut_Click(object sender, EventArgs e)
        {
            Create_Film_More form = new Create_Film_More(id, f[1],_con);
            form.Show();
        }

        private void LowLeftBut_Click(object sender, EventArgs e)
        {
            Create_Film_More form = new Create_Film_More(id, f[2],_con);
            form.Show();
        }

        private void LowRightBut_Click(object sender, EventArgs e)
        {
            Create_Film_More form = new Create_Film_More(id, f[3], _con);
            form.Show();
        }
        //ADD BUTTON
        private void TopLeftAdd_Click(object sender, EventArgs e)
        {
            Create_Film_Add form = new Create_Film_Add(id, _con);
            form.Show();
        }

        private void TopRightAdd_Click(object sender, EventArgs e)
        {
            Create_Film_Add form = new Create_Film_Add(id, _con);
            form.Show();
        }

        private void LowLeftAdd_Click(object sender, EventArgs e)
        {
            Create_Film_Add form = new Create_Film_Add(id, _con);
            form.Show();
        }

        private void LowRightAdd_Click(object sender, EventArgs e)
        {
            Create_Film_Add form = new Create_Film_Add(id, _con);
            form.Show();
        }

        void Create_Casting()
        {
            var films = new List<string>();
            var queryStatement = $"SELECT CONCAT(IdFIlm, ' - ', Name) FROM Films WHERE IdDirector = {id}";

            using (SqlCommand cmd = new SqlCommand(queryStatement, _con))
            {
                _con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        films.Add(reader.GetString(0));
                }
                _con.Close();
            }

            comboBoxFilm.DataSource = films;
        }

        void Create_Shifts()
        {
            var films = new List<string>();
            var queryStatement = $"SELECT CONCAT(IdFIlm, ' - ', Name) FROM Films WHERE IdDirector = {id}";

            using (SqlCommand cmd = new SqlCommand(queryStatement, _con))
            {
                _con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        films.Add(reader.GetString(0));
                }

                _con.Close();
            }

            comboBox1.DataSource = films;
        }

       
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    Update_First_Tab();
                    break;
                case 1:
                    Create_Film();
                    break;
                case 2:
                    Create_Casting();
                    break;
                case 3:
                    Create_Shifts();
                    break;

                default:

                    break;
            }
            
        }

        private void comboBoxFilm_SelectedIndexChanged(object sender, EventArgs e)
        {
            var filmId = comboBoxFilm.SelectedItem?.ToString().Split(' ')[0];

            var queryStatement2 = $"SELECT Concat(a.IdActor, ' - ', a.[Name]) FROM Actors as a " +

                                  "JOIN ActorFilm as af ON a.IdActor = af.IdActor " +

                                  "EXCEPT " +

                                  "SELECT Concat(a.IdActor, ' - ', a.[Name]) FROM Actors as a " +

                                  "JOIN ActorFilm as af ON a.IdActor = af.IdActor " +

                                  $" WHERE af.IdFilm = {filmId}";

            var actorList = new List<string>();
            using (SqlCommand cmd = new SqlCommand(queryStatement2, _con))
            {
                _con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        actorList.Add(reader.GetString(0));
                }
                _con.Close();
            }

            checkedListBox1.DataSource = actorList;
        }

        DateTime GetDate()
        {
            string timestr = "00:00";
            if (radioButton6.Checked)
            {
                timestr = radioButton6.Text;
            }
            if (radioButton7.Checked)
            {
                timestr = radioButton7.Text;
            }
            if (radioButton8.Checked)
            {
                timestr = radioButton8.Text;
            }
            if (radioButton9.Checked)
            {
                timestr = radioButton9.Text;
            }
            if (radioButton10.Checked)
            {
                timestr = radioButton10.Text;
            }
            if (radioButton11.Checked)
            {
                timestr = radioButton11.Text;
            }
            if (radioButton12.Checked)
            {
                timestr = radioButton12.Text;
            }
            if (radioButton13.Checked)
            {
                timestr = radioButton13.Text;
            }
            if (radioButton14.Checked)
            {
                timestr = radioButton14.Text;
            }
            if (radioButton15.Checked)
            {
                timestr = radioButton15.Text;
            }

            var s = monthCalendar1.SelectionStart;
            s = s.AddHours(Double.Parse(timestr.Substring(0, 2)));
            return s;
        }

        void Clear_Casting()
        {
            if (radioButton6.Checked)
            {
                radioButton6.Checked = false;
            }

            if (radioButton7.Checked)
            {
                radioButton7.Checked = false;
            }

            if (radioButton8.Checked)
            {
                radioButton8.Checked = false;
            }

            if (radioButton9.Checked)
            {
                radioButton9.Checked = false;
            }

            if (radioButton10.Checked)
            {
                radioButton10.Checked = false;
            }

            if (radioButton11.Checked)
            {
                radioButton11.Checked = false;
            }

            if (radioButton12.Checked)
            {
                radioButton12.Checked = false;
            }

            if (radioButton13.Checked)
            {
                radioButton13.Checked = false;
            }

            if (radioButton14.Checked)
            {
                radioButton14.Checked = false;
            }

            if (radioButton15.Checked)
            {
                radioButton15.Checked = false;
            }

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var filmId = comboBoxFilm.SelectedItem?.ToString().Split(' ')[0];

            var date = GetDate();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    string idActor = checkedListBox1.Items[i].ToString().Split(' ')[0];
                    string sqlFormattedDate = date.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    var queryStatement3 = $"INSERT INTO Castings (IdDirector, Date, IdActor, IdFilm) VALUES ({id}, '{sqlFormattedDate}', {idActor}, {filmId})";
                    using (SqlCommand cmd = new SqlCommand(queryStatement3, _con))
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                    }
                }
            }

            MessageBox.Show($"Casting at {date.ToShortDateString()} was created", "Information", MessageBoxButtons.OK);

            Clear_Casting();
        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var filmId = comboBox1.SelectedItem?.ToString().Split(' ')[0];

            var queryStatement2 = "SELECT Concat(a.IdActor, ' - ', a.[Name]) FROM ActorFilm as af " +
                                  "JOIN Actors as a ON a.IdActor = af.IdActor " +
                                  $"WHERE IdFilm = {filmId}";

            var actorList = new List<string>();
            using (SqlCommand cmd = new SqlCommand(queryStatement2, _con))
            {
                _con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        actorList.Add(reader.GetString(0));
                }
                _con.Close();
            }

            checkedListBox2.DataSource = actorList;
        }

        void Clear_Shifts()
        {
            if (radioButton1.Checked)
            {
                radioButton1.Checked = false;
            }

            if (radioButton2.Checked)
            {
                radioButton2.Checked = false;
            }

            if (radioButton3.Checked)
            {
                radioButton3.Checked = false;
            }

            if (radioButton4.Checked)
            {
                radioButton4.Checked = false;
            }

            if (radioButton5.Checked)
            {
                radioButton5.Checked = false;
            }

            if (radioButton19.Checked)
            {
                radioButton19.Checked = false;
            }

            if (radioButton20.Checked)
            {
                radioButton20.Checked = false;
            }

            if (radioButton21.Checked)
            {
                radioButton21.Checked = false;
            }

            if (radioButton22.Checked)
            {
                radioButton22.Checked = false;
            }

            if (radioButton23.Checked)
            {
                radioButton23.Checked = false;
            }

            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if (checkedListBox2.GetItemChecked(i))
                {
                    checkedListBox2.SetItemChecked(i, false);
                }
            }
        }

        DateTime GetDate2()
        {
            string timestr = "00:00";
            if (radioButton23.Checked)
            {
                timestr = radioButton23.Text;
            }
            if (radioButton1.Checked)
            {
                timestr = radioButton1.Text;
            }
            if (radioButton2.Checked)
            {
                timestr = radioButton2.Text;
            }
            if (radioButton3.Checked)
            {
                timestr = radioButton3.Text;
            }
            if (radioButton4.Checked)
            {
                timestr = radioButton4.Text;
            }
            if (radioButton5.Checked)
            {
                timestr = radioButton5.Text;
            }
            if (radioButton19.Checked)
            {
                timestr = radioButton19.Text;
            }
            if (radioButton20.Checked)
            {
                timestr = radioButton20.Text;
            }
            if (radioButton21.Checked)
            {
                timestr = radioButton21.Text;
            }
            if (radioButton22.Checked)
            {
                timestr = radioButton22.Text;
            }

            var s = monthCalendar2.SelectionStart;
            return s.AddHours(Double.Parse(timestr.Substring(0, 2)));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var date = GetDate2();
            var filmId = comboBox1.SelectedItem?.ToString().Split(' ')[0];

            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if (checkedListBox2.GetItemChecked(i))
                {
                    string idActor = checkedListBox2.Items[i].ToString().Split(' ')[0];
                    string sqlFormattedDate = date.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    var queryStatement3 = $"INSERT INTO Shifts (IdFilm, Date, IdActor) VALUES ({filmId}, '{sqlFormattedDate}', {idActor})";
                    using (SqlCommand cmd = new SqlCommand(queryStatement3, _con))
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                    }
                }
            }

            MessageBox.Show($"Shift at {date.ToShortDateString()} was created", "Information", MessageBoxButtons.OK);

            Clear_Shifts();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var name = textBox1.Text;
            var howmany = textBox2.Text;
            string status = "";

            if (radioButton16.Checked)
            {
                status = radioButton16.Text;
            }
            if (radioButton17.Checked)
            {
                status = radioButton17.Text;
            }
            if (radioButton18.Checked)
            {
                status = radioButton18.Text;
            }

            var queryStatement3 = $"INSERT INTO Needs (Name, HowMany, Status, IdDirector, IdProducer) VALUES ('{name}', {howmany}, '{status}', {id}, {1})";
            using (SqlCommand cmd = new SqlCommand(queryStatement3, _con))
            {
                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            MessageBox.Show($"Need '{name}' was created", "Information", MessageBoxButtons.OK);

            textBox1.Text = "";
            textBox2.Text = "";
            radioButton16.Checked = false;
            radioButton17.Checked = false;
            radioButton18.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.GetItemChecked(i))
                {
                    var splitArr = listBox1.Items[i].ToString().Split(' ');
                    string tableName = splitArr[0];
                    string whereId = splitArr[1];
                    var queryStatement3 = $"DELETE FROM {tableName} WHERE Id{tableName.Remove(tableName.Length - 1)} = {whereId};";
                    using (SqlCommand cmd = new SqlCommand(queryStatement3, _con))
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                        _con.Close();
                    }
                }
            }

            Update_First_Tab();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FilmStudio
{
    public partial class Login_Form : Form
    {
        public Login_Form()
        {
            InitializeComponent();
            AutoCompleteStringCollection source = new AutoCompleteStringCollection()
        {
            "Director1",
            "Director2",
            "Actor1",
            "Actor2",
            "Actor3",
            "Actor4",
            "Actor5",
            "Producer"
        };
            textBox1.AutoCompleteCustomSource = source;
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
         public Login_Form(Form1 f)
        {
            InitializeComponent();
            RemoveOwnedForm(f); 
        }
    

    

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source = DESKTOP-O8P5GSL; Initial Catalog = Film_Studio; integrated Security = SSPI";
            SqlConnection _con = new SqlConnection(connectionString);


                switch (textBox1.Text)
            {
                case "Actor1":
                    {
                        Actor_Form newForm = new Actor_Form(1, _con);
                        newForm.Show();
                        this.Hide();
                        break;
                    }
                case "Actor2":
                    {
                        Actor_Form newForm = new Actor_Form(2, _con);
                        newForm.Show();
                        this.Hide();
                        break;
                    }
                case "Actor3":
                    {
                        Actor_Form newForm = new Actor_Form(3, _con);
                        newForm.Show();
                        this.Hide();
                        break;
                    }
                case "Actor4":
                    {
                        Actor_Form newForm = new Actor_Form(4, _con);
                        newForm.Show();
                        this.Hide();
                        break;
                    }
                case "Actor5":
                    {
                        Actor_Form newForm = new Actor_Form(5, _con);
                        newForm.Show();
                        this.Hide();
                        break;
                    }
                case "Director1":
                    {
                        Director_Form newForm = new Director_Form(1,_con);
                        newForm.Show();
                        this.Hide();
                        break;
                    }
                case "Director2":
                    {
                        Director_Form newForm = new Director_Form(2,_con);
                        newForm.Show();
                        this.Hide();
                        break;
                    }
                case "Producer":
                    {
                        Producer_Form newForm = new Producer_Form(_con);
                        newForm.Show();
                        this.Hide();
                        break;
                    }
            }
          
        }
    }
}

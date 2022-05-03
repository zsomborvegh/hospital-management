namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            /* login felület, ezt txt be mentettük*/

            string username = txtUserName.Text;
            string pass = txtPassword.Text;

            if (username == "admin" && pass == "zsanki")
            {
                //MessageBox.Show("You have entered right Username and Password"); ez talán nem is kell, Andris + Kiki beszéljük át.

                this.Hide();
                Dashboard ds = new Dashboard();
                ds.Show();


            }
            else
            {
                MessageBox.Show("Wrong ID or Password");
            }
        }
    }
}
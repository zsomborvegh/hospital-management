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
            /* login fel�let, ezt txt be mentett�k*/

            string username = txtUserName.Text;
            string pass = txtPassword.Text;

            if (username == "admin" && pass == "zsanki")
            {
                //MessageBox.Show("You have entered right Username and Password"); ez tal�n nem is kell, Andris + Kiki besz�lj�k �t.

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
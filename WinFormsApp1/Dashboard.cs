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
using Npgsql;

namespace WinFormsApp1
{

    public partial class Dashboard : Form
    {


        public Dashboard()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        // add patient gomb + láthatóság
        private void btnAddPatient_Click(object sender, EventArgs e)
        {
            labelIndecator1.ForeColor = System.Drawing.Color.Red;
            labelIndecator2.ForeColor = System.Drawing.Color.Black;
            labelIndecator3.ForeColor = System.Drawing.Color.Black;
            labelIndecator4.ForeColor = System.Drawing.Color.Black;

            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
        }

        private void labelIndecator1_Click(object sender, EventArgs e)
        {

        }

        // AddDiagnosis gomb
        private void btnAddDiagnosis_Click(object sender, EventArgs e)
        {
            labelIndecator2.ForeColor = System.Drawing.Color.Red;
            labelIndecator1.ForeColor = System.Drawing.Color.Black;
            labelIndecator3.ForeColor = System.Drawing.Color.Black;
            labelIndecator4.ForeColor = System.Drawing.Color.Black;

            //panel1.Visible = false;
            panel2.Visible = true;
            //panel3.Visible = false;
            panel4.Visible = false;
            

        }
        // fullhistory gomb
        private async void btnFullHistory_Click(object sender, EventArgs e)
        {
            labelIndecator3.ForeColor = System.Drawing.Color.Red;
            labelIndecator2.ForeColor = System.Drawing.Color.Black;
            labelIndecator1.ForeColor = System.Drawing.Color.Black;
            labelIndecator4.ForeColor = System.Drawing.Color.Black;
            // panel láthatóságok- fontos hogy a dashboard form-ra kerüljön ne panel a panelra, mert látszódni nem fog
            panel3.Visible = true;
            panel1.Visible = false;
            panel2.Visible = false;
            panel4.Visible = false;

            var connString = "Host=veghz-hospital-db.cqw7dtxectvw.eu-central-1.rds.amazonaws.com;Username=postgres;Password=Test_1234;Database=hospital";

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            // Retrieve all rows - mindkét táblának az eredményeit akarjuk látni! AWS postgres DB a hostunk.Npgsql.system nélkül ez nem megy! using: using Npgsql;
            await using (var cmd = new NpgsqlCommand("select * from AddPatient inner join PatientMore on AddPatient.pid = PatientMore.pid", conn))
            {
                DataSet DS = new DataSet();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                da.Fill(DS);
                dataGridView2.DataSource = DS.Tables[0];
            }
        }

        // kórház infók
        private void btnHospital_Click(object sender, EventArgs e)
        {
            labelIndecator4.ForeColor = System.Drawing.Color.Red;
            labelIndecator2.ForeColor = System.Drawing.Color.Black;
            labelIndecator3.ForeColor = System.Drawing.Color.Black;
            labelIndecator1.ForeColor = System.Drawing.Color.Black;

            panel4.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
            panel1.Visible = false;
        }
        // exit gomb
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        //save button
        private async void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string address = txtAddress.Text;
            Int64 contact = Convert.ToInt64(txtContact.Text);
            int age = Convert.ToInt32(txtAge.Text);
            string gender = comboGender.Text;
            string blood = txtBlood.Text;
            string amy = txtAmy.Text;
            int pid = Convert.ToInt32(txtPid.Text);

            //itt ez nem működik, mert a connection string / sqlconnection sql szerverhez jó. Npgsql conn kell!
            
            
            /* SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\zsomb\Desktop\CODE\hospital-management\WinFormsApp1\Hospital.mdf;Integrated Security=True;Connect Timeout=30");
             SqlCommand cmd = new SqlCommand();
             cmd.Connection = con;
             cmd.CommandText = "Insert into AddPatient values ('" + name + "','" + address + "'," + contact + "," + age + ",'" + gender + "','" + blood + "','" + amy + "','" + pid+"')"; 

             SqlDataAdapter DA = new SqlDataAdapter(cmd);
             DataSet DS = new DataSet();
             DA.Fill(DS);*/


            var connString = "Host=veghz-hospital-db.cqw7dtxectvw.eu-central-1.rds.amazonaws.com;Username=postgres;Password=Test_1234;Database=hospital";

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            // Insert some data - sima insert query DB oldalon, commandot futtat
            await using (var cmd = new NpgsqlCommand("Insert into AddPatient values ('" + name + "','" + address + "'," + contact + "," + age + ",'" + gender + "','" + blood + "','" + amy + "','" + pid + "')", conn))
            {
                cmd.Parameters.AddWithValue("Hello world");
                await cmd.ExecuteNonQueryAsync();
            }
            
            // Retrieve all rows - minden kell az Addpatientből
            await using (var cmd = new NpgsqlCommand("SELECT * from AddPatient", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Console.WriteLine(reader.GetString(0));
                }
            }

            MessageBox.Show("Saved");

            txtName.Clear();
            txtAddress.Clear();
            txtContact.Clear();
            txtAge.Clear();
            txtBlood.Clear();
            txtAmy.Clear();
            txtPid.Clear();
            comboGender.ResetText();

        }

        private async void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (textBox1.Text != "")
            {



                int pid = Convert.ToInt32(textBox1.Text);

                var connString = "Host=veghz-hospital-db.cqw7dtxectvw.eu-central-1.rds.amazonaws.com;Username=postgres;Password=Test_1234;Database=hospital";
                // újra behúzzuk a csatlakozást
                await using var conn = new NpgsqlConnection(connString);
                await conn.OpenAsync();

                // Retrieve all rows
                await using (var cmd = new NpgsqlCommand("select * from AddPatient where pid = " + pid, conn))
                {
                    DataSet DS = new DataSet();
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    da.Fill(DS);
                    dataGridView1.DataSource = DS.Tables[0];
                }
                int a;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int pid = Convert.ToInt32(textBox1.Text);
                string sympt = txtBxSymptomps.Text;
                string diag = txtBxDiagnosis.Text;
                string medicine = txtBxMedicines.Text;
                string ward = comboBxWard.Text;
                string wardType = comboBxWardType.Text;

                // aws connection string
                var connString = "Host=veghz-hospital-db.cqw7dtxectvw.eu-central-1.rds.amazonaws.com;Username=postgres;Password=Test_1234;Database=hospital";

                await using var conn = new NpgsqlConnection(connString);
                await conn.OpenAsync();

                // Insert some data - mentés 
                await using (var cmd = new NpgsqlCommand("insert into PatientMore values(" + pid + ",'" + sympt + "','" + diag + "','" + medicine + "','" + ward + "','" + wardType + "')", conn))
                {
                    cmd.Parameters.AddWithValue("Hello world");
                    await cmd.ExecuteNonQueryAsync();

                    MessageBox.Show("Data Saved");
                }

            }
            catch (Exception ex)
            {
                // rossz a formátum amit beírunk, és ezt pop up mutassa!
                MessageBox.Show("Any field is empty OR Data is in WRONG format.");
                MessageBox.Show(ex.Message);
            }


        }
    }
}

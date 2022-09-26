using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO.Ports;
using System.Threading;
namespace DesignEngineering
{
    delegate void SetTextCallback(string text);
    public partial class Form2 : Form
    {
        string str;
        public Form2()
        {
            InitializeComponent();
            serialPort1.Open();
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1 = new Form1(); //create object of form2
            form1.Show(); //display form2 using display() method
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            Form1 form1 = new Form1(); //create object of form2
            form1.Show(); //display form2 using display() method
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            clear();
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                e.Cancel = true;
                textBox1.Focus();
                errorProvider1.SetError(textBox1, "Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox1, "");
            }  
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                e.Cancel = true;
                textBox2.Focus();
                errorProvider1.SetError(textBox2, "Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox2, "");
            }
        
       }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            int len = textBox3.Text.Length;
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                e.Cancel = true;
                textBox3.Focus();
                errorProvider1.SetError(textBox3, "Name should not be left blank!");
            }
            else if (len < 4)
            {
                e.Cancel = true;
                textBox3.Focus();
                errorProvider1.SetError(textBox3, "length should be more than 4");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox3, "");
            }
        }

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                e.Cancel = true;
                textBox4.Focus();
                errorProvider1.SetError(textBox4, "Name should not be left blank!");
            }
            else if (!(textBox4.Text).Equals(textBox3.Text))
            {
                e.Cancel = true;
                textBox4.Focus();
                errorProvider1.SetError(textBox4, "Password Must Be same ");
            }
           
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox4, "");
            }
        }

        private void textBox5_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                e.Cancel = true;
                textBox5.Focus();
                errorProvider1.SetError(textBox5, "Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox5, "");
            }
        }

        private void textBox6_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox6.Text))
            {
                e.Cancel = true;
                textBox6.Focus();
                errorProvider1.SetError(textBox6, "Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox6, "");
            }
        }

        private void textBox7_Validating(object sender, CancelEventArgs e)
        {
            Regex re = new Regex("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})");
            if (!re.IsMatch(textBox7.Text))
            {
                MessageBox.Show("Please enter valid email address");
                textBox7.Text = "";

            }
        }
       

        private void checkBox2_Validating(object sender, CancelEventArgs e)
        {
            if (!checkBox2.Checked)
            {
                    e.Cancel = true;
                    checkBox2.Focus();
                    errorProvider1.SetError(checkBox2, "Accept terms and condition");
             }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(checkBox2, "");
            }
        }

    

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            TextBox[] newTextBox = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7,textBox8};
            for (int inti = 0; inti < newTextBox.Length; inti++)
            {
                if (newTextBox[inti].Text == string.Empty)
                {
                    if (inti == 7)
                    {
                        newTextBox[inti].Focus();
                        errorProvider1.SetError(newTextBox[inti], "Scan the card ");
                        newTextBox[inti].Focus();

                    }
                    else
                    {
                        newTextBox[inti].Focus();
                        errorProvider1.SetError(newTextBox[inti], "Please fill the text box");
                        newTextBox[inti].Focus();
                        return;
                    }
                    
                }
            }
            SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\HP\\Documents\\DesignEngineering.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
            SqlCommand cmd;
            cmd = new SqlCommand("insert into userinfo(Adharno,RFIDno,Name,Password,Address,Email,Mobile,DOB) values(@adhar,@rfid,@nam,@pass,@add,@email,@mobile,@dob)", con);
            con.Open();
            cmd.Parameters.AddWithValue("@adhar", textBox1.Text);
            cmd.Parameters.AddWithValue("@rfid", textBox8.Text);
            cmd.Parameters.AddWithValue("@nam", textBox2.Text);
            cmd.Parameters.AddWithValue("@pass", textBox3.Text);
            cmd.Parameters.AddWithValue("@add", textBox5.Text);
            cmd.Parameters.AddWithValue("@email", textBox6.Text);
            cmd.Parameters.AddWithValue("@mobile", textBox7.Text);
            cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Value);

            cmd.ExecuteNonQuery();
            con.Close();
            clear();
            Form1 f = new Form1();
            this.Hide();
            f.Show();


        }
        void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            dateTimePicker1.Value = DateTime.Now;
           
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox6.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox6.Text = textBox6.Text.Remove(textBox6.Text.Length - 1);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
            }
        }

        private void dateTimePicker1_Validating(object sender, CancelEventArgs e)
        {
            if (dateTimePicker1.Value.ToString() == string.Empty)
            {
                e.Cancel = true;
                dateTimePicker1.Focus();
                errorProvider1.SetError(dateTimePicker1, "select date of birth");
               
                
            }
            else if (dateTimePicker1.Value.ToString() == DateTime.Now.ToString())
            {
                e.Cancel = true;
                dateTimePicker1.Focus();
                errorProvider1.SetError(dateTimePicker1, "Please select proper date ");

            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }
        private void SetText(string text)
        {
            this.textBox8.Text = text;
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //textBox1.Text = "";
            str = serialPort1.ReadLine();
            this.Invoke(new EventHandler(DisplayText));

        }

        private void DisplayText(object sender, EventArgs e)
        {
            textBox8.Text = str;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

       

       

       

       
       

        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Threading;
namespace DesignEngineering
{
    delegate void SetTextCallback1(string text);
    public partial class Form1 : Form
    {
        public static string str;
        public static string toStation = "Ambedkar Nagar";
        public static int toindex;
        public static string fromStation;
        public static int fromindex;
        public static int money = 0;
        public static string CustmerName;
        public static long rfid;
        public static string station;
        public Form1()
        {
            
            InitializeComponent();
           // serialPort1.Close();
            serialPort1.Open();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            serialPort1.Close();
            Form2 form2 = new Form2(); //create object of form2
            form2.Show(); //display form2 using display() method
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {


           // serialPort1.Close();
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select station");
                return;
            }
            else if (toStation.Equals(comboBox1.SelectedItem.ToString()))
            {
                MessageBox.Show("Please select Appropriate Station");
                return;
            }
            else if (numericUpDown1.Value == 0 && numericUpDown2.Value == 0)
            {
                MessageBox.Show("Please select no of passenger ");
                return;
            }           
           
            TextBox[] newTextBox = { textBox2 };
            for (int inti = 0; inti < newTextBox.Length; inti++)
            {
                if (newTextBox[inti].Text == string.Empty)
                {

                    newTextBox[inti].Focus();
                    errorProvider1.SetError(newTextBox[inti], "Please fill the box");
                    newTextBox[inti].Focus();
                    return;

                }
                else
                {
                    errorProvider1.SetError(newTextBox[inti], "");

                }
            }
            
            fromStation = comboBox1.SelectedItem.ToString();
            int temp = fromindex - toindex;
            if (temp < 0)
            {
                temp = (int)temp * (-1);

            }
            money = (int)(temp * 2 * (int)numericUpDown1.Value) + (temp * (int)numericUpDown2.Value);
            rfid = Convert.ToInt64(textBox1.Text.ToString());
            //MessageBox.Show(rfid.ToString());
            SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\HP\\Documents\\DesignEngineering.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand();
            SqlCommand cmd = new SqlCommand("select * from [users] where Rfidno='"+rfid+"' and password='"+textBox2.Text+"'", con);
            SqlDataReader rd = cmd.ExecuteReader();
            //MessageBox.Show(textBox1.Text);
            /*//String query = "select * from [userinfo] where Rfidno='"+textBox1.Text+"' AND Password='"+textBox2.Text+"'";
            //String query = "select * from [userinfo] where Rfidno='"+ textBox1.Text +"' AND Password='"+ textBox2.Text +"'";
            //cmd = new SqlCommand(query, con);
            MessageBox.Show(textBox1.Text.ToString(), textBox2.Text.ToString());
            cmd = new SqlCommand("select * from [userinfo] where Rfidno='611572082' AND Password='dv@123'", con);
           // cmd.Parameters.Add("@uname",textBox1.Text);
           // cmd.Parameters.Add("@pass", textBox2.Text);
            //check.Parameters.AddWithValue("@password", textBox2.Text);
            SqlDataReader rd = cmd.ExecuteReader();
            */
            if(rd.Read()){
           // MessageBox.Show("hello");
             str = rd.GetValue(2).ToString();
            
           // if (textBox2.Text.Equals("meet@123") || textBox2.Text.Equals("dv@123"))  
                

                    con.Close();
                    con.Open();
                    Form3 frm = new Form3();
                    cmd = new SqlCommand("insert into Ticket(RFIDno,Date,TOstation,FROMstation,Adult,child,Total) values(@rfidno,@date,@to,@from,@adult,@child,@total)", con);
                    cmd.Parameters.AddWithValue("@rfidno",textBox1.Text);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);
                    frm.label11.Text = DateTime.Now.ToString();
                    cmd.Parameters.AddWithValue("@to", toStation);
                    cmd.Parameters.AddWithValue("@from", fromStation);
                    cmd.Parameters.AddWithValue("@adult", numericUpDown1.Value);
                    cmd.Parameters.AddWithValue("@child", numericUpDown2.Value);
                    cmd.Parameters.AddWithValue("@total", money);
                    
                    cmd.ExecuteNonQuery();
                    con.Close();
                    
                    this.Hide();
                    frm.label9.Text="Mr/Mrs. "+str;
                    frm.label10.Text = textBox1.Text;
                    frm.label12.Text = toStation;
                    
                    frm.label13.Text = fromStation;
                    frm.label14.Text = numericUpDown1.Value.ToString();
                    frm.label15.Text = numericUpDown2.Value.ToString();
                    frm.label16.Text = money.ToString();
                    frm.qrstr = frm.label11.Text.ToString();
                    serialPort1.Close();    
                    frm.Show();           
                }
                else
                {
                    MessageBox.Show("Wrong Password ");
                }      
 
            //Count Amount
            
            clear();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            toindex = comboBox1.Items.IndexOf(toStation);
            fromindex = comboBox1.SelectedIndex;

        }
        void clear()
        {
            comboBox1.SelectedIndex = -1;
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //serialPort1.Close();
            clear();
        }
       
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //textBox1.Text = "";
             str = serialPort1.ReadLine();
             this.Invoke(new EventHandler(DisplayText));
            
        }

        private void DisplayText(object sender, EventArgs e)
        {
            textBox1.Text=str;
        }  
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    } 
}

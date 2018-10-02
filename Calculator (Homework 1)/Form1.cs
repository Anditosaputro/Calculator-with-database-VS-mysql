using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace Calculator__Homework_1_
{
    public partial class Form1 : Form
    {
        //membuat koneksi mysql
        MySqlConnection koneksi = new MySqlConnection("server=localhost;port=3306;database=kalkulator;uid=root;password=;SslMode=none");

        //deklarasi variabel
        Double resultValue = 0;
        String operationPerformed = "";
        bool isOperationPerformed = false;

        public Form1()
        {
            //inisialisasi form
            InitializeComponent();

            //buka koneksi
            koneksi.Open();

            //kosongkan database
            ResetData();

            //load database
            LoadData();

            //tutup koneksi
            koneksi.Close();
        }
 
        //fungsi mengkosongkan database
        public void ResetData()
        {
            //inisialisasi mysqlcommand
            MySqlCommand command;
            command = koneksi.CreateCommand();

            //kosongkan data
            command.CommandText = "TRUNCATE TABLE kalkulator";

            //Update database
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
        }

        //fungsi load database
        public void LoadData()
        {
            //inisisalisasi mysqlcommand
            MySqlCommand command;
            command = koneksi.CreateCommand();

            //ambil data dari kontak query
            command.CommandText = "select * from kalkulator";

            //masukkan data ke DataGrid
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView1.DataSource = dataset.Tables[0].DefaultView;
        }

        //fungsi memasukkan data ke database
        public void masukdatabase()
        {
            //buka koneksi
            koneksi.Open();

            //inisialisasi mysqlcommand
            MySqlCommand command;
            command = koneksi.CreateCommand();

            //inputdata query
            command.CommandText = "INSERT INTO kalkulator (result,operation) VALUES (@result,@operation);";

            //tambah parameter
            command.Parameters.AddWithValue("@result",textBox1.Text);
            command.Parameters.AddWithValue("@operation",textBox2.Text);

            //eksekusi query
            command.ExecuteNonQuery();

            //load data
            LoadData();

            //tutup koneksi
            koneksi.Close();
        }

        //fungsi tombol off
        public void disable()
        {
            textBox1.Enabled = false;
            button1.Show();
            button2.Hide();
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            button10.Enabled = false;
            button11.Enabled = false;
            button12.Enabled = false;
            button13.Enabled = false;
            button14.Enabled = false;
            button15.Enabled = false;
            button16.Enabled = false;
            button17.Enabled = false;
            button18.Enabled = false;
            button19.Enabled = false;
            button20.Enabled = false;
        }

        //fungsi tombol on
        public void enable()
        {
            textBox1.Enabled = true;
            button1.Hide();
            button2.Show();
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
            button10.Enabled = true;
            button11.Enabled = true;
            button12.Enabled = true;
            button13.Enabled = true;
            button14.Enabled = true;
            button15.Enabled = true;
            button16.Enabled = true;
            button17.Enabled = true;
            button18.Enabled = true;
            button19.Enabled = true;
            button20.Enabled = true;
        }

        private void offButton_Click(object sender, EventArgs e) //off button
        {
            disable();
        }

        private void onButton_Click(object sender, EventArgs e) //on button
        {
            enable();
        }
        
        //clear button
        private void clearButton_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
        }

        //backspace button
        private void backspace_Click(object sender, EventArgs e)
        {
            int lenght = textBox1.TextLength - 1;
            string text = textBox1.Text;
            textBox1.Clear();
            for (int i = 0; i < lenght; i++)
                textBox1.Text = textBox1.Text + text[i];
        }

        //membaca input angka
        private void button_click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0")
            {
                textBox1.Clear();
            }
            Button button = (Button)sender;
            if (isOperationPerformed == true)
            {
                textBox1.Clear();
                textBox2.Text = textBox2.Text +" "+ button.Text;
            }
            isOperationPerformed = false;
            if (button.Text == ",")
            {
                if (!textBox1.Text.Contains(","))
                    textBox1.Text = textBox1.Text + button.Text;
            }else
            textBox1.Text = textBox1.Text + button.Text;
        }

        //membaca input operator "=-x/"
        private void operator_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (resultValue != 0)
            {
                button20.PerformClick();
                operationPerformed = button.Text;
                textBox2.Text = resultValue + " " + operationPerformed;
                isOperationPerformed = true;
            }
            else
            {
                operationPerformed = button.Text;
                resultValue = Double.Parse(textBox1.Text);
                textBox2.Text = resultValue + " " + operationPerformed;
                isOperationPerformed = true;
            }
        }

        //tombol "=" (samadengan)
        private void result(object sender, EventArgs e)
        {
            switch (operationPerformed)
            {
                case "+":
                    textBox1.Text = (resultValue + Double.Parse(textBox1.Text)).ToString();
                    masukdatabase();
                    break;
                case "-":
                    textBox1.Text = (resultValue - Double.Parse(textBox1.Text)).ToString();
                    masukdatabase();
                    break;
                case "x":
                    textBox1.Text = (resultValue * Double.Parse(textBox1.Text)).ToString();
                    masukdatabase();
                    break;
                case "/":
                    textBox1.Text = (resultValue / Double.Parse(textBox1.Text)).ToString();
                    masukdatabase();
                    break;
                default:
                    break;
            }
            resultValue = Double.Parse(textBox1.Text);
            textBox2.Text = "";
            isOperationPerformed = true;
            operationPerformed = "";
        }
    }
}

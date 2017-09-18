using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {



        private MySqlConnection conn;
        private string server;
        private string database;
        private string uid;
        private string password;
        int i;
        public Form1()
        {
            server = "127.0.0.1";
            database = "objektno";
            uid = "root";
            password = "";

            string connstring;
            connstring = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password}";
            conn = new MySqlConnection(connstring);
            InitializeComponent();
        }
        private void Login_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            
            if (IsLogin(username, password))
            {
                MessageBox.Show($"Wellcome {username}");
                run_cmd();
            }
            else
            {
                MessageBox.Show($"{username} ne postoji ili netocna zaporka");
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //registracija korisnika
            string username = textBox1.Text;
            string password = textBox2.Text;
            Register(username, password);
            if (Register(username, password))
            {
                MessageBox.Show($"Korisnik {username} jos nije stvoren");
            }
            else
            {
                 MessageBox.Show($"Korisnik {username} je kreiran");
            }

        }

        public bool IsLogin(string username, string password)
        {
            string query = $"SELECT * FROM korisnici WHERE username = '{username}' AND password ='{password}';";
            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        reader.Close();
                        conn.Close();
                        return true;

                    }
                    else
                    {
                        reader.Close();
                        conn.Close();
                        return false;

                    }
                }
                else
                {

                    conn.Close();
                    return false;

                }

            }
            catch (Exception ex)
            {
                conn.Close();
                return false;

            }

            

            


        }

        private void run_cmd()
        {

            string fileName = @"C:\Users\Iohannes\Desktop\PythonApplication1.py";
            //C:\Users\Iohannes\Documents\Visual Studio 2017\Projects\WindowsFormsApp3\WindowsFormsApp3
            //C:\Users\Iohannes\Desktop
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(@"C:\Program Files\Python36\python.exe", fileName)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            p.Start();

            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            Console.WriteLine(output);

            Console.ReadLine();

        }
        


        public bool Register(string username, string password)
        {
            string query = $"INSERT INTO korisnici (username, password) VALUES ('{username}','{password}')";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }

                    catch (Exception ex)
                    {
                        return false;
                    }

                }
                else
                {
                    conn.Close();
                    return false;

                }
            }

            catch (Exception ex)
            {
                conn.Close();
                return false;
            }


        }

        private bool OpenConnection()
        {
            try
            {
                conn.Open();
                return true;

            }

            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Povezivanje na server neuspješno");
                        break;
                    case 1045:
                        MessageBox.Show("Netočno korisničko ime ili zaporka");
                        break;



                }
                return false;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("U projektu je demonstrirano povezivanje na lokalnu MySQL bazu podataka (realizirano u c#) \n" +
                 "prilikom logina korisniku je omogućeno pisanje nekog teksta (realizirano u pythonu)\n");
        }
    }
}

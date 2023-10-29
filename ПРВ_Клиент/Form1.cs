using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Pipes;
using System.IO;
namespace ПРВ_Клиент
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        NamedPipeClientStream pipe;
        private void button1_Click(object sender, EventArgs e)
        {
            pipe = new NamedPipeClientStream("myPipe");
            pipe.Connect();

            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            StreamReader rd = new StreamReader(pipe);
            StreamWriter wr = new StreamWriter(pipe);
            wr.AutoFlush = true;
            wr.WriteLine(textBox1.Text);
            wr.Flush();
            textBox1.Text = "";
            wr.AutoFlush = true;
            textBox2.Text = rd.ReadLine();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

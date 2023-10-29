using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ПРВ_КлиентДва
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        NamedPipeClientStream pipe;
        Task d;
        private void button1_Click(object sender, EventArgs e)
        {
            pipe = new NamedPipeClientStream(".", "myPipe", PipeDirection.InOut, PipeOptions.Asynchronous);
            pipe.Connect();
            d = getMessage();
            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //StreamReader rd = new StreamReader(pipe);
            StreamWriter wr = new StreamWriter(pipe);
            wr.AutoFlush = true;
            wr.WriteLine(textBox1.Text);
            wr.Flush();
            textBox1.Text = "";
           // wr.AutoFlush = true;
           // textBox2.Text = rd.ReadLine();
           
            //while (true)
            //{
               
           // }
        }

        string readMes(StreamReader StreamReader)
        {
            string m = StreamReader.ReadLine();
            return m;
            /*if (InvokeRequired)
                Invoke(listBox1.Items.Add(m));
            else
                action();*/
            //listBox1.Items.Add(m);
        }
        async private Task getMessage(/*Object source, ElapsedEventArgs e*/)
        {
            string inputm;
            StreamReader rd = new StreamReader(pipe);

            listBox1.Items.Add(await Task.Run(() => readMes(rd)));
            

           /* await inputm = rd.ReadLine();*/


               

           


            //StreamReader rd = new StreamReader(pipe);
            //listBox1.Items.Add(rd.ReadLine());

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (d.IsCompleted) {
                d = getMessage();
            }

        }
    }
}

using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace leituraUSB
{
    public partial class Form1 : Form
    {
        string indata = "";

        public Form1()
        {
            InitializeComponent();

            //preenche o comBox1 com as COMs disponíveis
            string[] portas = System.IO.Ports.SerialPort.GetPortNames();
            foreach (string porta in portas)
            {
                comboBox1.Items.Add(porta);
            }
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort serialPort1 = (SerialPort)sender;
            indata += serialPort1.ReadExisting();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //habilita ou não o botão CONECTAR
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                serialPort1.PortName = comboBox1.Text;//usa a porta selecionada na comBox1
                if (serialPort1.IsOpen == false)
                {
                    try
                    {
                        serialPort1.BaudRate = 4800;//velocidade da comunicação serial
                        serialPort1.Open();
                        serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                    }
                    catch
                    {
                        MessageBox.Show("A porta já está sendo utilizada!", "AVISO!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione uma Porta Serial!", "ATENÇÃO!");
            }
        }

        public void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Caixa de texto vazia!", "ATENÇÃO!");
            }
            else
            {
                if (comboBox1.Text != "")
                {
                    try
                    {
                        serialPort1.Write(textBox1.Text + "\r");
                    }
                    catch
                    {
                        MessageBox.Show("A porta já está sendo utilizada!", "AVISO!");
                    }
                }
                else
                {
                    MessageBox.Show("Selecione uma Porta Serial!", "ATENÇÃO!");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (indata != "") textBox2.Text += indata;
            indata = "";
        }
    }
}
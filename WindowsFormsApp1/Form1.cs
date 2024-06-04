using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(new [] { "Binary", "Octal", "Hexadecimal" });
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (!int.TryParse(textBox1.Text, out int inputNumber))
                {
                    textBox2.Text = "Invalid input. Please enter a number.";
                    return;
                }

                string convertedNumber = "";
                using (ServiceReference1.Service1Client client = new ServiceReference1.Service1Client())
                {
                    if (comboBox1.SelectedIndex == 0)
                    {
                        convertedNumber = client.Convert_2(inputNumber);
                    }
                    else if (comboBox1.SelectedIndex == 1)
                    {
                        convertedNumber = client.Convert_8(inputNumber);
                    }
                    else if (comboBox1.SelectedIndex == 2)
                    {
                        convertedNumber = client.Convert_16(inputNumber);
                    }
                }

                textBox2.Text = convertedNumber;
            }
            catch (FormatException)
            {
                textBox2.Text = "Invalid input format.";
            }
            catch (CommunicationException ex)
            {
                textBox2.Text = $"Communication error with the service: {ex.Message}";
            }
            catch (Exception ex)
            {
                textBox2.Text = $"An error occurred: {ex.Message}";
            }
        }
       }
}

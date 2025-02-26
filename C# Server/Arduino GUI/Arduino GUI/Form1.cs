using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO.Ports;

namespace Arduino_GUI
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private const string ESP32_IP = "YourIP";
        private const int ESP32_PORT = 80;
        private Boolean door_status = false;
        // for mixed color room
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            client = new TcpClient(ESP32_IP, ESP32_PORT);
            stream = client.GetStream();

            Image img1 = Image.FromFile("your file path");

            Image resizedImage1 = ResizeImage(img1, button7.Size);
            button7.Image = resizedImage1;

            button7.BackgroundImageLayout = ImageLayout.Zoom;

           
            //button1.Click += button1_Click;
            //button1.BackColor = Color.Blue;

        }

        private void SendCommand(string command)
        {
            byte[] data = Encoding.ASCII.GetBytes(command);
            stream.Write(data, 0, data.Length);

            //byte[] receiveBuffer = new byte[1024];
            //int bytesRead = stream.Read(receiveBuffer, 0, receiveBuffer.Length);

            //string response = Encoding.ASCII.GetString(receiveBuffer, 0, bytesRead);
            //MessageBox.Show(response, "Response from ESP32");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Button clickedButton = sender as Button; // Get the button that was clicked
            if (button1.BackColor != null)
            {
                
                    SendCommand("LED_OFF_Corridor\n");
                    button1.BackColor = Color.White;
                    pictureBox1.Image = Image.FromFile("your file path \\OFF1.jpg");
                    button2.BackColor = Color.White;

            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.BackColor != null)
            {
                SendCommand("LED_ON_Corridor\n");
                    button2.BackColor = Color.Blue;
                    pictureBox1.Image = Image.FromFile("your file path\\ON1.jpg");

              
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            stream.Close();
            client.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.BackColor != null)
            {

                SendCommand("LED_OFF_Hotel\n");
                button4.BackColor = Color.White;
                pictureBox2.Image = Image.FromFile("your file path\\OFF1.jpg");
                button3.BackColor = Color.White;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.BackColor != null)
            {
                SendCommand("LED_ON_Hotel\n");
                button3.BackColor = Color.Blue;
                pictureBox2.Image = Image.FromFile("your file path\\ON1.jpg");


            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (button6.BackColor != null)
            {

                SendCommand("LED_OFF_Front\n");
                button6.BackColor = Color.White;
                pictureBox3.Image = Image.FromFile("your file path\\OFF1.jpg");
                button5.BackColor = Color.White;

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.BackColor != null)
            {
                SendCommand("LED_ON_Front\n");
                button5.BackColor = Color.Blue;
                pictureBox3.Image = Image.FromFile("your file path\\ON1.jpg");


            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

            if (button7.Enabled && door_status == false)
            {
                
                SendCommand("OPEN_door\n");
                pictureBox4.Image = Image.FromFile("your file path\\OPEN_door.jpg");
            
                

                Image img = Image.FromFile("your file path\\OPEN.jpg");

                // Set the button's Image property to the loaded image
                Image resizedImage = ResizeImage(img, button7.Size);
                button7.Image = resizedImage;

                // Adjust the button's properties to display the image correctly
                button7.BackgroundImageLayout = ImageLayout.Zoom;
                door_status = true;
            }
            else if(button7.Enabled && door_status == true) {
                SendCommand("CLOSE_door\n");
                pictureBox4.Image = Image.FromFile("your file path\\CLOSE_door.jpg");

                Image img = Image.FromFile("your file path\\CLOSE.jpg");

                Image resizedImage = ResizeImage(img, button7.Size);
                button7.Image = resizedImage;

                button7.BackgroundImageLayout = ImageLayout.Zoom;
                door_status = false;

            }
        }

        private Image ResizeImage(Image img, Size size)
        {
            return new Bitmap(img, size);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (button9.BackColor != null)
            {

                SendCommand("light-sensor_OFF\n");
                button9.BackColor = Color.White;
                pictureBox5.Image = Image.FromFile("your file path \\light-sensor_OFF.jpg");
                button8.BackColor = Color.White;

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (button8.BackColor != null)
            {
                SendCommand("light-sensor_ON\n");
                button8.BackColor = Color.Blue;
                pictureBox5.Image = Image.FromFile("your file path\\light-sensor_ON.jpg");


            }
        }

       
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "ON_L")
            {
                SendCommand("living_room_ON\n");
                comboBox1.BackColor = Color.YellowGreen;
            }
            else if (comboBox1.SelectedItem.ToString() == "OFF_L")
            {
                SendCommand("living_room_OFF\n");
                comboBox1.BackColor = Color.White;
            }
            else if (comboBox1.SelectedItem.ToString() == "DarkBlue")
            {
                SendCommand("living_room_DarkBlue\n");
                comboBox1.BackColor = Color.DarkBlue;
            }
            else if (comboBox1.SelectedItem.ToString() == "GreenBlue")
            {
                SendCommand("living_room_GreenBlue\n");
                comboBox1.BackColor = Color.SeaGreen;
            }
            else if (comboBox1.SelectedItem.ToString() == "RedGreen")
            {
                SendCommand("living_room_RedGreen\n");
                comboBox1.BackColor = Color.YellowGreen;
            }
            /*else if (comboBox1.SelectedItem.ToString() == "MIX COLOR")
            {
                
                int red = random.Next(256);     // Generates a random value between 0 and 255
                int green = random.Next(256);
                int blue = random.Next(256);
                comboBox1.BackColor = Color.FromArgb(red, green, blue);

                string redHex = red.ToString("X2");      // Convert red to hexadecimal string with 2 digits
                string greenHex = green.ToString("X2");  // Convert green to hexadecimal string with 2 digits
                string blueHex = blue.ToString("X2");    // Convert blue to hexadecimal string with 2 digits

                // Concatenate the hexadecimal strings to form the color code string
                string colorCode = "#" + redHex + greenHex + blueHex;
                SendCommand(colorCode);
                
            }*/
        }
    }
}
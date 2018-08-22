using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace parking_meter
{
	public partial class Form1 : Form
	{
		//Using panel as titlebar
		public Point msloc;
		public const int WM_NCLBUTTONDOWN = 0xA1;
		public const int HT_CAPTION = 0x2;
		[DllImportAttribute("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
		[DllImportAttribute("user32.dll")]
		public static extern bool ReleaseCapture();

		// Global Variables
		double parkingprice;
		double ttlhrs;
		double parkingpayment;
		int change;
		decimal btnpress050;
		decimal btnpress1;
		decimal btnpress2;
		decimal btnpress5;
		decimal btnpress10;
		decimal btnpress20;
		decimal btnpress50;
		decimal btnpress100;
		decimal btnpress200;



		public Form1()
		{
			InitializeComponent();
		}

		//Make Panel Drag Window
		private void panel1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				ReleaseCapture();
				SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
			}
		}

		//Close the Program
		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		//Minimize The Program
		private void button3_Click(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
		}

		//Make the label to drag the Window
		private void label1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				ReleaseCapture();
				SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
			}
		}

		//Make the icon to drag the Window
		private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				ReleaseCapture();
				SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
			}
		}

		//Set Time Format
		private void Form1_Load(object sender, EventArgs e)
		{
			dtpEntry.CustomFormat = "HH:mm";
			dtpExit.CustomFormat = "HH:mm";
		}

		//Increase or decrease amount to pay R0,50
		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{

			if (nudR050.Value < btnpress050)
			{
				parkingpayment = parkingpayment - 0.50;
			}
			else if (nudR050.Value > btnpress050)
			{
				parkingpayment = parkingpayment + 0.50;
			}
			lblTest.Text = "R " + parkingpayment.ToString("F");
			btnpress050 = nudR050.Value;

		}

	
		//Calculate The Amount to Pay
		private void button2_Click(object sender, EventArgs e)
		{


			TimeSpan shoptime = dtpExit.Value.Subtract(dtpEntry.Value);
			ttlhrs = Math.Round(shoptime.TotalHours, 2);
			lblText.Text = "Total Hrs = " + ttlhrs.ToString();

			if (ttlhrs <= 1)
			{
				parkingprice = 0;
				lblPrice.Text = "R " + parkingprice.ToString("F");
				redOut.Text = "Your Parking is Free\nHave a nice day";
			}

			else if (ttlhrs <= 2 & ttlhrs > 1)
			{
				parkingprice = 5;
				lblPrice.Text = "R " + parkingprice.ToString("F");
			}

			else if (ttlhrs <= 4 & ttlhrs > 2)
			{
				parkingprice = 8;
				lblPrice.Text = "R " + parkingprice.ToString("F");
			}


			else if (ttlhrs <= 6 & ttlhrs > 4)
			{
				parkingprice = 11;
				lblPrice.Text = "R " + parkingprice.ToString("F");
			}

			else if (ttlhrs <= 10 & ttlhrs > 6)
			{
				parkingprice = 14;
				lblPrice.Text = "R " + parkingprice.ToString("F");
			}

			else if (ttlhrs <= 20 & ttlhrs > 10)
			{
				parkingprice = 25;
				lblPrice.Text = "R " + parkingprice.ToString("F");
			}

			else if (ttlhrs <= 24 & ttlhrs > 20)
			{
				parkingprice = 35;
				lblPrice.Text = "R " + parkingprice.ToString("F");
			}

			else if (ttlhrs > 24)
			{
				parkingprice = 50;
				lblPrice.Text = "R " + parkingprice.ToString("F");
			}


		}

         //Payment and Change Calculation

		private void btnPay_Click(object sender, EventArgs e)
		{
			change = Convert.ToInt32(parkingpayment - parkingprice); 
			if (change < 0)
			{
				MessageBox.Show("Please Enter Enough Cash");
				nudR050.Value = 0;
				nudR1.Value = 0;
				nudR2.Value = 0;
				nudR5.Value = 0;
				nudR10.Value = 0;
				nudR20.Value = 0;
				nudR50.Value = 0;
				nudR100.Value = 0;
				nudR200.Value = 0;
				parkingpayment = 0;
				lblPrice.Text = "R " + parkingprice.ToString("F");
			}

			else if(change == 0)
			{
				redOut.Text = "Change = R 0,00\nHave a nice day";
			}

			else if (change> 0)
			{
				double den05,den1,den2,den5,den10,den20,den50,den100,den200;

				den200 = change / 200;
				den100 = (change% 200)/100;
				den50  = (((change % 200)%100)/50);
				den20  = (((((change % 200) % 100)%50)/20));
				den10  = ((((((change % 200) % 100) % 50)%20)/10));
				den5   = (((((((change % 200) % 100) % 50) % 20)%10)/5));
				den2   = ((((((((change % 200) % 100) % 50) % 20) % 10) % 5)/ 2));
				den1   = (((((((((change % 200) % 100) % 50) % 20) % 10)%5)%2)/1));
				den05  = ((((((((((change % 200) % 100) % 50) % 20) % 10) % 5) % 2)%1)/0.5));
				
				redOut.ResetText();
				redOut.Text= "Total Hours: " + ttlhrs.ToString()+
							 "\nYour Payment: "+parkingpayment.ToString()+
							 "\nYour Change: "+change.ToString()+
							 "\nDenominations: "+
							 "\nR200,00 x" + den200.ToString()+
							 "\nR100,00 x" + den100.ToString() +
							 "\nR50,00  x" + den50.ToString() +
							 "\nR20,00  x" + den20.ToString() +
							 "\nR10,00  x" + den10.ToString() +
							 "\nR5,00   x" + den5.ToString() +
							 "\nR2,00   x" + den2.ToString() +
							 "\nR1,00   x" + den1.ToString() +
							 "\nR0,50   x" + den05.ToString() 
							 ;
				using (StreamWriter record = File.AppendText("record.txt"))
				{

					record.Write("Start Time: " + dtpEntry.Value.ToString("HH:mm") +
								  "\nEnd Time: " + dtpExit.Value.ToString("HH:mm") +
								  "\nPrice: R " + parkingprice.ToString("F") +
								  "\nPayment:  R " + parkingpayment.ToString("F") +
								  "\nChange:  R " + change.ToString("F")
								  );

				}


			}




		}

		//Increase or decrease amount to pay R1,00
		private void nudR1c_ValueChanged(object sender, EventArgs e)
		{
			if (nudR1.Value < btnpress1)
			{
				parkingpayment = parkingpayment - 1.00;
			}
			else if (nudR1.Value > btnpress1)
			{
				parkingpayment = parkingpayment + 1.00;
			}
			lblTest.Text = "R " + parkingpayment.ToString("F");
			btnpress1 = nudR1.Value;
		}

		//Increase or decrease amount to pay R2,00
		private void nudR2_ValueChanged(object sender, EventArgs e)
		{
			if (nudR2.Value < btnpress2)
			{
				parkingpayment = parkingpayment - 2.00;
			}
			else if (nudR2.Value > btnpress2)
			{
				parkingpayment = parkingpayment + 2.00;
			}
			lblTest.Text = "R " + parkingpayment.ToString("F");
			btnpress2 = nudR2.Value;
		}

		//Increase or decrease amount to pay R5,00
		private void nudR5_ValueChanged(object sender, EventArgs e)
		{
			if (nudR5.Value < btnpress5)
			{
				parkingpayment = parkingpayment - 5.00;
			}
			else if (nudR5.Value > btnpress5)
			{
				parkingpayment = parkingpayment + 5.00;
			}
			lblTest.Text = "R " + parkingpayment.ToString("F");
			btnpress5 = nudR5.Value;
		}

		//Increase or decrease amount to pay R10,00
		private void nudR10_ValueChanged(object sender, EventArgs e)
		{
			if (nudR10.Value < btnpress10)
			{
				parkingpayment = parkingpayment - 10.00;
			}
			else if (nudR10.Value > btnpress10)
			{
				parkingpayment = parkingpayment + 10.00;
			}
			lblTest.Text = "R " + parkingpayment.ToString("F");
			btnpress10 = nudR10.Value;
		}

		//Increase or decrease amount to pay R20,00
		private void nudR20_ValueChanged(object sender, EventArgs e)
		{
			if (nudR20.Value < btnpress20)
			{
				parkingpayment = parkingpayment - 20.00;
			}
			else if (nudR20.Value > btnpress20)
			{
				parkingpayment = parkingpayment + 20.00;
			}
			lblTest.Text = "R " + parkingpayment.ToString("F");
			btnpress20 = nudR20.Value;
		}

		//Increase or decrease amount to pay R50,00
		private void nudR50_ValueChanged(object sender, EventArgs e)
		{
			if (nudR50.Value < btnpress50)
			{
				parkingpayment = parkingpayment - 50.00;
			}
			else if (nudR50.Value > btnpress50)
			{
				parkingpayment = parkingpayment + 50.00;
			}
			lblTest.Text = "R " + parkingpayment.ToString("F");
			btnpress50 = nudR50.Value;
		}

		//Increase or decrease amount to pay R100,00
		private void nudR100_ValueChanged(object sender, EventArgs e)
		{
			if (nudR100.Value < btnpress100)
			{
				parkingpayment = parkingpayment - 100.00;
			}
			else if (nudR100.Value > btnpress100)
			{
				parkingpayment = parkingpayment + 100.00;
			}
			lblTest.Text = "R " + parkingpayment.ToString("F");
			btnpress100 = nudR100.Value;
		}

		//Increase or decrease amount to pay R200,00
		private void nudR200_ValueChanged(object sender, EventArgs e)
		{
			if (nudR200.Value < btnpress200)
			{
				parkingpayment = parkingpayment - 200.00;
			}
			else if (nudR200.Value > btnpress200)
			{
				parkingpayment = parkingpayment + 200.00;
			}
			lblTest.Text = "R " + parkingpayment.ToString("F");
			btnpress200 = nudR200.Value;
		}


		//Reset The Program

		private void btnReset_Click(object sender, EventArgs e)
		{
			dtpEntry.ResetText();
			dtpExit.ResetText();
			ttlhrs = 0;
			lblText.Text = "Total Hrs = " + ttlhrs.ToString();
			parkingprice = 0;
			lblPrice.Text = "R " + parkingprice.ToString("F");
			redOut.Clear();
			nudR050.Value = 0;
			nudR1.Value = 0;
			nudR2.Value = 0;
			nudR5.Value = 0;
			nudR10.Value = 0;
			nudR20.Value = 0;
			nudR50.Value = 0;
			nudR100.Value = 0;
			nudR200.Value = 0;
			parkingpayment = 0;
			lblTest.Text = "R " + parkingpayment.ToString("F");
		}

		//Admin Settings To Display or Clear The Text File
		private void btnAdmin_Click(object sender, EventArgs e)
		{
			string adminQ = Microsoft.VisualBasic.Interaction.InputBox("Write C to Clear or D to Display", "Display Or Clear",
				"",0,0);

			if (adminQ.ToString().ToUpper() == "C")
			{
				File.WriteAllText("record.txt", string.Empty);
				TextReader readtext = new StreamReader("record.txt");
				redOut.Text = readtext.ReadToEnd();
				readtext.Close();

			}
			else if(adminQ.ToString().ToUpper() == "D")
			{
				TextReader readtext = new StreamReader("record.txt");
				redOut.Text = readtext.ReadToEnd();
				readtext.Close();
			}
			else
			{
				MessageBox.Show("Write Only D or C");
			}
		}
	}


	/* Move window/form without Titlebar in C#
	 *  Author: FreewareFire
	 *  link :https://bit.ly/2OFR8yh
	 *  
	 *  Parking Meter icon made by Freepik 
	 *  link https://www.flaticon.com/authors/freepik
	 */
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VncSharp;

namespace IOS_AutoLead
{
    public partial class Form3 : Form
    {
       
        public static string MouseMove ="0,0";
        public static string ScriptCode = string.Empty;
        public Form3()
        {
            InitializeComponent();
           
            string host = iStatic.ipIphone;


            if (host != null)
            {
                try
                {

                    rd.Connect(host, false, true);

                    rd.SetScalingMode(true);
                    rd.FullScreenUpdate();
                }
                catch (VncProtocolException vex)
                {
                    MessageBox.Show(this,
                                    string.Format("Unable to connect to VNC host:\n\n{0}.\n\nCheck that a VNC host is running there.", vex.Message),
                                    string.Format("Unable to Connect to {0}", host),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    iStatic.closeForm3 = "success";
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this,
                                    string.Format("Unable to connect to host.  Error was: {0}", ex.Message),
                                    string.Format("Unable to Connect to {0}", host),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    iStatic.closeForm3 = "success";
                    this.Close();
                }
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            // If the user tries to close the window without doing a clean
            // shutdown of the remote connection, do it for them.
            if (rd.IsConnected)
                rd.Disconnect();

            base.OnClosing(e);
        }

      
        private void Form3_Load(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            this.Location = new Point(frm.Location.X + frm.Size.Width -50, frm.Location.Y-60);
        }

        private void rd_ConnectComplete(object sender, ConnectEventArgs e)
        {

            ClientSize = new Size(e.DesktopWidth, e.DesktopHeight);

            // Change the Form's title to match the remote desktop name
            Text = e.DesktopName;
         
           
            // Give the remote desktop focus now that it's connected
            rd.Focus();
        }

        private void rd_ConnectionLost(object sender, EventArgs e)
        {
            iStatic.closeForm3 = "success";
            this.Close();
        }
        int x, y;
        public static int xm, ym;
        int xu, yu;

        private void rd_MouseUp(object sender, MouseEventArgs e)
        {
            MouseMove = "Touch(" + xm + "," + ym + ")";
            if (Form1.record == 1)
            {
                xu = e.X;
                yu = e.Y;
                if ((x + y) == (xu + ym))
                {
                    ScriptCode = "Touch(" + x + "," + y + ")";
                    ScriptCode += "\r\n";
                    ScriptCode += "Wait(" + Math.Round((DateTime.Now - this.recprev).TotalMilliseconds / 1000.0, 2).ToString() + ")";
                    ScriptCode += "\r\n";
                }
                else
                {
                    ScriptCode = "Swipe(" + x + "," + y + "," + xu + "," + yu + "," + Math.Round((DateTime.Now - this.recprev).TotalMilliseconds / 1000.0, 2).ToString() + ")";
                    ScriptCode += "\r\n";
                }
            }
            recprev = DateTime.Now;
        }

        private void rd_MouseMove(object sender, MouseEventArgs e)
        {
            xm = e.X;
            ym = e.Y;
            
        }
        DateTime recprev ;
        int d = 0;
        private void rd_MouseDown(object sender, MouseEventArgs e)
        {
            if (Form1.record ==1 && d !=0)
            {
                
                ScriptCode = "Wait(" + Math.Round((DateTime.Now - this.recprev).TotalMilliseconds / 1000.0, 2).ToString() + ")";
                ScriptCode += "\r\n";
                x = e.X;
                y = e.Y;
                recprev = DateTime.Now;
            }
            d = 1;
        }

    }
}

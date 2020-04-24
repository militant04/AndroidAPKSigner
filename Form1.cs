using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Web;

namespace AndroidSigner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // progressBar2.Visible = false;
            txtJarsigner.Text = "C:/Key";
            txtZipAligner.Text = "C:/Key";
            txtZipAligner.ReadOnly = true;
            txtJarsigner.ReadOnly = true;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

       

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //progressBar2.Visible = true;
          
            button1.Text = "Please Wait...";
            button1.Enabled = false;
            if (txtUnsignedAPK.Text.Length < 4)
            {
                MessageBox.Show("Enter path to access the APK file you want to sign");
                return;
            }

            if (txtSignedAPK.Text.Length < 4)
            {
                MessageBox.Show("Enter the Output name of the signed APK");
                return;
            }
            //string path = HttpContext.Current.Server.MapPath("Packages");
            string argument = @"/C jarsigner -verbose -keypass militant  -sigalg SHA1withRSA -digestalg SHA1 -keystore C:\Key\my-release-key.keystore" + " " + txtUnsignedAPK.Text + " " + "alias_name -storepass militant";
            Process p = new Process
          
            {
                StartInfo =
                    {
                        WorkingDirectory = @"C:\Program Files\Java\jdk1.8.0_231\bin",
                        FileName =  "cmd.exe",
                        //Arguments = @"/C mkdir C:\Key\asd" ,
                        Arguments =  argument,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = false,
                        RedirectStandardInput = false,
                        CreateNoWindow = true,
                       
            //WorkingDirectory = @"C:\Program Files\wkhtmltopdf\bin"
        }
            };

            try
            {
               // progressBar2.Value = 50;
                p.Start();
                
                textBox5.Text = p.StandardOutput.ReadToEnd();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //textBox5.Text = ex.Message;
                //Log(ex.Message, "Fee Generated");
            }

            // I think it's timimg out here for shoo
            // Emmanuel Picked this up 
            // This should be put conditionally if records are more then increase the wait period
            p.WaitForExit(999999999);

            //dataLayer.LogStep("p WaitForExit", "Generate PDF finished waiting");

            //p.WaitForExit(); 

            int returnCode = p.ExitCode;
            p.Close();

            

            //dataLayer.LogStep("PDF Generator", "PDF successfully generated");

            if (returnCode != 0 && returnCode != 2)
            {
                //throw new Exception();
            }

           

            p.Dispose();
           // progressBar2.Visible = false;

            processZipalign();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void progressBar2_Click(object sender, EventArgs e)
        {

        }


        public void processZipalign()
        {

            Process proc = new Process
            {
                StartInfo =
                    {
                        WorkingDirectory = @"C:\Key",
                        FileName =  "cmd.exe",
                        //Arguments = @"/C mkdir C:\Key\asd" ,
                        Arguments = @"/C zipalign -v 4 "+" "+txtUnsignedAPK.Text+" "+" "+txtSignedAPK.Text+".apk" ,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = false,
                        RedirectStandardInput = false,
                        CreateNoWindow = true,
                       
            //WorkingDirectory = @"C:\Program Files\wkhtmltopdf\bin"
             }
            };

            try
            {
               // progressBar2.Value = 50;
                proc.Start();

                textBox5.Text = proc.StandardOutput.ReadToEnd();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //textBox5.Text = ex.Message;
                //Log(ex.Message, "Fee Generated");
            }

            // I think it's timimg out here for shoo
            // Emmanuel Picked this up 
            // This should be put conditionally if records are more then increase the wait period
            proc.WaitForExit(999999999);

            //dataLayer.LogStep("p WaitForExit", "Generate PDF finished waiting");

            //p.WaitForExit(); 

            int returnCode = proc.ExitCode;
            proc.Close();



            //dataLayer.LogStep("PDF Generator", "PDF successfully generated");

            if (returnCode != 0 && returnCode != 2)
            {
                //throw new Exception();
            }



            proc.Dispose();
           // progressBar2.Visible = false;
            button1.Enabled = true;
            button1.Text = "Generate Another Signed APK";

            MessageBox.Show("APK was signed Successfully!!!!");
            Process.Start(@"C:\Key");

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}

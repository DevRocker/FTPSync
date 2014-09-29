using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.FtpClient;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTPSyncWinConsole
{
    public partial class FTPSync : Form
    {
        public const string FtpServer = "mrtesting.net84.net";
        public const string FtpUserName = "a8767344";
        public const string FtpPassword = "backup94";
        public const string File = @"C:\Users\MrDance\Desktop\steve-steve-jobs-black-and-white-2513383-1920x1200.jpg";
       

        public FTPSync()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            FtpClient ftp = new FtpClient(FtpServer, FtpUserName, FtpPassword);
            ftp.Login();
            ftp.Upload(File);
            ftp.Close();
        }

    }
}

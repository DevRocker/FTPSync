using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.FtpClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTPSyncWinConsole
{
    public partial class FTPSync : Form
    {
        static ManualResetEvent m_reset = new ManualResetEvent(false);
        public FTPSync()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            BeginConnect();
        }

        public  void BeginConnect()
        {
            // The using statement here is OK _only_ because m_reset.WaitOne()
            // causes the code to block until the async process finishes, otherwise
            // the connection object would be disposed early. In practice, you
            // typically would not wrap the following code with a using statement.
            using (FtpClient conn = new FtpClient())
            {
                m_reset.Reset();

                conn.Host = txtHost.Text;
                conn.Port = 990;
                conn.Credentials = new NetworkCredential(txtUser.Text,txtPassword.Text);
                conn.BeginConnect(new AsyncCallback(ConnectCallback), conn);

                m_reset.WaitOne();
                conn.Disconnect();
            }
        }

        static void ConnectCallback(IAsyncResult ar)
        {
            FtpClient conn = ar.AsyncState as FtpClient;

            try
            {
                if (conn == null)
                    throw new InvalidOperationException("The FtpControlConnection object is null!");

                conn.EndConnect(ar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                m_reset.Set();
            }
        }

    }
}

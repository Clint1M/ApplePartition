using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ApplePartition
{
    public partial class Compartment : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeft,
            int nTop,
            int nRight,
            int nBottom,
            int nWidthEllipse,
            int nHeightEllipse
            );
        public Compartment()
        {
            InitializeComponent();
        }
        private void Compartment_Load(object sender, EventArgs e)
        {
            btnTranslate.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnTranslate.Width, btnTranslate.Height, 10, 10));
            btnCancel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnCancel.Width, btnCancel.Height, 10, 10));
            rtxtState.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, rtxtState.Width, rtxtState.Height, 10, 10));
            rtxtCondition.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, rtxtCondition.Width, rtxtCondition.Height, 10, 10));
        }
        private void btnTranslate_Click(object sender, EventArgs e)
        {
            string fileOp = "C:\\C\\CS\\ApplePartition\\Data_R.txt";
            string content = File.ReadAllText(fileOp);

            rtxtState.Text = content;
            this.Refresh();
            Thread.Sleep(600);

            content = content.Replace("Brown spots", "Rotten");
            content = content.Replace("Mold growth", "Rotten");
            content = content.Replace("Soft Texture", "Rotten");
            content = content.Replace("Good state", "Fresh");
            content = content.Replace("F_Obj_S", "Result");

            string fileWr = @"C:\\C\\CS\\ApplePartition\\Data_F.txt";

            FileInfo fileInf = new FileInfo(fileWr);
            if (!fileInf.Directory.Exists)
            {
                Directory.CreateDirectory(fileInf.Directory.FullName);
            }
            File.WriteAllText(fileWr, content);
            rtxtCondition.Text = content;

            this.Refresh();
            Thread.Sleep(1000);
            DialogResult reS = MessageBox.Show("Translation Successful", "Status", MessageBoxButtons.OK);
            if (reS == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            rtxtState.Text = "";
            this.Refresh();
            Thread.Sleep(600);
            rtxtCondition.Text = "";
            Application.Exit();
        }
    }
}
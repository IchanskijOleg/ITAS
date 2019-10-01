using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITAS
{
    public partial class FmMain : Form
    {
        private PolicyXmlConvert policyPanel;


        public FmMain()
        {
            InitializeComponent();
            policyPanel = new PolicyXmlConvert();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SwitchContent(policyPanel);

        }

        private void SwitchContent(Control control)
        {
            container.Controls.Clear();
            container.Controls.Add(control);
        }

        private void MenuButton_MouseEnter(object sender, EventArgs e)
        {
            var button = sender as Button;
            button.BackColor = Color.Red;
        }

        private void MenuButton_MouseLeave(object sender, EventArgs e)
        {
            var button = sender as Button;
            button.BackColor = Color.Transparent;
        }
    }
}

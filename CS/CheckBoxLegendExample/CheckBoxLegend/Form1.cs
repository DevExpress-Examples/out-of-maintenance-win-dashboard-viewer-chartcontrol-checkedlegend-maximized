using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckBoxLegend
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ChartLegendHelper helper = new CheckBoxLegend.ChartLegendHelper();
            helper.Attach(dashboardViewer1, "chartDashboardItem1");
            dashboardViewer1.LoadDashboard("nwindDashboard.xml");
        }
    }
}

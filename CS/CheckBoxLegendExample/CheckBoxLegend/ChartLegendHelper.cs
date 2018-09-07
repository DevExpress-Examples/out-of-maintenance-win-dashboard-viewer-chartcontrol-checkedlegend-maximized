using DevExpress.DashboardWin;
using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckBoxLegend
{
    class ChartLegendHelper
    {
        DashboardViewer viewer;
        DashboardDesigner designer;
        string chartItemName;

        List<ChartControl> chartStorage;
        List<string> uncheckedSeries = new List<string>();


        public void Attach( DashboardViewer viewer, string chartItemName)
        {
            if (designer != null || viewer != null)
                Detach();
            chartStorage = new List<ChartControl>();
            this.viewer = viewer;
            this.chartItemName = chartItemName;
            viewer.DashboardItemControlCreated += DashboardItemControlCreated;
            viewer.DashboardItemControlUpdated += DashboardItemControlUpdated;
            viewer.DashboardItemBeforeControlDisposed += DashboardItemBeforeControlDisposed;

        }

        public void Attach(DashboardDesigner designer, string chartItemName)
        {
            if (designer != null || viewer != null)
                Detach();
            chartStorage = new List<ChartControl>();
            this.designer = designer;
            this.chartItemName = chartItemName;
            designer.DashboardItemControlCreated += DashboardItemControlCreated;
            designer.DashboardItemControlUpdated += DashboardItemControlUpdated;
            designer.DashboardItemBeforeControlDisposed += DashboardItemBeforeControlDisposed;
        }

        private void DashboardItemBeforeControlDisposed(object sender, DashboardItemControlEventArgs e)
        {
            if (e.DashboardItemName!=chartItemName || e.ChartControl == null) return;

            e.ChartControl.LegendItemChecked -= ChartControl_LegendItemChecked;
            chartStorage.Remove(e.ChartControl);
        }

        private void DashboardItemControlUpdated(object sender, DashboardItemControlEventArgs e)
        {
            if (e.DashboardItemName != chartItemName || e.ChartControl == null) return;

            ApplyCheckedState(e.ChartControl);
        }

        private void ApplyCheckedState(ChartControl chart)
        {
            foreach (Series series in chart.Series)
            {
                series.Visible = !uncheckedSeries.Contains(series.Name);
                series.CheckedInLegend = !uncheckedSeries.Contains(series.LegendTextPattern);
            }

        }

        private void DashboardItemControlCreated(object sender, DashboardItemControlEventArgs e)
        {
            if (e.DashboardItemName != chartItemName || e.ChartControl == null) return;
            chartStorage.Add(e.ChartControl);
            e.ChartControl.Legend.MarkerMode = DevExpress.XtraCharts.LegendMarkerMode.CheckBoxAndMarker;
            e.ChartControl.LegendItemChecked += ChartControl_LegendItemChecked;
        }

        private void ChartControl_LegendItemChecked(object sender, DevExpress.XtraCharts.LegendItemCheckedEventArgs e)
        {
            ChartControl chart = (ChartControl)sender;
            if (e.CheckedElement is Series)
            {
                var series = (Series)e.CheckedElement;
                chart.Series[series.LegendTextPattern].Visible = e.NewCheckState;
                if (!e.NewCheckState)
                {
                    if (!uncheckedSeries.Contains(series.LegendTextPattern))
                        uncheckedSeries.Add(series.LegendTextPattern);
                }
                else if (uncheckedSeries.Contains(series.LegendTextPattern))
                    uncheckedSeries.Remove(series.LegendTextPattern);
            }

            // Update minimized chart
            if ( chartStorage.Count > 1)
            foreach(ChartControl chartControl in chartStorage)
            {
                if (chart == chartControl) continue;
                    ApplyCheckedState(chartControl);
            }
        }


        public void Detach()
        {
            this.chartItemName = string.Empty;
            if (this.designer != null)
            {
                designer.DashboardItemControlCreated -= DashboardItemControlCreated;
                designer.DashboardItemControlUpdated -= DashboardItemControlUpdated;
                designer.DashboardItemBeforeControlDisposed -= DashboardItemBeforeControlDisposed;
                this.designer = null;
            }
            if (this.viewer != null)
            {
                viewer.DashboardItemControlCreated -= DashboardItemControlCreated;
                viewer.DashboardItemControlUpdated -= DashboardItemControlUpdated;
                viewer.DashboardItemBeforeControlDisposed -= DashboardItemBeforeControlDisposed;
                this.viewer = null;
            }
                chartStorage = null;


        }
    }
}

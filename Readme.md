# How to Apply the ChartControl CheckedLegend State to the Maximized ChartDashboardItem 

This example demonstrates how to obtain the ChartDashboardItem's ChartControl settings modified by end-user and apply them to the [ChartDashboardItem](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.ChartDashboardItem) in [maximized mode](https://docs.devexpress.com/Dashboard/15619/creating-dashboards/creating-dashboards-in-the-winforms-designer/dashboard-layout/dashboard-items-layout).
The end-user can check or uncheck series items in the chart legend to select visible series. These settings are retained when the ChartDashboardItem is maximized; and modifications in maximized mode are applied to the ChartDashboardItem when it is restored to its initial state. 

![](~/images/win-dashboard-viewer-chartcontrol-checkedlegend-maximized.png)

When the end-user checks the legend check box for a chart series, the [ChartControl.LegendItemChecked](https://docs.devexpress.com/WindowsForms/DevExpress.XtraCharts.ChartControl.LegendItemChecked) occurs. The event handler updates the list of unchecked series names, mainatined in the application main form and applies checked state to the chart controls in the application storage.

When the Chart item is maximized, a new instance of the ChartDashboardItem is created and the [DashboardViewer.DashboardItemControlCreated](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWin.DashboardViewer.DashboardItemControlCreated) event occurs. In the event handler we add the chart control to the inner application storage and subscribe to the [ChartControl.LegendItemChecked](https://docs.devexpress.com/WindowsForms/DevExpress.XtraCharts.ChartControl.LegendItemChecked) event. 

When the [DashboardViewer.DashboardItemControlUpdated](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWin.DashboardViewer.DashboardItemControlUpdated) event occurs, we uncheck chart series for the chart control that originated the event. Series names are retrieved from the list of unchecked series names maintained by the application. The [SeriesBase.CheckedInLegend](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraCharts.SeriesBase.CheckedInLegend) property is changed for the required series.

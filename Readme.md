<!-- default file list -->
*Files to look at*:

* [ChartLegendHelper.cs](./CS/CheckBoxLegendExample/CheckBoxLegend/ChartLegendHelper.cs)
<!-- default file list end -->

# Dashboard for WinForms - How to apply the ChartControl CheckedLegend state to the maximized ChartDashboardItem

This example demonstrates how to obtain the ChartDashboardItem's ChartControl settings modified by end-user and apply them to the [ChartDashboardItem](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.ChartDashboardItem) in [maximized mode](https://docs.devexpress.com/Dashboard/15619/creating-dashboards/creating-dashboards-in-the-winforms-designer/dashboard-layout/dashboard-items-layout).
The end-user can check or uncheck series items in the chart legend to select visible series. These settings are retained when the ChartDashboardItem is maximized; and modifications in maximized mode are applied to the ChartDashboardItem when it is restored to its initial state. 

![](https://github.com/DevExpress-Examples/win-dashboard-viewer-chartcontrol-checkedlegend-maximized/blob/18.1.3%2B/images/win-dashboard-viewer-chartcontrol-checkedlegend-maximized.png)

When the end-user checks the legend check box for a chart series, the [ChartControl.LegendItemChecked](https://docs.devexpress.com/WindowsForms/DevExpress.XtraCharts.ChartControl.LegendItemChecked) occurs. The event handler updates the list of unchecked series names, mainatined in the application main form and applies checked state to the chart controls in the application storage.

When the Chart item is maximized, a new instance of the ChartDashboardItem is created and the [DashboardViewer.DashboardItemControlCreated](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWin.DashboardViewer.DashboardItemControlCreated) event occurs. In the event handler we add the chart control to the inner application storage and subscribe to the [ChartControl.LegendItemChecked](https://docs.devexpress.com/WindowsForms/DevExpress.XtraCharts.ChartControl.LegendItemChecked) event. 

When the [DashboardViewer.DashboardItemControlUpdated](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWin.DashboardViewer.DashboardItemControlUpdated) event occurs, we uncheck chart series for the chart control that originated the event. Series names are retrieved from the list of unchecked series names maintained by the application. The [SeriesBase.CheckedInLegend](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraCharts.SeriesBase.CheckedInLegend) property is changed for the required series.

## Documentation

- [Chart](https://docs.devexpress.com/Dashboard/14719/winforms-dashboard/winforms-designer/create-dashboards-in-the-winforms-designer/dashboard-item-settings/chart?p=netframework)
- [Access to Underlying Controls](https://docs.devexpress.com/Dashboard/401095/winforms-dashboard/winforms-designer/access-to-underlying-controls?p=netframework)

## More Examples

- [Dashboard for WinForms - How to Display Each Series in a Separate Pane for Chart Dashboard Items](https://github.com/DevExpress-Examples/how-to-display-each-series-in-a-separate-pane-for-chart-dashboard-items)

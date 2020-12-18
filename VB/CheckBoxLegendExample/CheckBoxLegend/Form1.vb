Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

Namespace CheckBoxLegend
	Partial Public Class Form1
		Inherits Form

		Public Sub New()
			InitializeComponent()
			Dim helper As ChartLegendHelper = New CheckBoxLegend.ChartLegendHelper()
			helper.Attach(dashboardViewer1, "chartDashboardItem1")
			dashboardViewer1.LoadDashboard("nwindDashboard.xml")
		End Sub
	End Class
End Namespace

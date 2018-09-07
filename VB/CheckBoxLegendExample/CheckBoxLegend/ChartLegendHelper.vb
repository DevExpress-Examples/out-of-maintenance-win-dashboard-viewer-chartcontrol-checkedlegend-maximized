Imports DevExpress.DashboardWin
Imports DevExpress.XtraCharts
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace CheckBoxLegend
    Friend Class ChartLegendHelper
        Private viewer As DashboardViewer
        Private designer As DashboardDesigner
        Private chartItemName As String

        Private chartStorage As List(Of ChartControl)
        Private uncheckedSeries As New List(Of String)()


        Public Sub Attach(ByVal viewer As DashboardViewer, ByVal chartItemName As String)
            If designer IsNot Nothing OrElse viewer IsNot Nothing Then
                Detach()
            End If
            chartStorage = New List(Of ChartControl)()
            Me.viewer = viewer
            Me.chartItemName = chartItemName
            AddHandler viewer.DashboardItemControlCreated, AddressOf DashboardItemControlCreated
            AddHandler viewer.DashboardItemControlUpdated, AddressOf DashboardItemControlUpdated
            AddHandler viewer.DashboardItemBeforeControlDisposed, AddressOf DashboardItemBeforeControlDisposed

        End Sub

        Public Sub Attach(ByVal designer As DashboardDesigner, ByVal chartItemName As String)
            If designer IsNot Nothing OrElse viewer IsNot Nothing Then
                Detach()
            End If
            chartStorage = New List(Of ChartControl)()
            Me.designer = designer
            Me.chartItemName = chartItemName
            AddHandler designer.DashboardItemControlCreated, AddressOf DashboardItemControlCreated
            AddHandler designer.DashboardItemControlUpdated, AddressOf DashboardItemControlUpdated
            AddHandler designer.DashboardItemBeforeControlDisposed, AddressOf DashboardItemBeforeControlDisposed
        End Sub

        Private Sub DashboardItemBeforeControlDisposed(ByVal sender As Object, ByVal e As DashboardItemControlEventArgs)
            If e.DashboardItemName<>chartItemName OrElse e.ChartControl Is Nothing Then
                Return
            End If

            RemoveHandler e.ChartControl.LegendItemChecked, AddressOf ChartControl_LegendItemChecked
            chartStorage.Remove(e.ChartControl)
        End Sub

        Private Sub DashboardItemControlUpdated(ByVal sender As Object, ByVal e As DashboardItemControlEventArgs)
            If e.DashboardItemName <> chartItemName OrElse e.ChartControl Is Nothing Then
                Return
            End If

            ApplyCheckedState(e.ChartControl)
        End Sub

        Private Sub ApplyCheckedState(ByVal chart As ChartControl)
            For Each series As Series In chart.Series
                series.Visible = Not uncheckedSeries.Contains(series.Name)
                series.CheckedInLegend = Not uncheckedSeries.Contains(series.LegendTextPattern)
            Next series

        End Sub

        Private Sub DashboardItemControlCreated(ByVal sender As Object, ByVal e As DashboardItemControlEventArgs)
            If e.DashboardItemName <> chartItemName OrElse e.ChartControl Is Nothing Then
                Return
            End If
            chartStorage.Add(e.ChartControl)
            e.ChartControl.Legend.MarkerMode = DevExpress.XtraCharts.LegendMarkerMode.CheckBoxAndMarker
            AddHandler e.ChartControl.LegendItemChecked, AddressOf ChartControl_LegendItemChecked
        End Sub

        Private Sub ChartControl_LegendItemChecked(ByVal sender As Object, ByVal e As DevExpress.XtraCharts.LegendItemCheckedEventArgs)
            Dim chart As ChartControl = DirectCast(sender, ChartControl)
            If TypeOf e.CheckedElement Is Series Then
                Dim series = CType(e.CheckedElement, Series)
                chart.Series(series.LegendTextPattern).Visible = e.NewCheckState
                If Not e.NewCheckState Then
                    If Not uncheckedSeries.Contains(series.LegendTextPattern) Then
                        uncheckedSeries.Add(series.LegendTextPattern)
                    End If
                ElseIf uncheckedSeries.Contains(series.LegendTextPattern) Then
                    uncheckedSeries.Remove(series.LegendTextPattern)
                End If
            End If

            ' Update minimized chart
            If chartStorage.Count > 1 Then
            For Each chartControl As ChartControl In chartStorage
                If chart Is chartControl Then
                    Continue For
                End If
                    ApplyCheckedState(chartControl)
            Next chartControl
            End If
        End Sub


        Public Sub Detach()
            Me.chartItemName = String.Empty
            If Me.designer IsNot Nothing Then
                RemoveHandler designer.DashboardItemControlCreated, AddressOf DashboardItemControlCreated
                RemoveHandler designer.DashboardItemControlUpdated, AddressOf DashboardItemControlUpdated
                RemoveHandler designer.DashboardItemBeforeControlDisposed, AddressOf DashboardItemBeforeControlDisposed
                Me.designer = Nothing
            End If
            If Me.viewer IsNot Nothing Then
                RemoveHandler viewer.DashboardItemControlCreated, AddressOf DashboardItemControlCreated
                RemoveHandler viewer.DashboardItemControlUpdated, AddressOf DashboardItemControlUpdated
                RemoveHandler viewer.DashboardItemBeforeControlDisposed, AddressOf DashboardItemBeforeControlDisposed
                Me.viewer = Nothing
            End If
                chartStorage = Nothing


        End Sub
    End Class
End Namespace

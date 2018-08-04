using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Veritaware.Toolkits.LightVM.WpfMock.Behaviors
{
    internal class DataGridBehavior : Behavior<DataGrid>
    {
        protected override void OnAttached()
        {
            var dataGrid = this.AssociatedObject;

            base.OnAttached();

            dataGrid.AutoGeneratingColumn += DataGrid_AutoGeneratingColumn;
        }

        private static void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (e.PropertyName)
            {
                case "HasErrors":
                case "FirstName":
                case "LastName":
                case "Details":
                    e.Column.Visibility = Visibility.Collapsed;
                    return;
                case "Customer":
                    {
                        var tempCol = new DataGridTemplateColumn
                        {
                            Header = e.Column.Header,
                            SortMemberPath = e.Column.SortMemberPath,
                            CellTemplate = Application.Current.FindResource("CustomerDataCell") as DataTemplate,
                            CellEditingTemplate = Application.Current.FindResource("CustomerDataEditCell") as DataTemplate
                        };

                        e.Column = tempCol;
                        return;
                    }
                case "Product":
                    {
                        var tempCol = new DataGridTemplateColumn
                        {
                            Header = e.Column.Header,
                            SortMemberPath = e.Column.SortMemberPath,
                            CellTemplate = Application.Current.FindResource("ProductDataCell") as DataTemplate,
                            CellEditingTemplate = Application.Current.FindResource("ProductDataEditCell") as DataTemplate,
                        };

                        e.Column = tempCol;
                        return;
                    }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Dialog.xaml
    /// </summary>
    public partial class EditDlg : Window
    {
        private List<ctemp> rows = new List<ctemp>();

        public int tip { get; set; }        // add / update dialog

        public EditDlg()
        {
            InitializeComponent();
            this.grid1.ItemsSource = rows;
            this.grid1.Columns[0].IsReadOnly = true;
            this.grid1.Columns[1].IsReadOnly = false;
        }

        public void set_data(parameter d)
        {
            rows.Clear();
            rows.Add(new ctemp() { Key = "ParameterName", Value = d.ParameterName });
            rows.Add(new ctemp() { Key = "RequiredValue", Value = d.RequiredValue.ToString() });
            rows.Add(new ctemp() { Key = "MeasuredValue", Value = d.MeasuredValue.ToString() });
        }

        public parameter get_data()
        {
            var items = this.grid1.Items;
            return new parameter()
            {
                ParameterName =((ctemp)items[0]).Value,
                RequiredValue = (((ctemp)items[1]).Value).Str2Dec(),
                MeasuredValue = (((ctemp)items[2]).Value).Str2Dec()
            };
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

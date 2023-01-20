using System;
using System.IO;
using System.Linq;
//using System.Runtime.Remoting.Contexts;
using System.Windows;
using System.Xml.Serialization;
using System.Windows.Data;
//using System.Data.Entity;
//using System.Windows.Media.TextFormatting;
using Microsoft.Win32;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public string fn1 = @"..\..\test1.xml";
        public string fn2 = @"..\..\test1.xml";
        public static string scon = "Data Source=V500GB\\SQLEXPRESS;Initial Catalog=NPOSPARCK;Integrated Security=True";

        public MainWindow()
        {
            //initdb();
            //var dd = read_xml(fn1);
            //write_xml(fn2, dd);

            InitializeComponent();

            //this.employyesTab.
        }


        // insert data for testing
        private void initdb()
        {
            using (var db = new NPODB(scon))
            {
                var sql = @"insert into tests (testdate, blockname, note) values (CONVERT(smalldatetime,'2022-03-21', 120), 'block2', null);
 insert into tests (testdate, blockname, note) values (CONVERT(smalldatetime,'2021-07-01', 120), 'block3', 'xxxxxx');
insert into tests (testdate, blockname, note) values (CONVERT(smalldatetime,'2019-11-12', 120), 'block4', 'yyyyyyy');
";
                db.DDL(sql);

                sql = @"insert into parameters (testid, parametername, requiredvalue, measuredvalue) values (3, 'par1', 1, 1.01);
 insert into parameters (testid, parametername, requiredvalue, measuredvalue) values (2, 'par1', 2, 1.98);
insert into parameters (testid, parametername, requiredvalue, measuredvalue) values (4, 'par1', 3, 2.6);
";
                db.DDL(sql);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new NPODB(scon))
            {
                var tab = db.Select("select testid, testdate, blockname, ISNULL(CAST(note as nvarchar(200)),'') from tests", null);
                var t2 = tab.Select(o => new test() { TestId=(int)o[0], TestDate = (DateTime)o[1], BlockName = (string)o[2], Note = (string)o[3] }).ToList();
                this.testTab.ItemsSource = t2;
                this.RefreshParamTab(t2[0].TestId);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EditDlg dlg = new EditDlg();
            dlg.set_data(new parameter());
            dlg.tip = 0;

            if (dlg.ShowDialog() == true)
            {
                using (var db = new NPODB(scon))
                {
                    var trow = (test)this.testTab.SelectedItem;
                    var d = dlg.get_data();
                    var sql = @"insert into parameters (testid, parametername, requiredvalue, measuredvalue) values (@tid, @pname, @vr, @vm)";
                    db.DDL(sql, trow.TestId, d.ParameterName, d.RequiredValue, d.MeasuredValue, "tid", "pname", "vr", "vm");
                    this.RefreshParamTab(trow.TestId);    
                }
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var trow = (test)this.testTab.SelectedItem;
            var row = (parameter)this.propTab.SelectedItem;

            using (var db = new NPODB(scon))
            {
                var sql = @"delete from parameters where parameterid=@pid";
                db.DDL(sql, row.ParameterId, "pid");
                this.RefreshParamTab(trow.TestId);
            }
        }

        private void btnUpd_Click(object sender, RoutedEventArgs e)
        {
            var row = (parameter)this.propTab.SelectedItem;

            EditDlg dlg = new EditDlg();
            dlg.set_data(row);

            if (dlg.ShowDialog() == true)
            {
                using (var db = new NPODB(scon))
                {
                    var trow = (test)this.testTab.SelectedItem;
                    var d = dlg.get_data();
                    d.TestId = row.TestId;
                    d.ParameterId = row.ParameterId;
                    var sql = @"update parameters set testid=@tid, parametername=@pname, requiredvalue=@vr, measuredvalue=@vm where parameterid=@pid";
                    db.DDL(sql, trow.TestId, d.ParameterName, d.RequiredValue, d.MeasuredValue, d.ParameterId, "tid", "pname", "vr", "vm", "pid");
                    this.RefreshParamTab(trow.TestId);
                }
            }

        }

        private void testTab_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var row = (test)this.testTab.SelectedItem;
            this.RefreshParamTab(row.TestId);
        }

        private void RefreshParamTab(int testid)
        {
            using (var db = new NPODB(scon))
            {
                SqlParameter q = new SqlParameter("tid", testid);
                var tab = db.Select("select * from parameters where testid=@tid", new object[] { q });
                var t2 = tab.Select(o => new parameter()
                {
                    ParameterId = (int)o[0],
                    TestId = (int)o[1],
                    ParameterName = (string)o[2],
                    RequiredValue = (decimal)o[3],
                    MeasuredValue = (decimal)o[4]
                }).ToList();
                this.propTab.ItemsSource = t2;
            }
        }

    }
}

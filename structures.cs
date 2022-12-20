//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity;
//using System.Runtime.Remoting.Contexts;
//using System.Xml.Serialization;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Linq;

namespace WpfApp1
{
    public class ctemp
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class parameter
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParameterId { get; set; }
        public int TestId { get; set; }
        public string ParameterName { get; set; }
        public decimal RequiredValue { get; set; }
        public decimal MeasuredValue { get; set; }
    }

    public class test
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestId { get; set; }
        public DateTime TestDate { get; set; }
        public string BlockName { get; set; }
        public string Note { get; set; }
    }


    public class NPODB: IDisposable
    {
        public SqlConnection con { get; set; }

        public NPODB(string scon)
        {
            con = new SqlConnection(scon);
            con.Open();
        }

        public void Close()
        {
            if (con != null) con.Close();
            if (con.State == System.Data.ConnectionState.Closed) con = null;
        }

        public void Dispose()
        {
            if (con != null)
            {
                con.Close();
                con = null;
            }
        }

        public List<object[]> Select(string sql, params object[] p)
        {
            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = sql;
            if (p != null)
                cmd.Parameters.AddRange(p);

            //SqlParameter nameParam = new SqlParameter("@name", name);
            //command.Parameters.Add(nameParam);

            List<object[]> tab = new List<object[]>();
            using (var r = cmd.ExecuteReader())
            {
                while (r.Read())
                {
                    var n = r.FieldCount;
                    object[] ob = new object[n];
                    r.GetValues(ob);
                    tab.Add(ob);
                }
            }

            return tab;
        }

        public int DDL(string sql, params object[] pp)
        {
            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = sql;

            if (pp != null)
            {
                var pars = cmd.Parameters;
                var m = pp.Length / 2;

                for (int i = 0; i < m; i++)
                {
                    SqlParameter q = new SqlParameter((string)pp[i + m], pp[i]);
                    cmd.Parameters.Add(q);
                }
            }
            return cmd.ExecuteNonQuery();
        }

    }

    public static class NPOExtensions
    {
        //public static void Clear<T>(this DbSet<T> dbSet) where T : class
        //{
        //    dbSet.RemoveRange(dbSet);
        //}

        public static decimal Str2Dec(this string s)
        {
            NumberStyles dstyle = NumberStyles.Float;
            //NumberFormatInfo nfi = NumberFormatInfo.InvariantInfo;

            try
            {
                var d = decimal.Parse(s, dstyle, NumberFormatInfo.InvariantInfo);
                return d;
            }
            catch
            {
                var d = decimal.Parse(s, dstyle, NumberFormatInfo.CurrentInfo);
                return d;
            }
        }

    }

}



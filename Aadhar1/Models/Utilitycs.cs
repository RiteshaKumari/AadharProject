using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Aadhar1.Models
{
    public class Utilitycs
    {

        string CS = string.Empty;
        private static object lockObject = new object();

        public Utilitycs()
        {
            if (string.IsNullOrEmpty(CS))
            {
                CS = ConfigurationManager.ConnectionStrings["mycon"].ToString();
            }
        }

        #region Execute Non Query
         public int func_ExecuteNonQuery(string procedure, params SqlParameter[] _SqlParam)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("dbo" + "." + procedure.ToString(), con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (SqlParameter para in _SqlParam)
                    {
                        cmd.Parameters.Add(para);
                    }
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region Execute Scalar
        public object func_ExecuteScalar(string procedure, params SqlParameter[] _SqlParam)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("dbo" + "." + procedure.ToString(), con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (SqlParameter para in _SqlParam)
                    {
                        cmd.Parameters.Add(para);
                    }
                    con.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
        #endregion


        #region DataTable
        public DataTable fn_DataTable(string procedure)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlDataAdapter tda = new SqlDataAdapter("dbo" + "." + procedure, con))
                {
                    tda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataTable DT = new DataTable();
                    lock (lockObject)
                    {
                        tda.Fill(DT);
                    }

                    return DT;
                }
            }
        }
        #endregion

    }
}
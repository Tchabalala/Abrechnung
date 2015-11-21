using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SqlLib
{
    public static class MySqlConnection
    {
        #region Klassenvariable(n)
        private static SqlConnection _sqlconn = null;
        private static string _server = null;
        private static string _database = null;
        #endregion
        #region Eigenschaft(en)
        public static string Server
        {
            get
            {
                return _server;
            }
            set
            {
                _server = value;
            }
        }
        public static string Database
        {
            get
            {
                return _database;
            }
            set
            {
                _database = value;
            }
        }
        #endregion

        #region Connect
        public static bool Connect()
        {
            bool lRet = false;

            string connectionstring = "server=" + Server + ";database=" + Database + ";user id=sa;password=sqladmin";
            try
            {
                _sqlconn = new SqlConnection(connectionstring);
                _sqlconn.Open();

                lRet = true;
            }
            catch (System.Exception)
            {
                MessageBox.Show("SQL-Verbindung konnte nicht hergestellt werden!");
            }

            return lRet;
        }
        #endregion
        #region Disconnect
        public static void Disconnect()
        {
            try
            {
                _sqlconn.Close();
            }
            catch (System.Exception)
            {
            }
        }
        #endregion
        #region ExecuteSelectInDataSet
        public static DataSet ExecuteSelectInDataSet(string select)
        {
            DataSet dsRet = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(select, _sqlconn);

                adapter.Fill(dsRet);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message, "Fehler beim Select");
            }
            return dsRet;
        }
        #endregion
        #region ExecuteStoredProcedureInDataSet
        public static DataSet ExecuteStoredProcedureInDataSet(string procname, List<SqlParameter> sqlparams = null)
        {
            DataSet dsRet = new DataSet();

            try
            {
                SqlCommand command = new SqlCommand(procname, _sqlconn);
                if (sqlparams != null && sqlparams.Count > 0)
                {
                    foreach (SqlParameter sqlparam in sqlparams)
                    {
                        command.Parameters.Add(sqlparam);
                    }
                }
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(dsRet);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message, "Fehler beim Aufruf der gespeicherten Prozedur");
            }

            return dsRet;
        }
        #endregion
    }
}

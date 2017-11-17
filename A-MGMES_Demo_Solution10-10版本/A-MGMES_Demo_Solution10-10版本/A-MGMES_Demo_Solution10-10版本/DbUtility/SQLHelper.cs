namespace DBUtility
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.Data.SqlClient;
    using System.Configuration;

    public abstract class SqlHelper
    {
        public static readonly string SqlConnString = ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString;
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());
        // public static readonly string SystemConnString = ConfigurationManager.ConnectionStrings["SystemConnString"].ConnectionString;

        protected SqlHelper()
        {
        }

        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    if (((parameter.Direction == ParameterDirection.InputOutput) || (parameter.Direction == ParameterDirection.Input)) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }
            return command;
        }

        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        public static bool ExecuteHashSql(string connectionString, Hashtable SQLStringList, CommandType cmType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                try
                {
                    foreach (string str in SQLStringList.Keys)
                    {
                        string cmdText = str;
                        SqlParameter[] cmdParms = (SqlParameter[])SQLStringList[str];
                        PrepareCommand(cmd, connection, null, cmType, cmdText, cmdParms);
                        int num = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (connection != null)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
                return true;
            }
        }

        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            int num2;
            try
            {
                SqlCommand cmd = new SqlCommand();
                connection.Open();
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                int num = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                connection.Close();
                connection.Dispose();
                num2 = num;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return num2;
        }

        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int num = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return num;
        }

        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlConnection conn = null;
            int num2;
            try
            {
                conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                conn.Open();
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int num = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                conn.Close();
                conn.Dispose();
                num2 = num;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return num2;
        }

        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlDataReader reader2;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                reader2 = reader;
            }
            catch
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
                throw;
            }
            return reader2;
        }

        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            object obj3;
            try
            {
                SqlCommand cmd = new SqlCommand();
                connection.Open();
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object obj2 = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                connection.Close();
                connection.Dispose();
                obj3 = obj2;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return obj3;
        }

        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlConnection conn = null;
            object obj3;
            try
            {
                SqlCommand cmd = new SqlCommand();
                conn = new SqlConnection(connectionString);
                conn.Open();
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                object obj2 = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                conn.Close();
                conn.Dispose();
                obj3 = obj2;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return obj3;
        }

        public static bool ExecuteSqlTran(string connectionString, List<string> SQLStringList)
        {
            bool flag;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                command.Transaction = transaction;
                try
                {
                    for (int i = 0; i < SQLStringList.Count; i++)
                    {
                        string str = SQLStringList[i];
                        if (str.Trim().Length > 1)
                        {
                            command.CommandText = str;
                            command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                    flag = true;
                }
                catch
                {
                    transaction.Rollback();
                    flag = false;
                }
                finally
                {
                    connection.Dispose();
                    command.Dispose();
                }
            }
            return flag;
        }

        public static bool ExecuteSqlTran(string connectionString, Hashtable SQLStringList, CommandType cmType)
        {
            SqlConnection connection = null;
            bool flag;
            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        SqlCommand cmd = new SqlCommand();
                        try
                        {
                            foreach (string str in SQLStringList.Keys)
                            {
                                string cmdText = str;
                                SqlParameter[] cmdParms = (SqlParameter[])SQLStringList[str];
                                PrepareCommand(cmd, transaction.Connection, transaction, cmType, cmdText, cmdParms);
                                int num = cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }
                            transaction.Commit();
                        }
                        catch (Exception exception)
                        {
                            string message = exception.Message;
                            transaction.Rollback();
                            return false;
                        }
                    }
                    flag = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return flag;
        }

        public static bool ExecuteSqlTran(string connectionString, string sql, List<SqlParameter> pms)
        {
            bool flag;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                command.Transaction = transaction;
                try
                {
                    if (pms!=null && pms.Count>0)
                    {
                        foreach (SqlParameter parameter in pms)
                        {
                            if (parameter != null)
                            {
                                command.Parameters.Add(parameter);
                            }
                        }
                    }
                    
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                    flag = true;
                }
                catch
                {
                    transaction.Rollback();
                    flag = false;
                }
                finally
                {
                    connection.Dispose();
                    command.Dispose();
                }
            }
            return flag;
        }

        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] parameterArray = (SqlParameter[])parmCache[cacheKey];
            if (parameterArray == null)
            {
                return null;
            }
            SqlParameter[] parameterArray2 = new SqlParameter[parameterArray.Length];
            int index = 0;
            int length = parameterArray.Length;
            while (index < length)
            {
                parameterArray2[index] = (SqlParameter)((ICloneable)parameterArray[index]).Clone();
                index++;
            }
            return parameterArray2;
        }

        public static DataTable GetDataDataTable(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand selectCommand = new SqlCommand(cmdText, connection);
                selectCommand.CommandType = cmdType;
                if (commandParameters != null)
                {
                    foreach (SqlParameter parameter in commandParameters)
                    {
                        if (parameter != null)
                        {
                            selectCommand.Parameters.Add(parameter);
                        }
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "aa");
                DataTable table = dataSet.Tables["aa"];
                selectCommand.Parameters.Clear();
                connection.Close();
                connection.Dispose();
                return table;
            }
        }

        public static DataSet GetDataSet(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlConnection conn = null;
            DataSet set2;
            try
            {
                conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                conn.Open();
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                cmd.Parameters.Clear();
                conn.Close();
                conn.Dispose();
                set2 = dataSet;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return set2;
        }

        public static DataSet GetDataSetTableMapping(string connectionString, CommandType cmdType, string cmdText, string[] tableName, params SqlParameter[] commandParameters)
        {
            SqlConnection conn = null;
            DataSet set2;
            try
            {
                conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                conn.Open();
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                for (int i = 0; i < tableName.Length; i++)
                {
                    if (i == 0)
                    {
                        adapter.TableMappings.Add("Table", tableName[i]);
                    }
                    else
                    {
                        adapter.TableMappings.Add("Table" + i, tableName[i]);
                    }
                }
                adapter.Fill(dataSet);
                cmd.Parameters.Clear();
                conn.Close();
                conn.Dispose();
                set2 = dataSet;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return set2;
        }

        public static DataSet GetExcelData(string sql, string filenameurl)
        {
            DataSet set2;
            using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OleDb.12.0;data source=" + filenameurl + ";Extended Properties='Excel 12.0; HDR=Yes; IMEX=1';"))
            {
                using (OleDbCommand command = new OleDbCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    connection.Close();
                    set2 = dataSet;
                }
            }
            return set2;
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        public static DataSet RunProcedure(string con, string storedProcName, IDataParameter[] parameters, string tableName)
        {
            SqlConnection connection = null;
            DataSet set2;
            try
            {
                connection = new SqlConnection(con);
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                adapter.SelectCommand.CommandTimeout = 0xe10;
                if (!string.IsNullOrEmpty(tableName))
                {
                    adapter.Fill(dataSet, tableName);
                }
                else
                {
                    adapter.Fill(dataSet);
                }
                connection.Close();
                connection.Dispose();
                set2 = dataSet;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return set2;
        }

        public static DataSet RunProcedureTables(string con, string storedProcName, IDataParameter[] parameters, string[] tableNames)
        {
            SqlConnection connection = null;
            DataSet set2;
            try
            {
                connection = new SqlConnection(con);
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                adapter.SelectCommand.CommandTimeout = 0xe10;
                for (int i = 0; i < tableNames.Length; i++)
                {
                    if (i == 0)
                    {
                        adapter.TableMappings.Add("Table", tableNames[i]);
                    }
                    else
                    {
                        adapter.TableMappings.Add("Table" + i, tableNames[i]);
                    }
                }
                adapter.Fill(dataSet);
                connection.Close();
                connection.Dispose();
                set2 = dataSet;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return set2;
        }
    }
}

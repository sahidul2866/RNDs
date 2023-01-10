using System; 
using System.Data;
using Oracle.ManagedDataAccess.Client; 
using NLog; 
public class DataAccess
{
    static Logger sqllogger = LogManager.GetLogger("DataAccess");
    public DataAccess()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable GetDataFromDB(string vSQL, string _sConn)
    {
        DataTable dt = new DataTable();
        try
        {

            using (OracleConnection oConn = new OracleConnection(_sConn))
            {
                using (OracleCommand oCmd = new OracleCommand())
                {
                    oConn.Open();
                    oCmd.CommandType = CommandType.Text;
                    oCmd.CommandText = vSQL;
                    oCmd.Connection = oConn;
                    sqllogger.Info(" SQL cmd:  " + oCmd.CommandText);

                    using (OracleDataAdapter da = new OracleDataAdapter(oCmd))
                    {
                        da.Fill(dt);
                    }

                    sqllogger.Info(dt.Rows.Count + " - Rows Returned");
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        sqllogger.Info("Returned DataTable is NULL or Empty ");
                    }
                }
            }
        }
        catch (OracleException ex)
        {
            sqllogger.Error(" Oracle Error: " + ex.Message + ex.StackTrace, ex);
            throw new Exception(" Oracle Error: " + ex.Message + ex.StackTrace, ex);
        }
        catch (Exception e)
        {
            sqllogger.Error(" Exception: " + e.ToString());
            throw new Exception(" Exception: " + e.ToString());
        }

        return dt;
    }
    public static bool RunSQL_cmd(string vSQL, string _sConn)
    {
        sqllogger.Info(" SQL cmd:  " + vSQL);

        bool bReturn = false;
        try
        {
            using (OracleConnection oConn = new OracleConnection(_sConn))
            {
                using (OracleCommand oCmd = new OracleCommand())
                {
                    oConn.Open();
                    oCmd.CommandType = CommandType.Text;
                    oCmd.CommandText = vSQL;
                    oCmd.InitialLONGFetchSize = 2000;
                    oCmd.Connection = oConn;
                    oCmd.ExecuteNonQuery();                      
                }
            }
            bReturn = true; 
        }
        catch (OracleException ex)
        {
            sqllogger.Error(" Oracle Error: " + ex.Message + ex.StackTrace, ex);
            throw new Exception(" Oracle Error: " + ex.Message + ex.StackTrace, ex);
        }
        catch (Exception e)
        {
            sqllogger.Error(" Exception: " + e.ToString());
            throw new Exception(" Exception: " + e.ToString());
        }

        return bReturn;
    }
    public static object RunSQL_ExecuteScalar(string vSQL, string _sConn)
    {
        sqllogger.Info(" ExecuteScalar SQL cmd:  " + vSQL);

        object oReturn ;
        try
        {
            using (OracleConnection oConn = new OracleConnection(_sConn))
            {
                using (OracleCommand oCmd = new OracleCommand())
                {
                    oConn.Open();
                    oCmd.CommandType = CommandType.Text;
                    oCmd.CommandText = vSQL;
                    oCmd.InitialLONGFetchSize = 2000;
                    oCmd.Connection = oConn;
                    oReturn = oCmd.ExecuteScalar();
                }
            }
            if (oReturn == null)
            {
                sqllogger.Info("DB oReturn: null" );
                oReturn = "";
            }
            else
            {
                sqllogger.Info("DB oReturn: " + oReturn.ToString());
            }
        }
        catch (OracleException ex)
        {
            sqllogger.Error(" Oracle Error: " + ex.Message + ex.StackTrace, ex);
            throw new Exception(" Oracle Error: " + ex.Message + ex.StackTrace, ex);
        }
        catch (Exception e)
        {
            sqllogger.Error(" Exception: " + e.ToString());
            throw new Exception(" Exception: " + e.ToString());
        }

        return oReturn;
    }
}
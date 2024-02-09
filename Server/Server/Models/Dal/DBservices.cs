using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Server.Models;


/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{
    const string InsertedIdParameter = "@insertedId";
    public SqlDataAdapter da;
    public DataTable dt;
    public enum InsertResult { Succses, Duplicate, Error }

    public DBservices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("DB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    //--------------------------------------------------------------------------------------------------
    // inserts a student to the settlements table 
    //--------------------------------------------------------------------------------------------------
    public InsertResult Insert_Settlement(Settlement settlement, out int id)
    {
        SqlConnection con;
        SqlCommand cmd;

        id = -1;

        try
        {
            con = connect("DB");
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureInsert("spInsertSettlement", con, settlement);
        try
        {
            int numEffected = cmd.ExecuteNonQuery();
            id = Convert.ToInt32(cmd.Parameters[InsertedIdParameter].Value);
            return InsertResult.Succses;

        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
            {
                return InsertResult.Duplicate;
            }
            return InsertResult.Error;
        }

        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureInsert(String spName, SqlConnection con, Settlement settlement)
    {
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con;

        cmd.CommandText = spName;

        cmd.CommandTimeout = 10;

        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@name", settlement.Name);
        cmd.Parameters.Add(new SqlParameter(InsertedIdParameter, SqlDbType.Int) { Direction = ParameterDirection.Output });
        return cmd;
    }

    //--------------------------------------------------------------------------------------------------
    // update a student to the settlements table 
    //--------------------------------------------------------------------------------------------------
    public InsertResult Update_Settlement(Settlement settlement)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("DB");
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureUpdate("spUpdateSettlement", con, settlement);

        try
        {
            int numEffected = cmd.ExecuteNonQuery();
            return InsertResult.Succses;

        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
            {
                return InsertResult.Duplicate;
            }
            return InsertResult.Error;
        }

        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureUpdate(String spName, SqlConnection con, Settlement settlement)
    {
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con;

        cmd.CommandText = spName;

        cmd.CommandTimeout = 10;

        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@id", settlement.Id);
        cmd.Parameters.AddWithValue("@name", settlement.Name);

        return cmd;
    }

    //--------------------------------------------------------------------------------------------------
    // Delete a student to the settlements table 
    //--------------------------------------------------------------------------------------------------
    public int Delete_Settlement(int id)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("DB");
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureDelete("spDeleteSettlement", con, id);

        try
        {
            int numEffected = cmd.ExecuteNonQuery();
            return numEffected;
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureDelete(String spName, SqlConnection con, int id)
    {
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con;

        cmd.CommandText = spName;

        cmd.CommandTimeout = 10;

        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@id", id);

        return cmd;
    }

    //--------------------------------------------------------------------------------------------------
    // Read a settlement to the settlements table 
    //--------------------------------------------------------------------------------------------------
    public List<Settlement> Read_Settlements()
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("DB");
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureRead("spReadSettlements", con);

        List<Settlement> settlementsList = new List<Settlement>();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dataReader.Read())
            {
                Settlement settlement = new Settlement();
                settlement.Id = Convert.ToInt32(dataReader["Id"]);
                settlement.Name = dataReader["Name"].ToString();

                settlementsList.Add(settlement);
            }
            return settlementsList;
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureRead(String spName, SqlConnection con)
    {
        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con;

        cmd.CommandText = spName;

        cmd.CommandTimeout = 10;

        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        return cmd;
    }
}



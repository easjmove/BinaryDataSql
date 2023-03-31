// See https://aka.ms/new-console-template for more information
using System.Data.SqlClient;

Console.WriteLine("Binary data");

byte[] data = File.ReadAllBytes("c:/temp/test.exe");

SqlConnection sqlConn = new SqlConnection(
    "server=localhost;user id=fileuser;password=fileuser;database=NormalTest");

sqlConn.Open();

SqlCommand inserterCmd = new SqlCommand(
    "INSERT INTO [files] ([FileName], [data]) " +
    "VALUES ('test.exe', @data)", sqlConn);

inserterCmd.Parameters.AddWithValue("@data", data);

inserterCmd.ExecuteNonQuery();

SqlCommand readerCmd = new SqlCommand(
    "SELECT [FileName], [Data] FROM [files]", sqlConn);

using (SqlDataReader reader = readerCmd.ExecuteReader())
{
    if (reader.Read())
    {
        File.WriteAllBytes("c:/temp/output/" +
            reader["FileName"], reader["data"] as byte[]);
    }
}


sqlConn.Close();
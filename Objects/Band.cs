using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTracker.Objects
{
  public class Band
  {
    private int _id;
    private string _bandName;
    private string _genre;

    public Band(string BandName, string Genre, int Id = 0)
    {
      _id = Id;
      _bandName = BandName;
      _genre = Genre;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetBandName()
    {
      return _bandName;
    }
    public string GetGenre()
    {
      return _genre;
    }

    public override bool Equals(Object otherBand)
    {
      if (!(otherBand is Band))
      {
        return false;
      }
      else
      {
        Band newBand = (Band) otherBand;
        bool bandIdEquality = (this.GetId() == newBand.GetId());
        bool bandNameEquality = (this.GetBandName() == newBand.GetBandName());
        bool bandGenreEquality = (this.GetGenre() == newBand.GetGenre());
        return (bandGenreEquality && bandNameEquality && bandIdEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetBandName().GetHashCode();
    }
    //
    // public void Save()
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("INSERT INTO template (Banddescription) OUTPUT INSERTED.id VALUES (@BandDescription);", conn);
    //
    //   SqlParameter BanddescriptionParameter = new SqlParameter("@BandDescription", this.Banddescription);
    //   cmd.Parameters.Add(BanddescriptionParameter);
    //
    //   SqlDataReader rdr = cmd.ExecuteReader();
    //
    //   while(rdr.Read())
    //   {
    //     this.Id = rdr.GetInt32(0);
    //   }
    //   if (rdr != null)
    //   {
    //     rdr.Close();
    //   }
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    // }

    public static List<Band> GetAll()
    {
      List<Band> allBands = new List<Band>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int BandId = rdr.GetInt32(0);
        string BandName = rdr.GetString(1);
        string BandGenre = rdr.GetString(2);
        Band newBand = new Band(BandName, BandGenre, BandId);
        allBands.Add(newBand);
      }

      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();
      return allBands;
    }
    //
    // public static TEMPLATE Find(int id)
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("SELECT * FROM template WHERE id = @TEMPLATEId;", conn);
    //   SqlParameter TEMPLATEIdParameter = new SqlParameter("@TEMPLATEId", id.ToString());
    //   cmd.Parameters.Add(TEMPLATEIdParameter);
    //   SqlDataReader rdr = cmd.ExecuteReader();
    //
    //   int foundTEMPLATEId = 0;
    //   string foundTEMPLATEDescription = null;
    //   while(rdr.Read())
    //   {
    //     foundTEMPLATEId = rdr.GetInt32(0);
    //     foundTEMPLATEDescription = rdr.GetString(1);
    //   }
    //   TEMPLATE foundTEMPLATE = new TEMPLATE(foundTEMPLATEDescription, foundTEMPLATEId);
    //
    //   if (rdr != null)
    //   {
    //     rdr.Close();
    //   }
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    //
    //   return foundTEMPLATE;
    // }
    // public void Edit(string description)
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //   Console.WriteLine(this.TEMPLATEdescription);
    //
    //   SqlCommand cmd = new SqlCommand("UPDATE template SET TEMPLATEdescription = @TEMPLATEdescription WHERE id = @TEMPLATEId;", conn);
    //
    //   SqlParameter TEMPLATEParameter = new SqlParameter("@TEMPLATEId", this.Id);
    //
    //    SqlParameter TEMPLATEdescriptionParameter = new SqlParameter("TEMPLATEdescription", description);
    //
    //    cmd.Parameters.Add(TEMPLATEParameter);
    //    cmd.Parameters.Add(TEMPLATEdescriptionParameter);
    //
    //    SqlDataReader rdr = cmd.ExecuteReader();
    //
    //    while(rdr.Read())
    //    {
    //      this.Id = rdr.GetInt32(0);
    //      this.TEMPLATEdescription = rdr.GetString(1);
    //    }
    //    if (rdr != null)
    //    {
    //      rdr.Close();
    //    }
    //    if (conn != null)
    //    {
    //      conn.Close();
    //    }
    //  }
    //
    // public static List<TEMPLATE> Sort()
    // {
    //   List<Band> allTEMPLATE = new List<Band>{};
    //
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("SELECT * FROM template ORDER BY TEMPLATEdate;", conn);
    //   SqlDataReader rdr = cmd.ExecuteReader();
    //
    //   while(rdr.Read())
    //   {
    //     int TEMPLATEId = rdr.GetInt32(0);
    //     string TEMPLATEDescription = rdr.GetString(1);
    //     TEMPLATE newTEMPLATE = new TEMPLATE(TEMPLATEDescription, TEMPLATEId);
    //     allTEMPLATE.Add(newTEMPLATE);
    //   }
    //
    //   if (rdr != null)
    //   {
    //     rdr.Close();
    //   }
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    //
    //   return allTEMPLATE;
    // }
    // public void Delete()
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //   SqlCommand cmd = new SqlCommand("DELETE FROM template WHERE id = @TEMPLATEId; DELETE FROM join_table WHERE template_id = @TEMPLATEId", conn);
    //   SqlParameter TEMPLATEIdParameter = new SqlParameter("@TEMPLATEId", this.Id);
    //   cmd.Parameters.Add(TEMPLATEIdParameter);
    //   cmd.ExecuteNonQuery();
    //
    //   if(conn!=null)
    //   {
    //     conn.Close();
    //   }
    // }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM bands;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

  }
}

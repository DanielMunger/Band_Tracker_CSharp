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

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands (name , genre) OUTPUT INSERTED.id VALUES (@BandName, @BandGenre);", conn);
      cmd.Parameters.AddWithValue("BandName", this.GetBandName());
      cmd.Parameters.AddWithValue("@BandGenre", this.GetGenre());

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();
    }

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

    public static Band Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands WHERE id = @BandId;", conn);
      cmd.Parameters.AddWithValue("BandId", id);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundBandId = 0;
      string foundBandName = null;
      string foundBandGenre = null;
      while(rdr.Read())
      {
        foundBandId = rdr.GetInt32(0);
        foundBandName = rdr.GetString(1);
        foundBandGenre = rdr.GetString(2);
      }
      Band foundBand = new Band(foundBandName, foundBandGenre, foundBandId);
      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();

       return foundBand;
     }
     public void AddVenue(Venue newVenue)
     {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues (band_id, venue_id) VALUES (@BandId, @VenueId);", conn);
      cmd.Parameters.AddWithValue("@BandId", this.GetId());
      cmd.Parameters.AddWithValue("@VenueId", newVenue.GetId());
      cmd.ExecuteNonQuery();

      if(conn!=null) conn.Close();
     }
     public List<Venue> GetVenues()
     {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT venues.* FROM bands JOIN bands_venues ON (bands.id = bands_venues.band_id) JOIN venues ON (bands_venues.venue_id = venues.id) WHERE bands.id = @BandId;", conn);
      cmd.Parameters.AddWithValue("@BandId", this.GetId());
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Venue> bandsVenues = new List<Venue> {};
      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        int venueCapacity = rdr.GetInt32(2);
        string venueLocation = rdr.GetString(3);
        Venue newVenue = new Venue(venueName, venueCapacity, venueLocation, venueId);
        bandsVenues.Add(newVenue);
      }
      if(rdr!=null) rdr.Close();
      if(conn!=null) conn.Close();
      return bandsVenues;
     }

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

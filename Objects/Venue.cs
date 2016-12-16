using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTracker.Objects
{
  public class Venue
  {
    private int _id;
    private string _name;
    private int _capacity;
    private string _location;

    public Venue(string Name, int Capacity, string Location, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _capacity = Capacity;
      _location = Location;
    }
    private int GetId()
    {
      return _id;
    }
    private string GetName()
    {
      return _name;
    }
    private int GetCapacity()
    {
      return _capacity;
    }
    private string GetLocation()
    {
      return _location;
    }

    public override bool Equals(Object otherVenue)
    {
      if (!(otherVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;
        bool venueIdEquality = (this.GetId() == newVenue.GetId());
        bool venueNameEquality = (this.GetName() == newVenue.GetName());
        bool venueCapacityEquality = (this.GetCapacity() == newVenue.GetCapacity());
        bool venueLocationEquality = (this.GetLocation() == newVenue.GetLocation());

        return (venueIdEquality && venueLocationEquality && venueNameEquality && venueCapacityEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetLocation().GetHashCode();
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("INSERT INTO venues (name, capacity, venue_location) OUTPUT INSERTED.id VALUES (@VenueName, @VenueCapcity, @VenueLocation);", conn);
      cmd.Parameters.AddWithValue("@VenueName", this.GetName());
      cmd.Parameters.AddWithValue("@VenueCapcity", this.GetCapacity());
      cmd.Parameters.AddWithValue("@VenueLocation", this.GetLocation());
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();
    }

    public static List<Venue> GetAll()
    {
      List<Venue> allVenues = new List<Venue>{};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int capacity = rdr.GetInt32(2);
        string location = rdr.GetString(3);
        Venue newVenue = new Venue(name, capacity, location, id);
        allVenues.Add(newVenue);
      }
      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();
      return allVenues;
    }

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
    //   List<Task> allTEMPLATE = new List<Task>{};
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
      SqlCommand cmd = new SqlCommand("DELETE FROM venues;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

  }
}

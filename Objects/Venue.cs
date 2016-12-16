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
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public int GetCapacity()
    {
      return _capacity;
    }
    public string GetLocation()
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

    public static Venue Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @VenueId;", conn);
      cmd.Parameters.AddWithValue("@VenueId", id);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundVenueId = 0;
      string foundVenueName = null;
      int foundVenueCapacity = 0;
      string foundVenueLocation = null;
      while(rdr.Read())
      {
        foundVenueId = rdr.GetInt32(0);
        foundVenueName = rdr.GetString(1);
        foundVenueCapacity = rdr.GetInt32(2);
        foundVenueLocation = rdr.GetString(3);
      }
      Venue foundVenue = new Venue(foundVenueName, foundVenueCapacity, foundVenueLocation, foundVenueId);

      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();
      return foundVenue;
    }
    public void Update(string newName, int newCapacity, string newLocation)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE venues SET name = @NewName, capacity=@NewCapacity, venue_location=@NewLocation OUTPUT INSERTED.name, INSERTED.capacity, INSERTED.venue_location WHERE id = @VenueId;", conn);

      SqlParameter venueIdParameter = new SqlParameter("@VenueId", this.GetId());
      SqlParameter venueNameParameter = new SqlParameter("@NewName", newName);
      SqlParameter venueCapacityParameter = new SqlParameter("@NewCapacity", newCapacity);
      SqlParameter venueLocationParameter = new SqlParameter("@NewLocation", newLocation);
      cmd.Parameters.Add(venueIdParameter);
      cmd.Parameters.Add(venueNameParameter);
      cmd.Parameters.Add(venueCapacityParameter);
      cmd.Parameters.Add(venueLocationParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {

        _name = rdr.GetString(0);
        _capacity = rdr.GetInt32(1);
        _location = rdr.GetString(2);
      }
      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();
    }
    public void AddBand(Band newBand)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues (band_id, venue_id) VALUES (@BandId, @VenueId);", conn);
      cmd.Parameters.AddWithValue("@BandId", newBand.GetId());
      cmd.Parameters.AddWithValue("@VenueId", this.GetId());
      cmd.ExecuteNonQuery();

      if(conn!=null) conn.Close();
    }
    public List<Band> GetBands()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("SELECT bands.* FROM venues JOIN bands_venues ON (venues.id = bands_venues.venue_id) JOIN bands ON (bands_venues.band_id = bands.id) WHERE venues.id = @VenueId;", conn);
      cmd.Parameters.AddWithValue("@VenueId", this.GetId());
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Band> venueBands = new List<Band>{};
      while(rdr.Read())
      {
        int bandId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        string bandGenre = rdr.GetString(2);
        Band newBand = new Band(bandName, bandGenre, bandId);
        venueBands.Add(newBand);
      }
      if (rdr != null) rdr.Close();
      if (conn != null) conn.Close();
      return venueBands;
    }
    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM venues WHERE id = @VenueId;", conn);
      cmd.Parameters.AddWithValue("@VenueId", this.GetId());
      cmd.ExecuteNonQuery();
      if(conn!=null) conn.Close();
    }
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

using Xunit;
using System;
using System.Collections.Generic;
using BandTracker.Objects;
using System.Data;
using System.Data.SqlClient;

namespace  BandTracker.Tests
{
  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=DESKTOP-GC3DC7B\\SQLEXPRESS;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void GetAll_ReturnsEmptyList_True()
    {
      int result = Venue.GetAll().Count;
      int expected = 0;

      Assert.Equal(expected, result);
    }
    [Fact]
    public void Save_SavesVenueToDatabase_true()
    {
      //Arrange
      Venue newVenue = new Venue("Belly Up", 455, "213 E. Durant");
      newVenue.Save();
      //Act
      List<Venue> result = Venue.GetAll();
      List<Venue> expected = new List<Venue> {newVenue};
      //Assert
      Assert.Equal(expected, result);
    }
    [Fact]
    public void Find_FindsVenueGivenID_True()
    {
      Venue newVenue = new Venue("Belly Up", 455, "213 E. Durant");
      newVenue.Save();
      int id = newVenue.GetId();
      Venue foundVenue = Venue.Find(id);

      Assert.Equal(newVenue, foundVenue);
    }
    [Fact]
    public void Update_UpdatesVenueInformationinDB_True()
    {
      Venue newVenue = new Venue("Belly Up", 455, "213 E. Durant");
      newVenue.Save();
      string newName = "Crystal Palace";
      int newCapacity = 1200;
      string newLocation = "517 NE 71st";

      newVenue.Update(newName, newCapacity, newLocation);
      string expected = newLocation;
      string result = newVenue.GetLocation();

      Assert.Equal(expected , result);
    }
    [Fact]
    public void Delete_DeletesSingleInstanceFromDB_True()
    {
      Venue newVenue = new Venue("Belly Up", 455, "213 E. Durant");
      newVenue.Save();

      newVenue.Delete();
      List<Venue> expected = new List<Venue> {};
      List<Venue> result = Venue.GetAll();

      Assert.Equal(expected, result);
    }
    [Fact]
    public void AddBand_AddsBandToJoinTable_True()
    {
      Band newBand = new Band("Kings of Leon", "Indie Rock");
      newBand.Save();
      Venue newVenue = new Venue("Belly Up", 455, "213 E. Durant");
      newVenue.Save();

      newVenue.AddBand(newBand);
      List<Band> result = newVenue.GetBands();
      List<Band> expected = new List<Band>{newBand};

      Assert.Equal(expected, result);
    }
    public void Dispose()
    {
      Venue.DeleteAll();
      Band.DeleteAll();
    }

  }
}

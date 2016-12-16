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
      //Assert
      Assert.Equal(true/false, TEMPLATE);
    }

    public void Dispose()
    {
      TEMPLATE_OBJECT.DeleteAll(); //TEMPLATE_OBJECT references the object name
    }

  }
}

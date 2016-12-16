using Xunit;
using System;
using System.Collections.Generic;
using BandTracker.Objects;
using System.Data;
using System.Data.SqlClient;

namespace  BandTracker.Tests
{
  public class BandTest : IDisposable
  {
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=DESKTOP-GC3DC7B\\SQLEXPRESS;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void GetAll_ReturnsEmptyList_True()
    {
      int result = Band.GetAll().Count;
      int expected = 0;

      Assert.Equal(expected, result);
    }
    [Fact]
    public void Save_SavesBandtoDB_True()
    {
      Band newBand = new Band("Kings of Leon", "Indie Rock");
      newBand.Save();

      List<Band> result = Band.GetAll();
      List<Band> expected = new List<Band> {newBand};

      Assert.Equal(expected, result);
    }
    [Fact]
    public void Find_FindsBandinDB_True()
    {
      Band newBand = new Band("Kings of Leon", "Indie Rock");
      newBand.Save();

      Band foundBand = Band.Find(newBand.GetId());

      Assert.Equal(foundBand, newBand);
    }
    [Fact]
    public void GetVenues_ReturnsVenuesBandPlaysAt_True()
    {
      Band newBand = new Band("Kings of Leon", "Indie Rock");
      newBand.Save();
      Venue newVenue = new Venue("Belly Up", 1500, "1414 NE Snail ln");
      newVenue.Save();

      newBand.AddVenue(newVenue);
      List<Venue> result = newBand.GetVenues();
      List<Venue> expected = new List<Venue> {newVenue};
      Assert.Equal(expected, result);
    }
    [Fact]
    public void Delete_DeletesSingleInstance_True()
    {
      Band newBand = new Band("Kings of Leon", "Indie Rock");
      newBand.Save();

      newBand.Delete();
      List<Band> result = Band.GetAll();
      List<Band> expected = new List<Band>{};

      Assert.Equal(expected, result);
    }

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }

  }
}

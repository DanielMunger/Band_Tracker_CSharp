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

    public void Dispose()
    {
      Band.DeleteAll();
    }

  }
}

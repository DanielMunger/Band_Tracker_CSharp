using Nancy;
using System.Collections.Generic;
using System;
using BandTracker.Objects;

namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Get["/bands"] = _ => {
        List<Band> allBands = Band.GetAll();
        return View["bands.cshtml", allBands];
      };
      Get["/venues"] = _ => {
        List<Venue> allVenues = Venue.GetAll();
        return View["venues.cshtml", allVenues];
      };
      Get["/band/new"] = _ => {
        return View["new_band_form.cshtml"];
      };
      Post["band/new"] = _ => {
        string bandName = Request.Form["band-name"];
        string bandGenre = Request.Form["band-genre"];
        Band newBand = new Band(bandName, bandGenre);
        newBand.Save();
        return View["success.cshtml"];
      };
      Get["/venue/new"] = _ => {
        return View["new_venue_form.cshtml"];
      };
      Post["/venue/new"] = _ => {
        string venueName = Request.Form["venue-name"];
        int venueCapacity = Request.Form["venue-capacity"];
        string venueLocation = Request.Form["venue-capacity"];
        Venue newVenue = new Venue(venueName, venueCapacity, venueLocation);
        newVenue.Save();
        return View["success.cshtml"];
      };
      Get["/band/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Band selectedBand = Band.Find(parameters.id);
        List<Venue> selectedVenues = selectedBand.GetVenues();
        List<Venue> allVenues = Venue.GetAll();
        model.Add("SelectedBand", selectedBand);
        model.Add("SelectedVenues", selectedVenues);
        model.Add("AllVenues", allVenues);
        return View["band.cshtml", model];
      };
      Post["/venue/add_band"] = _ => {
        Band newBand = Band.Find(Request.Form["band-id"]);
        Venue newVenue = Venue.Find(Request.Form["venue-id"]);
        newVenue.AddBand(newBand);
        return View["success.cshtml"];
      };
      Get["/venue/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Venue selectedVenue = Venue.Find(parameters.id);
        List<Band> selectedBands = selectedVenue.GetBands();
        List<Band> allBands = Band.GetAll();
        model.Add("SelectedVenue", selectedVenue);
        model.Add("SelectedBands", selectedBands);
        model.Add("AllBands", allBands);
        return View["venue.cshtml", model];
      };
      Post["/band/add_venue"] = _ => {
        Venue newVenue = Venue.Find(Request.Form["venue-id"]);
        Band newBand = Band.Find(Request.Form["band-id"]);
        newBand.AddVenue(newVenue);
        return View["success.cshtml"];
      };
      Get["/venue/delete/{id}"] = parameters => {
        Venue selectedVenue = Venue.Find(parameters.id);
        selectedVenue.Delete();
        return View["success.cshtml"];
      };
    }
  }
}

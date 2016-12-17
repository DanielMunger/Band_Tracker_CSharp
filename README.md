# _Band Tracker_

#### _A Program to Log Bands and Venues_

#### By _**Daniel Munger**_

## Description
_This program uses a database to keep track of bands and the venues that are in a given area. The user is able to input a band or venue as well as add associations between the two. Such as adding a band to a venue that the band has played at and vice-versa._

## Specs
| Behavior                                                                                   | Input                        | Output                                                                         |
|--------------------------------------------------------------------------------------------|------------------------------|--------------------------------------------------------------------------------|
| The application has an empty database with two separate tables, Bands and Venues           | null                         | null                                                                           |
| The application is able to Create and Save a Venue, and confirm that the object was saved. | "Dirtbag Ernies"             | "Dirtbag Ernies"                                                               |
| The application shows a list of all Venues and their information                           | "Show Venues"                | "Dirtbag Ernies", "Crystal Ballroom", "Belly Up"                               |
| The application can find a Specific Venue.                                                 | Find: "Dirtbag Ernies"       | "Dirtbag Ernies"                                                               |
| The application allows a user to Update information about the Venue.                       | "Erics Messy Dancefloor"     | "Dirtbag Ernies"                                                               |
| The application can Delete a single Venue.                                                 | Delete: "Dirtbag Ernies"     | null                                                                           |
| The application can Delete all Venues.                                                     | Delete: Venues               | null                                                                           |
| The applicaiton is able to Create and Save a Band, and confirm the object was saved.       | "Kings of Leon"              | "Kings of Leon"                                                                |
| The application is able to Find a Band.                                                    | "Kings of Leon"              | "Kings of Leon"                                                                |
| The application allows a user to add a Band to a Venue.                                    | "Kings of Leon" + "Belly Up" | The Kings of Leon are playing at the Belly Up.                                 |
| The application will list all the Bands that play at a given Venue.                        | "Belly Up"                   | "Kings of Leon", "Portugal, The Man", "Portland Elementary School Orchestra"   |
| The application will list all the venues a Band plays at.                                  | "Kings of Leon"              | "Belly Up" , "Crystal Ballroom" , "Billy's Garage"                             |



## Setup/Installation Requirements

* _Clone the repository (https://github.com/solgo/TEMPLATE.git)._
* _Change the database location in "Data Source=DESKTOP-GC3DC7B\\SQLEXPRESS;Initial Catalog=band_tracker;Integrated Security=SSPI;" to your local database._
* _Follow SQL instructions below to set up your tables._
* _run 'dnu restore' in powershell to create a unique project.lock.json file._
* _run 'dnx kestrel' to start your local server._
* _open the webpage 'localhost:5004' to view the application._
* _follow website instructions._
* _enjoy!!_


## SQL Server Setup Instructions

* Open Microsoft SQL Server Manager.
* Select File > Open > File and select "band_tracker_schema.sql"
* Add the following lines to the top of the file:
* CREATE DATABASE [band_tracker]
* GO
* Save the File and Click 'Execute'
* Repeat Process for 'band_tracker_test'

## Known Bugs

_No Known Bugs._

## Support and contact details

_Please contact Daniel through GitHub "https://www.github.com/solgo"_

## Technologies Used

_HTML, CSS, C#, Nancy, Razor, Git, GitHub_

### License

*MIT*

Copyright (c) 2016 **_Daniel Munger_**

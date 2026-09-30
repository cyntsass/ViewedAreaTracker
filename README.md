# Viewed Area Tracker

An ArcGIS Pro add-in for recording which areas of a map have been viewed while navigating.

Viewed Area Tracker listens for changes to the active map view and records the visible map extent as a polygon. A user-selectable maximum scale controls when areas are recorded.

Developed at the University of Gothenburg.

## TL;DR

1. Download **`ViewedAreaTracker.esriAddInX`**, double-click it, and install the add-in.
2. (!) Your map **must contain a polygon shapefile named exactly `Viewed_Areas.shp`** (layer name: `Viewed_Areas`), or the tracker will not work.
3. Choose a scale → **Start Tracking** → explore your map → **Stop Tracking**.

---

## Features

- Start and stop tracking from the ArcGIS Pro **Add-In** tab
- Records the visible map extent while navigating
- Stores each viewed extent as a polygon
- Records:
  - viewing time
  - map scale
- User-selectable maximum recording scale
- Supports scales from **1:1,000 to 1:10,000,000**
- Includes an **All scales** option

---

## Maximum Scale

The **Maximum Scale** control determines when viewed areas are recorded.

For example:

- `1:50,000` records views at 1:50,000 and closer
- `1:500,000` records views at 1:500,000 and closer
- `1:2,000,000` records views at 1:2,000,000 and closer
- `All scales` records regardless of scale

Available options include:

- All scales
- 1:1,000
- 1:2,000
- 1:5,000
- 1:10,000
- 1:20,000
- 1:50,000
- 1:70,000
- 1:100,000
- 1:200,000
- 1:300,000
- 1:400,000
- 1:500,000
- 1:750,000
- 1:1,000,000
- 1:1,500,000
- 1:2,000,000
- 1:3,000,000
- 1:5,000,000
- 1:10,000,000

---

## Requirements

- ArcGIS Pro 3.7
- Windows
- A polygon feature layer named:

`Viewed_Areas`

The layer must contain the fields used by the tracker:

| Field | Description |
|---|---|
| `ViewedTime` | Date and time at which the extent was recorded |
| `Scale` | Map scale at which the extent was viewed |

---

## Installation

### TL;DR

**The `.esriAddInX` file is the only file you need to install the add-in.**

Download:

`ViewedAreaTracker.esriAddInX`

Then:

**Double-click it → Install Add-In → open/restart ArcGIS Pro.**

The Viewed Area Tracker controls will appear under the **Add-In** tab.

---

## Usage

1. Open an ArcGIS Pro project.
2. Add a polygon feature layer named `Viewed_Areas`.
3. Make sure it contains the `ViewedTime` and `Scale` fields.
4. Open the **Add-In** tab.
5. Select the desired **Maximum Scale**.
6. Click **Start Tracking**.
7. Pan and zoom around the map.
8. Click **Stop Tracking** when finished.

The viewed map extents will be stored as polygons in `Viewed_Areas`.

---

## How It Works

When tracking is enabled, the add-in listens for changes to the ArcGIS Pro map camera.

For each map-view change:

1. The current map scale is read.
2. The scale is compared with the selected maximum scale.
3. If the scale is within the selected range, the current map extent is retrieved.
4. The extent is converted to a polygon.
5. A new feature is created in `Viewed_Areas`.
6. The current time and map scale are stored with the feature.

Conceptually:

    Pan / Zoom
        |
        v
    Camera Changed
        |
        v
    Check Maximum Scale
        |
        v
    Get Visible Extent
        |
        v
    Create Polygon
        |
        v
    Viewed_Areas

---

## Output

Each recorded feature represents one viewed map extent.

The tracker writes:

- Polygon geometry
- `ViewedTime`
- `Scale`

This makes it possible to visualize where a map has been inspected and at what scale.

---

## Building From Source

Viewed Area Tracker is written in C# using the ArcGIS Pro SDK for .NET.

Development requirements:

- Visual Studio
- .NET 10 SDK
- ArcGIS Pro 3.7
- ArcGIS Pro SDK for .NET

Clone the repository and open the Visual Studio solution.

For development/debugging:

`F5`

will build the add-in and launch ArcGIS Pro.

For distribution, switch Visual Studio from **Debug** to **Release** and rebuild the solution.

The generated:

`ViewedAreaTracker.esriAddInX`

can then be distributed to other ArcGIS Pro users.

---

## Project Structure

    ViewedAreaTracker/
    |
    |-- Config.daml
    |-- Module1.cs
    |-- ScaleComboBox.cs
    |-- StartTrackingButton.cs
    |-- StopTrackingButton.cs
    |-- ViewTracker.cs
    |-- ViewedAreaTracker.csproj
    |
    |-- Images/
    `-- DarkImages/

### `ViewTracker.cs`

Contains the main tracking logic. It listens for map camera changes, checks the selected maximum scale, and creates viewed-area polygons.

### `ScaleComboBox.cs`

Defines the available maximum scales and converts the selected scale into the numeric scale used by the tracker.

### `StartTrackingButton.cs`

Starts recording map-view changes.

### `StopTrackingButton.cs`

Stops recording map-view changes.

### `Config.daml`

Defines the ArcGIS Pro ribbon group, Start/Stop buttons, Maximum Scale ComboBox, icons, and add-in metadata.

---

## Current Limitations

- A polygon layer named exactly `Viewed_Areas` must already exist in the active map.
- The `ViewedTime` and `Scale` fields must already exist.
- The tracker records the current map extent as a rectangular polygon.
- Camera changes may generate multiple polygons during navigation.
- Tracking operates on the active ArcGIS Pro map view.

---

## Author

Cynthia Sassenroth, Department of Earth Sciences, University of Gothenburg

## Development Note

This project was built with a healthy amount of curiosity, trial and error, and AI-assisted coding.

The tool was designed around a practical GIS workflow and iteratively tested in ArcGIS Pro. AI was used as a coding assistant during development, while the functionality, testing, debugging, and final design decisions were guided by the author.

In other words: a little bit vibe coded, but tested with care.

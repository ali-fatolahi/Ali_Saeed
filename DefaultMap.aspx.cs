using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;

using GeocodeService;
using SearchService;
using ImageryService;
using RouteService;

public partial class BingMapTest_DefaultMap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private String GeocodeAddress(string address)
    {
        string results = "";
        string key = "ArLeGdHOcc5h7j3L4W37oFGcU9E-LF3tAZi4o0DfhXbPJ8aiyTGbIDNHex08R2u7";
        GeocodeRequest geocodeRequest = new GeocodeRequest();

        // Set the credentials using a valid Bing Maps key
        geocodeRequest.Credentials = new GeocodeService.Credentials();
        geocodeRequest.Credentials.ApplicationId = key;

        // Set the full address query
        geocodeRequest.Query = address;

        // Set the options to only return high confidence results 
        ConfidenceFilter[] filters = new ConfidenceFilter[1];
        filters[0] = new ConfidenceFilter();
        filters[0].MinimumConfidence = GeocodeService.Confidence.High;

        // Add the filters to the options
        GeocodeOptions geocodeOptions = new GeocodeOptions();
        geocodeOptions.Filters = filters;
        geocodeRequest.Options = geocodeOptions;

        // Make the geocode request
        GeocodeServiceClient geocodeService = new GeocodeServiceClient("BasicHttpBinding_IGeocodeService");
        GeocodeResponse geocodeResponse = geocodeService.Geocode(geocodeRequest);

        if (geocodeResponse.Results.Length > 0)
            results = String.Format("Latitude: {0}\nLongitude: {1}",
              geocodeResponse.Results[0].Locations[0].Latitude,
              geocodeResponse.Results[0].Locations[0].Longitude);
        else
            results = "No Results Found";

        return results;
    }

    private string ReverseGeocodePoint(string locationString)
    {
        string results = "";
        string key = "ArLeGdHOcc5h7j3L4W37oFGcU9E-LF3tAZi4o0DfhXbPJ8aiyTGbIDNHex08R2u7";
        ReverseGeocodeRequest reverseGeocodeRequest = new ReverseGeocodeRequest();

        // Set the credentials using a valid Bing Maps key
        reverseGeocodeRequest.Credentials = new GeocodeService.Credentials();
        reverseGeocodeRequest.Credentials.ApplicationId = key;

        // Set the point to use to find a matching address
        GeocodeService.Location point = new GeocodeService.Location();
        string[] digits = locationString.Split(',');

        point.Latitude = double.Parse(digits[0].Trim());
        point.Longitude = double.Parse(digits[1].Trim());

        reverseGeocodeRequest.Location = point;

        // Make the reverse geocode request
        GeocodeServiceClient geocodeService = new GeocodeServiceClient("BasicHttpBinding_IGeocodeService");
        GeocodeResponse geocodeResponse = geocodeService.ReverseGeocode(reverseGeocodeRequest);

        if (geocodeResponse.Results.Length > 0)
            results = geocodeResponse.Results[0].DisplayName;
        else
            results = "No Results found";

        return results;
    }

    private string SearchKeywordLocation(string keywordLocation)
    {
        String results = "";
        String key = "ArLeGdHOcc5h7j3L4W37oFGcU9E-LF3tAZi4o0DfhXbPJ8aiyTGbIDNHex08R2u7";
        SearchRequest searchRequest = new SearchRequest();

        // Set the credentials using a valid Bing Maps key
        searchRequest.Credentials = new SearchService.Credentials();
        searchRequest.Credentials.ApplicationId = key;

        //Create the search query
        StructuredSearchQuery ssQuery = new StructuredSearchQuery();
        string[] parts = keywordLocation.Split(';');
        ssQuery.Keyword = parts[0];
        ssQuery.Location = parts[1];
        searchRequest.StructuredQuery = ssQuery;

        //Define options on the search
        searchRequest.SearchOptions = new SearchOptions();
        searchRequest.SearchOptions.Filters =
            new FilterExpression()
            {
                PropertyId = 3,
                CompareOperator = CompareOperator.GreaterThanOrEquals,
                FilterValue = 8.16
            };

        //Make the search request 
        SearchServiceClient searchService = new SearchServiceClient("BasicHttpBinding_ISearchService");
        SearchResponse searchResponse = searchService.Search(searchRequest);

        //Parse and format results
        if (searchResponse.ResultSets[0].Results.Length > 0)
        {
            StringBuilder resultList = new StringBuilder("");
            for (int i = 0; i < searchResponse.ResultSets[0].Results.Length; i++)
            {
                resultList.Append(String.Format("{0}. {1}\n", i + 1,
                    searchResponse.ResultSets[0].Results[i].Name));
            }

            results = resultList.ToString();
        }
        else
            results = "No results found";

        return results;
    }

    private string GetImagery(string locationString)
    {
        string key = "ArLeGdHOcc5h7j3L4W37oFGcU9E-LF3tAZi4o0DfhXbPJ8aiyTGbIDNHex08R2u7";
        MapUriRequest mapUriRequest = new MapUriRequest();

        // Set credentials using a valid Bing Maps key
        mapUriRequest.Credentials = new ImageryService.Credentials();
        mapUriRequest.Credentials.ApplicationId = key;

        // Set the location of the requested image
        mapUriRequest.Center = new ImageryService.Location();
        string[] digits = locationString.Split(',');
        mapUriRequest.Center.Latitude = double.Parse(digits[0].Trim());
        mapUriRequest.Center.Longitude = double.Parse(digits[1].Trim());

        // Set the map style and zoom level
        MapUriOptions mapUriOptions = new MapUriOptions();
        mapUriOptions.Style = MapStyle.AerialWithLabels;
        mapUriOptions.ZoomLevel = 17;

        // Set the size of the requested image in pixels
        mapUriOptions.ImageSize = new ImageryService.SizeOfint();
        mapUriOptions.ImageSize.Height = 200;
        mapUriOptions.ImageSize.Width = 300;

        mapUriRequest.Options = mapUriOptions;

        //Make the request and return the URI
        ImageryServiceClient imageryService = new ImageryServiceClient("BasicHttpBinding_IImageryService");
        MapUriResponse mapUriResponse = imageryService.GetMapUri(mapUriRequest);
        return mapUriResponse.Uri;
    }

    private string RequestImageMetadata(string locationString)
    {
        string results = "";
        string key = "ArLeGdHOcc5h7j3L4W37oFGcU9E-LF3tAZi4o0DfhXbPJ8aiyTGbIDNHex08R2u7";

        ImageryMetadataRequest metadataRequest = new ImageryMetadataRequest();

        // Set credentials using a valid Bing Maps key
        metadataRequest.Credentials = new ImageryService.Credentials();
        metadataRequest.Credentials.ApplicationId = key;

        // Set the imagery metadata request options
        ImageryService.Location centerLocation = new ImageryService.Location();
        string[] digits = locationString.Split(',');
        centerLocation.Latitude = double.Parse(digits[0].Trim());
        centerLocation.Longitude = double.Parse(digits[1].Trim());

        metadataRequest.Options = new ImageryMetadataOptions();
        metadataRequest.Options.Location = centerLocation;
        metadataRequest.Options.ZoomLevel = 10;
        metadataRequest.Style = MapStyle.AerialWithLabels;

        // Make the imagery metadata request 
        ImageryServiceClient imageryService = new ImageryServiceClient("BasicHttpBinding_IImageryService");
        ImageryMetadataResponse metadataResponse =
          imageryService.GetImageryMetadata(metadataRequest);

        ImageryMetadataResult result = metadataResponse.Results[0];
        if (metadataResponse.Results.Length > 0)
            results = String.Format("Uri: {0}\nVintage: {1} to {2}\nZoom Levels: {3} to {4}",
                result.ImageUri,
                result.Vintage.From.ToString(),
                result.Vintage.To.ToString(),
                result.ZoomRange.From.ToString(),
                result.ZoomRange.To.ToString());
        else
            results = "Metadata is not available";
        return results;
    }

    private string CreateRoute(string waypointString)
    {
        string results = "";
        string key = "ArLeGdHOcc5h7j3L4W37oFGcU9E-LF3tAZi4o0DfhXbPJ8aiyTGbIDNHex08R2u7";
        RouteRequest routeRequest = new RouteRequest();

        // Set the credentials using a valid Bing Maps key
        routeRequest.Credentials = new RouteService.Credentials();
        routeRequest.Credentials.ApplicationId = key;

        //Parse user data to create array of waypoints
        string[] points = waypointString.Split(';');
        Waypoint[] waypoints = new Waypoint[points.Length];

        int pointIndex = -1;
        foreach (string point in points)
        {
            pointIndex++;
            waypoints[pointIndex] = new Waypoint();
            string[] digits = point.Split(','); waypoints[pointIndex].Location = new RouteService.Location();
            waypoints[pointIndex].Location.Latitude = double.Parse(digits[0].Trim());
            waypoints[pointIndex].Location.Longitude = double.Parse(digits[1].Trim());

            if (pointIndex == 0)
                waypoints[pointIndex].Description = "Start";
            else if (pointIndex == points.Length)
                waypoints[pointIndex].Description = "End";
            else
                waypoints[pointIndex].Description = string.Format("Stop #{0}", pointIndex);
        }

        routeRequest.Waypoints = waypoints;

        // Make the calculate route request
        RouteServiceClient routeService = new RouteServiceClient("BasicHttpBinding_IRouteService");
        RouteResponse routeResponse = routeService.CalculateRoute(routeRequest);

        // Iterate through each itinerary item to get the route directions
        StringBuilder directions = new StringBuilder("");

        if (routeResponse.Result.Legs.Length > 0)
        {
            int instructionCount = 0;
            int legCount = 0;

            foreach (RouteLeg leg in routeResponse.Result.Legs)
            {
                legCount++;
                directions.Append(string.Format("Leg #{0}\n", legCount));

                foreach (ItineraryItem item in leg.Itinerary)
                {
                    instructionCount++;
                    directions.Append(string.Format("{0}. {1}\n",
                        instructionCount, item.Text));
                }
            }
            //Remove all Bing Maps tags around keywords.  
            //If you wanted to format the results, you could use the tags
            Regex regex = new Regex("<[/a-zA-Z:]*>",
              RegexOptions.IgnoreCase | RegexOptions.Multiline);
            results = regex.Replace(directions.ToString(), string.Empty);
        }
        else
            results = "No Route found";

        return results;
    }

    private string CreateMajorRoutes(string locationString)
    {
        string results = "";
        string key = "ArLeGdHOcc5h7j3L4W37oFGcU9E-LF3tAZi4o0DfhXbPJ8aiyTGbIDNHex08R2u7";
        MajorRoutesRequest majorRoutesRequest = new MajorRoutesRequest();

        // Set the credentials using a valid Bing Maps key
        majorRoutesRequest.Credentials = new RouteService.Credentials();
        majorRoutesRequest.Credentials.ApplicationId = key;

        // Set the destination of the routes from major roads
        Waypoint endPoint = new Waypoint();
        endPoint.Location = new RouteService.Location();
        string[] digits = locationString.Split(',');
        endPoint.Location.Latitude = double.Parse(digits[0].Trim());
        endPoint.Location.Longitude = double.Parse(digits[1].Trim());
        endPoint.Description = "Location";

        // Set the option to return full routes with directions
        MajorRoutesOptions options = new MajorRoutesOptions();
        options.ReturnRoutes = true;

        majorRoutesRequest.Destination = endPoint;
        majorRoutesRequest.Options = options;

        // Make the route-from-major-roads request
        RouteServiceClient routeService = new RouteServiceClient("BasicHttpBinding_IRouteService");

        // The result is an MajorRoutesResponse Object
        MajorRoutesResponse majorRoutesResponse = routeService.CalculateRoutesFromMajorRoads(majorRoutesRequest);

        Regex regex = new Regex("<[/a-zA-Z:]*>",
          RegexOptions.IgnoreCase | RegexOptions.Multiline);

        if (majorRoutesResponse.StartingPoints.Length > 0)
        {
            StringBuilder directions = new StringBuilder();

            for (int i = 0; i < majorRoutesResponse.StartingPoints.Length; i++)
            {
                directions.Append(String.Format("Coming from {1}\n", i + 1,
                    majorRoutesResponse.StartingPoints[i].Description));

                for (int j = 0;
                  j < majorRoutesResponse.Routes[i].Legs[0].Itinerary.Length; j++)
                {
                    //Strip tags
                    string step = regex.Replace(
                      majorRoutesResponse.Routes[i].Legs[0].Itinerary[j].Text, string.Empty);
                    directions.Append(String.Format("     {0}. {1}\n", j + 1, step));
                }
            }

            results = directions.ToString();
        }
        else
            results = "No Routes found";

        return results;
    }
    
    public void Geocode_Click(object sender, EventArgs e)
    {
        labelResults.Visible = true;
        imageResults.Visible = false;
        labelResults.Text = GeocodeAddress(textInput.Text);
    }

    public void ReverseGeocode_Click(object sender, EventArgs e)
    {
        labelResults.Visible = true;
        imageResults.Visible = false;
        labelResults.Text = ReverseGeocodePoint(textInput.Text);
    }

    public void Search_Click(object sender, EventArgs e)
    {
        labelResults.Visible = true;
        imageResults.Visible = false;
        labelResults.Text = SearchKeywordLocation(textInput.Text);
    }

    public void RequestImage_Click(object sender, EventArgs e)
    {
        // Make label hidden and image visible
        labelResults.Visible = false;
        imageResults.Visible = true;

        //Get URI and set image
        String imageURI = GetImagery(textInput.Text);
        imageResults.ImageUrl = (new Uri(imageURI)).ToString();
    }

    public void RequestMetadata_Click(object sender, EventArgs e)
    {
        labelResults.Text = RequestImageMetadata(textInput.Text);
    }

    public void Route_Click(object sender, EventArgs e)
    {
        labelResults.Text = CreateRoute(textInput.Text);
    }

    public void MajorRoutes_Click(object sender, EventArgs e)
    {
        labelResults.Text = CreateMajorRoutes(textInput.Text);
    }
}
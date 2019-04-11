using Esri.ArcGISRuntime.Portal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime;
using Esri.ArcGISRuntime.UI.Controls;
using System.Drawing;


namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class MainWindow //: Window
    {

        // Create globally available feature table for easy referencing.
        private ServiceFeatureTable _featureTable;
        private ServiceFeatureTable _airFeatureTable;
        private ServiceFeatureTable _windFeatureTable;
        private ServiceFeatureTable _seaTempFeatureTable;

        // Create globally available feature layer for easy referencing.
        private FeatureLayer _featureLayer;
        private FeatureLayer _airFeatureLayer;
        private FeatureLayer _windFeatureLayer;
        private FeatureLayer _seaTempFeatureLayer;

        // Create globally available DateTime range for easy referencing.
        DateTime start = new DateTime(1958, 9, 1);
        DateTime end = new DateTime(2018, 10, 30);

        // Create globally available map for easy referencing.
        private Map myMap;

        private List<TurtleData> turtleDataList = new List<TurtleData>();

        public MainWindow()
        {
            InitializeComponent();
            //get_Online_Map();
            get_feature_layer();
            //get_sublayers_for_visibility();
            //Initialize();
        }

        private async void get_Online_Map()
        {
            /***********************************************************************
             *  This function is for grabbing an entire web map from ArcGIS.com 
             ************************************************************************/

            // connect to ArcGIS Online (default portal if no URL is passed to ArcGISPortal.CreateAsync)
            ArcGISPortal arcGISOnline = await ArcGISPortal.CreateAsync();

            // create an PortalItem using a portal item ID
            var portalItem = await PortalItem.CreateAsync(arcGISOnline, "ea9edab4000d4e5c85f327dbc03f7f80");

            // pass the portal item to the constructor for a new Map
            var webMap = new Map(portalItem);

            // add the map to the map view
            MyMapView.Map = webMap;

        }

        private void get_feature_layer()
        {
            /***********************************************************************
             *   This function loads a feature layer from arcGIS online
             ************************************************************************/
            // Create new Map with basemap
            myMap = new Map(Basemap.CreateTopographic());


            // Create and set initial map area
            Envelope initialLocation = new Envelope(
                -160.6739, 13.3844, -179.5449, 80.2319,
                SpatialReferences.Wgs84);
            myMap.InitialViewpoint = new Viewpoint(initialLocation);


            /***********************************************************************
             *   Turtle Feature Layer
             ************************************************************************/
            // Create uri to the used feature service
            var serviceUri = new Uri(
                "https://services5.arcgis.com/qRhgKuZN9Zwtwsjc/ArcGIS/rest/services/Turtle_Group_Layer/FeatureServer/1");

            // Create feature table using a URL.
            _featureTable = new ServiceFeatureTable(serviceUri);

            MyMapView.Map = myMap;

            // Create feature layer using this feature table. Make it slightly transparent.
            _featureLayer = new FeatureLayer(_featureTable);

            string theJSON_String =
             @"{
                    ""labelExpressionInfo"":{""expression"":""$feature.Species""},
                    ""labelPlacement"":""esriServerLinePlacementAboveAlong"",
                    ""where"":""Species <> ' '"",
                    ""symbol"":
                        { 
                            ""angle"":0,
                            ""backgroundColor"":[0,0,0,0],
                            ""borderLineColor"":[0,0,0,0],
                            ""borderLineSize"":0,
                            ""color"":[51, 51, 51, 255],
                            ""font"":
                                {
                                    ""decoration"":""none"",
                                    ""size"":10,
                                    ""style"":""normal"",
                                    ""weight"":""normal""
                                },
                            ""haloColor"":[0,0,0,0],
                            ""haloSize"":1.5,
                            ""horizontalAlignment"":""center"",
                            ""kerning"":false,
                            ""type"":""esriTS"",
                            ""verticalAlignment"":""middle"",
                            ""xoffset"":0,
                            ""yoffset"":0
                        }
               }";

            // Create a label definition from the JSON string. 
            LabelDefinition turtleSpeciesLabelDefinition = LabelDefinition.FromJson(theJSON_String);
           

            /***********************************************************************
             *   Air Temp Feature Layer
             ************************************************************************/
            var airServiceUri = new Uri(
                "https://services5.arcgis.com/A8199VwGsdtPOFY6/arcgis/rest/services/BMC_WeatherData_1957_2018_Av1/FeatureServer/0");

            // Create feature table using a URL.
            _airFeatureTable = new ServiceFeatureTable(airServiceUri);

            // Create feature layer using this feature table. Make it slightly transparent.
            _airFeatureLayer = new FeatureLayer(_airFeatureTable)
            {
                Opacity = 0.6
            };

             /***********************************************************************
             *   Wind Feature Layer
             ************************************************************************/
            var windServiceUri = new Uri(
                "https://services5.arcgis.com/A8199VwGsdtPOFY6/arcgis/rest/services/Wind/FeatureServer/0");

            // Create feature table using a URL.
            _windFeatureTable = new ServiceFeatureTable(windServiceUri);

            // Create feature layer using this feature table. Make it slightly transparent.
            _windFeatureLayer = new FeatureLayer(_windFeatureTable)
            {
                Opacity = 0.6
            };

            /***********************************************************************
             *   Sea Surface Temperature Feature Layer
             ************************************************************************/
            var seaTempServiceUri = new Uri(
                "https://services5.arcgis.com/A8199VwGsdtPOFY6/arcgis/rest/services/Sea_Surface_Temp_C/FeatureServer/0");

            // Create feature table using a URL.
            _seaTempFeatureTable = new ServiceFeatureTable(seaTempServiceUri);

            // Create feature layer using this feature table. Make it slightly transparent.
            _seaTempFeatureLayer = new FeatureLayer(_seaTempFeatureTable)
            {
                Opacity = 0.6
            };

            // add feature layers to map
           /* myMap.OperationalLayers.Add(_featureLayer);
            myMap.OperationalLayers.Add(_airFeatureLayer);
            myMap.OperationalLayers.Add(_windFeatureLayer);
            myMap.OperationalLayers.Add(_seaTempFeatureLayer);*/


            // Add the label definition to the feature layer's label definition collection.
            _featureLayer.LabelDefinitions.Add(turtleSpeciesLabelDefinition);

            // Enable the visibility of labels to be seen.
            _featureLayer.LabelsEnabled = true;

            MyMapView.TimeExtent = new TimeExtent(start, end);

            // add the map to the map view
            MyMapView.Map = myMap;

        }

        private void onChangeDateRange(object sender, RoutedEventArgs e)
        {
            if (start > end)
            {
                MessageBox.Show("Start Date Cannot Occur After End Date");
            }
            else
            {
                MyMapView.TimeExtent = new TimeExtent(start, end);
            }
        }

        private void onDate1Changed(object sender, SelectionChangedEventArgs e)
        {

            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                MessageBox.Show("Error: No Date Was Selected");
            }
            else
            {
                
                // ... No need to display the time.
                var stringDate = date.Value.ToShortDateString();
                
                //MessageBox.Show(date.Value.ToShortDateString());
                var formattedDate = DateTime.ParseExact(stringDate, "d", null);
                var Day = formattedDate.Day;
                var Month = formattedDate.Month;
                var Year = formattedDate.Year;
              
                
                MessageBox.Show(Year.ToString());
                start = new DateTime(Year, Month, Day);
               
                //MyMapView.TimeExtent = new TimeExtent(start, end);
            }
        }

        private void onDate2Changed(object sender, SelectionChangedEventArgs e)
        {

            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                MessageBox.Show("Error: No Date Was Selected");
            }
            else
            {
                // ... No need to display the time.
                var stringDate = date.Value.ToShortDateString();

                //MessageBox.Show(date.Value.ToShortDateString());
                var formattedDate = DateTime.ParseExact(stringDate, "d", null);
                var Day = formattedDate.Day;
                var Month = formattedDate.Month;
                var Year = formattedDate.Year;


                end = new DateTime(Year, Month, Day);

            }
        }

        private void feature_layer_query()
        {
            // Create new Map with basemap.
            Map myMap = new Map(Basemap.CreateTopographic());

            // Create and set initial map location.
            MapPoint initialLocation = new MapPoint(-171.6739, -15.3844, SpatialReferences.WebMercator);
            myMap.InitialViewpoint = new Viewpoint(initialLocation, 100000000);

            // Create feature table using a URL.
            _featureTable = new ServiceFeatureTable(new Uri("https://services5.arcgis.com/qRhgKuZN9Zwtwsjc/ArcGIS/rest/services/Turtle_Group_Layer/FeatureServer/1"));

            // Create feature layer using this feature table. Make it slightly transparent.
            _featureLayer = new FeatureLayer(_featureTable)
            {
                Opacity = 0.6
            };

            // Add feature layer to the map.
            myMap.OperationalLayers.Add(_featureLayer);

            // Assign the map to the MapView.
            MyMapView.Map = myMap;
        }


        private async void MakeTurtleTable (object sender, RoutedEventArgs e)
        {
            // Begin query process.
            await MakeTableFromTurtleData();
        }

        private async Task MakeTableFromTurtleData()
        {
            if (TurtleTableButton.Content.Equals("Hide Turtle Data Table"))
            {
                TurtleDataGrid.Visibility = Visibility.Hidden;
                TurtleTableButton.Content = "Show Turtle Data Table";
            }

            else
            {
                try
                {
                    // Create a query parameters that will be used to Query the feature table.
                    QueryParameters queryParams = new QueryParameters();

                    // Construct and assign the where clause that will be used to query the feature table.
                    queryParams.WhereClause = "1=1";

                    // Query the feature table.
                    FeatureQueryResult queryResult = await _featureTable.QueryFeaturesAsync(queryParams);

                    // Cast the QueryResult to a List so the results can be interrogated.
                    List<Feature> features = queryResult.ToList();


                    if (features.Any())
                    {


                        // Loop over each feature from the query result.
                        foreach (Feature feature in features)
                        {
                            TurtleData turtleData = new TurtleData();
                            turtleData.Species = feature.GetAttributeValue("Species").ToString();
                            turtleData.Date_Found = feature.GetAttributeValue("Date").ToString();
                            //turtleData.Alive_or_Dead = feature.GetAttributeValue("Alive_Dead").ToString();
                            //turtleData.Area_Found = feature.GetAttributeValue("Area").ToString();
                            //turtleData.State_Found = feature.GetAttributeValue("State").ToString();
                            //turtleData.Longitude = feature.GetAttributeValue("Longitude").ToString();
                            //turtleData.Latitude = feature.GetAttributeValue("Latitude").ToString();
                            //turtleData.Notes = feature.GetAttributeValue("Notes").ToString();      
                            turtleDataList.Add(turtleData);
                        }

                        TurtleDataGrid.ItemsSource = turtleDataList;
                        TurtleDataGrid.Visibility = Visibility.Visible;
                        TurtleTableButton.Content = "Hide Turtle Data Table";

                    }
                    else
                    {
                        MessageBox.Show("No Features to Select");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred.\n" + ex, "Sample error");
                }
            }
        }

        private async void turtleCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            // Begin query process.
            await ShowTurtleData();
        }

        private async Task ShowTurtleData()
        {
            myMap.OperationalLayers.Add(_featureLayer);
            GreenSeaTurtleCheckbox.IsChecked = true;
            LeatherbackCheckbox.IsChecked = true;
            OliveRidleyCheckbox.IsChecked = true;
            LoggerheadCheckbox.IsChecked = true;
            UnidentifiedTurtleCheckBox.IsChecked = true;

            try
            {
                // Create a query parameters that will be used to Query the feature table.
                QueryParameters queryParams = new QueryParameters();

                // Construct and assign the where clause that will be used to query the feature table.
                queryParams.WhereClause = "1=1";

                // Query the feature table.
                FeatureQueryResult queryResult = await _featureTable.QueryFeaturesAsync(queryParams);

                // Cast the QueryResult to a List so the results can be interrogated.
                List<Feature> features = queryResult.ToList();
               
                if (features.Any())
                {
                    // Loop over each feature from the query result.
                    foreach (Feature feature in features)
                    {
                        var attributevalue = feature.GetAttributeValue("Species").ToString();
                        var attributeDatevalue = feature.GetAttributeValue("Date").ToString();
                        // Select each feature.
                        _featureLayer.SetFeatureVisible(feature, true);
                    }
                }
                else
                {
                    MessageBox.Show("No Features to Select");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred.\n" + ex, "Sample error");
            }
        }

        private async void turtleCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            await HideTurtleData();
        }

        private async Task HideTurtleData()
        {
            myMap.OperationalLayers.Remove(_featureLayer);
            GreenSeaTurtleCheckbox.IsChecked = false;
            LeatherbackCheckbox.IsChecked = false;
            OliveRidleyCheckbox.IsChecked = false;
            LoggerheadCheckbox.IsChecked = false;
            UnidentifiedTurtleCheckBox.IsChecked = false;

            try
            {
                // Create a query parameters that will be used to Query the feature table.
                QueryParameters queryParams = new QueryParameters();

                // Construct and assign the where clause that will be used to query the feature table.
                queryParams.WhereClause = "1=1";

                // Query the feature table.
                FeatureQueryResult queryResult = await _featureTable.QueryFeaturesAsync(queryParams);

                // Cast the QueryResult to a List so the results can be interrogated.
                List<Feature> features = queryResult.ToList();

                if (features.Any())
                {
                    // Loop over each feature from the query result.
                    foreach (Feature feature in features)
                    {
                        // Select each feature.
                        _featureLayer.SetFeatureVisible(feature, false);
                    }
                }
                else
                {
                    MessageBox.Show("No Features to Select");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred.\n" + ex, "Sample error");
            }
        }


        private async void airCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            // Begin query process.
            await ShowAirData();
        }

        private async Task ShowAirData()
        {
            myMap.OperationalLayers.Add(_airFeatureLayer);
            try
            {
                // Create a query parameters that will be used to Query the feature table.
                QueryParameters queryParams = new QueryParameters();

                // Construct and assign the where clause that will be used to query the feature table.
                queryParams.WhereClause = "1=1";

                // Query the feature table.
                FeatureQueryResult queryResult = await _airFeatureTable.QueryFeaturesAsync(queryParams);

                // Cast the QueryResult to a List so the results can be interrogated.
                List<Feature> features = queryResult.ToList();

                if (features.Any())
                {

                    // Loop over each feature from the query result.
                    foreach (Feature feature in features)
                    {
                        // Select each feature.
                        _airFeatureLayer.SetFeatureVisible(feature, true);
                    }
                }
                else
                {
                    MessageBox.Show("No Features to Select");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred.\n" + ex, "Sample error");
            }
        }

        private async void airCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            await HideAirData();
        }

        private async Task HideAirData()
        {
           myMap.OperationalLayers.Remove(_airFeatureLayer);

            try
            {
                // Create a query parameters that will be used to Query the feature table.
                QueryParameters queryParams = new QueryParameters();

                // Construct and assign the where clause that will be used to query the feature table.
                queryParams.WhereClause = "1=1";

                // Query the feature table.
                FeatureQueryResult queryResult = await _airFeatureTable.QueryFeaturesAsync(queryParams);

                // Cast the QueryResult to a List so the results can be interrogated.
                List<Feature> features = queryResult.ToList();

                if (features.Any())
                {
                    // Loop over each feature from the query result.
                    foreach (Feature feature in features)
                    {
                        // Select each feature.
                        _airFeatureLayer.SetFeatureVisible(feature, false);
                    }
                }
                else
                {
                    MessageBox.Show("No Features to Select");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred.\n" + ex, "Sample error");
            }
        }

        private async void windCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            // Begin query process.
            await ShowWindData();
        }

        private async Task ShowWindData()
        {
            myMap.OperationalLayers.Add(_windFeatureLayer);
            try
            {
                // Create a query parameters that will be used to Query the feature table.
                QueryParameters queryParams = new QueryParameters();

                // Construct and assign the where clause that will be used to query the feature table.
                queryParams.WhereClause = "1=1";

                // Query the feature table.
                FeatureQueryResult queryResult = await _windFeatureTable.QueryFeaturesAsync(queryParams);

                // Cast the QueryResult to a List so the results can be interrogated.
                List<Feature> features = queryResult.ToList();

                if (features.Any())
                {
                    // Loop over each feature from the query result.
                    foreach (Feature feature in features)
                    {
                        // Select each feature.
                        _windFeatureLayer.SetFeatureVisible(feature, true);
                        _windFeatureLayer.MinScale = 100000000;
                    }
                }
                else
                {
                    MessageBox.Show("No Features to Select");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred.\n" + ex, "Sample error");
            }
        }

        private async void windCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            await HideWindData();
        }

        private async Task HideWindData()
        {
            myMap.OperationalLayers.Remove(_windFeatureLayer);
            try
            {
                // Create a query parameters that will be used to Query the feature table.
                QueryParameters queryParams = new QueryParameters();

                // Construct and assign the where clause that will be used to query the feature table.
                queryParams.WhereClause = "1=1";

                // Query the feature table.
                FeatureQueryResult queryResult = await _windFeatureTable.QueryFeaturesAsync(queryParams);

                // Cast the QueryResult to a List so the results can be interrogated.
                List<Feature> features = queryResult.ToList();

                if (features.Any())
                {
                    // Loop over each feature from the query result.
                    foreach (Feature feature in features)
                    {
                        // Select each feature.
                        _windFeatureLayer.SetFeatureVisible(feature, false);
                    }
                }
                else
                {
                    MessageBox.Show("No Features to Select");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred.\n" + ex, "Sample error");
            }
        }


        private async void seaTempCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            // Begin query process.
            await ShowSeaTempData();
        }

        private async Task ShowSeaTempData()
        {
            myMap.OperationalLayers.Add(_seaTempFeatureLayer);
            try
            {
                // Create a query parameters that will be used to Query the feature table.
                QueryParameters queryParams = new QueryParameters();

                // Construct and assign the where clause that will be used to query the feature table.
                queryParams.WhereClause = "1=1";

                // Query the feature table.
                FeatureQueryResult queryResult = await _seaTempFeatureTable.QueryFeaturesAsync(queryParams);

                // Cast the QueryResult to a List so the results can be interrogated.
                List<Feature> features = queryResult.ToList();

                if (features.Any())
                {
                    // Loop over each feature from the query result.
                    foreach (Feature feature in features)
                    {
                        // Select each feature.
                        _seaTempFeatureLayer.SetFeatureVisible(feature, true);
                    }
                }
                else
                {
                    MessageBox.Show("No Features to Select");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred.\n" + ex, "Sample error");
            }
        }

        private async void seaTempCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            await HideSeaTempData();
        }

        private async Task HideSeaTempData()
        {
            myMap.OperationalLayers.Remove(_seaTempFeatureLayer);
            try
            {
                // Create a query parameters that will be used to Query the feature table.
                QueryParameters queryParams = new QueryParameters();

                // Construct and assign the where clause that will be used to query the feature table.
                queryParams.WhereClause = "1=1";

                // Query the feature table.
                FeatureQueryResult queryResult = await _seaTempFeatureTable.QueryFeaturesAsync(queryParams);

                // Cast the QueryResult to a List so the results can be interrogated.
                List<Feature> features = queryResult.ToList();

                if (features.Any())
                {
                    // Loop over each feature from the query result.
                    foreach (Feature feature in features)
                    {
                        // Select each feature.
                        _seaTempFeatureLayer.SetFeatureVisible(feature, false);
                    }
                }
                else
                {
                    MessageBox.Show("No Features to Select");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred.\n" + ex, "Sample error");
            }
        }

        /********************************************************************************
         * 
         *          Logic for showing/hiding turtles based on species
         * 
         * ******************************************************************************/

        private async void greenTurtleCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            // Begin query process.
            await ShowGreenTurtleSpecies();
        }

        private async Task ShowGreenTurtleSpecies()
        {           
            try
            {
                // Create a query parameters that will be used to Query the feature table.
                QueryParameters queryParams = new QueryParameters();

                // Construct and assign the where clause that will be used to query the feature table.
                queryParams.WhereClause = "Species = 'Green Sea Turtle' or Species = 'Pacific Green Sea Turtle'";

                // Query the feature table.
                FeatureQueryResult queryResult = await _featureTable.QueryFeaturesAsync(queryParams);

                // Cast the QueryResult to a List so the results can be interrogated.
                List<Feature> features = queryResult.ToList();

                if (features.Any())
                {
                    // Loop over each feature from the query result.
                    foreach (Feature feature in features)
                    {
                        // Select each feature.
                        _featureLayer.SetFeatureVisible(feature, true);
                    }
                }
                else
                {
                    MessageBox.Show("No Features to Select");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred.\n" + ex, "Sample error");
            }
        }


        private async void greenTurtleCheckBoxUnChecked(object sender, RoutedEventArgs e)
        {
            // Begin query process.
            await HideGreenTurtleSpecies();
        }

        private async Task HideGreenTurtleSpecies()
        {          
            try
            {
                // Create a query parameters that will be used to Query the feature table.
                QueryParameters queryParams = new QueryParameters();

                // Construct and assign the where clause that will be used to query the feature table.
                queryParams.WhereClause = "Species = 'Green Sea Turtle' or Species = 'Pacific Green Sea Turtle'";

                // Query the feature table.
                FeatureQueryResult queryResult = await _featureTable.QueryFeaturesAsync(queryParams);

                // Cast the QueryResult to a List so the results can be interrogated.
                List<Feature> features = queryResult.ToList();

                if (features.Any())
                {
                    // Loop over each feature from the query result.
                    foreach (Feature feature in features)
                    {
                        // Select each feature.
                        _featureLayer.SetFeatureVisible(feature, false);
                    }
                }
                else
                {
                    MessageBox.Show("No Features to Select");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred.\n" + ex, "Sample error");
            }
        }

        /**********************************
         * show/hide loggerhead species 
         **********************************/
        private async void loggerheadTurtleCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            // Begin query process.
            await ShowLoggerheadTurtleSpecies();
        }

        private async Task ShowLoggerheadTurtleSpecies()
        {
            try
            {
                // Create a query parameters that will be used to Query the feature table.
                QueryParameters queryParams = new QueryParameters();

                // Construct and assign the where clause that will be used to query the feature table.
                queryParams.WhereClause = "Species = 'Loggerhead' or Species = 'Loggerhead Sea Turtle'";

                // Query the feature table.
                FeatureQueryResult queryResult = await _featureTable.QueryFeaturesAsync(queryParams);

                // Cast the QueryResult to a List so the results can be interrogated.
                List<Feature> features = queryResult.ToList();

                if (features.Any())
                {
                    // Loop over each feature from the query result.
                    foreach (Feature feature in features)
                    {
                        // Select each feature.
                        _featureLayer.SetFeatureVisible(feature, true);
                    }
                }
                else
                {
                    MessageBox.Show("No Features to Select");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred.\n" + ex, "Sample error");
            }
        }


        private async void loggerheadCheckBoxUnChecked(object sender, RoutedEventArgs e)
        {
            // Begin query process.
            await HideLoggerheadTurtleSpecies();
        }

        private async Task HideLoggerheadTurtleSpecies()
        {
            try
            {
                // Create a query parameters that will be used to Query the feature table.
                QueryParameters queryParams = new QueryParameters();

                // Construct and assign the where clause that will be used to query the feature table.
                queryParams.WhereClause = "Species = 'Loggerhead' or Species = 'Loggerhead Sea Turtle'";

                // Query the feature table.
                FeatureQueryResult queryResult = await _featureTable.QueryFeaturesAsync(queryParams);

                // Cast the QueryResult to a List so the results can be interrogated.
                List<Feature> features = queryResult.ToList();

                if (features.Any())
                {
                    // Loop over each feature from the query result.
                    foreach (Feature feature in features)
                    {
                        // Select each feature.
                        _featureLayer.SetFeatureVisible(feature, false);
                    }
                }
                else
                {
                    MessageBox.Show("No Features to Select");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred.\n" + ex, "Sample error");
            }
        }


        /**********************************
        * show/hide leatherback species 
        **********************************/
        private async void leatherbackTurtleCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            // Begin query process.
            await ShowLeatherbackTurtleSpecies();
        }

        private async Task ShowLeatherbackTurtleSpecies()
        {
            try
            {
                // Create a query parameters that will be used to Query the feature table.
                QueryParameters queryParams = new QueryParameters();

                // Construct and assign the where clause that will be used to query the feature table.
                queryParams.WhereClause = "Species = 'Leatherback' or Species = 'Leatherback Sea Turtle'";

                // Query the feature table.
                FeatureQueryResult queryResult = await _featureTable.QueryFeaturesAsync(queryParams);

                // Cast the QueryResult to a List so the results can be interrogated.
                List<Feature> features = queryResult.ToList();

                if (features.Any())
                {
                    // Loop over each feature from the query result.
                    foreach (Feature feature in features)
                    {
                        // Select each feature.
                        _featureLayer.SetFeatureVisible(feature, true);
                    }
                }
                else
                {
                    MessageBox.Show("No Features to Select");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred.\n" + ex, "Sample error");
            }
        }


        private async void leatherbackCheckBoxUnChecked(object sender, RoutedEventArgs e)
        {
            // Begin query process.
            await HideLeatherbackTurtleSpecies();
        }

        private async Task HideLeatherbackTurtleSpecies()
        {
            try
            {
                // Create a query parameters that will be used to Query the feature table.
                QueryParameters queryParams = new QueryParameters();

                // Construct and assign the where clause that will be used to query the feature table.
                queryParams.WhereClause = "Species = 'Leatherback' or Species = 'Leatherback Sea Turtle'";

                // Query the feature table.
                FeatureQueryResult queryResult = await _featureTable.QueryFeaturesAsync(queryParams);

                // Cast the QueryResult to a List so the results can be interrogated.
                List<Feature> features = queryResult.ToList();

                if (features.Any())
                {
                    // Loop over each feature from the query result.
                    foreach (Feature feature in features)
                    {
                        // Select each feature.
                        _featureLayer.SetFeatureVisible(feature, false);
                    }
                }
                else
                {
                    MessageBox.Show("No Features to Select");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred.\n" + ex, "Sample error");
            }
        }

        /**********************************
       * show/hide olive ridley species 
       **********************************/
        private async void oliveRidleyTurtleCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            // Begin query process.
            await ShowOliveRidleyTurtleSpecies();
        }

        private async Task ShowOliveRidleyTurtleSpecies()
        {
            try
            {
                // Create a query parameters that will be used to Query the feature table.
                QueryParameters queryParams = new QueryParameters();

                // Construct and assign the where clause that will be used to query the feature table.
                queryParams.WhereClause = "Species = 'Olive Ridley' or Species = 'Olive Ridley Sea Turtle'";

                // Query the feature table.
                FeatureQueryResult queryResult = await _featureTable.QueryFeaturesAsync(queryParams);

                // Cast the QueryResult to a List so the results can be interrogated.
                List<Feature> features = queryResult.ToList();

                if (features.Any())
                {
                    // Loop over each feature from the query result.
                    foreach (Feature feature in features)
                    {
                        // Select each feature.
                        _featureLayer.SetFeatureVisible(feature, true);
                    }
                }
                else
                {
                    MessageBox.Show("No Features to Select");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred.\n" + ex, "Sample error");
            }
        }


        private async void oliveRidleyCheckBoxUnChecked(object sender, RoutedEventArgs e)
        {
            // Begin query process.
            await HideOliveRidleyTurtleSpecies();
        }

        private async Task HideOliveRidleyTurtleSpecies()
        {
            try
            {
                // Create a query parameters that will be used to Query the feature table.
                QueryParameters queryParams = new QueryParameters();

                // Construct and assign the where clause that will be used to query the feature table.
                queryParams.WhereClause = "Species = 'Olive Ridley' or Species = 'Olive Ridley Sea Turtle'";

                // Query the feature table.
                FeatureQueryResult queryResult = await _featureTable.QueryFeaturesAsync(queryParams);

                // Cast the QueryResult to a List so the results can be interrogated.
                List<Feature> features = queryResult.ToList();

                if (features.Any())
                {
                    // Loop over each feature from the query result.
                    foreach (Feature feature in features)
                    {
                        // Select each feature.
                        _featureLayer.SetFeatureVisible(feature, false);
                    }
                }
                else
                {
                    MessageBox.Show("No Features to Select");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred.\n" + ex, "Sample error");
            }
        }

      /**********************************
       * show/hide unidentified ridley species 
       **********************************/
        private async void unidentifiedTurtleCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            // Begin query process.
            await ShowUnidentifiedTurtleSpecies();
        }

        private async Task ShowUnidentifiedTurtleSpecies()
        {
            try
            {
                // Create a query parameters that will be used to Query the feature table.
                QueryParameters queryParams = new QueryParameters();

                // Construct and assign the where clause that will be used to query the feature table.
                queryParams.WhereClause = "Species = 'Unidentified Sea Turtle' or Species = 'Sea Turtle' or Species = 'Unknown'";

                // Query the feature table.
                FeatureQueryResult queryResult = await _featureTable.QueryFeaturesAsync(queryParams);

                // Cast the QueryResult to a List so the results can be interrogated.
                List<Feature> features = queryResult.ToList();

                if (features.Any())
                {
                    // Loop over each feature from the query result.
                    foreach (Feature feature in features)
                    {
                        // Select each feature.
                        _featureLayer.SetFeatureVisible(feature, true);
                    }
                }
                else
                {
                    MessageBox.Show("No Features to Select");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred.\n" + ex, "Sample error");
            }
        }


        private async void unidentifiedCheckBoxUnChecked(object sender, RoutedEventArgs e)
        {
            // Begin query process.
            await HideUnidentifiedTurtleSpecies();
        }

        private async Task HideUnidentifiedTurtleSpecies()
        {
            try
            {
                // Create a query parameters that will be used to Query the feature table.
                QueryParameters queryParams = new QueryParameters();

                // Construct and assign the where clause that will be used to query the feature table.
                queryParams.WhereClause = "Species = 'Unidentified Sea Turtle' or Species = 'Sea Turtle' or Species = 'Unknown'";

                // Query the feature table.
                FeatureQueryResult queryResult = await _featureTable.QueryFeaturesAsync(queryParams);

                // Cast the QueryResult to a List so the results can be interrogated.
                List<Feature> features = queryResult.ToList();

                if (features.Any())
                {
                    // Loop over each feature from the query result.
                    foreach (Feature feature in features)
                    {
                        // Select each feature.
                        _featureLayer.SetFeatureVisible(feature, false);
                    }
                }
                else
                {
                    MessageBox.Show("No Features to Select");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred.\n" + ex, "Sample error");
            }
        }
    
}

}
    



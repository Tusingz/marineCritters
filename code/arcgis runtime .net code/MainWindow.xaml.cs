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
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.UI.Controls;
using System;
using System.Windows;
using System.Drawing;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // Create globally available feature table for easy referencing.
        private ServiceFeatureTable _featureTable;
        private ServiceFeatureTable _airFeatureTable;
        private ServiceFeatureTable _windFeatureTable;


        // Create globally available feature layer for easy referencing.
        private FeatureLayer _featureLayer;
        private FeatureLayer _airFeatureLayer;
        private FeatureLayer _windFeatureLayer;



        private ArcGISMapImageLayer _imageLayer;


        public MainWindow()
        {
            InitializeComponent();
            //get_Online_Map();
            get_feature_layer();
            Title = "Stranded Turtle Data";
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

        private async void get_feature_layer()
        {
            /***********************************************************************
             *   This function loads a feature layer from arcGIS online
             ************************************************************************/
            // Create new Map with basemap
            Map myMap = new Map(Basemap.CreateTopographic());


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
            _featureLayer = new FeatureLayer(_featureTable)
            {
                Opacity = 0.6
            };

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



            // add feature layers to map
            myMap.OperationalLayers.Add(_featureLayer);
            myMap.OperationalLayers.Add(_airFeatureLayer);
            myMap.OperationalLayers.Add(_windFeatureLayer);

            //DateTime start = new DateTime(1958, 9, 1);
            //DateTime end = new DateTime(2018, 10, 30);

            //MyMapView.TimeExtent = new TimeExtent(start, end);

            // add the map to the map view
            MyMapView.Map = myMap;

        }



    }
}


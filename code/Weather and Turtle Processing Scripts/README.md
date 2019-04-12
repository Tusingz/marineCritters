The folders called "airTemperatureProcessingScripts," "currentsInfoProcessingScripts,"
and "seaSurfaceTempProcessingScripts" contain three bash scripts each used to process
the netCDF's (.nc) files for weather data. The python scripts were made using python 3,
and they were intended for use in a Windows environment. "WindowsWeatherData.py" did the
heavy work for the weather data.  It took the raw data from ICOADS at NOAA in a .dat file
format and parsed and processed the data for our area of interest.  We took the data that weather
needed and saved it in our own standard format in a .csv. "condenseWeather.py" was used to take a running
average of the data.  The data contained too many entries.  This script reduced the entries
by 40%.  "weatherData_Nth_Condense.py" condensed the weather data even further by taking every
Nth entry.  The weather was now in a usable format.  "parts.py" was used to break bigger files
into smaller files that would be more manageable for software.  "data.py" was used to standardize
all of the turtle data into one format and take only necessary information.
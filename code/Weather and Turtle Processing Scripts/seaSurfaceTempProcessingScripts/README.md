These bash scripts were used to automate calling CDO functions to quickly
edit netCDFs for our project's needs.  "fixcoord.sh" changes the coordinates
system to a standard system. "bashful.sh" changes 360 degree coordinates to
180. "cropper.sh" crops the weather data for our area of interest.  This helped
to reduce file sizes so they could be merged and were more manageable. These 
scripts were used in Cygwin using the CDO library on Windows.
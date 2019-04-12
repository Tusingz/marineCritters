#!/usr/bin/python
import csv
import glob, os
import time
#Intended for Windows using Python 3 due to limited server storage space

#Script Creator: Daniel Domme
#Winter 2019
#Script Description:  This program opens and reads all files in the directory ending 
    #in .dat and filters and parses them.  Then, it takes the data that pertains to 
    #the bounding box of latitudes and longitudes and prints them in a .csv file.
    #This file was meant to parse NOAA International Comprehensive Ocean-Atmosphere Data Set (ICOADS)
    #enhanced trim files. FTP: https://www.ncei.noaa.gov/data/marine/icoads3.0/enhanced-trim/
    #Longitude needed to be reformated to become standardized with our datasets.
#Bounding Box: North: 61.152 South: 29.975 East: -123.221 West: -166.311
#Turned 747 files @ 205 gigs into one file at 1.58 gigs

with open ('try.csv', 'w', newline='') as csv_file:
    csv_writer = csv.writer(csv_file, delimiter=',', quotechar='"', quoting=csv.QUOTE_MINIMAL)
    csv_writer.writerow(['Date (DD-MM-YYYY)','Latitude','Longitude','Air Temp. (C)', 'Sea Surface Temp. (C)','Wind Direction (Degrees)','Wind Speed (m/s)'])
    os.chdir(r"C:\Users\Toastylegs\Desktop\weatherparse")
    counter = 0
    for file in glob.glob("*.dat"):
        counter +=1
        print(file)
        print("Working on file: ", counter)
        print(len(glob.glob("*.dat")))
        with open (file) as input_file: 
            for line in input_file:               
                #Latitude
                lat = line[12:17].strip()
                if len(lat) == 1:
                    lat = float('0.0' + lat)                    
                elif len(lat) == 2:                
                    if lat[0] == '-':
                        lat = float(lat[0] + '0.0' + lat[1])                        
                    else:                        
                        lat = float('0.' + lat)                       
                elif len(lat) == 3:
                    if lat[0] == '-':                        
                        lat = float(lat[0] + '0.' + lat[1:])                        
                    else:
                        
                        lat = float(lat[0] + '.' + lat[1:])
                else:                    
                    lat = float(lat[:-2] + '.' + lat[-2:])                    
                
                #Longitude
                longi = line[18:24].strip()
                if len(longi)==1: 
                    longi = float('0.0' + longi)                    
                elif len(longi)==2:
                    longi = float('0.' + longi)
                else:
                    longi = float(longi[:-2] + '.' + longi[-2:])
                   
                if longi > 180:
                    longi = float("{0:.2f}".format(longi - 360))                

                if lat < 61.15 and lat > 29.98 and longi < -123.22 and longi > -166.31: 
                    #Date
                    year = line[0:4]
                    month = line[4:6].strip()
                    day = line[6:8].strip()                
                    if len(month) == 1 :
                        month = '0' + month
                    datum = day + '-' + month + '-' + line[0:4]
                    if len(day) == 1:
                        day = '0' + day
                    #Wind Details
                    windDir = line[46:49].strip()
                    windSpd = line[50:53].strip()
                    if len(windSpd) == 1:
                        windSpd = float('0.' + windSpd)
                    elif len(windSpd) > 1:
                        windSpd = float(windSpd[:-1] + '.' + windSpd[-1])
                    #Air Temperature
                    airTemp = line[69:73].strip()
                    if len(airTemp) == 1:
                        airTemp = float('0.' + airTemp)                    
                    elif len(airTemp) == 2:
                        if airTemp[0] == '-':
                            airTemp = float(airTemp[0] + '0.' + airTemp[1])
                        else:
                            airTemp = float(airTemp[0] + '.' + airTemp[1])
                    elif len(airTemp) > 2:
                        airTemp = float(airTemp[:-1] + '.' + airTemp[-1])                        
                    #Sea Surface Temperature
                    surfaceTemp = line[85:89].strip()
                    if len(surfaceTemp) == 1:
                        surfaceTemp = float('0.' + surfaceTemp)
                    elif len(surfaceTemp) == 2:
                        if surfaceTemp[0] == '-':
                            surfaceTemp = float(surfaceTemp[0] + '0.' + surfaceTemp[1])
                        else:
                            surfaceTemp = float(surfaceTemp[0] + '.' +surfaceTemp[1])
                    elif len(surfaceTemp) > 2:
                        surfaceTemp = float(surfaceTemp[:-1] + '.' + surfaceTemp[-1])
                    #finished
                    #print("Date: ", datum," Latitude: ", lat, " Longitude: ", longi, " Air Temperature: ", 
                    #airTemp, " Sea Surface Temperature: ", surfaceTemp," Wind Direction: ", windDir, " Wind Speed: ", windSpd)
                    csv_writer.writerow([datum, lat, longi, airTemp, surfaceTemp, windDir, windSpd])
    print("Number of Files Processed: ", counter)

                    

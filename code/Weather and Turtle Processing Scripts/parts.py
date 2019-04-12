#!/usr/bin/python
#Intended for windows using Python 3
#Script Creator: Daniel Domme
#Winter 2019
#Script Description:  This program is meant to break a larger Weather Data
    #.csv file into a file that is more manageable for programs to open and access,
    #especially 32-bit systems.  Excel has a line size limitation, and ArcGIS would 
    #crash with large file sizes.  The larger file is broken into parts defined as needed.
    #Files sizes of both 800,000 and 750,000 lines were created out of much larger files
    #with millions of lines of historical data.  The input is a weather data .csv, and the 
    #output is the same data broken up into multiple .csv parts.  Each file has the header data.


import csv

with open('BMC_WeatherData_1957_2018_Avg_10th.csv', 'r') as csv_file:
        csv_reader = csv.reader(csv_file, delimiter=',')
        line_count = 0
        file_count = 0
        for row in csv_reader:
            if line_count == 0:
                if file_count == 0:
                    heads = row
                print("file #: ", file_count)
                csv_file1 = open('BMC_WeatherData_1957_2018_Avg_10th_Part' + str(file_count) + '.csv', 'w',newline='')
                csv_writer = csv.writer(csv_file1, delimiter=',', quotechar='"', quoting=csv.QUOTE_MINIMAL)
                csv_writer.writerow([heads[0],heads[1],heads[2],heads[3],heads[4],heads[5],heads[6]])
                line_count += 1   
            else:
                csv_writer.writerow([row[0],row[1],row[2],row[3],row[4],row[5],row[6]])
                line_count += 1
            
                if line_count == 750000:
                    line_count = 0
                    file_count += 1
print("DOne")
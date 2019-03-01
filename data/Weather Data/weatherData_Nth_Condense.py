#!/usr/bin/python
#Intended to be used using Linux-based OS with Python 3
#If using on Windows, the .csv output will have newlines after each entry
import csv
import time

#Script Creator: Daniel Domme
#Date: 29 February 2019
#Script Description: This program takes one .csv file containing archival weather data.
    #Each line of the original file is gone through and every nth line is printed in
    #the output .csv file.  The header is first printed.  This allows for smaller data files.
    #Originally, a single day could contain as many as 15,000 data entries.  This file 
    #reduces the amount of data by 90%.  The line number that has been iterated on 
    #in the original file is printed to console for the user.  This file was made for
    #a linux environment because the engr servers were much faster than my Microsoft Surface.

with open('BMC_WeatherData_1957_2018_Avg.csv', 'r') as csv_file:
    csv_reader = csv.reader(csv_file, delimiter=',')
    csv_file1 = open('BMC_WeatherData_1957_2018_Avg_10th.csv', 'w')
    csv_writer = csv.writer(csv_file1, delimiter=',', quotechar='"', quoting=csv.QUOTE_MINIMAL)
    line_count = 0
    counter = 0
    for row in csv_reader:
        counter += 1
        if line_count == 0:
            heads = row                
            csv_writer.writerow([heads[0],heads[1],heads[2],heads[3],heads[4],heads[5],heads[6]])
        else:
            #if counter == 5:
            #if counter == 8:
            if counter == 10:
                csv_writer.writerow([row[0],row[1],row[2],row[3],row[4],row[5],row[6]])
                counter = 0
                #print("Written: ", row, "Line: ", line_count)
        #time.sleep(1.5)
        print("Line: ", line_count)
        line_count += 1
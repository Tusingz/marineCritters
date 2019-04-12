#!/usr/bin/python
#Intended for use on Windows using Python 3
import csv
import glob, os
import time

#Script Creator: Daniel Domme
#Date: 26 February 2019
#Script Description: This program is mean to take a file of archival weather data
    #and average the data point's information occurring on the same days if they are three degrees
    #within eachother in latitude and longitude.  If the data isn't near another point,
    #it is just printed to the new .csv file.  THe intent was to maintain information
    #for areas of the sea while reducing the amount of data stored and needing to be
    #loaded by software.  This software helped to reduce our original data of 54 million lines
    #to 32 million lines.  This was a 40% reduction in data.  Having the averages helped to 
    #preserve data integrity.  Nearby data points to an origin data point are kept in a running
    #total and then averaged if there aren't any more nearby points.  Then it is printed.
    #The run_add() function keeps a running total, and the avg_data() function averages.

def run_add(row1,row2):
    lat = float("{0:.2f}".format((float(row1[1]) + float(row2[1]))))
    longi = float("{0:.2f}".format((float(row1[2]) + float(row2[2]))))
    if row1[3] == '':
        airtemp = row2[3]
    elif row2[3] == '':
        airtemp = row1[3]
    else:
        airtemp = float("{0:.2f}".format((float(row1[3]) + float(row2[3]))))
    if row1[4] == '':
        sst = row2[4]
    elif row2[4] == '':
        sst = row1[4]
    else:
        sst = float("{0:.2f}".format((float(row1[4]) + float(row2[4]))))
    if row1[5] == '':
        winddir = row2[5]
    elif row2[5] == '':
        winddir = row1[5]
    else:
        winddir = float("{0:.2f}".format((float(row1[5]) + float(row2[5]))))
    if row1[6] == '':
        windspd = row2[6]
    elif row2[6] == '':
        windspd = row1[6]
    else:
        windspd = float("{0:.2f}".format((float(row1[6]) + float(row2[6]))))
    row1[1]= str(lat)
    row1[2]= str(longi)
    row1[3]= str(airtemp)
    row1[4]= str(sst)
    row1[5]= str(winddir)
    row1[6]= str(windspd)
    #print("Returning: ", row1)
    return row1

def avg_data(row1,numArgs):
    lat = float("{0:.2f}".format(float(row1[1]) /numArgs))
    longi = float("{0:.2f}".format(float(row1[2]) /numArgs))
    if row1[3] == '':
        airtemp = row1[3]
    else:
        airtemp = float("{0:.2f}".format(float(row1[3]) /numArgs))
    if row1[4] == '':
        sst = row1[4]
    else:
        sst = float("{0:.2f}".format(float(row1[4]) /numArgs))
    if row1[5] == '':
        winddir = row1[5]
    else:
        winddir = float("{0:.2f}".format(float(row1[5]) /numArgs))
    if row1[6] == '':
        windspd = row1[6]
    else:
        windspd = float("{0:.2f}".format(float(row1[6]) /numArgs))
    row1[1]= str(lat)
    row1[2]= str(longi)
    row1[3]= str(airtemp)
    row1[4]= str(sst)
    row1[5]= str(winddir)
    row1[6]= str(windspd)
    return row1
  
with open('BMC_WeatherData_1957_2018.csv', 'r') as csv_file:
        csv_reader = csv.reader(csv_file, delimiter=',')
        csv_file1 = open('BMC_WeatherData_1957_2018_Avg.csv', 'w',newline='')
        csv_writer = csv.writer(csv_file1, delimiter=',', quotechar='"', quoting=csv.QUOTE_MINIMAL)
        line_count = 0
        avgNum = 1
        prevRows = None
        procRows = []
        for row in csv_reader:            
            #print("new row to process: ", row)
            if line_count == 0:
                heads = row                
                csv_writer.writerow([heads[0],heads[1],heads[2],heads[3],heads[4],heads[5],heads[6]])
                print(heads)               
            elif line_count == 1:
                prevRows = row
                for val in row:
                    procRows.append(val)
                #print("length: ",prevRows, procRows)
                #print("zero: ",row)
            elif ((abs(float(prevRows[1]) - float(row[1]))<= 3) and (abs(float(prevRows[2])-float(row[2])) <= 3) and (prevRows[0] == row[0])):
                procRows = run_add(procRows, row)
                #print("composition: ", prevRows, procRows)
                avgNum += 1
                #print("Average: ", avgNum)
                #print("Line proc: ", line_count)
            else:
                prevRows = avg_data(procRows,avgNum)
                #print("average: ", prevRows)
                csv_writer.writerow([prevRows[0],prevRows[1],prevRows[2],prevRows[3],prevRows[4],prevRows[5],prevRows[6]])
                prevRows = row
                procRows.clear()
                for val in row:                
                    procRows.append(val)
                avgNum = 1
                print("printed",line_count)
            line_count += 1        
            #time.sleep(2.5)
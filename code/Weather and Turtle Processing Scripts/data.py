#!/usr/bin/python
#Made by: Daniel Domme for Beached Marine Critters Project
#uses: Python 3 on windows
#Parses data from .csv
import csv

with open('Clean_BMC_DB_ORTurt.csv', 'w') as csv_file:
    csv_writer = csv.writer(csv_file, delimiter=',', quotechar='"', quoting=csv.QUOTE_MINIMAL)
    line_count2 = 0
    with open('new_BMC_database_reordering.csv', 'r') as csv_file:
        csv_reader = csv.reader(csv_file, delimiter=',')
        line_count = 0
        for row in csv_reader:
            if line_count == 0:
                print('Column names are ',", ".join(row))
                print('Count: ', len(row))
                csv_writer.writerow([row[0],row[1],row[5],row[6],row[8],row[9],row[10],row[13],row[14],row[15],row[16],row[17],row[18],row[19],row[22],row[23],row[24]])
                line_count += 1
                line_count2 += 1
            else:
                #print('\tDate: ',row[0],', Species:',row[5],', Area: ',row[8],', State?:',row[9],', Lat:',row[11],', Long:',row[12],'.')
                if row[8].strip() == "":
                    row[8] = 'No Data'
                if row[14].strip() == "":
                    row[14] = 0
                if row[13].strip() == "":
                    row[13] = 0
                if row[15].strip() == "":
                    row[15] = 'No Data'
                if row[16].strip() == "":
                    row[16] = 0
                if row[17].strip() == "":
                    row[17] = 0
                if row[18].strip() == "":
                    row[18] = 0
                if row[19].strip() == "":
                    row[19] = 'No Data'
                if row[20].strip() == "":
                    row[20] = 'No Data'
                if row[21].strip() == "":
                    row[21] = 'No Data'
                if row[22].strip() == "":
                    row[22] = 'No Data'
                if row[10] != "California" and row[5] == 'Turtle' and row[13] != '0':
                    csv_writer.writerow([row[0],row[1],row[5],row[6],row[8],row[9],row[10],row[13],row[14],row[15],row[16],row[17],row[18],row[19],row[22],row[23],row[24]])
                line_count += 1
                line_count2 += 1
        print('Processed reading ',line_count,' lines and ',line_count2,' lines.')

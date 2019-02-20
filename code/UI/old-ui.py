import sys
from PyQt5.QtWidgets import *
from PyQt5.QtGui import *
from PyQt5.QtCore import *

"""
# Name:         fileCLicked()
# Description:  When the file button is clicked, ...
# Creator:
"""
def fileClicked(self):
    print('You clicked on file')

"""
# Name:         importClicked()
# Description:  When the import button is clicked, ...
# Creator:
"""
def importClicked(self):
    print('You clicked on import')

"""
# Name:         editClicked()
# Description:  When the edit button is clicked, ...
# Creator:
"""
def editClicked(self):
    print('You clicked on edit')

"""
# Name:         viewClicked()
# Description:  When the view button is clicked, ...
# Creator:
"""
def viewClicked(self):
    print('You clicked on view')

"""
# Name:         propertiesClicked()
# Description:  When the properties button is clicked, ...
# Creator:
"""
def propertiesClicked(self):
    print('You clicked on properties')

"""
# Name:         seaTutlesClicked()
# Description:  When the Sea Tutles option is selected, ...
# Creator:
"""
def seaTutlesClicked(self):
    print('you clicked on Sea Tutles')

"""
# Name:         salmonSharkClicked()
# Description:  When the Salmon Shark option is selected, ...
# Creator:
"""
def salmonSharkClicked(self):
    print('you clicked on Salmon Shark')

"""
# Name:         currentsClicked()
# Description:  When the Sea Currents option is selected, ...
# Creator:
"""
def currentsClicked(self):
    print('you clicked on Sea Currents')

"""
# Name:         tempClicked()
# Description:  When the Sea Temperature option is selected, ...
# Creator:
"""
def tempClicked(self):
    print('you clicked on Sea Surface Temperature')

"""
# Name:         windClicked()
# Description:  When the Wind Speed option is selected, ...
# Creator:
"""
def windClicked(self):
    print('you clicked on Wind Speed')

"""
# Name:         createButton()
# Description:  When this function is called, it will create a button, "name", and assign
#               it to a VBoxLayout or HBoxLayout, "layout". When the button is clicked, it will
#               call the function "funcPointer"
# Creator(s):   Justin
"""
def createButton(name, funcPointer, layout):
    button = QPushButton(name)
    layout.addWidget(button)
    button.clicked.connect(funcPointer)
    #button.setStyleSheet("background-color: rgb(0, 112, 210);")

"""
# Name:         createCheckBox()
# Description:  When this function is called, it will create a checkbox with
#               the name "name" and will be added to the grid layout "layout" at
#               possition (x, y). When clicked, it will call funcPointer.
# Creator(s):   Justin
"""
def createCheckBox(name, funcPointer, layout, x, y):
    checkBox = QCheckBox(name)
    layout.addWidget(checkBox, x, y)
    checkBox.clicked.connect(funcPointer)
"""
# Name:         window()
# Description:  The main function that creates the window and everthing in it.
# Creator(s):   Zach and Justin
"""
def window():
    app = QApplication(sys.argv)
    window = QWidget()
    lbl = QLabel(window)

    mainLayout = QGridLayout()
    window.setLayout(mainLayout)

    toolBar = QHBoxLayout()
    createButton("File", fileClicked, toolBar)
    createButton("Import", importClicked, toolBar)
    createButton("Edit", editClicked, toolBar)
    createButton("View", viewClicked, toolBar)
    createButton("Properties", propertiesClicked, toolBar)
    mainLayout.addLayout(toolBar, 0, 0)

    mapFilters = QGridLayout()
    mapFiltersLbl = QLabel("Map Filters")
    mapFilters.addWidget(mapFiltersLbl, 0, 0)
    createCheckBox("Sea Turtles", seaTutlesClicked, mapFilters, 1, 0)
    createCheckBox("Salmon Shark", salmonSharkClicked, mapFilters, 2, 0)
    fromLbl = QLabel("From")
    toLbl = QLabel("To")
    fromDate = QDateEdit()
    toDate = QDateEdit()
    mapFilters.addWidget(fromLbl, 1, 1)
    mapFilters.addWidget(toLbl, 2, 1)
    mapFilters.addWidget(fromDate, 1, 2)
    mapFilters.addWidget(toDate, 2, 2)
    createCheckBox("Sea Currents", currentsClicked, mapFilters, 1, 3)
    createCheckBox("Surface Temperature", tempClicked, mapFilters, 2, 3)
    createCheckBox("Wind Speed", windClicked, mapFilters, 3, 3)
    mainLayout.addLayout(mapFilters, 1, 0)

    dataSort = QVBoxLayout()
    dataSortLbl = QLabel("Data Sort")
    dataSortOptions = QComboBox()
    dataSortOptions.addItem("Species")
    dataSortOptions.addItem("Date")
    dataSortOptions.addItem("Location")
    dataSortOptions.addItem("Age")
    dataSort.addWidget(dataSortLbl)
    dataSort.addWidget(dataSortOptions)
    mainLayout.addLayout(dataSort, 1, 1)

    map = QVBoxLayout()
    mapLbl = QLabel("Map")
    map.addWidget(mapLbl)
    #add the map that is changing with different filters
    mainLayout.addLayout(map, 2, 0)

    mapData = QVBoxLayout()
    mapDataLbl = QLabel("Map Data")
    mapData.addWidget(mapDataLbl)
    #dynamicly create buttons with data for the map
    mainLayout.addLayout(mapData, 2, 1)

    mainLayout.setRowStretch(0, 1)
    mainLayout.setRowStretch(1, 2)
    mainLayout.setRowStretch(2, 2)
    mainLayout.setColumnStretch(3, 0)
    mainLayout.setColumnStretch(3, 1)

    window.setGeometry(20, 50, 1250, 800)
    window.setWindowTitle("Beached Marine Critters")
    window.setWindowIcon(QIcon('sea-turtle-icon.png'))
    p = window.palette()
    p.setColor(window.backgroundRole(), QColor(134, 199, 255))
    window.setPalette(p)
    window.show()
    sys.exit(app.exec_())

def main():
    window()

main()

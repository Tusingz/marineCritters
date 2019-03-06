# -*- coding: utf-8 -*-

# Form implementation generated from reading ui file '.\bmc.ui'
#
# Created by: PyQt5 UI code generator 5.11.3
#
# WARNING! All changes made in this file will be lost!

from PyQt5 import QtCore, QtGui, QtWidgets

class Ui_MainWindow(object):

    def setupUi(self, MainWindow):
        MainWindow.setObjectName("MainWindow")
        MainWindow.resize(1009, 742)
        self.centralwidget = QtWidgets.QWidget(MainWindow)
        self.centralwidget.setObjectName("centralwidget")
        self.gridLayout = QtWidgets.QGridLayout(self.centralwidget)
        self.gridLayout.setObjectName("gridLayout")
        spacerItem = QtWidgets.QSpacerItem(20, 40, QtWidgets.QSizePolicy.Minimum, QtWidgets.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem, 7, 0, 1, 1)
        spacerItem1 = QtWidgets.QSpacerItem(40, 20, QtWidgets.QSizePolicy.Minimum, QtWidgets.QSizePolicy.Minimum)
        self.gridLayout.addItem(spacerItem1, 3, 3, 1, 1)
        self.dataComboBox = QtWidgets.QComboBox(self.centralwidget)
        self.dataComboBox.setObjectName("dataComboBox")
        self.dataComboBox.addItem("")
        self.dataComboBox.addItem("")
        self.dataComboBox.addItem("")
        self.dataComboBox.addItem("")
        self.gridLayout.addWidget(self.dataComboBox, 3, 2, 1, 1)
        self.dataLabel = QtWidgets.QLabel(self.centralwidget)
        self.dataLabel.setObjectName("dataLabel")
        self.gridLayout.addWidget(self.dataLabel, 3, 1, 1, 1)
        self.mapFilterLabel = QtWidgets.QLabel(self.centralwidget)
        self.mapFilterLabel.setObjectName("mapFilterLabel")
        self.gridLayout.addWidget(self.mapFilterLabel, 0, 0, 1, 1)
        self.mapGraphic = QtWidgets.QGraphicsView(self.centralwidget)
        self.mapGraphic.setMinimumSize(QtCore.QSize(467, 237))
        self.mapGraphic.setObjectName("mapGraphic")
        self.gridLayout.addWidget(self.mapGraphic, 5, 0, 1, 1)
        self.mapLabel = QtWidgets.QLabel(self.centralwidget)
        self.mapLabel.setObjectName("mapLabel")
        self.gridLayout.addWidget(self.mapLabel, 3, 0, 1, 1)
        self.verticalLayout_4 = QtWidgets.QVBoxLayout()
        self.verticalLayout_4.setObjectName("verticalLayout_4")
        self.gridLayout.addLayout(self.verticalLayout_4, 6, 0, 1, 1)
        self.mapFilterLayout = QtWidgets.QHBoxLayout()
        self.mapFilterLayout.setObjectName("mapFilterLayout")
        self.turtleLayout = QtWidgets.QVBoxLayout()
        self.turtleLayout.setObjectName("turtleLayout")
        self.leatherbackCheck = QtWidgets.QCheckBox(self.centralwidget)
        self.leatherbackCheck.setObjectName("leatherbackCheck")
        self.turtleLayout.addWidget(self.leatherbackCheck)
        self.greenSeaCheck = QtWidgets.QCheckBox(self.centralwidget)
        self.greenSeaCheck.setObjectName("greenSeaCheck")
        self.turtleLayout.addWidget(self.greenSeaCheck)
        self.loggerheadCheck = QtWidgets.QCheckBox(self.centralwidget)
        self.loggerheadCheck.setObjectName("loggerheadCheck")
        self.turtleLayout.addWidget(self.loggerheadCheck)
        self.oliveRidleyCheck = QtWidgets.QCheckBox(self.centralwidget)
        self.oliveRidleyCheck.setObjectName("oliveRidleyCheck")
        self.turtleLayout.addWidget(self.oliveRidleyCheck)
        self.unknownCheck = QtWidgets.QCheckBox(self.centralwidget)
        self.unknownCheck.setObjectName("unknownCheck")
        self.turtleLayout.addWidget(self.unknownCheck)
        self.mapFilterLayout.addLayout(self.turtleLayout)

        spacerItem2 = QtWidgets.QSpacerItem(40, 20, QtWidgets.QSizePolicy.Minimum, QtWidgets.QSizePolicy.Minimum)

        self.mapFilterLayout.addItem(spacerItem2)
        self.dateLayout = QtWidgets.QGridLayout()
        self.dateLayout.setObjectName("dateLayout")
        self.toDate = QtWidgets.QDateEdit(self.centralwidget)
        self.toDate.setObjectName("toDate")
        self.dateLayout.addWidget(self.toDate, 1, 2, 1, 1)
        self.fromDate = QtWidgets.QDateEdit(self.centralwidget)
        self.fromDate.setObjectName("fromDate")
        self.dateLayout.addWidget(self.fromDate, 0, 2, 1, 1)
        self.fromLabel = QtWidgets.QLabel(self.centralwidget)
        self.fromLabel.setObjectName("fromLabel")
        self.dateLayout.addWidget(self.fromLabel, 0, 1, 1, 1)
        self.toLabel = QtWidgets.QLabel(self.centralwidget)
        self.toLabel.setObjectName("toLabel")
        self.dateLayout.addWidget(self.toLabel, 1, 1, 1, 1)
        self.mapFilterLayout.addLayout(self.dateLayout)

        spacerItem3 = QtWidgets.QSpacerItem(40, 20, QtWidgets.QSizePolicy.Minimum, QtWidgets.QSizePolicy.Minimum)

        self.mapFilterLayout.addItem(spacerItem3)
        self.conditionLayout = QtWidgets.QVBoxLayout()
        self.conditionLayout.setObjectName("conditionLayout")
        self.seaCurrentsCheck = QtWidgets.QCheckBox(self.centralwidget)
        self.seaCurrentsCheck.setObjectName("seaCurrentsCheck")
        self.conditionLayout.addWidget(self.seaCurrentsCheck)
        self.surfaceTempCheck = QtWidgets.QCheckBox(self.centralwidget)
        self.surfaceTempCheck.setObjectName("surfaceTempCheck")
        self.conditionLayout.addWidget(self.surfaceTempCheck)
        self.windSpeedCheck = QtWidgets.QCheckBox(self.centralwidget)
        self.windSpeedCheck.setObjectName("windSpeedCheck")
        self.conditionLayout.addWidget(self.windSpeedCheck)
        self.mapFilterLayout.addLayout(self.conditionLayout)

        spacerItem4 = QtWidgets.QSpacerItem(40, 20, QtWidgets.QSizePolicy.Expanding, QtWidgets.QSizePolicy.Minimum)

        self.mapFilterLayout.addItem(spacerItem4)
        self.gridLayout.addLayout(self.mapFilterLayout, 2, 0, 1, 1)
        self.dataLayout = QtWidgets.QVBoxLayout()
        self.dataLayout.setObjectName("dataLayout")
        self.exampleDataPushButton = QtWidgets.QPushButton(self.centralwidget)
        self.exampleDataPushButton.setObjectName("exampleDataPushButton")
        self.dataLayout.addWidget(self.exampleDataPushButton)

        spacerItem5 = QtWidgets.QSpacerItem(20, 40, QtWidgets.QSizePolicy.Minimum, QtWidgets.QSizePolicy.Expanding)

        self.dataLayout.addItem(spacerItem5)
        self.gridLayout.addLayout(self.dataLayout, 5, 1, 1, 2)

        MainWindow.setCentralWidget(self.centralwidget)

        self.menubar = QtWidgets.QMenuBar(MainWindow)
        self.menubar.setGeometry(QtCore.QRect(0, 0, 1009, 26))
        self.menubar.setObjectName("menubar")

        self.menuFile = QtWidgets.QMenu(self.menubar)
        self.menuFile.setObjectName("menuFile")

        self.menuImport = QtWidgets.QMenu(self.menubar)
        self.menuImport.setEnabled(True)
        self.menuImport.setObjectName("menuImport")

        self.menuEdit = QtWidgets.QMenu(self.menubar)
        self.menuEdit.setObjectName("menuEdit")

        self.menuView = QtWidgets.QMenu(self.menubar)
        self.menuView.setObjectName("menuView")

        self.menuProperties = QtWidgets.QMenu(self.menubar)
        self.menuProperties.setObjectName("menuProperties")

        MainWindow.setMenuBar(self.menubar)

        self.statusbar = QtWidgets.QStatusBar(MainWindow)
        self.statusbar.setObjectName("statusbar")
        MainWindow.setStatusBar(self.statusbar)

        self.actionOpen = QtWidgets.QAction(MainWindow)
        self.actionOpen.setObjectName("actionOpen")

        self.actionSave = QtWidgets.QAction(MainWindow)
        self.actionSave.setObjectName("actionSave")

        self.actionExit = QtWidgets.QAction(MainWindow)
        self.actionExit.setObjectName("actionExit")
        self.actionExit.triggered.connect(app.quit)

        self.menuFile.addAction(self.actionOpen)

        self.menuFile.addAction(self.actionSave)

        self.menuFile.addSeparator()
        self.menuFile.addAction(self.actionExit)
        #actionExit.triggered.connect(app.quit)

        self.menubar.addAction(self.menuFile.menuAction())
        self.menubar.addAction(self.menuImport.menuAction())
        self.menubar.addAction(self.menuEdit.menuAction())
        self.menubar.addAction(self.menuView.menuAction())
        self.menubar.addAction(self.menuProperties.menuAction())

        self.retranslateUi(MainWindow)
        QtCore.QMetaObject.connectSlotsByName(MainWindow)

    def retranslateUi(self, MainWindow):
        _translate = QtCore.QCoreApplication.translate
        MainWindow.setWindowTitle(_translate("MainWindow", "MainWindow"))
        self.dataComboBox.setItemText(0, _translate("MainWindow", "Species"))
        self.dataComboBox.setItemText(1, _translate("MainWindow", "Date"))
        self.dataComboBox.setItemText(2, _translate("MainWindow", "Location"))
        self.dataComboBox.setItemText(3, _translate("MainWindow", "Age"))
        self.dataLabel.setText(_translate("MainWindow", "Data"))
        self.mapFilterLabel.setText(_translate("MainWindow", "Map Filters"))
        self.mapLabel.setText(_translate("MainWindow", "Map"))
        self.leatherbackCheck.setText(_translate("MainWindow", "Leatherback"))
        self.greenSeaCheck.setText(_translate("MainWindow", "Green Sea"))
        self.loggerheadCheck.setText(_translate("MainWindow", "Loggerhead"))
        self.oliveRidleyCheck.setText(_translate("MainWindow", "Olive Ridley"))
        self.unknownCheck.setText(_translate("MainWindow", "Unknown"))
        self.fromLabel.setText(_translate("MainWindow", "From"))
        self.toLabel.setText(_translate("MainWindow", "To"))
        self.seaCurrentsCheck.setText(_translate("MainWindow", "Sea Currents"))
        self.surfaceTempCheck.setText(_translate("MainWindow", "Surface Temperature"))
        self.windSpeedCheck.setText(_translate("MainWindow", "Wind Speed"))
        self.exampleDataPushButton.setText(_translate("MainWindow", "Sea Turtle - New Port"))
        self.menuFile.setTitle(_translate("MainWindow", "File"))
        self.menuImport.setTitle(_translate("MainWindow", "Import"))
        self.menuEdit.setTitle(_translate("MainWindow", "Edit"))
        self.menuView.setTitle(_translate("MainWindow", "View"))
        self.menuProperties.setTitle(_translate("MainWindow", "Properties"))
        self.actionOpen.setText(_translate("MainWindow", "Open"))
        self.actionSave.setText(_translate("MainWindow", "Save"))
        self.actionExit.setText(_translate("MainWindow", "Exit"))


if __name__ == "__main__":
    import sys
    app = QtWidgets.QApplication(sys.argv)
    MainWindow = QtWidgets.QMainWindow()
    ui = Ui_MainWindow()
    ui.setupUi(MainWindow)
    MainWindow.setWindowTitle('Beached Marrine Critters')
    MainWindow.setWindowIcon(QtGui.QIcon('sea-turtle-icon.png'))
    MainWindow.show()
    sys.exit(app.exec_())

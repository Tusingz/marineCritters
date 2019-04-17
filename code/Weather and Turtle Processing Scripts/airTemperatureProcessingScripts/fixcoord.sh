#!/bin/bash
#must have cdo installed
#Made by: Daniel Domme for Beached Marine Critters Project
date=1992
for file in oscar_vel{1992..2019}.nc.gz.nc
    do
    echo $date
    echo $file
    #cdo sellonlatbox,-180,180,-90,90 ${file} sst.${date}.180.nc
    cdo sellonlatbox,20,360,-90,90 ${file} currents.fixedcoord.${date}.nc

    ((date++))
done

#cdo commands
#cdo sellonlatbox,-167,-122,30,62 sst.day.mean.1983.nc 1983croppedtry.nc
#cdo sellonlatbox,-180,180,-90,90 tranformed1992currents.nc tryagain.nc
#cdo sellonlatbox,20,360,-90,90 oscar_vel1992.nc.gz.nc tranformed1992currents.nc
#cdo sellonlatbox,360,380,-90,90 oscar_vel1992.nc.gz.nc try2.nc  
#cdo sellonlatbox,-167,-122,30,62 ${file} sst.${date}.Cropped.nc
#cdo mergetime *.nc sst.mean.1981_2019.cropped.180.nc
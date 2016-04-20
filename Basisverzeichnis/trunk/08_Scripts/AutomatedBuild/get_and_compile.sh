rm -rf workingdir/
mkdir workingdir

wget http://github.com/SPoelloth/SWP16/zipball/master/ -O workingdir/latest.zip
cd workingdir
unzip latest.zip
cd "`ls | grep SPoelloth`"
cd Basisverzeichnis/trunk/03_Implementierung/NetworkSimulatorAnalyzer/
xbuild NetworkSimulatorAnalyzer.sln
cd NetworkSimulatorAnalyzer/BuildOutput/
mono NetworkSimulatorAnalyzer.exe
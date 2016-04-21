# alles das für den build für mono gebraucht wird installieren
sudo apt-get update
sudo apt-get -y install build-essential autoconf automake curl binutils libtool libglib2.0-dev libxrender-dev libfontconfig1-dev libpng12-dev libgif-dev libjpeg8-dev libtiff5-dev libexif-dev gettext libcairo2-dev libgdiplus

# ordner anlegen, unsere zielversion runterladen und entpacken
# unsere zielversion ist 4.4.0.122
# andere versionen gibt es hier: http://download.mono-project.com/sources/mono/

mkdir sources
cd sources
wget http://download.mono-project.com/sources/mono/mono-4.4.0.122.tar.bz2
tar -jxvf mono-4.4.0.122.tar.bz2

# mono compilieren
cd mono-4.4.0
./configure --prefix=/usr/local/
sudo make -j8

#mono installieren
sudo make install
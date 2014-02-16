MapServer MapManager
====================

Introduction
------------

MapServer MapManager is an easy to use WYSWYG desktop GUI application that allows you to create and configure good looking MapServer map files. The application provides a set of dialogues for setting the various properties in the map configuration and renders the map directly on the screen allowing panning and zooming the displayed area by using the mouse. The MapManager user interface provides common operations for selecting the features and inspecting the attributes for setting up labeling and changing the style and symbology and to save the map images in various output formats supported by MapServer. The created map configuration can be saved into a map file which can be used by various MapServer CGI/MapScript applications.

Licensing
---------

MapServer MapManager is provided with GPLv3 (see MapManagerLicense.rtf for the details).

Downloads
---------

The following download locations can be used to download the MapManager installer and the corresponding GDAL plugin installers which are not included in the installer of MapManager due to their custom license terms.
When installing the plugins you should select the MapManager install location as the target directory.

Installers based on the current MapServer release (6.4.1)  
[MapServer MapManager](http://www.gisinternals.com/sdk/downloads/release-1600-gdal-1-10-1-mapserver-6-4-1/MapManager.msi)  
[GDAL ECW driver](http://www.gisinternals.com/sdk/downloads/release-1600-gdal-1-10-1-mapserver-6-4-1/gdal-110-1600-ecw.msi)
[GDAL FileGDB driver](http://www.gisinternals.com/sdk/downloads/release-1600-gdal-1-10-1-mapserver-6-4-1/gdal-110-1600-filegdb.msi)  
[GDAL MrSID driver](http://www.gisinternals.com/sdk/downloads/release-1600-gdal-1-10-1-mapserver-6-4-1/gdal-110-1600-mrsid.msi)  
[GDAL Oracle driver](http://www.gisinternals.com/sdk/downloads/release-1600-gdal-1-10-1-mapserver-6-4-1/gdal-110-1600-oracle.msi)

Installers based on the current MapServer stable branch (6.4)  
[MapServer MapManager](http://www.gisinternals.com/sdk/downloads/release-1600-gdal-1-10-mapserver-6-4/MapManager.msi)  
[GDAL ECW driver](http://www.gisinternals.com/sdk/downloads/release-1600-gdal-1-10-mapserver-6-4/gdal-110-1600-ecw.msi)  
[GDAL FileGDB driver](http://www.gisinternals.com/sdk/downloads/release-1600-gdal-1-10-mapserver-6-4/gdal-110-1600-filegdb.msi)  
[GDAL MrSID driver](http://www.gisinternals.com/sdk/downloads/release-1600-gdal-1-10-mapserver-6-4/gdal-110-1600-mrsid.msi)  
[GDAL Oracle driver](http://www.gisinternals.com/sdk/downloads/release-1600-gdal-1-10-mapserver-6-4/gdal-110-1600-oracle.msi)

Building
--------

To compile MapServer MapManager install the following prerequisites on the development machine:

1. Microsoft Visual Studio 2010 or above
2. Wix Toolset v3.8 with Visual Studio integration
3. [GDAL and Mapserver build SDK](http://www.gisinternals.com/sdk/downloads/release-1600-dev.zip) from http://www.gisinternals.com/sdk

The compilation can be done with the following steps:

1. Download GDAL and Mapserver build SDK and extract the files into a directory.
2. Checkout the MapManager source tree from the git repository (https://github.com/DMS-Aus/MapManager.git) into a subfolder of the SDK root directory.
3. In the solution explorer right click on the solution 'Buid Solution'
4. To build the installer right click on the project 'Installer' and select 'Build'


To submit bug reports or patches please refer to the [github issue tracker](https://github.com/DMS-Aus/MapManager/issues)

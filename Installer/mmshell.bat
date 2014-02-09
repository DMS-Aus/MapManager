@echo off
@echo Setting environment for using the GDAL and MapServer tools.

SET PATH=%CD%;%PATH%
SET GDAL_DATA=%CD%
SET GDAL_DRIVER_PATH=%CD%\gdalplugins
SET PYTHONPATH=%CD%\python;%CD%\python\osgeo;%CD%\python\scripts
SET PROJ_LIB=%CD%\ProjLib
CD %USERPROFILE%

CREATE DATABASE CEP
ON PRIMARY (NAME = 'CEP', FILENAME = '/var/opt/mssql/temp/CEP.mdf')
LOG ON (NAME = 'CEP_log', FILENAME = '/var/opt/mssql/temp/CEP_log.ldf')
FOR ATTACH_REBUILD_LOG;
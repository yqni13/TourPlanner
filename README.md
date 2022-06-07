# TourPlanner
SWEN2 Project for Desktop Application with C# (WPF) building a functional Tour Planning Tool.


--------------------SETUP-----------------

-------------------CONFIG-----------------

To run the App needs two config files wich are 
located in the setup folder. 

1) The general config file named "TourPlanner.json", which is suposed to be placed into
a folder named "Config" in the same location as the exe

2) The logger config file "log4net.config", which is also suppoed to be placed into 
the same lacation as the exe.


-------------------DATABASE---------------

The postgres database has to be setup according to the "createdb.sql" file in the setup folder.
The connection string has to be set accordingly i the "TourPlanner.json" file under "TourPlannerDB": "connection string"

If a docker container is used to run the database the resources found in the Folder "Docker" can be used. 
There you can find a Dockerfile usefull .bat files and a .txt file with some instructions and common commands 










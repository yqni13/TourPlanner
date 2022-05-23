----------------get Docker image -------------------------------------------------

1)  Dowload docker desktop 
2)  Create dockerhub account 
3)  login (via GUI or commandline): 

    docker login --username=yourhubusername --email=youremail@company.com

4) get collaborater status for tomeindl/swen2-repo (ask me)
5) run the pullandrundockerimage.bat 
6) optional check for success by listing all containers 

    docker container ls

---------------------------------------------------------------------------------------
-----------------------run SQL script in docker container -----------------------------

1)change what you want to change in the createdb.sql file
2)run the updateSQL.bat 

----------------------------------------------------------------------------------------

Docker command cheat sheet 

docker build -t tourplanner-postgres-image ./

docker run -d --name tourplanner-postgres-container -p 5432:5432 tomeindl/swen2-repo:tourplanner-postgres-image

docker login --username=yourhubusername --email=youremail@company.com

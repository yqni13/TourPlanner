@echo off

docker pull tomeindl/swen2-repo:tourplanner-postgres-image

docker run -d --name tourplanner-postgres-container -p 5432:5432 tomeindl/swen2-repo:tourplanner-postgres-image



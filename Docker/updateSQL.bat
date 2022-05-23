docker cp ./createdb.sql tourplanner-postgres-container:/home/createdb.sql

docker exec -t tourplanner-postgres-container psql -U swen2 -h localhost -d tourplanner -f /home/createdb.sql

pause

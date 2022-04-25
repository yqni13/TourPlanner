docker build -t tourplanner-postgres-image ./

docker run -d --name tourplanner-postgres-container -p 5432:5432 tourplanner-postgres-image

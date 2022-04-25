docker build -t my-postgres-db ./

docker run -d --name postgres-container -p 5432:5432 my-postgres-db

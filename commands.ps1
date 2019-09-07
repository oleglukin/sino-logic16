# build solution
dotnet publish --framework netcoreapp2.2 --configuration Release --output dist

docker-compose build
docker-compose up -d
docker-compose stop

docker stop $(docker ps -q)
docker rm $(docker ps -a)

docker exec -it sino-logic16_webui_1 pwd

# Map port 3000 to container's port 80 -> should be done in docker compose
docker run -p 3000:80 --rm --name sino-logic16_webui_1 sino-logic16_webui
# http://localhost:3000/

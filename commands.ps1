# build solution
dotnet --info
dotnet new mvc -n WebUI
dotnet new webapi -n JobTypeA
dotnet new webapi -n JobTypeB

dotnet new sln -n sino-logic16
dotnet sln add WebUI JobTypeA JobTypeB


dotnet publish --framework netcoreapp2.2 --configuration Release --output dist
dotnet .\dist\WebUI.dll
dotnet .\WebUI\bin\Debug\netcoreapp2.2\WebUI.dll
dotnet .\dist\JobTypeA.dll --urls=http://localhost:5004/
dotnet .\JobTypeA\bin\Debug\netcoreapp2.2\JobTypeA.dll --urls=http://localhost:5004/


docker-compose build
docker-compose up -d
docker-compose stop

docker stop $(docker ps -q)
docker rm $(docker ps -a)

docker exec -it sino-logic16_webui_1 pwd

# Map port 3000 to container's port 80 -> should be done in docker compose
docker run -p 3000:80 --rm --name sino-logic16_webui_1 sino-logic16_webui
# http://localhost:3000/

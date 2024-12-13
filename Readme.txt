Migration commands:

 - Start migration: 
Add-Migration "Initial migration" / dotnet ef migrations add "InitialMigration" --project ./EntityFrameworkProject

 - Update a DB with the migration:
dotnet ef database update --project GlobalMarket.API

 - Apply a migration
Update-Database NameOfTheMigration / dotnet ef database update NameOfTheMigration

 - Revert to the empty database
Update-Database 0 / dotnet ef database update 0
-------------------------------------------------------------------------------------------------------------------------
1. Controller loh и просто ловит данные
2. Service их обрабатывает и кидает в DB
3. Для контроллера можно создавать отдельные API модели, назвать из Dto с уточнением назначения (например Create) и хранить в отдельном фолдере в Core

Золотой стандарт (Controller)
- Получил
- Смапил и закинул в сервис
- Вернул резалт из сервиса

О репозитории:
- запросы к БД должны быть наиболее отфильтрованными, чтобы избежать вытягивания 
больших объёмов данных

?* Фильтрация

 "Jwt": {
     "Key": "MySuperSecretKeyHere - нужно сгенерить. https://generate.plus/en/base64

Хэширование:
1. https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.cryptography.keyderivation.keyderivation.pbkdf2?view=aspnetcore-8.0
Хэшированный пароль хранится в БД
2. Эта же функция будет юзать и при аутентификации

AzureSearch + AzureServiceBase
----------------------------------------------------------------------
Структура базы данных
Таблица юзеров
Таблица мануфактур
Таблица продуктов (содержит в себе обязательный ID мануфактуры и модель мануфактуры, у одной мануфактуры может быть много продуктов)
Таблица магазинов
Таблица магазина продуктов (связь многие ко многим между продуктами и магазинами)
----------------------------------------------------------------------
Dockerizing the App:
1. Create a Docker file describing stages for building and publishing the App inside the Docker container
2. Create a docker-compose.yml file with services for the DB and the App by setting Environment variables, ports, and network
3. Create a .dockerignore file which has the same function as .gitignore
4. Create .env file where values of some of the Environment variables will be stored

 - * Connection to the BD inside the Docker container can be made with an updated connection string where the Server is 'Server=db,1433(container port, that is defined in the docker-compose file)'
with port number and user and password must be in the connection string. Password is defined in the docker-compose.yml file. 
The connection string is stored in .env (Values from the .env file should be bound with Environment values in the docket-compose.yml and then will be read in the App)*
 - * To apply the migration on the empty DB in the container an option is to run only the db service of the docker-compose.yml and then update the connection
string in the App and apply migrations manually*

Note to remember. Every time when the container with the DB shutted down it delets all data, that was in the DB
Next time, when the container is deployed migrations are needed to be applied. Outside the docker container, the connection string should be
"Server=localhost,8002(external port, that is defined in the docker-compose file)"

Docker commands:
 - docker-compose up --build (build the docker-compose.yml)
 - docker-compose down (stop all containers from the docker-compose.yml)
 - docker compose up db -d (start only selected services in the docker-compose.yml)

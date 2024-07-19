Migration commands:

 - Start migration: 
Add-Migration "Initial migration" / dotnet ef migrations add "InitialMigration" --project ./EntityFrameworkProject

 - Update a DB with the migration:
dotnet ef database update --project ./EntityFrameworkProject
UPDATE-DATABASE

 - Apply a migration
Update-Database NameOfTheMigration / dotnet ef database update NameOfTheMigration

 - Revert to the empty database
Update-Database 0 / dotnet ef database update 0

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

?* Авторизация, куки, кэш, middleware с обработчиками ошибок
?* Фильтрация

 "Jwt": {
     "Key": "MySuperSecretKeyHere - нужно сгенерить. https://generate.plus/en/base64

Хэширование:
1. https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.cryptography.keyderivation.keyderivation.pbkdf2?view=aspnetcore-8.0
Хэшированный пароль хранится в БД
2. Эта же функция будет юзать и при аутентификации
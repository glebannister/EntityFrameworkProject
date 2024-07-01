Migration commands:

 - Start migration: 
Add-Migration "Initial migration" / dotnet ef migrations add "InitialMigration" --project ./EntityFrameworkProject

 - Update a DB with the migration:
dotnet ef database update --project ./EntityFrameworkProject
UPDATE-DATABASE

1. Controller loh и просто ловит данные
2. Service их обрабатывает и кидает в DB
3. Для контроллера можно создавать отдельные API модели

Золотой стандарт (Controller)
- Получил
- Смапил и закинул в сервис
- Вернул резалт из сервиса
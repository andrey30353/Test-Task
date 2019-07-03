# Test-Task

CacheService можно запустить как консольное приложение из студии или как службу
После запуска EF создаст базу и таблицы и заполнит их данными
Коннекты к базе в файлах: MongoContext.cs и MsSqlContext.cs
Проверка кэша каждые 30 секунд (файл Program.cs)

Чтобы обновление кэша заработало, нужно создать хранимые процедуры и тригеры утилитой aspnet_regsql.
Она находится здесь:
C:\Windows\Microsoft.NET\Framework[framework version]\aspnet_regsql

Например:
cd C:\Windows\Microsoft.NET\Framework\v4.0.30319

В cmd выполняем команды:
aspnet_regsql.exe -C "Data Source=localhost\SQLEXPRESS;Database=sport;Integrated Security=True;" -ed -et -t Sports
aspnet_regsql.exe -C "Data Source=localhost\SQLEXPRESS;Database=sport;Integrated Security=True;" -ed -et -t Categories
aspnet_regsql.exe -C "Data Source=localhost\SQLEXPRESS;Database=sport;Integrated Security=True;" -ed -et -t Matches

aspnet_regsql.exe -C "Data Source=localhost\SQLEXPRESS;Database=sport;Integrated Security=True;" -ed -et -t Markets
aspnet_regsql.exe -C "Data Source=localhost\SQLEXPRESS;Database=sport;Integrated Security=True;" -ed -et -t Outcomes

aspnet_regsql.exe -C "Data Source=localhost\SQLEXPRESS;Database=sport;Integrated Security=True;" -ed -et -t Configuration
aspnet_regsql.exe -C "Data Source=localhost\SQLEXPRESS;Database=sport;Integrated Security=True;" -ed -et -t ConfigurationMatchDisabled
aspnet_regsql.exe -C "Data Source=localhost\SQLEXPRESS;Database=sport;Integrated Security=True;" -ed -et -t ConfigurationSportMargin

При изменении данных в MsSql данные будут обновляться в MongoDB.

В SportWebApi строка подключения к MongoDB в файле appsettings.json.
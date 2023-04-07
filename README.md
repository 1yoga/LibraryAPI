# LibraryAPI

LibraryAPI - это REST API, разработанный для управления электронной библиотекой. Он позволяет пользователям добавлять, обновлять и удалять книги и читателей, а также выдавать книги читателям и возвращать их обратно в библиотеку.

## Используемые технологии

- ASP.NET Core 6.0
- Entity Framework Core 6.0
- PostgreSQL

## Установка и настройка

1. Склонируйте репозиторий с помощью команды:

```
git clone https://github.com/1yoga/LibraryAPI.git
```

2. Установите необходимые зависимости:

```
cd LibraryAPI
```
```
dotnet restore
```

3. Откройте файл `appsettings.json` и настройте строку подключения к базе данных PostgreSQL:

<pre>
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5433;Database=LibraryDB;Username=postgres;Password=your_password;"
  },
...
}
</pre>

4. Примените миграции базы данных:

```
dotnet ef database update
```

5. Запустите проект:
```
dotnet run
```

## API Endpoints

### Книги

- `GET /api/Books` - получить список всех книг.
- `POST /api/Books` - добавить новую книгу.
- `GET /api/Books/{id}` - получить информацию о книге по ID.
- `PUT /api/Books/{id}` - обновить информацию о книге по ID.
- `DELETE /api/Books/{id}` - удалить книгу по ID.
- `GET /api/Books/search` - поиск книг по наименованию (части наименования).
- `GET /api/Books/issued` - получить список выданных книг.
- `GET /api/Books/available` - получить список доступных книг.

### Читатели

- `GET /api/Readers` - получить список всех читателей.
- `POST /api/Readers` - добавить нового читателя.
- `GET /api/Readers/{id}` - получить информацию о читателе по ID.
- `PUT /api/Readers/{id}` - обновить информацию о читателе по ID.
- `DELETE /api/Readers/{id}` - удалить читателя по ID.
- `POST /api/Readers/{readerId}/Books/{bookId}` - выдать книгу читателю по ID читателя и ID книги.
- `DELETE /api/Readers/{readerId}/Books/{bookId}` - вернуть книгу в библиотеку по ID читателя и ID книги.
- `GET /api/Readers/search` - поиск читателей по ФИО (части ФИО).

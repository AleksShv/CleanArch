# ASP.NET Clean Architecture application example

## 1 Запуск приложения в Visual Studio
- Запускаем [Docker Desktop](https://docs.docker.com/desktop/install/windows-install/)
- В качестве начального проекта выбираем **docker-compose**
- Запускаем приложение, с приложением запустится вся необходимая инфраструктура (Sql Server, Mongo, Redis)
- Миграции к БД применяются автоматически при запуске приложения

## 2 Описание
- Приложение представляет из себя пример простого маркет плейса для демонстрации организации кодовой базы с применением приёмов чисто архитектуры и архитектуры вертикальных срезов
- Технические подробности реализации проекта описаны в файле [StyleGuide.md](/StyleGuide.md)

## 3 Инфраструктура
- *Sql Server* для хранения реляционных данных
- *Mongo* для хранения блобов
- *Redis* кэш

## 4 Внутренняя инфраструктура приложения
- *MediatR*
- *Auto Mapper*
- *Fluent Validation*
- *Entity Framework Core*
- *Dapper*
- *Serilog*
- *Mini Profiler*
- *Swagger*
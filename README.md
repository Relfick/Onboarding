# Сервис для онбординга
Сервис состоит из трех частей:
- Бэкенд (этот проект)
- Фронтенд (https://github.com/AleksandrSdkv/hakaton)
- Телеграм-бот (https://github.com/mmoschenskikh/onboarding)

## Реализованная функциональность:
- Модели и эндпоинты для базы знаний и контактов
- Безопасность для Telegram-бота. Сотрудник, не предоставивший контакты своего Telegram-аккаунта, не сможет пользоваться ботом
- Раздельные шлюзы для запросов от бота и веба
- Настроенный docker-compose для развёртывания на сервере вместе с MySQL

## Технологии:
- ASP.NET Core 7
- MySQL
- EntityFramework Core
- Docker

## Установка в Linux
Из корня проекта:
```
docker-compose up -d
```

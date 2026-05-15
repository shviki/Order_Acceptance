# Order_Acceptance - Приём заказов на доставку
Веб-приложение для создания и просмотра заказов на доставку.

## Технологии и версии
- .NET Framework          4.7.2 
- ASP.NET MVC             5.2.9 
- Entity Framework        6.0 (Database First) 
- SQL Server LocalDB     (localdb)\MSSQLLocalDB 

## Требования
- Visual Studio 2019/2022
- .NET Framework 4.7.2
- SQL Server LocalDB (входит в Visual Studio)

## Скачивание проекта с GitHub
### Способ 1: Через Git 
git clone https://github.com/shviki/Order_Acceptance.git

### Способ 2: Скачать ZIP-архивом
1. Откройте репозиторий на GitHub
2. Нажмите зелёную кнопку <> Code
3. Выберите Download ZIP
4. Распакуйте архив в удобную папку

Если скачали ZIP-архивом — удалите папки bin и obj.
После распаковки архива может возникнуть ошибка bin\roslyn\csc.exe. Чтобы её исправить:

1. Закройте Visual Studio (если открыт)
2. В папке проекта найдите и удалите папки:
- bin
- obj
3. Откройте проект заново в Visual Studio
4. Перестройте проект:
- Сборка → Очистить решение
- Сборка → Перестроить решение

## Создание базы данных
База данных не включена в репозиторий (находится в .gitignore). Её нужно создать вручную.

Шаг 1. Откройте Visual Studio
Загрузите проект: Файл → Открыть проект → выберите Order_Acceptance.sln

Шаг 2. Откройте Обозреватель объектов SQL Server
Меню: Вид → Обозреватель объектов SQL Server
Найдите сервер: (localdb)\MSSQLLocalDB

Шаг 3. Создайте базу данных
Правой кнопкой по серверу → Новый запрос → выполните:

```xml
CREATE DATABASE OrderAcceptanceDb;

GO

USE OrderAcceptanceDb;

GO

CREATE TABLE [dbo].[Orders] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [SenderCity]       NVARCHAR (100)  NOT NULL,
    [SenderAddress]    NVARCHAR (200)  NOT NULL,
    [RecipientCity]    NVARCHAR (100)  NOT NULL,
    [RecipientAddress] NVARCHAR (200)  NOT NULL,
    [Weight]           DECIMAL (10, 3) NOT NULL,
    [PickupDate]       DATETIME        NOT NULL,
    [CreatedAt]        DATETIME        DEFAULT (GETDATE()) NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
```
Шаг 4. Добавьте тестовые данные (по желанию)

```xml
INSERT INTO [dbo].[Orders] (SenderCity, SenderAddress, RecipientCity, RecipientAddress, Weight, PickupDate, CreatedAt)

VALUES 
    (N'Москва', N'ул. Тверская, д. 15', N'Санкт-Петербург', N'Невский пр., д. 25', 15.5, '2026-05-20', GETDATE()),
    (N'Казань', N'ул. Баумана, д. 5', N'Екатеринбург', N'пр. Ленина, д. 50', 8.2, '2026-05-21', GETDATE()),
    (N'Новосибирск', N'Красный пр., д. 30', N'Томск', N'ул. Кирова, д. 12', 23.7, '2026-05-22', GETDATE()),
    (N'Нижний Новгород', N'ул. Большая Покровская, д. 8', N'Москва', N'Кутузовский пр., д. 20', 11.3, '2026-05-23', GETDATE()),
    (N'Самара', N'ул. Ленинградская, д. 45', N'Уфа', N'пр. Октября, д. 100', 5.9, '2026-05-24', GETDATE()),
    (N'Краснодар', N'ул. Красная, д. 10', N'Ростов-на-Дону', N'пр. Буденновский, д. 15', 7.5, '2026-05-25', GETDATE()),
    (N'Владивосток', N'ул. Светланская, д. 20', N'Хабаровск', N'ул. Муравьева-Амурского, д. 30', 42.0, '2026-05-26', GETDATE()),
    (N'Иркутск', N'ул. Ленина, д. 1', N'Красноярск', N'пр. Мира, д. 10', 18.3, '2026-05-27', GETDATE()),
    (N'Воронеж', N'пр. Революции, д. 5', N'Липецк', N'ул. Советская, д. 20', 3.2, '2026-05-28', GETDATE()),
    (N'Челябинск', N'ул. Кирова, д. 15', N'Пермь', N'ул. Ленина, д. 40', 9.8, '2026-05-29', GETDATE());
    
GO
```
Шаг 5. Обновите строку подключения в Web.config

Убедитесь, что в Web.config указана ваша база данных:

```xml
<connectionStrings>
    <add name="OrdersEntities" 
         connectionString="metadata=res://*/Models.Model1.csdl|res://*/Models.Model1.ssdl|res://*/Models.Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=OrderAcceptanceDb;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" 
         providerName="System.Data.EntityClient" />
</connectionStrings>
```
Шаг 6. Запустите проект
Нажмите F5 — приложение откроется в браузере.

## Функционал
|    Страница     |                 Действия                    |
| --------------- | ------------------------------------------- |
| Список заказов  | Просмотр всех заказов, поиск по номеру      |
| Создание заказа | Форма с 6 обязательными полями              |
| Детали заказа   | Просмотр полной информации (клик по строке) |
| Контакты        | Контактная информация компании              |

## Поля формы создания
- Город отправителя
- Адрес отправителя
- Город получателя
- Адрес получателя
- Вес груза (кг)
- Дата забора груза
> Номер заказа (Id) и дата создания генерируются автоматически.

## Возможные проблемы и решения
- Ошибка: "Не удалось найти часть пути bin\roslyn\csc.exe"

Решение: Удалите папки bin и obj, затем перестройте проект:

1. Закройте Visual Studio
2. Удалите папки bin и obj в корне проекта
3. Откройте проект заново
4. Сборка → Очистить решение
5. Сборка → Перестроить решение

- Ошибка подключения к базе данных

Решение: Убедитесь, что запущен LocalDB:

```xml
sqllocaldb start MSSQLLocalDB

MyDatabaseMarket — веб-приложение на ASP.NET MVC + Blazor, которое реализует маркетплейс с авторизацией пользователей, управлением товарами и заказами. 
Сервис демонстрирует возможности интеграции классического MVC и современного Blazor для создания удобного и расширяемого интерфейса.

Функционал

🔐 Авторизация и регистрация пользователей (cookie + claims).
🛍 Управление товарами: добавление, редактирование, удаление, просмотр.
📦 Создание и обработка заказов, связь заказов с пользователями и платежами.
💳 Модуль платежей с привязкой к заказам.
📊 Панель управления с отображением данных через Blazor-компоненты.
⚡️ Entity Framework Core + SQLite для работы с БД.

Стек
ASP.NET MVC, Blazor Server, C#, EF Core, SQLite, Identity, Bootstrap, Git.

Роль
Полностью разработал архитектуру проекта:
реализовал модели данных (Users, Orders, Products, Payments) и связи между ними;
настроил Entity Framework и миграции;
создал UI в MVC и Blazor (разделение клиентских и серверных задач);
реализовал авторизацию через claims + cookies;
проработал бизнес-логику заказов и платежей;
интегрировал панели управления в Blazor.

Ценность проекта
MyDatabaseMarket показывает мой опыт в фулстек-разработке на ASP.NET: от проектирования базы данных и backend-логики до UI и интеграции Blazor для динамических компонентов. 
Этот проект можно развивать в сторону полноценного маркетплейса с платежными системами и аналитикой.


## 🔐 Authentication
![Login](screenshots/login.png)  
![Register](screenshots/register.png)

## 👤 Personal Account
![Personal Account](screenshots/personal-account.png)  
![My Orders](screenshots/my-orders.png)  
![Payments](screenshots/payments.png)

## 🧮 Calculator
![Calculator](screenshots/calculator.png)  
![Calculator in Use](screenshots/calculator-in-use.png)  
![Advanced Calculator](screenshots/calculator-advanced.png)

## 🗄 Database
![Database Catalog](screenshots/database-catalog.png)

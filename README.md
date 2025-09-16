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


### 🗄 Каталог баз данных
![Database Catalog](screenshots/Database_catalog.jpg)

### 🔐 Вход/Регистрация аккаунта
![Login](screenshots/Login_to_your_personal_account.jpg)  
![Register](screenshots/Register_an_account.jpg)

### 👤 Личный кабинет
![Personal Account](screenshots/personal_account.jpg)  
![My Orders](screenshots/my_orders.jpg)  
![Payments](screenshots/payments.jpg)

### 🧮 Калькулятор
![Calculator](screenshots/calculator.jpg)  
![Calculator in Use](screenshots/Calculator_work.jpg)  
![Advanced Calculator](screenshots/Calculator_work2.jpg)

# WeatherStation API

## 1. Domain Analysis
**Domain:** Information system for the aggregation, storage, and monitoring of meteorological data (RESTful Web API).

**Domain Description:**
Modern users require rapid access not only to current weather forecasts, but also to historical weather conditions (temperature, humidity, wind speed) across various locations. This project is a backend application (API) that acts as an intermediary between the client and an external weather data provider (Open-Meteo).

Instead of making slow requests to external services every time, our system synchronizes data and accumulates it in its own relational database (MySQL). This allows for fast retrieval of historical weather slices for selected cities. The system is multi-user: it provides secure authentication (JWT) and allows users to manage their own lists of cities to track. The architecture is built according to N-Layer/Clean Architecture principles, ensuring a clear separation between data access, business logic, and the web layer.

---

## 2. Key System Features

Below are 5 core features implemented within this domain:

1. **Secure User Registration and Authorization (JWT Authentication)**
   The system allows users to create accounts. Passwords are not stored in plain text (secure hashing via the BCrypt algorithm is used). Upon successful login, the user receives a JWT (JSON Web Token), which is used for secure access to protected API routes.

2. **Geographical Location Management (CRUD Operations for Cities)**
   The ability to add new cities to the system by specifying their exact geographical coordinates (latitude and longitude). Retrieving the list of all available cities, editing them, and deleting them from the database are also supported.

3. **Weather Data Synchronization with External API (Open-Meteo Integration)**
   A mechanism for retrieving real weather data from a third-party provider is implemented. The system forms HTTP requests based on the coordinates of the selected city, parses the received JSON response, and filters the data (for example, keeping only every third hour for optimization).

4. **Weather History Accumulation and Optimization**
   Data received from the API (temperature, humidity, wind speed, and timestamp) is automatically converted into internal entities and stored in the MySQL database. At the same time, the system features "self-cleaning": before writing new data, outdated forecasts for a specific city are automatically deleted to prevent database overflow. The client can instantly retrieve weather history from the local database.

5. **Monitoring Personalization (Linking Users to Cities)**
   Thanks to a Many-to-Many relationship, an authorized user can build their own list of cities by adding them to their "favorites." This allows for personalized weather feeds tailored exclusively to the locations of interest to a specific user.
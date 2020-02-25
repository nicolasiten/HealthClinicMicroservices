# HealthClinicMicroservices
.Net Core Microservice Application. Save and manipulate patient data and determine Diabetes Risk.
![Architecture Overview](https://github.com/nicolasiten/HealthClinicMicroservices/blob/master/images/Architecture.JPG)
# Projects
## Project Structure: MicroService Architecture
## Gateway
Public Gateway to access the Microservice endpoints.
Requests get routed with the help of the Ocelot library.
## Common
Code that's common throughout the project. 
Examples: Exception classes, Custom validation attributes.
## Patient
Microservice to Create, Edit and Get Patient data.
Data gets stored in an SQL Server database.
## PatientNotes
Microservice to Create, Edit and Get PatientNotes data.
Data gets stored in a NoSQL database (MongoDb).
## DiabetesRisk
Microservice to determine the Diabetes risk of a patient. Accesses Date from the Patient and PatientNotes Microservices.
## HealthClinic.Web
Simple .NET Core MVC application to execute all the functionality through a GUI. The application is sending requests to the Microservices through the API Gateway.
# Setup
## ConnectionStrings
The ConnectionStrings in the Project Patient and PatientNotes need to be adjusted.
**Patient**
"DefaultConnection" needs to point to a SQL Server Database. The Database and Structure will be created automatically once the Service gets started the first time.
**PatientNotes**
"DefaultConnection" needs to point to MongoDB.
## VS Startup
To run the project in Visual Studio the docker compose project should be set as startup project. Once all the containers are running the Frontend can be accessed through the HealthClinic.Web Container.

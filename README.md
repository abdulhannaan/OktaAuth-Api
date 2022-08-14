# OktaAuth-Api

# Prerequirements
* Visual Studio 2022
* .NET Core 6

# How to run?
* Open solution in Visual Studio
* Build Project
* Run Application

# Project Name: Okta

## Controllers
* Token Controller contains login endpoint.
* UserController contains sign-up & clear-session endpoint.

## Middleware
* ExceptionMiddleware is added for handling exepection in the application.

## DependencyRegistrar
This folder is for adding dependency injection in services. Further if we have Repositories in our project we can register them in separate class in same folder.

## Services
* UserService has all login,sign-up & clear-session services added.
* LogginService is used to log errors exceptions in the project.

# Project Name: OktaAuth

## Helpers
* ApiHelper class is used to create helper for HttpClient. It is a generic class which accept and return generic response. Get POST & DELETE calls are handled in the ApiHelper class.
* OktaHelper class is inherited by ApiHelper class. It has all the api calls configured here that are used in this project.

## Models
* All Response Request Models are in models folder.

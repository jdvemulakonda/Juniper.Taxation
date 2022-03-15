# TaxApi

TaxApi API endpoints provide detailed sales tax rates and calculations. Currently this Api supports below two methods 
1. `GET /v1/tax/rate` : Gets the Tax rates for a given location
2. `POST /v1/tax/ordersaletax` : Calculates the taxes for an order

## Api Specifications:

* [Getting Started](#getting-started)
* [Prerequisites](#prerequisites)
* [Required software instalations](#required-software)
* [Code Structure](#code-structure)
* [ApiConsumer Configuration](#api-config)
* [Api Actions/Methods](#api-method) 
* [Swagger Documentation](#swagger) 



## Getting Started

These instructions will help you to get a copy of this project up and running on your local machine for development and testing purposes. 

### Prerequisites

#### Required software instalations

1. Coding Platform : [.Net Core](https://dotnet.microsoft.com/download) v3.1
1. Testing Tool (Optional) : [Postman](https://www.getpostman.com/downloads/)
1. Code Editors : [Visual Studio 2022](https://msdn.microsoft.com/en-us/) / [Visual Studio Code](https://code.visualstudio.com/download) 


## Code Structure
As per the Screenshot there are 6 projects in the solution  
1. **Juniper.TaxApi** :WebApi Starting point of the project with all the dependency injections and settings required to start the solution.This Also contains middleware and extensions which are required
2. **Juniper.TaxApi.Core.Application** : This is the heart of the business logic layer.It interacts with the Entities project This Project has the following directories. 
    1. **Exceptions** : Different kind of exception thrown by the project.  
    3. **Interfaces** :(This can be seperated out to a different project all together) This contains the interface definitions for all the infrastructure services and any other interfaces required for communicating with webapi. 
    4. **Models** : Contains all the Request and response objects for the webapi. 
    5. **Wrappers** : Implementations for any wrapper required for Entities or response objects. 
3. **Juniper.TaxApi.Core.Domain** : This contains all the domain entities and core objects required for the business layer. This should be independent from any other external serivces or influences.This could also encompass relations between entitiesa and other pure business validations.
4. **Juniper.TaxApi.Tests** : Unit test project for for Juniper.TaxApi project
5. **Juniper.Taxation.Infrastructure.Providers** : Any external apis/providers which are required for accomplishing the work required by the business layer. For example Taxjar is the provider now, but it could have some more provider in the future
6.. **Juniper.Taxation.Infrastructure.ExternalCommunicationService** : This project contains all the external communication methods like HttpClient, Wcf services, Socker Communication etc.

![image](https://github.com/jdvemulakonda/Juniper.Taxation/blob/master/Solution%20Structure.png)


## ApiConsumer Configuration
The Actions require a consumer key for the provide ( Taxjar in this case). The provider configurations are stored here
**appSettings.json**
    "ConsumerKeyProviderConfiguration": {
            "ProviderConfigurations": [
             {
                "ProviderKey": "Juniper",
                "ProviderBaseEndPoint": "https://api.taxjar.com/",
                "AuthToken": "5da2f821eee4035db4771edab942a4cc",
                "IsTokenAuthRequired": true
            },
            {
                "ProviderKey": "Test",
                "ProviderBaseEndPoint": "https://sample.com",
                "AuthToken": "test_key",
                "IsTokenAuthRequired": false
            }
        ]
    }
  
## Api Actions/Methods
1. `GET /v1/tax/rate` : Gets the Tax rates for a given location
       
    ![image](https://github.com/jdvemulakonda/Juniper.Taxation/blob/master/GetTaxRateByLocationSeq.png)

        
3. `POST /v1/tax/ordersaletax` : Calculates the taxes for an order
    
 
    ![image](https://github.com/jdvemulakonda/Juniper.Taxation/blob/master/PostCallSequence.png)



## Swagger Documentation
LaunchSetting.js has the swagger URL as the launch URL. Once the code is executed users will be able to see the SwaggerUI on their browsers as below. 

![image](https://github.com/jdvemulakonda/Juniper.Taxation/blob/master/swagger1.png)

# CompanyClaimsApi

### Prerequisites
Please make sure you have installed [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).

### Getting Started
Follow the steps below to get the project up and running:
1. Clone [this repository](https://github.com/sathish-nagula/CompanyClaimsService) in IDE like Visual Studio
2. Build the solution
3. Run unit tests
4. Run (F5)
5. Access the endpoints in tools like Postman ([Postman collection](https://github.com/sathish-nagula/CompanyClaimsService/blob/master/CompanyClaimsApi.postman_collection.json) is available in the solution folder, however, value needs to be updated for a variable called `env`).
     - Get a single company: `/api/Company/{id}`
     - Get claims for a company: `/api/Company/{id}/claims`
     - Get details of a claim: `/api/Claim/{ucr}`
     - Update a claim: `/api/Claims`
       ```
       {
          "ucr": {ucr},
          "companyId": {id},
          "assuredName": {string},
          "claimDate": {date in ISO_8601 format},
          "lossDate": {date in ISO-8601 format},
          "incurredLoss": {decimal},
          "closed": {boolean}
        }
       ```

### Background

A restful API to access the Claims and Company data.

### Requirements

· The output must be in json format

· We need an endpoint that will give me a single company. We need a property to be returned that will tell us if the company has an active insurance policy

· We need an endpoint that will give me a list of claims for one company

· We need an endpoint that will give me the details of one claim. We need a property to be returned that tells us how old the claim is in days

· We need an endpoint that will allow us to update a claim

· We need at least one unit test to be created

### Database Structure

```
CREATE TABLE Claims
(
UCR VARCHAR(20),
CompanyId INT,
ClaimDate DATETIME,
LossDate DATETIME,
[Assured Name] VARCHAR(100),
[Incurred Loss] DECIMAL(15,2),
Closed BIT
)

CREATE TABLE ClaimType
(
Id INT,
Name VARCHAR(20)
)

CREATE TABLE Company
(
Id INT,
Name VARCHAR(200),
Address1 VARCHAR(100),
Address2 VARCHAR(100),
Address3 VARCHAR(100),
Postcode VARCHAR(20),
Country VARCHAR(50),
Active BIT,
InsuranceEndDate DATETIME
)
```

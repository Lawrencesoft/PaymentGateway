# Payment Gateway
Payment Gateway repository is used to do the payment using Checkout API 

# Challenge Description:

The product requirements for this initial phase are the following:
1. A merchant should be able to process a payment through the payment gateway and receive either
a successful or unsuccessful response.
2. A merchant should be able to retrieve the details of a previously made payment. <br><br>

More details please refer this [Link](https://github.com/Lawrencesoft/PaymentGateway/blob/main/Images/CheckoutChallenge.pdf)

# Required version and Technologies
  .NET 5.0 version
  Visual Studio 2019 IDE
- C# Core Web API
- Test cases created separately
- Added Dependancy Injection
- Added Swagger - Easy to invoke API
- Implemented Client Factory to control the client creation and invocation

# Areas of Improvement

- Add Api authentication - Checkout API has the API security validation so didn't add this project.
- Api Logging
- Api request Validator

# Build and Run the Repository
Build any .NET Core project using the .NET Core CLI, which is installed with the [.NET Core SDK](https://dotnet.microsoft.com/download). Then run these commands from the CLI in the directory of this project:<br />

``dotnet build``<br />
``dotnet run``<br />

These will install any needed dependencies, build the project, and run the project respectively.  

**Other Options** - 
1) **Buid :** Open the Visual Studio(2019) IDE **Build**  Menu --> **Build solution**
2) **Run :** Open the [PaymentGateway.sln](https://github.com/Lawrencesoft/PaymentGateway/blob/main/PaymentGateway.sln) in Visual Studio(2022) IDE and make this project [PaymentGateway](https://github.com/Lawrencesoft/PaymentGateway/blob/main/PaymentGateway/PaymentGateway.csproj) as startup project and run it or publish the API project in IIS and run it from there. <br />

**Update the Secret Key and Public Key in the config file** <br />

**Publish :** Open the Visual Studio(2019) IDE **Build**  Menu --> **Publish PaymentGateway** <br />
&nbsp;&nbsp;&nbsp;&nbsp;Select the path to publish it. Once it is publish to the path, This path can be link from IIS and run from there <br />

**Test Project Execution:** Open the Visual Studio(2019) IDE **Test**  Menu --> Run All Tests<br />
    Once it is executed, Test explorer will show the test results(Executed screenshot added below) 

# Cloud Technologies

Purpose of using Cloud Technologies
 - This Payment gatways can effortlessly audit processes due to incredible transparency 
 - Information is delivered in real-time
- Offers top-quality security controls to the Financial applications
- Easy control over Data encryption
- Unrestricted access to all the information from any part of the world
- All data can be backed up with no trouble
- It gives you full control over choosing who to grant the access and not

I prefer to use **"Amazon Web Services"** cloud technologies because it has lot of security feature and lot of reports to verify the transactions. 
# ScreenShots
![image](https://user-images.githubusercontent.com/63959021/166166955-f66b58f1-e1ca-465a-bf19-18d26f094bd1.png) <br>
![image](https://user-images.githubusercontent.com/63959021/166166985-b8922afa-005b-4f95-bb06-c194ffc21d63.png) <br>
![image](https://user-images.githubusercontent.com/63959021/166167033-4920fa6e-29f0-4e31-82a7-876fea1948c5.png) <br>
![image](https://user-images.githubusercontent.com/63959021/166167055-7d27135d-77b7-4999-9251-d5de18dc8761.png) <br>
![image](https://user-images.githubusercontent.com/63959021/166167094-871cf998-9f4b-4221-9e02-03c6ce644092.png) <br>
![image](https://user-images.githubusercontent.com/63959021/166167125-b0e02dbd-eff0-494d-9ba7-cecb7713eab8.png) <br>
![image](https://user-images.githubusercontent.com/63959021/166167143-ed14c0db-a35f-4778-9cdc-72b4a9a79ea2.png) <br>
![image](https://user-images.githubusercontent.com/63959021/166167160-6e5b1489-44d2-47ef-b179-096b5e1e5130.png) <br>
![image](https://user-images.githubusercontent.com/63959021/166167198-e84b9d9e-4211-4e3e-9da7-936ac09bc5c2.png) <br>
![image](https://user-images.githubusercontent.com/63959021/166167275-dc6185d9-0f20-44c2-a1aa-f997b4cc6ae0.png) <br>
![image](https://user-images.githubusercontent.com/63959021/166167293-bfa3d70d-ae73-480a-ae43-f67b5a2767ab.png) <br>


# Test Cases
![image](https://user-images.githubusercontent.com/63959021/166167311-f0da7cea-0232-4440-bc71-91745f5474db.png)


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
2) **Run :** Open the [PaymentGateway.sln](https://github.com/Lawrencesoft/PaymentGateway/blob/main/PaymentGateway.sln) in Visual Studio(2022) IDE and make this project [PaymentGateway](https://github.com/Lawrencesoft/PaymentGateway/blob/main/PaymentGateway/PaymentGateway.csproj) as startup project and run it or publish the API project in IIS and run it from there. 

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

Solution is being built successfully with all provided test cases being passed (further detail provided below).

I have commented out existing code in RebaseService and did not delete for reference and then added my code as shown below.

![image](https://github.com/aashahzad/Smartwyre.DeveloperTest/assets/41636456/c9962d06-f7aa-43db-976d-2cd9f1775a17)

NOTE: In order to support new incentive tyoes, i have created a new base abstract class and inherited child classes under ths same service, as shown in below image.

![image](https://github.com/aashahzad/Smartwyre.DeveloperTest/assets/41636456/aa9a8cc2-4490-49f8-806a-eeb0978b5796)

Dependency injection logic has been written in developerTest.Runner project in Program.cs file as shown below with one startup file name ServiceHelper.cs which contains a commented call to service and further three calls to test methods as shown below.

![image](https://github.com/aashahzad/Smartwyre.DeveloperTest/assets/41636456/37a0f627-3111-47bb-a403-2b8f62c3f48a)

![image](https://github.com/aashahzad/Smartwyre.DeveloperTest/assets/41636456/40f83dc1-035e-44a7-a7c1-8db3f4bf97aa)


In DeveloperTest.Test project, I have written only three success test cases covering all existing incentive types and all test cases are being pass successdfully as shown below.

![image](https://github.com/aashahzad/Smartwyre.DeveloperTest/assets/41636456/229f2389-427a-4bee-be6d-daa3fbf787cd)


![image](https://github.com/aashahzad/Smartwyre.DeveloperTest/assets/41636456/bfaa558d-0d73-46f8-884d-8b77863670e2)


# Smartwyre Developer Test Instructions

In the 'RebateService.cs' file you will find a method for calculating a rebate. At a high level the steps for calculating a rebate are:

 1. Lookup the rebate that the request is being made against.
 2. Lookup the product that the request is being made against.
 2. Check that the rebate and request are valid to calculate the incentive type rebate.
 3. Store the rebate calculation.

What we'd like you to do is refactor the code with the following things in mind:

 - Adherence to SOLID principles
 - Testability
 - Readability
 - In the future we will add many more incentive types. Determining the incentive type should be made as easy and intuitive as possible for developers who will edit this in the future.

We’d also like you to 
 - Add some unit tests to the Smartwyre.DeveloperTest.Tests project to show how you would test the code that you’ve produced 
 - Run the RebateService from the Smartwyre.DeveloperTest.Runner console application accepting inputs

The only specific 'rules' are:

- The solution should build
- The tests should all pass

You are free to use any frameworks/NuGet packages that you see fit. You should plan to spend around 1 hour completing the exercise.

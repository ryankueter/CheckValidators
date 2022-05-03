 Check Validators (.NET)

Author: Ryan Kueter  
Updated: May, 2022

## About

**Check Validators** is a free .NET library, available from the [NuGet Package Manager](https://www.nuget.org/packages/CheckValidators), that provides a simple, elegant, and powerful way to validate and guard your data. You can write your own validation extensions and use them in your project without modifying this library.

### Targets:
- .NET 6

   


## Introduction

Each "Check" contains validation rules that can be chained together using method extension syntax (builder pattern). The **If**, **IfNot**, **AndIf**, **AndIfNot**, **OrIf**, and **OrIfNot** rules allow you to use LINQ to validate the different members of the class, including properties, lists, dictionaries, and other complex types. If an exception is thrown, it will aggrigate a list of errors that can be retrieved with **GetErrors()** or the errors can be thrown with **ThrowErrors()**. It also has an **IsValid()** method that returns true if all conditions were met and false if at least one condition failed. 

```csharp
using CheckValidators;

string? i = null;

var c = new Check<string>(i)
    .IfNull("The string is null.")
    .IfEmptyOrWhitespace("The string is empty.")
    .AndIfNot(s => s.Contains("keyword"), "The string did not contain the keyword.");

// Getting errors
if (c.HasErrors())
{
    foreach (var s in c.GetErrors())
    {
        Console.WriteLine(s);
    }
}

// Throwing errors
if (!c.IsValid())
{
    try
    {
        c.ThrowErrors();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
```
###
## A Realistic Example

In this example, if the value "MyServiceRequest" is null, no errors will be thrown unless the IfNull rule is applied, and IsValid() will evaluate to false. This example also uses a check statement inside another check statement to validate complex items.

```csharp
try
{
    new Check<MyServiceRequest>(request)
        .IfNull("The service request cannot be null.")
        .If(p => p.User == null, "The user is invalid.")
        .If(p => new Check<string>(p.Email).IfNull().IfNotEmail().HasErrors(), "An email is invalid.") 
        // OrIf and OrIfNot only execute when a previous If or IfNot validation fails.
        .OrIf(p => p.Id == 0, "The user id is invalid.")
        .OrIfNot(p => p.Id > 0, "The user id is invalid.")
        .IfNot(p => p.People.Any(), "The request does not contain any people.")
        // AndIf and AndIfNot only execute when a previous If or IfNot validation succeeds.
        .AndIf(p => p.People.Where(x => new Check<string>(x.Email).IfNull().IfNotEmail().HasErrors()).Any(), "A user has an invalid email.")
        .AndIfNot(p => p.People.Where(x => x.Id > 0).Any(), "One or more user ids are invalid.")
        .If(p => p.TimeStamp == default, "Invalid timestamp.")
        .ThrowErrors();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
``` 
###
#### AndIf, AndIfNot

The **AndIfNot** statement will not execute if any previous **If** rule was invalid. This provides better performance, and prevents unnecessary code execution and unnecessary errors. An example may include checking a child value of a value that was previously determined to be null. The example above checks for an empty list of people. If the list contains people, it will continue to check their email addresses. If the People list is empty, it will skip all following AndIf or AndIfNot statements until it evaluates a new If rule.

#### OrIf, OrIfNot

**OrIf** and **OrIfNot** are the opposite of **AndIf** and **AndIfNot** and only execute if a previous **If** rule fails validation.

###
#### Output:

```console
Errors: 1) An email is invalid., 2) A user has an invalid email., 3) Invalid timestamp. (Parameter 'request [MyServiceRequest]')
```
###
## Extension Methods

You can create your own custom extension methods anywhere in your project to add custom validators that are specific to your needs. If you choose to use a try/catch block, consider throwing the same error in the catch block when using an IfNot, AndIfNot, or OrIfNot rule since they evaluate to false. Avoid throwing the same error in a catch block of an If, AndIf, or OrIf rule since they evaluate to true. 

```csharp
public static partial class CheckValidatorsExtensions
{
    public static Check<List<T>?> IfEmpty<T>(this Check<List<T>?> data, string msg = "")
    {
        if (data.InvalidModel()) { return data; }
        try
        {
            if (!data.Value.Any())
            {
                data.ThrowError(msg, "The list is empty.");
            }
        }
        catch { }
        return data;
    }
}
```
###
## Supported Datatypes (include but are not limited to)
- Custom Datatypes (classes)
- Array
- Dictionary
- Double
- Float
- Int
- List
- Long
- String

###
## Contributions

This project is being developed for free by me, Ryan Kueter, in my spare time. So, if you would like to contribute, please submit your ideas on the Github project page.
# Check Validators (.NET)

Author: Ryan Kueter  
Updated: Febrary, 2022

## About

**Check Validators** is a free .NET library, available from the [NuGet Package Manager](https://www.nuget.org/packages/FreeDataExports), that provides a simple, elegant, and powerful way to validate and guard your data. It also provides the ability to write your own validation extensions.  

#### Targets:
- .NET 6

   


## Introduction

Each "Check" contains validation rules that can be chained together using method extension syntax. The **If**, **IfNot**, **ElseIf**, and **ElseIfNot** rules allow you to use Linq to validate the different members of the class, including properties, lists, dictionaries, and other complex types. If an exception is thrown, it will aggrigate a list of errors that can be retrieved with **GetErrors()** or the errors can be thrown with **ThrowErrors()**. It also has an **IsValid()** method that returns true if all conditions were met and false if at least one condition failed. 

```csharp
using CheckValidators;

string? i = null;

var c = new Check<string?>(i)
    .IfNull("The string is null.")
    .IfEmptyOrWhitespace("The string is empty.")
    .ElseIfNot(s => s.Contains("keyword"), "The string did not contain the keyword.");

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
    c.ThrowErrors();
}
```
###
## A Realistic Example

In this example, if the People list is null, the fourth condition will produce an error. And the following **ElseIfNot** statement will not execute if any previous **If** rule produced an error. This provides better performance, and prevents unnecessary code execution and unnecessary errors. An example may include checking a child value of a value that was previously determined to be null. If a value is null, no errors will be thrown unless the IfNull rule is applied, and IsValid() will always evaluate to false. Check statements can be used inside Check statements to validate complex items.

```csharp
try
{
    new Check<MyServiceRequest>(request)
        .IfNull("The service request cannot be null.")
        .If(p => p.User == null, "The user is invalid.")
        .If(p => new Check<string>(p.Email).IfNull().IfNotEmail().HasErrors(), "The email is invalid.")
        .IfNot(p => p.People.Any(), "The request does not contain any people.")
        .ElseIf(p => p.People.Where(x => new Check<string>(x.Email).IfNull().IfNotEmail().HasErrors()).Any(), "User has an invalid email.")
        .If(p => p.TimeStamp == default, "Invalid timestamp.")
        .ThrowErrors();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
```  
###
#### Output:

```console
Errors: 1) The email is invalid., 2) User has an invalid email., 3) Invalid timestamp. (Parameter 'MyServiceRequest')
```
###
## Extension Methods

You can create your own custom extension methods anywhere in your project to add custom validators that are specific to your needs. If you choose to use a try/catch block, consider throwing the same error in the catch block when using an IfNot or ElseIfNot rule since they evaluate to false. Avoid throwing the same error in a catch block of an If or ElseIf rule since they evaluate to true. 

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
## Contributions

If you would like to contribute to this project, please contribute on the Github project page.
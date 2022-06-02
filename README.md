# Check Validators (.NET)

Author: Ryan Kueter  
Updated: June, 2022

## About

**Check Validators** is a free .NET library, available from the [NuGet Package Manager](https://www.nuget.org/packages/CheckValidators), that provides the most flexible, simple, and powerful way to validate and guard your data. You can write your own validation extensions and use them in your project without modifying this library.

### Targets:
- .NET 6

## Introduction

Each "Check" contains validation rules that can be chained together using method extension syntax (builder pattern).


```csharp
// Validating a date
try
{
    DateTime datetime = DateTime.Now;
    new Check<DateTime>(datetime)
        .IfNull()
        .IfDefault()
        .IfNotUtcTime()
        .IfNot(date => date == DateTime.Now.AddDays(-1), "The date was not yesterday!")
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
Errors: 1) The datetime format is not Utc., 2) The date was not yesterday! (Parameter 'datetime [DateTime]')
```  
###
#### A Realistic Example

One of the features that makes this library so power is not just the ability to use LINQ to validate class members and collections, but you can also nest Check validators inside of those validation rules.

In the following examples, the following checks are being used inside of user-defined validation rules.
* Check<string>(request.Email).IfNotEmail()
* Check<DateTime>(request.TimeStamp).IfNull().IfDefault().IfNotUtcTime()
* Check<string>(request.User).IfEquals("String Comparison Example", StringComparison.InvariantCulture)
* Check<string>(request.User).IfNotMatches("^letterstomatch", System.Text.RegularExpressions.RegexOptions.None)

```csharp
// Validating a service request
try
{
    var request = new MyServiceRequest();
    new Check<MyServiceRequest>(request)
        .IfNull()
        .If(request => request.Email == null, "The email address was null!")
        .AndIf(request => new Check<string>(request.Email).IfNotEmail().HasErrors())
        .If(request => request.TimeStamp == default, "The timestamp was not set!")
        .AndIf(request => new Check<DateTime>(request.TimeStamp).IfNull().IfDefault().IfNotUtcTime().HasErrors(), "An error occured with the timestamp.")
        .If(request => request.Id == 0)
        .If(request => request.People is null || request.People.Count == 0)
        .AndIf(request => request.People.Where(person => new Check<string>(person.Email).IfNull().IfNotEmail().HasErrors()).Any(), "A person in the list of people did not have a valid email address.")
        .AndIfNot(p => p.People.Where(x => x.Id > 0).Any())
        .If(request => new Check<string>(request.User).IfEquals("String Comparison Example", StringComparison.InvariantCulture).HasErrors())
        .If(request => new Check<string>(request.User).IfNotMatches("^rya", System.Text.RegularExpressions.RegexOptions.IgnoreCase).HasErrors())
        .IfNot(request => request.Count > 20 && request.Count < 300)
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
Errors: 1) The email address was null!, 2) The timestamp was not set!, 3) A person in the list of people did not have a valid email address., 4) IfNot(request => request.Count > 20 && request.Count < 300). (Parameter 'request [MyServiceRequest]')
```
###
## Validations

Each validation rule provides a default error message that includes specifics about the causes of the error. With the user-defined validation rules, the errors display the logic used in the validation rule, or you can specify your own custom error.

### Utilities

Check validators comes with a number of utilities that can make validating data easier and more flexible. These utilities can be used inside of user-defined validation rules to create more powerful set of validations.

```csharp
using CheckValidators;

string? data = "Adam Smith";

var c = new Check<string>(data)
    .IfNull()
    .IfEmptyOrWhitespace()
    .IfLengthEquals(10)
    .IfNot(s => s.Contains("keyword"), "The string does not contain the keyword.");
```

##### HasErrors()

If you want to determine if any validation errors occured, you could call this to give you a boolean result.

##### GetErrors()

You can also retrieve each individual error for your own error handling.

```csharp
// Getting errors
if (c.HasErrors())
{
    foreach (var s in c.GetErrors())
    {
        Console.WriteLine(s);
    }
}
``` 

##### IsValid()

IsValid() returns a boolean true or false depending on whether all the rules passed validation. If they do, it will return true. If one failed, it will return false. This can be useful for validation scenarios where you don't want to throw an error.

##### ThrowErrors()

ThrowErrors() will throw all the errors in a nicely formatted error message.

```csharp
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
The expression above, for example, will throw the following errors:
```console
Errors: 1) String length should not equal 10 characters., 2) The string does not contain the keyword. (Parameter 'data [String]')
```

### User-Defined Validation Rules

User-defined validation rules allow you to use LINQ to validate your data. When writing user-defined validation rules, you typically want to write your own error message. If you don't, the expression that fails will be recorded.

###
#### If and IfNot

If and IfNot conditions allow you to use LINQ to query objects and lists in the object. If you use the standard default error message, it will include the query you are using in your message, in addition to the name and type of object you are checking.

```csharp
If(user => user.User is null)
If(user => user.User is null, "Custom error message.")
// Default Error: If({expression}).

IfNot(user => user.User is null)
IfNot(user => user.User is null, "Custom error message.")
// Default Error: IfNot({expression}).
```  
###
#### AndIf and AndIfNot

AndIf and AndIfNot are similar to If and IfNot conditions, except that they only execute when the previous If or IfNot condition is valid. This provides better performance, and prevents unnecessary code execution and unnecessary errors. An example may include checking a child value of a value that was previously determined to be null. For example, if a list of people is null, you don’t want to check that list for other validation errors because they will all fail. So, in that event, all of the following AndIf and AndIfNot statements are skipped. This can be reset by defining a new If or IfNot validation rule.

```csharp
AndIf(user => user.User is null)
AndIf(user => user.User is null, "Custom error message.")
// Default Error: AndIf({expression}).

AndIfNot(user => user.User is null)
AndIfNot(user => user.User is null, "Custom error message.")
// Default Error: AndIfNot({expression}).
```
###
#### OrIf and OrIfNot

OrIf and OrIfNot are the opposite of AndIf and AndIfNot and only execute if a previous If rule fails validation. This can be reset by defining a new If or IfNot validation rule.

```csharp
OrIf(user => user.User is null)
OrIf(user => user.User is null, "Custom error message.")
// Default Error: OrIf({expression}).

OrIfNot(user => user.User is null)
OrIfNot(user => user.User is null, "Custom error message.")
// Default Error: OrIfNot({expression}).
```  
###  
### Built-in Validations

Check validators also include a large number of built-in validation rules. Each of these rules has their own predefined errors that will provide the parameters you are supplying in the error messages.

### General

If you want to throw an error that the object you are checking is null, then you need to include IfNull(). If the object you are checking is null, none of the other validations will be checked. So, this is a good one to include.
```csharp
IfNull()
// Error: The value is null.

IfNotNull()  
// Error: The value is not null.
```

### String

```csharp
IfNotDate()
// Error: String {data.Value} is not a date.

IfNotInteger()
// Error: String {data.Value} is not an integer.

IfNotEmail()
IfNotEmail(RegexPattern, System.Text.RegularExpressions.RegexOptions.None)
// Error: String '{data.Value}' is not an email address.

IfNotURL()
IfNotURL(RegexPattern, System.Text.RegularExpressions.RegexOptions.None)
// Error: String {data.Value} is not a URL.

IfNotValidPassword()
IfNotValidPassword(8, 2, 2, 2, 2)
IfNotValidPassword(MinLength, Uppercase, Lowercase, Numbers, SpecialCharacters)

// Possible errors:
The password does not contain:
A minimum of {minLength} characters.
A minimum of {numUpper} upper-case characters.
A minimum of {numLower} lower-case characters.
A minimum of {numNumbers} numbers.
A minimum of {numSpecial} special characters.

IfMatches(@"\b[R]\w+")
IfMatches(@"\b[R]\w+", System.Text.RegularExpressions.RegexOptions.None)
// Error: String should not match the regular expressions pattern '{pattern}'.

IfNotMatches(@"\b[R]\w+")
IfNotMatches(@"\b[R]\w+", System.Text.RegularExpressions.RegexOptions.None)
// Error: String should match the regular expressions pattern '{pattern}'.

IfEmpty()
// Error: String is empty.

IfWhitespace()
// Error: String is whitepace. 

IfEmptyOrWhitespace()
// Error: String is empty or whitespace.

IfEquals("String")
IfEquals("String", StringComparison.InvariantCulture)
// Error: String should not be equal to '{compareString}' [StringComparison: '{compareType}'].

IfNotEquals("String")
IfNotEquals("String", StringComparison.InvariantCulture)
// Error: String should be equal to '{compareString}' [StringComparison: '{compareType}'].

IfLengthGreaterThan(5)
// Error: String has exceeded the character limit of {length} characters.

IfLengthLessThan(5)
// Error: String does not meet the minimum character length of {length} characters.

IfLengthEquals(5)
// Error: String length should not equal {length} characters.

IfNotLengthEquals(5)
// Error: String length should equal {length} characters.

IfEndsWith("abc")
IfEndsWith("abc", StringComparison.InvariantCulture)
// Error: String should not end with '{ending}' [StringComparison: '{compareType}'].

IfNotEndsWith("abc")
IfNotEndsWith("abc", StringComparison.InvariantCulture)
// Error: String does not end with '{ending}' [StringComparison: '{compareType}'].

IfStartsWith("abc")
IfStartsWith("abc", StringComparison.InvariantCulture)
// Error: String should not start with '{beginning}' [StringComparison: '{compareType}'].

IfNotStartsWith("abc")
IfNotStartsWith("abc", StringComparison.InvariantCulture)
// Error: String does not start with '{beginning}' [StringComparison: '{compareType}'].

IfContains("abc")
IfContains("abc", StringComparison.InvariantCulture)
// Error: String should not contain '{compare}' [StringComparison: '{compareType}'].

IfNotContains("abc")
IfNotContains("abc", StringComparison.InvariantCulture)
// Error: String should contain '{compare}' [StringComparison: '{compareType}'].
```  

### Arrays
```csharp
IfEmpty()
// Error: The array is empty.

IfNotEmpty()
// Error: The array is not empty.

IfCount(5)
// Error: The item count should not be {count}.

IfNotCount(5)
// Error: The item count is not {count}.

IfCountGreaterThan(5)
// Error: The item count is greater than {count}.

IfCountLessThan(5)
// Error: The item count is less than {count}.
```
### DateTime
```csharp
IfUtcTime()
// Error: The datetime format is Utc.

IfNotUtcTime()
// Error: The datetime format is not Utc.

IfLocalTime()
// Error: The datetime format is local.

IfNotLocalTime()
// Error: The datetime format is not local.

IfUnspecifiedTimeFormat()
// Error: The datetime format is unspecified.

IfDefault()
// Error: The datetime is set to the default value.

IfNotDefault()
// Error: The datetime is not set to the default value.
```
### Dictionary
```csharp
IfEmpty()
// Error: Dictionary is empty.

IfNotEmpty()
// Error: The Dictionary is not empty.

IfCount(5)
// Error: The item count should not be {count}.

IfNotCount(5)
// Error: The item count is not {count}.

IfCountGreaterThan(5)
// Error: The item count is greater than {count}.

IfCountLessThan(5)
// Error: The item count is less than {count}.
```
### Double
```csharp
IfNegative()
// Error: The double is negative.

IfPositive()
// Error: The double is positive.

IfZero()
// Error: The double is zero.

IfNotZero()
// Error: The double is not zero.

IfGreaterThan()
// Error: The double is greater than {value}.

IfLessThan()
// Error: The double is less than {value}.

IfEquals()
// Error: The double should not be {value}.

IfNotEquals()
// Error: The double should be {value}.
```
### Float
```csharp
IfNegative()
// Error: The float is negative.

IfPositive()
// Error: The float is positive.

IfZero()
// Error: The float is zero.

IfNotZero()
// Error: The float is not zero.

IfGreaterThan()
// Error: The float is greater than {value}.

IfLessThan()
// Error: The float is less than {value}.

IfEquals()
// Error: The float should not be {value}.

IfNotEquals()
// Error: The float should be {value}.
```
### Int
```csharp
IfNegative()
// Error: The integer is negative.

IfPositive()
// Error: The integer is positive.

IfZero()
// Error: The integer is zero.

IfNotZero()
// Error: The integer is not zero.

IfGreaterThan()
// Error: The integer is greater than {value}.

IfLessThan()
// Error: The integer is less than {value}.

IfEquals()
// Error: The integer should not be {value}.

IfNotEquals()
// Error: The integer should be {value}.
```
### List
```csharp
IfEmpty()
// Error: The list is empty.

IfNotEmpty()
// Error: The list is not empty.

IfCount(5)
// Error: The item count should not be {count}.

IfNotCount(5)
// Error: The item count is not {count}.

IfCountGreaterThan(5)
// Error: The item count is greater than {count}.

IfCountLessThan(5)
// Error: The item count is less than {count}.
```
### Long
```csharp
IfNegative()
// Error: The long is negative.

IfPositive()
// Error: The long is positive.

IfZero()
// Error: The long is zero.

IfNotZero()
// Error: The long is not zero.

IfGreaterThan()
// Error: The long is greater than {value}.

IfLessThan()
// Error: The long is less than {value}.

IfEquals()
// Error: The long should not be {value}.

IfNotEquals()
// Error: The long should be {value}.
```
### Uri
```csharp
IfScheme()
// Error: Uri scheme should not be '{scheme}'.

IfNotScheme()
// Error: Uri scheme is not '{scheme}'.

IfAbsoluteUri()
// Error: Uri is absolute, consider changing it to relative.

IfRelativeUri()
// Error: Uri is relative, consider changing it to absolute.

IfUriPort(80)
// Error: Uri port should not be {port}.

IfNotUriPort(80)
// Error: Uri port should be {port}.

IfFile()
// Error: Uri is a file path.

IfNotFile()
// Error: Uri is not a file.

IfUnc()
// Error: Uri is a UNC path.

IfNotUnc()
// Error: Uri is not a UNC path.

IfLoopback()
// Error: Uri is the loopback address.

IfNotLoopback()
// Error: Uri is not the loopback address.
```

###
## Extensibility

You can create your own custom extension methods anywhere in your project to add custom validators that are specific to your needs, similar to the following:

```csharp
public static partial class CheckValidatorsExtensions
{
    public static Check<List<T>?> IfEmpty<T>(this Check<List<T>?> data)
    {
        if (data.InvalidModel()) { return data; }
        try
        {
            if (!data.Value.Any())
            {
                data.ThrowError("The list is empty.");
            }
        }
        catch { }
        return data;
    }
}
```
###
## Contributions

This project is being developed for free by me, Ryan Kueter, in my spare time. So, if you would like to contribute, please submit your ideas on the Github project page.
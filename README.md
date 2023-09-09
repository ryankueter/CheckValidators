# Check Validators (.NET)

Author: Ryan Kueter  
Updated: September, 2023

## About

**Check Validators** is a free .NET library, available from the [NuGet Package Manager](https://www.nuget.org/packages/CheckValidators), that provides the most flexible, simple, and powerful way to validate and guard your data. It provides a variety of user-defined validators that allow you to use LINQ to validate your data, in addition to hundreds of built-in validators. And it allows you to write your own custom validators and use them in your project without modifying this library.

### Targets:
- .NET 6, .NET 7

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
Errors: 1) The datetime format is not Utc, 2) The date was not yesterday!.
```   
###
Or you can add the filename, line number, and parameter by using ThrowErrors(true):
```console
Errors: 1) The datetime format is not Utc, 2) The date was not yesterday!, in Program.cs:line 48. (Parameter 'datetime <DateTime>')
```
###
#### A Realistic Example


One of the more powerful features of this library is the ability to use LINQ to validate custom datatypes and include other check validators inside those validation rules. This can allow you to apply validators to collections.

Notice that the following checks are being used inside of user-defined validation rules.
* Check<string>(request.Email).IfNotEmail()
* Check<DateTime>(request.TimeStamp).IfNull().IfDefault().IfNotUtcTime()
* Check<string>(request.User).IfNotMatches("^letterstomatch", System.Text.RegularExpressions.RegexOptions.None)

```csharp
//Validating a service request
var request = new MyServiceRequest();
var mycheck = new Check<MyServiceRequest>(request)
.IfNull()
.If(request => request.Email == null,
    "The email address was null")
.AndIf(request => new Check<string>(request.Email).IfNotEmail().HasErrors(),
    "The email address was not valid")
.If(request => request.TimeStamp == default,
    "The timestamp was not set")
.AndIf(request => new Check<DateTime>(request.TimeStamp).IfNull().IfDefault().IfNotUtcTime().HasErrors(),
    "An error occured with the timestamp")
.If(request => request.Id == 0,
    "The requested id was 0")
.If(request => request.People is null || request.People.Count == 0,
    "No people are in the list of people")
.AndIf(request => request.People.Where(person => new Check<string>(person.Email).IfNull().IfNotEmail().HasErrors()).Any(),
    "A person in the list of people did not have a valid email address")
.AndIfNot(p => p.People.Where(x => x.Id > 0).Any(),
    "You have an invalid person in your list of people.")
.If(request => new Check<string>(request.Email).IfEquals("adam@eden.eternal", StringComparison.InvariantCulture).HasErrors(),
    "The email address was set to 'adam@eden.eternal'")
.If(request => new Check<string>(request.User).IfNotMatches("^rya", System.Text.RegularExpressions.RegexOptions.IgnoreCase).HasErrors(),
    "The username does not begin with 'rya'")
.IfNot(request => request.Count > 20 && request.Count < 300,
    "The count was not in the specified range");

Console.WriteLine(mycheck.ReturnErrors(true));
``` 
###
#### Output:

```console
Errors: 1) The email address was null, 2) The timestamp was not set, 3) A person in the list of people did not have a valid email address, 4) The count was not in the specified range, in Program.cs:line 63.
```

Providing a meaningful error message is always a good idea. But you can also leave off the error and it will include the validation expression as the error:

```csharp
var request = new MyServiceRequest();
var mycheck = new Check<MyServiceRequest>(request)
.IfNull()
.If(request => request.Email == null);

Console.WriteLine(mycheck.ReturnErrors(true));
```
###
#### Output:

```console
Errors: 1) If(request => request.Email == null), in Program.cs:line 89.
```

###
## Validations

Each validation rule provides a default error message that includes specifics about the causes of the error. Error messages for user-defined validation rules consist of the logic used in the validation rule or you specify your own custom error.

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

If you want to determine if any validation errors occured, you could call HasErrors() to give you a boolean value.

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

IsValid() returns a boolean value depending on whether all the rules passed validation. If they do, it will return true. If one failed, it will return false. This can be useful for validation scenarios where you don't want to throw an error.

##### ThrowErrors()

ThrowErrors() will throw all the errors in a formatted error message. If you want to include the the filename, line number, and parameter, you can use ThrowErrors(true).

```csharp
// Throwing errors
if (!c.IsValid())
{
    try
    {
        c.ThrowErrors(true);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
```
The expression above, for example, will throw the following errors:
```console
Errors: 1) String length should not equal 10 characters, 2) The string does not contain the keyword, in Program.cs:line 5. (Parameter 'data <String>')
```

##### ReturnErrors()

ReturnErrors() returns the errors as a string that would typically be thrown as an exception. If you want to include the the filename, line number, and parameter you can use ReturnErrors(true).

```csharp
// Returning errors
if (!c.IsValid())
{
    c.ReturnErrors(true);
}
```
The expression above will produce the following errors:
```console
Errors: 1) String length should not equal 10 characters, 2) The string does not contain the keyword, in Program.cs:line 5. (Parameter 'data <String>')
```

### User-Defined Validation Rules

User-defined validation rules allow you to use LINQ to validate your data. When writing user-defined validation rules, you typically want to write your own error message. If you don't, the expression that fails will be displayed in the error.

###
#### If and IfNot

If and IfNot conditions allow you to use LINQ to query objects and lists in the object. If the expression is false, it will produce an error message.

```csharp
If(user => user.User is null)
If(user => user.User is null, "Custom error message.")
// Default Error: If({expression})

IfNot(user => user.User is null)
IfNot(user => user.User is null, "Custom error message.")
// Default Error: IfNot({expression})
```  
###
#### AndIf and AndIfNot

AndIf and AndIfNot are similar to If and IfNot conditions, except that they only execute when the previous If or IfNot condition is valid. This provides better performance, and prevents unnecessary code execution and unnecessary errors. An example may include checking a child value of a value that was previously determined to be null. For example, if a list of people is null, you don't want to check that list for other validation errors because they will all fail. So, in that event, all of the following AndIf and AndIfNot statements are skipped. This can be reset by defining a new If or IfNot validation rule.

```csharp
AndIf(user => user.User is null)
AndIf(user => user.User is null, "Custom error message.")
// Default Error: AndIf({expression})

AndIfNot(user => user.User is null)
AndIfNot(user => user.User is null, "Custom error message.")
// Default Error: AndIfNot({expression})
```
###
#### OrIf and OrIfNot

OrIf and OrIfNot are the opposite of AndIf and AndIfNot and only execute if a previous If rule fails validation. This can be reset by defining a new If or IfNot validation rule.

```csharp
OrIf(user => user.User is null)
OrIf(user => user.User is null, "Custom error message.")
// Default Error: OrIf({expression})

OrIfNot(user => user.User is null)
OrIfNot(user => user.User is null, "Custom error message.")
// Default Error: OrIfNot({expression})
```  
###  
### Built-in Validations

Check validators also include a large number of built-in validation rules. Each of these rules has their own predefined errors that will provide the parameters you are supplying in the error messages.

###
## Extensibility

You can write your own custom extension methods anywhere in your project to write custom validators that are specific to your needs, similar to the following:

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

### General

If you want to throw an error when the object you are checking is null, then you need to include IfNull(). If the object you are checking is null, none of the other validations will be checked. So, this is a good one to include.
```csharp
IfNull()
// Error: The value is null

IfNotNull()  
// Error: The value is not null
```

### String

```csharp
IfNotEmail()
IfNotEmail(RegexPattern, System.Text.RegularExpressions.RegexOptions.None)
// Error: String '{value}' is not an email address

IfNotURL()
IfNotURL(RegexPattern, System.Text.RegularExpressions.RegexOptions.None)
// Error: String {value} is not a URL

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
// Error: String should not match the regular expressions pattern '{pattern}'

IfNotMatches(@"\b[R]\w+")
IfNotMatches(@"\b[R]\w+", System.Text.RegularExpressions.RegexOptions.None)
// Error: String should match the regular expressions pattern '{pattern}'

IfNotDate()
// Error: String {value} is not a date

IfNotInteger()
// Error: String {value} is not an integer

IfNotInt16()
// Error: String {value} is not an Int16

IfNotInt64()
// Error: String {value} is not an Int64

IfNotTimeOnly()
// Error: String {value} is not a TimeOnly

IfNotDateOnly()
// Error: String {value} is not a DateOnly

IfNotDouble()
// Error: String {value} is not a double

IfNotFloat()
// Error: String {value} is not a float

IfEmpty()
// Error: String is empty

IfWhitespace()
// Error: String is whitepace

IfEmptyOrWhitespace()
// Error: String is empty or whitespace

IfEquals("String")
IfEquals("String", StringComparison.InvariantCulture)
// Error: String should not be equal to '{compareString}' [StringComparison: '{compareType}']

IfNotEquals("String")
IfNotEquals("String", StringComparison.InvariantCulture)
// Error: String should be equal to '{compareString}' [StringComparison: '{compareType}']

IfLengthGreaterThan(5)
// Error: String has exceeded the character limit of {length} characters

IfLengthLessThan(5)
// Error: String does not meet the minimum character length of {length} characters

IfLengthEquals(5)
// Error: String length should not equal {length} characters

IfNotLengthEquals(5)
// Error: String length should equal {length} characters

IfEndsWith("abc")
IfEndsWith("abc", StringComparison.InvariantCulture)
// Error: String should not end with '{ending}' [StringComparison: '{compareType}']

IfNotEndsWith("abc")
IfNotEndsWith("abc", StringComparison.InvariantCulture)
// Error: String does not end with '{ending}' [StringComparison: '{compareType}']

IfStartsWith("abc")
IfStartsWith("abc", StringComparison.InvariantCulture)
// Error: String should not start with '{beginning}' [StringComparison: '{compareType}']

IfNotStartsWith("abc")
IfNotStartsWith("abc", StringComparison.InvariantCulture)
// Error: String does not start with '{beginning}' [StringComparison: '{compareType}']

IfContains("abc")
IfContains("abc", StringComparison.InvariantCulture)
// Error: String should not contain '{compare}' [StringComparison: '{compareType}']

IfNotContains("abc")
IfNotContains("abc", StringComparison.InvariantCulture)
// Error: String should contain '{compare}' [StringComparison: '{compareType}']
```  

### Arrays
```csharp
IfEmpty()
// Error: The array is empty

IfNotEmpty()
// Error: The array is not empty

IfCount(5)
// Error: The item count should not be {count}

IfNotCount(5)
// Error: The item count is not {count}

IfCountGreaterThan(5)
// Error: The item count is greater than {count}

IfCountLessThan(5)
// Error: The item count is less than {count}
```
### List
```csharp
IfEmpty()
// Error: The list is empty

IfNotEmpty()
// Error: The list is not empty

IfCount(5)
// Error: The item count should not be {count}

IfNotCount(5)
// Error: The item count is not {count}

IfCountGreaterThan(5)
// Error: The item count is greater than {count}

IfCountLessThan(5)
// Error: The item count is less than {count}
```
### Dictionary
```csharp
IfEmpty()
// Error: Dictionary is empty

IfNotEmpty()
// Error: The Dictionary is not empty

IfCount(5)
// Error: The item count should not be {count}

IfNotCount(5)
// Error: The item count is not {count}

IfCountGreaterThan(5)
// Error: The item count is greater than {count}

IfCountLessThan(5)
// Error: The item count is less than {count}
```
### DateTime
```csharp
IfDaysOlderThan(5)
// Error: The datetime '{value}' is older than {days} days

IfNotDaysOlderThan(5)
// Error: The datetime '{value}' is not older than {days} days

IfMinutesOlderThan(60)
// Error: The datetime '{value}' is older than {minutes} minutes

IfNotMinutesOlderThan(60)
// Error: The datetime '{value}' is not older than {minutes} minutes

IfSecondsOlderThan(60)
// Error: The datetime '{value}' is older than {seconds} seconds

IfNotSecondsOlderThan(60)
// Error: The datetime '{value}' is not older than {seconds} seconds

IfMillisecondsOlderThan(6000)
// Error: The datetime '{value}' is older than {milliseconds} milliseconds

IfNotMillisecondsOlderThan(6000)
// Error: The datetime '{value}' is not older than {milliseconds} milliseconds

IfEarlierThan(dateTime)
// Error: The datetime '{value}' is earlier than '{dateTime}'

IfLaterThan(dateTime)
// Error: The datetime '{value}' is later than '{dateTime}'

IfEqual(dateTime)
// Error: The datetime '{value}' is equal to '{dateTime}'

IfNotEqual(dateTime)
// Error: The datetime '{value}' is not equal to '{dateTime}'

IfBetween(startTime, endTime)
// Error: The datetime '{value}' is between '{startTime}' and '{endTime}'

IfNotBetween(startTime, endTime)
// Error: The datetime '{value}' is not between '{startTime}' and '{endTime}'

IfUtcTime()
// Error: The datetime format is Utc

IfNotUtcTime()
// Error: The datetime format is not Utc

IfLocalTime()
// Error: The datetime format is local

IfNotLocalTime()
// Error: The datetime format is not local

IfUnspecifiedTimeFormat()
// Error: The datetime format is unspecified

IfDayLightSavingsTime()
// Error: The datetime '{value}' is on daylight savings time

IfNotDayLightSavingsTime()
// Error: The datetime '{value}' is not on daylight savings time

IfDefault()
// Error: The datetime is set to the default value

IfNotDefault()
// Error: The datetime is not set to the default value

IfSunday()
// Error: The day of the week should not be Sunday

IfNotSunday()
// Error: The day of the week should be Sunday

IfMonday()
// Error: The day of the week should not be Monday

IfNotMonday()
// Error: The day of the week should be Monday

IfTuesday()
// Error: The day of the week should not be Tuesday

IfNotTuesday()
// Error: The day of the week should be Tuesday

IfWednesday()
// Error: The day of the week should not be Wednesday

IfNotWednesday()
// Error: The day of the week should be Wednesday

IfThursday()
// Error: The day of the week should not be Thursday

IfNotThursday()
// Error: The day of the week should be Thursday

IfFriday()
// Error: The day of the week should not be Friday

IfNotFriday()
// Error: The day of the week should be Friday

IfSaturday()
// Error: The day of the week should not be Saturday

IfNotSaturday()
// Error: The day of the week should be Saturday
```
### DateTimeOffset
```csharp
IfDaysOlderThan(5)
// Error: The datetime '{value}' is older than {days} days

IfNotDaysOlderThan(5)
// Error: The datetime '{value}' is not older than {days} days

IfMinutesOlderThan(60)
// Error: The datetime '{value}' is older than {minutes} minutes

IfNotMinutesOlderThan(60)
// Error: The datetime '{value}' is not older than {minutes} minutes

IfSecondsOlderThan(60)
// Error: The datetime '{value}' is older than {seconds} seconds

IfNotSecondsOlderThan(60)
// Error: The datetime '{value}' is not older than {seconds} seconds

IfMillisecondsOlderThan(6000)
// Error: The datetime '{value}' is older than {milliseconds} milliseconds

IfNotMillisecondsOlderThan(6000)
// Error: The datetime '{value}' is not older than {milliseconds} milliseconds

IfEarlierThan(dateTime)
// Error: The datetime '{value}' is earlier than '{dateTime}'

IfLaterThan(dateTime)
// Error: The datetime '{value}' is later than '{dateTime}'

IfEqual(dateTime)
// Error: The datetime '{value}' is equal to '{dateTime}'

IfNotEqual(dateTime)
// Error: The datetime '{value}' is not equal to '{dateTime}'

IfBetween(startTime, endTime)
// Error: The datetime '{value}' is between '{startTime}' and '{endTime}'

IfNotBetween(startTime, endTime)
// Error: The datetime '{value}' is not between '{startTime}' and '{endTime}'

IfUtcTime()
// Error: The datetime format is Utc

IfNotUtcTime()
// Error: The datetime format is not Utc

IfDayLightSavingsTime()
// Error: The datetime '{value}' is on daylight savings time

IfNotDayLightSavingsTime()
// Error: The datetime '{value}' is not on daylight savings time

IfDefault()
// Error: The datetime is set to the default value

IfNotDefault()
// Error: The datetime is not set to the default value

IfSunday()
// Error: The day of the week should not be Sunday

IfNotSunday()
// Error: The day of the week should be Sunday

IfMonday()
// Error: The day of the week should not be Monday

IfNotMonday()
// Error: The day of the week should be Monday

IfTuesday()
// Error: The day of the week should not be Tuesday

IfNotTuesday()
// Error: The day of the week should be Tuesday

IfWednesday()
// Error: The day of the week should not be Wednesday

IfNotWednesday()
// Error: The day of the week should be Wednesday

IfThursday()
// Error: The day of the week should not be Thursday

IfNotThursday()
// Error: The day of the week should be Thursday

IfFriday()
// Error: The day of the week should not be Friday

IfNotFriday()
// Error: The day of the week should be Friday

IfSaturday()
// Error: The day of the week should not be Saturday

IfNotSaturday()
// Error: The day of the week should be Saturday
```
### TimeOnly
```csharp
IfDefault()
// Error: The timeonly is set to the default value of {value}

IfNotDefault()
// Error: The timeonly '{value}' is not set to the default value

IfMinValue()
// Error: The timeonly is set to the minimum value of {value}

IfNotMinValue()
// Error: The timeonly '{value}' is not set to the minimum value of {TimeOnly.MinValue}

IfMaxValue()
// Error: The timeonly is set to the maximum value of {value}

IfNotMaxValue()
// Error: The timeonly '{value}' is not set to the maximum value of {TimeOnly.MaxValue}

IfBetween(startTime, endTime)
// Error: The timeonly is between '{startTime}' and '{endTime}'

IfNotBetween(startTime, endTime)
// Error: The timeonly is not between '{startTime}' and '{endTime}'
```
### DateOnly
```csharp
IfDefault()
// Error: The dateonly is set to the default value of {value}

IfNotDefault()
// Error: The dateonly '{value}' is not set to the default value

IfMinValue()
// Error: The dateonly is set to the minimum value of {value}

IfNotMinValue()
// Error: The dateonly '{value}' is not set to the minimum value of {DateOnly.MinValue}

IfMaxValue()
// Error: The dateonly is set to the maximum value of {value}

IfNotMaxValue()
// Error: The dateonly '{value}' is not set to the maximum value of {DateOnly.MaxValue}
```
### Double
```csharp
IfBetween(startValue, endValue)
// Error: The number '{value}' is between '{startValue}' and '{endValue}'

IfBetweenOrEqual(startValue, endValue)
// Error: The number '{value}' is between or equal to '{startValue}' and '{endValue}'

IfNotBetween(startValue, endValue)
// Error: The number '{value}' is not between '{startValue}' and '{endValue}'

IfNegative()
// Error: The double is negative

IfPositive()
// Error: The double is positive

IfZero()
// Error: The double is zero

IfNotZero()
// Error: The double is not zero

IfGreaterThan(5)
// Error: The double is greater than {value}

IfLessThan(5)
// Error: The double is less than {value}

IfEquals(5)
// Error: The double should not be {value}

IfNotEquals(5)
// Error: The double should be {value}
```
### Float
```csharp
IfBetween(startValue, endValue)
// Error: The float '{value}' is between '{startValue}' and '{endValue}'

IfBetweenOrEqual(startValue, endValue)
// Error: The float '{value}' is between or equal to '{startValue}' and '{endValue}'

IfNotBetween(startValue, endValue)
// Error: The float '{value}' is not between '{startValue}' and '{endValue}'

IfNegative()
// Error: The float is negative

IfPositive()
// Error: The float is positive

IfZero()
// Error: The float is zero

IfNotZero()
// Error: The float is not zero

IfGreaterThan(5)
// Error: The float is greater than {value}

IfLessThan(5)
// Error: The float is less than {value}

IfEquals(5)
// Error: The float should not be {value}

IfNotEquals(5)
// Error: The float should be {value}
```
### Short (Int16)
```csharp
IfBetween(startValue, endValue)
// Error: The number '{value}' is between '{startValue}' and '{endValue}'

IfBetweenOrEqual(startValue, endValue)
// Error: The number '{value}' is between or equal to '{startValue}' and '{endValue}'

IfNotBetween(startValue, endValue)
// Error: The number '{value}' is not between '{startValue}' and '{endValue}'

IfNegative()
// Error: The number is negative

IfPositive()
// Error: The number is positive

IfZero()
// Error: The number is zero

IfNotZero()
// Error: The number is not zero

IfGreaterThan(5)
// Error: The number is greater than {value}

IfLessThan(5)
// Error: The number is less than {value}

IfEquals(5)
// Error: The number should not be {value}

IfNotEquals(5)
// Error: The number should be {value}
```
### Int (Int32)
```csharp
IfBetween(startValue, endValue)
// Error: The number '{value}' is between '{startValue}' and '{endValue}'

IfBetweenOrEqual(startValue, endValue)
// Error: The number '{value}' is between or equal to '{startValue}' and '{endValue}'

IfNotBetween(startValue, endValue)
// Error: The number '{value}' is not between '{startValue}' and '{endValue}'

IfNegative()
// Error: The number is negative

IfPositive()
// Error: The number is positive

IfZero()
// Error: The number is zero

IfNotZero()
// Error: The number is not zero

IfGreaterThan(5)
// Error: The number is greater than {value}

IfLessThan(5)
// Error: The number is less than {value}

IfEquals(5)
// Error: The number should not be {value}

IfNotEquals(5)
// Error: The number should be {value}
```
### Long (Int64)
```csharp
IfBetween(startValue, endValue)
// Error: The number '{value}' is between '{startValue}' and '{endValue}'

IfBetweenOrEqual(startValue, endValue)
// Error: The number '{value}' is between or equal to '{startValue}' and '{endValue}'

IfNotBetween(startValue, endValue)
// Error: The number '{value}' is not between '{startValue}' and '{endValue}'

IfNegative()
// Error: The number is negative

IfPositive()
// Error: The number is positive

IfZero()
// Error: The number is zero

IfNotZero()
// Error: The number is not zero

IfGreaterThan(5)
// Error: The number is greater than {value}

IfLessThan(5)
// Error: The number is less than {value}

IfEquals(5)
// Error: The number should not be {value}

IfNotEquals(5)
// Error: The number should be {value}
```
### BigInteger
```csharp
IfBetween(startValue, endValue)
// Error: The number '{value}' is between '{startValue}' and '{endValue}'

IfBetweenOrEqual(startValue, endValue)
// Error: The number '{value}' is between or equal to '{startValue}' and '{endValue}'

IfNotBetween(startValue, endValue)
// Error: The number '{value}' is not between '{startValue}' and '{endValue}'

IfNegative()
// Error: The number is negative

IfPositive()
// Error: The number is positive

IfZero()
// Error: The number is zero

IfNotZero()
// Error: The number is not zero

IfGreaterThan(5)
// Error: The number is greater than {value}

IfLessThan(5)
// Error: The number is less than {value}

IfEquals(5)
// Error: The number should not be {value}

IfNotEquals(5)
// Error: The number should be {value}
```
### Decimal
```csharp
IfBetween(startValue, endValue)
// Error: The decimal '{value}' is between '{startValue}' and '{endValue}'

IfBetweenOrEquals(startValue, endValue)
// Error: The decimal '{value}' is between or equal to '{startValue}' and '{endValue}'

IfNotBetween(startValue, endValue)
// Error: The decimal '{value}' is not between '{startValue}' and '{endValue}'

IfNegative()
// Error: The decimal is negative

IfPositive()
// Error: The decimal is positive

IfZero()
// Error: The decimal is zero

IfNotZero()
// Error: The decimal is not zero

IfGreaterThan(5)
// Error: The decimal is greater than {value}

IfLessThan(5)
// Error: The decimal is less than {value}

IfEquals(5)
// Error: The decimal should not be {value}

IfNotEquals(5)
// Error: The decimal should be {value}
```
### UShort (UInt16)
```csharp
IfBetween(startValue, endValue)
// Error: The number '{value}' is between '{startValue}' and '{endValue}'

IfBetweenOrEqual()
// Error: The number '{value}' is between or equal to '{startValue}' and '{endValue}'

IfNotBetween(startValue, endValue)
// Error: The number '{value}' is not between '{startValue}' and '{endValue}'

IfNegative()
// Error: The number is negative

IfPositive()
// Error: The number is positive

IfZero()
// Error: The number is zero

IfNotZero()
// Error: The number is not zero

IfGreaterThan(5)
// Error: The number is greater than {value}

IfLessThan(5)
// Error: The number is less than {value}

IfEquals(5)
// Error: The number should not be {value}

IfNotEquals(5)
// Error: The number should be {value}
```
### UInt (UInt32)
```csharp
IfBetween(startValue, endValue)
// Error: The number '{value}' is between '{startValue}' and '{endValue}'

IfBetweenOrEqual(startValue, endValue)
// Error: The number '{value}' is between or equal to '{startValue}' and '{endValue}'

IfNotBetween(startValue, endValue)
// Error: The number '{value}' is not between '{startValue}' and '{endValue}'

IfNegative()
// Error: The number is negative

IfPositive()
// Error: The number is positive

IfZero()
// Error: The number is zero

IfNotZero()
// Error: The number is not zero

IfGreaterThan(5)
// Error: The number is greater than {value}

IfLessThan(5)
// Error: The number is less than {value}

IfEquals(5)
// Error: The number should not be {value}

IfNotEquals(5)
// Error: The number should be {value}
```
### ULong (UInt64)
```csharp
IfBetween(startValue, endValue)
// Error: The number '{value}' is between '{startValue}' and '{endValue}'

IfBetweenOrEqual(startValue, endValue)
// Error: The number '{value}' is between or equal to '{startValue}' and '{endValue}'

IfNotBetween(startValue, endValue)
// Error: The number '{value}' is not between '{startValue}' and '{endValue}'

IfNegative()
// Error: The number is negative

IfPositive()
// Error: The number is positive

IfZero()
// Error: The number is zero

IfNotZero()
// Error: The number is not zero

IfGreaterThan(5)
// Error: The number is greater than {value}

IfLessThan(5)
// Error: The number is less than {value}

IfEquals(5)
// Error: The number should not be {value}

IfNotEquals(5)
// Error: The number should be {value}
```
### Enum
```csharp
IfContainsValue(value)
// Error: The enum contains the value '{enumvalue}'

IfNotContainsValue(value)
// Error: The enum does not contain the value '{enumvalue}'

IfDefined(value)
// Error: The enum should not define a value for index {enumvalue}
// Error: The enum should not define a value '{enumvalue}'

IfNotDefined(value)
// Error: The enum does not define a value for index {enumvalue}
// Error: The enum does not define the value '{enumvalue}'
```
### Uri
```csharp
IfScheme()
// Error: Uri scheme should not be '{scheme}'

IfNotScheme()
// Error: Uri scheme is not '{scheme}'

IfAbsoluteUri()
// Error: Uri is absolute, consider changing it to relative

IfRelativeUri()
// Error: Uri is relative, consider changing it to absolute

IfUriPort(80)
// Error: Uri port should not be {port}

IfNotUriPort(80)
// Error: Uri port should be {port}

IfFile()
// Error: Uri is a file path

IfNotFile()
// Error: Uri is not a file

IfUnc()
// Error: Uri is a UNC path

IfNotUnc()
// Error: Uri is not a UNC path

IfLoopback()
// Error: Uri is the loopback address

IfNotLoopback()
// Error: Uri is not the loopback address
```

###
## Contributions

This project is being developed for free by me, Ryan Kueter, in my spare time. So, if you would like to contribute, please submit your ideas on the Github project page.
# Express Delivery Service
Delivery service information system. Made by order of my professor.
There are 3 users in the system: client, admin and employee.

* ### Client
The client can **create**, **update** and **delete** his orders,  
as well as change personal data in his personal account.

* ### Employe
The employee can **accept** the order, fulfill it or **refuse**,  
change personal data in the personal account.  
A system is available for him, in which it becomes possible  
to select the order that is closest to the employee.
> *A similar system exists in all aggregators for dating*.

* ### Admin
The administrator can **delete**, **update** all orders.  
And, if desired, create new ones. He can also change his account data   
if he sees fit.   
> **He does not have access to changing customer and employee data**.

### Details
---

* Implemented cascading update and deletion of data
* An algorithm for calculating the cost of the order has been implemented  
> Which depends not only on the incoming parameters in the form of the cost of  
the cargo or the size of the box. And also from the distance where you need to deliver this package.  

#### Formula:  
```csharp
TotalCost += BoxHeight * BoxLenght * BoxWidth / 30;   // The cost depends on the size of the box.
 
TotalCost += ProductCost / 5;   // The cost also depends on the value of the item being transported.

TotalCost += ProductWeight / 10;   // And also from his weight.

if (AvailabilityOfInsurancePurchased)  // It is possible to choose insurance if you want,
  TotalCost += ProductCost / 10;       // but then the cost will increase by 10% of the value of the transported item.
  
if (PackagingPurchased)   // Packing the goods in a special way will also increase the cost of the order.
  TotalCost += 150;
  
if (ComplianceTemperatureRegimePurchased)      // If the goods require proper temperature conditions during transportation.
  TotalCost += TotalCost / 100 * 20; // + 20%  //Then its cost of the entire order will increase by 20%
  
double distance = /* Here is the distance between the points. */ ;

TotalCost += distance / 1000 / 100 * 20; // The cost of transportation also depends on the distance.
```

## Technologies and Patterns
* ### [WPF (Model - View - ViewModel Pattern)](https://github.com/dotnet/wpf)
UI rendering technology and architecture design pattern.
* ### [Entity Framework](https://github.com/dotnet/ef6)
Used as an ORM. I used SQLite as a database because I needed 
a simple and fast solution that would allow me to store data in a structured way.
* ### [Gmap](https://github.com/judero01col/GMap.NET) 
Used for working with google maps.

This is iBizProduct v3!!! Welcome...

At the heart of this library is the iBizAPIClient. From that single class you can easily call any of the API Functions that are 
available to a Product.

This also includes all of the data contracts you will require and a number of utilities to make your life easier. Lastly there are 
some base models that should assist you in developing your database schema. Please note that there is no Marketplace support in
the iBizProduct.Common library for version 3.0.0.*.

-----------------------------------------------------------------------------------------------------------------------------------
iBizAPIClient Functionality
Product Order's
Add a new order: iBizAPIClient.ProductOrderAdd
Edit an existing order: iBizAPIClient.ProductOrderEdit
View an existing order: iBizAPIClient.ProductOrderView

CASES:
Open a New Case: iBizAPIClient.ProductOpenCaseWithOwner
Update an Existing Case: iBizAPIClient.ProductUpdateCaseWithOwner ( Will error out if the case has already been closed. )

General Functionality:
Make a one time charge ( On Demand Billing ): iBizAPIClient.ProductOrderBillOrderAddOneTime
Get the next Charge Date for a specific order: iBizAPIClient.GetNextChargeDate
Get the preferred language of the ProductOrder's owner: iBizAPIClient.GetOwnerLanguage

Event Handling:
Update a requested Event: iBizAPIClient.UpdateEvent

-----------------------------------------------------------------------------------------------------------------------------------
Models
The iBizProduct.Common library includes template Models for Products to use, and a complete Entity Framework Model for it's own
database for Marketplace implementations.

-----------------------------------------------------------------------------------------------------------------------------------
Utilities
DateTimeExtensions: Allow you to convert to & from Linux Timestamps from a give DateTime Instance
String Extensions: Adds functions to Convert first letters to UpperCase and to convert the string to an EventAction Enum
UnixTime: Provides a framework for working with UnixTimes
ProductOrderUtility: Provides an ability to determine what the appropriate status should be for a particular order.
EventLogCollection: Provides a framework for create a list of Event Logs which can be created in the Event Logger
EventLogger: Encapsulates the EventLog class to provide an easier to use interface for creating custom logging experiences.

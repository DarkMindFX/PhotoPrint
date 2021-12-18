REM ################# Cleaning the file

break > ".\docker\sql\create_db.sql"

REM ################# Logins

for /r ".\logins" %%F in (*.sql) do @type "%%F" >> ".\docker\sql\create_db.sql"

REM ################# DB
type ".\CreateDB.sql" >> ".\docker\sql\create_db.sql"

REM ################# Users

for /r ".\users" %%F in (*.sql) do @type "%%F" >> ".\docker\sql\create_db.sql"

REM ################# Tables
type ".\tables\dbo.UserStatus.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.UserType.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.OrderStatus.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.Unit.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.User.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.AddressType.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.Category.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.ContactType.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.Country.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.Region.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.City.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.Address.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.Contact.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.Currency.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.DeliveryService.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.DeliveryServiceCity.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.FrameType.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.Image.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.ImageCategory.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.ImageRelated.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.ImageThumbnail.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.Mat.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.MaterialType.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.MountingType.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.Order.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.Size.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.PrintingHouse.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.PaymentMethod.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.OrderItem.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.OrderPaymentDetails.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.OrderStatusFlow.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.OrderTracking.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.PrintingHouseAddress.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.PrintingHouseContact.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.UserConfirmation.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.UserContact.Table.sql" >> ".\docker\sql\create_db.sql"

type ".\tables\dbo.UserAddress.Table.sql" >> ".\docker\sql\create_db.sql"

REM ################# Views

for /r ".\views" %%F in (*.sql) do @type "%%F" >> ".\docker\sql\create_db.sql"

REM ################# Functions

for /r ".\functions" %%F in (*.sql) do @type "%%F" >> ".\docker\sql\create_db.sql"

REM ################# StorProcs

for /r ".\storprocs" %%F in (*.sql) do @type "%%F" >> ".\docker\sql\create_db.sql"

REM ################# Populate Test Data

type ".\PopulateReferenceData.sql" >> ".\docker\sql\create_db.sql"




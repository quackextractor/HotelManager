# Hotel Manager Application

A comprehensive Windows Forms application for managing hotel reservations, guests, rooms, and payments.

## Features

- **Order Management**
  - Create new reservations with room assignments
  - Edit existing orders
  - Track payment status (paid/unpaid)
  - Multiple order statuses (pending/confirmed)

- **Guest Management**
  - Add/remove guests with personal details
  - Email validation and unique guest identification
  - Guest status tracking (active/inactive)

- **Room Management**
  - Add new rooms with capacity and pricing
  - Automatic room availability updates
  - Room type categorization

- **Payment Processing**
  - Record payments with multiple methods
  - Payment history tracking
  - Edit/delete payment records

- **Data Import**
  - Bulk import from XML files
  - Drag-and-drop XML loading
  - Supports rooms, guests, orders, and payments

- **Search & Reporting**
  - Multi-criteria order search
  - Double-click to edit search results
  - Real-time data grid updates

## Installation

### Requirements
- .NET Framework 4.7.2+
- SQL Server Express LocalDB / external DB
- Windows OS
- System.Configuration.ConfigurationManager
- Microsoft.Data.SqlClient

### Setup
1. Clone repository
2. Configure database connection:
   ```xml
   <!-- App.config -->
   <connectionStrings>
     <add name="ConnectionString" 
          connectionString="Server=(localdb)\localDB1;Database=HotelDB;Integrated Security=True;TrustServerCertificate=true"
          providerName="System.Data.SqlClient"/>
   </connectionStrings>
   ```
3. Create `HotelDB` database in SQL Server
4. Build solution in Visual Studio

## Usage

### Creating a New Order
1. From main menu: New Order
2. Set date of arrival
3. Select room from dropdown or add a new
4. Add guests using Add guest button
5. Set check-in date and price/night
6. Save order

### Importing Data
1. Use main menu: Tools → Import Data
2. Drag-and-drop XML file or browse
3. Supported XML structure:

```xml
<Tableset>
    <Rooms>
        <Room RoomNumber="102" RoomType="Twin" Capacity="2" Price="110.00"/>
    </Rooms>
    <Persons>
        <Person FirstName="Emma" LastName="Brown" Email="emma@example.com"
                Status="VIP" RegistrationDate="2023-03-20" LastVisitDate="2023-09-25"/>
    </Persons>
    <Orders>
        <Order XmlId="ORD-VIP1" RoomNumber="VIP-1" PricePerNight="500.00" Nights="5"
               OrderDate="2025-11-15" CheckinDate="2025-11-20" Status="confirmed" Paid="true"/>
    </Orders>
    <OrderRoles>
        <OrderRole OrderXmlId="ORD-VIP1" PersonEmail="emma@example.com" Role="Guest"/>
    </OrderRoles>
    <Payments>
        <Payment OrderXmlId="ORD-VIP1" Amount="2500.00" PaymentDate="2025-11-15"
                 PaymentMethod="CreditCard" Note="Full payment"/>
    </Payments>
</Tableset>
   ```

### Processing Payments
1. Search for order
2. Double-click to open details
3. Payments tab → Add Payment
4. Select method (Cash/Credit Card/etc.)
5. Enter amount and notes

## Validation Rules
- **Prices**: Decimal numbers with max 2 decimal places
- **Emails**: Standard format validation
- **Dates**: Check-in cannot be in past

## Troubleshooting

| Issue | Solution |
|-------|----------|
| Database connection failed | Verify LocalDB instance is running |
| XML import errors | Check XML structure matches sample |
| Duplicate email | Use unique email for each guest |
| Past check-in date | Set date to today or future |

## License
Free for educational use. Contains AI-generated components from DeepSeek AI model.

![DeepSeek Logo](https://upload.wikimedia.org/wikipedia/commons/e/ec/DeepSeek_logo.svg)Hangzhou DeepSeek Artificial Intelligence Co., Ltd.
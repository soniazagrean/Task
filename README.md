# Task Siemens 2026

## Problem 1
This project is a digitization of operations for Sarah's coffee shop chain. The system is designed to handle orders, customize beverages, track barista performance, and manage a customer loyalty program.

**Features:**
* **1:** Supports various drinks (Espresso, Latte, Cappuccino) across three sizes (Small, Medium, Large) with dynamic pricing.
* **2:** Allows adding extras (extra shot, vanilla syrup, caramel syrup, whipped cream) to any drink.
* **3:** Tracks customer purchases to award points. Regular members earn 1 point per euro, while Gold members earn 2 points per euro. Points can be redeemed for free drinks.
* **4:** Records which barista prepared which order, along with timestamps and total order prices.

### 1.1 Class Diagram
The UML diagram below outlines the system's object-oriented architecture. It uses a composition relationship between Order and OrderItem to ensure data integrity and includes a BeveragePrice class to handle the dynamic pricing of different sizes for each drink type.

```mermaid
classDiagram
    class Customer {
        -int id
        -String name
        -String memberType
        -int points
        +earnPoints(double amount)
        +redeemPoints(int points)
    }
    class Barista {
        -int id
        -String name
        +prepareOrder(Order order)
    }
    class Order {
        -int id
        -Date timestamp
        -double totalPrice
        +calculateTotal()
        +addItem(OrderItem item)
    }
    class OrderItem {
        -int id
        -int quantity
        -List~Extra~ extras
        +calculateItemPrice()
    }
    class Beverage {
        -int id
        -String name
    }
    class BeveragePrice {
        -String size
        -double price
    }
    class Extra {
        -int id
        -String name
        -double price
    }

    Customer "1" -- "*" Order
    Barista "1" -- "*" Order
    Order "1" *-- "*" OrderItem
    OrderItem "1" --> "1" BeveragePrice
    Beverage "1" -- "*" BeveragePrice
    OrderItem "1" --> "*" Extra
```

### 1.2 Database diagram
The relational schema is designed for 3rd Normal Form (3NF) normalization. It successfully resolves the many to many relationship between orders and extras using a junction table (ORDER_ITEM_EXTRA) and ensures pricing consistency through BEVERAGE_CONFIG.


``` mermaid
erDiagram
    CUSTOMER ||--o{ ORDER : "(1:m)"

    BARISTA ||--o{ ORDER : "(1:m)"
    
    ORDER ||--|{ ORDER_ITEM : "(1:m)"

    BEVERAGE ||--|{ BEVERAGE_CONFIG : " (1:m)"
    
    ORDER_ITEM ||--o{ ORDER_ITEM_EXTRA : "(1:m)"

    EXTRA ||--o{ ORDER_ITEM_EXTRA : "(1:m)"
    
    ORDER_ITEM }|--|| BEVERAGE_CONFIG : "(m:1)"

    

    CUSTOMER {
        int CustomerID PK
        string Name
        string MemberType
        int Points
    }
    
    ORDER {
        int OrderID PK
        int CustomerID FK
        int BaristaID FK
        datetime OrderTimestamp
        decimal TotalPrice
    }
    
    BEVERAGE_CONFIG {
        int ConfigID PK
        int BeverageID FK
        string Size
        decimal Price
    }
    
    BEVERAGE {
        int BeverageID PK
        string Name
    }

    ORDER_ITEM {
        int OrderItemID PK
        int OrderID FK
        int ConfigID FK
        decimal FinalItemPrice
    }

    EXTRA {
        int ExtraID PK
        string Name
        decimal Price
    }
    
    ORDER_ITEM_EXTRA {
        int OrderItemID PK, FK
        int ExtraID PK, FK
    }
```
 ECommerceOMS

A role-based e-commerce Order Management System built with ASP.NET Core MVC, Entity Framework Core, ASP.NET Core Identity, Azure SQL Database, and Azure App Service.

## Live Demo

https://cmps-ecommerce-gzgtg5dzh2d6gffb.canadacentral-01.azurewebsites.net/

---

## Features

### Authentication & Authorization
- ASP.NET Core Identity authentication
- Role-based authorization
- Super Admin role
- Admin role
- Seller role
- Buyer role

### Product Management
- Create products
- Edit products
- Product image uploads
- Product category assignment
- Product activation/deactivation
- Inventory tracking

### Shopping Experience
- Browse active products
- Category filtering
- Dynamic product discounts
- Shopping cart management
- Inventory-aware cart validation
- Checkout workflow

### Orders
- Order creation from cart
- Order history
- Order details
- Inventory deduction upon purchase
- Payment and order status tracking

### Administration
- User management
- Product management
- Cart management
- Order management
- Payment status updates
- Order status updates

---

## Technologies Used

- ASP.NET Core MVC
- ASP.NET Core Identity
- Entity Framework Core
- SQL Server
- Azure SQL Database
- Azure App Service
- GitHub Actions
- Razor Views
- HTML
- CSS
- JavaScript

---

## Demo Accounts

### Super Admin

Email:
```text
superadmin@test.com
```

Password:
```text
SuperAdmin123!
```

### Admin

Email:
```text
admin@test.com
```

Password:
```text
Admin123!
```

### Buyer

Email:
```text
buyer@test.com
```

Password:
```text
Buyer123!
```

### Seller 1

Email:
```text
seller1@test.com
```

Password:
```text
Seller123!
```

### Seller 2

Email:
```text
seller2@test.com
```

Password:
```text
Seller123!
```

### Seller 3

Email:
```text
seller3@test.com
```

Password:
```text
Seller123!
```

---

## Dynamic Product Discounts

Product discounts are calculated dynamically based on inventory levels.

Example:

```text
40+ units in stock = 20% discount
20+ units in stock = 10% discount
Below 20 units = No discount
```

Discounted pricing is displayed on the storefront and carried through to cart and order calculations.

---

## Product Categories

Products can be assigned to the following categories:

- Peripherals
- Components
- Monitors
- Accessories
- Furniture

Products can be filtered by category directly from the storefront.

---

## AI Usage Disclosure

AI tools were used to assist with brainstorming ideas, debugging issues, researching implementation approaches, and answering development questions throughout this project. All implementation decisions, code integration, testing, deployment, and final submitted work were completed and verified by the author.

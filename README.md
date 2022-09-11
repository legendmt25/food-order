# Food order

Apply migrations
```
dotnet ef database update -s food-order/Web -p food-order/Repository
```

Step 1
Add configuration json files

food-order/Web/appsettings.Development.json
```
{
  "mailConfig": {
    "from": "Your email",
    "host": "smtp.gmail.com if you are using gmail",
    "port": 587,
    "username": "Your email",
    "password": "Your password"
  },
  "paymentConfig": {
    "merchantId": "Your merchant id",
    "publicKey": "Your public key",
    "privateKey": "Your private key"
  },
  "jwtConfig": {
    "validIssuer": "https://localhost:5001",
    "validAudience": "https://localhost:5001",
    "secretKey": "eThWmZq4t7w!z%C*"
  },
  "ConnectionStrings": {
    "sqlite": "Data Source=food-order.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```


food-order/Web/appsettings.json
```
{
  "mailConfig": {
    "from": "Your email",
    "host": "smtp.gmail.com if you are using gmail",
    "port": 587,
    "username": "Your email",
    "password": "Your password"
  },
  "paymentConfig": {
    "merchantId": "Your merchant id",
    "publicKey": "Your public key",
    "privateKey": "Your private key"
  },
  "jwtConfig": {
    "validIssuer": "https://localhost:5001",
    "validAudience": "https://localhost:5001",
    "secretKey": "eThWmZq4t7w!z%C*"
  },
  "ConnectionStrings": {
    "sqlite": "Data Source=food-order.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
  "AllowedHosts": "*"
}
```

Step 2
```
dotnet ef database update -s food-order/Web -p food-order/Repository
dotnet watch run food-order/Web
```

Step 3
```
cd food-order-client
npm run generate-types
npm run start
```
###### *Step 2 is required for generate-types to work


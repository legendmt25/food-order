# Food order

Apply migrations
```
dotnet ef database update -s food-order/Web -p food-order/Repository
```

Step 1
```
dotnet watch run food-order/Web
```

Step 2
```
cd food-order-client
npm run generate-types
npm run start
```
###### *Step 1 is required for generate-types to work

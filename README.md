# customer-app

### Prerequisites
dotnet --version
2.1.4

### Clone or unpack zip
git clone https://github.com/krisbien/customer-app.git

### Build Angular client app
cd customer-app
cd ClientApp\customer
npm install
ng build

### Build .Net core WebApi service
cd ../../CustomerWebApi

dotnet restore
dotnet ef database update
dotnet run

Open browser as instructed in the console window:
http://localhost:57429

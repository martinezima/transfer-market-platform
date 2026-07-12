# Transfer Market Platform
Crud operations for football players information.

## Versions
.NET 10 for All backend.

Angular v15 for front end.

## How to build application
```
clear && dotnet clean && dotnet build
```

## How to run tests (dotnet solution)
```
dotnet test TransferMarketPlatform.slnx
```


## How to run application
Web API:
```
dotnet run --project WebApi/TransferMarketPlatform.WebApi.csproj
```
Angular Project:
```
npm start
```
or
```
ng serve -o
```
## Code Coverage (dotnet only)
```
TransferMarketPlatform.Application --> 50%

TransferMarketPlatform.Domain --> 50%

TransferMarketPlatform.Infrastructure --> 83.3%
```

## Approach used for build this application

Initally decided to create Repo on GitHub first, then clone locally:
- `Remote exists immediately:` No extra steps to link local repo to GitHub
- `Automatic setup:` GitHub creates README, .gitignore, and license templates if you want them
- `No manuel remote configuration:` git clone automatically sets up the remote origin
- `Easier collaboration:` Repository is accessible to others from the start
- `No push errors:` Repository is accessible to others from the start

### Multiplatform Development Environments
I have experience working with both Windows and Ubuntu. On both platforms, I use Visual Studio Code, and I rely on Git Bash for version control operations.

### AI Tools
- GitHub Copilot integrated with VSCode
- Gemini google
- Deepseek 

### Regular commands
From my computer I have created the solution for dotnet:
```
m@m:~/Source/repos/transfer-market-platform$ dotnet new classlib -o TransferMarketPlatform.Domain
// Renaming the folder
m@m:~/Source/repos/transfer-market-platform$ mv TransferMarketPlatform.Domain/ Domain

m@m:~/Source/repos/transfer-market-platform$ dotnet new classlib -o TransferMarketPlatform.Application
// Renaming the folder
m@m:~/Source/repos/transfer-market-platform$ mv TransferMarketPlatform.Application/ Application

m@m:~/Source/repos/transfer-market-platform$ dotnet new classlib -o TransferMarketPlatform.Infrastructure
// Renaming the folder
m@m:~/Source/repos/transfer-market-platform$ mv TransferMarketPlatform.Infrastructure/ Infrastructure

m@m:~/Source/repos/transfer-market-platform$ dotnet new webapi -o TransferMarketPlatform.WebApi
// Renaming the folder
m@m:~/Source/repos/transfer-market-platform$ mv TransferMarketPlatform.WebApi/ WebApi

```
From my computer I have created Angular project:
```
npx @angular/cli@15 new transfer-market-web-app
ng g c component
ng g s service
ng g i interface
```

### Git custom shortcuts
- gits --> git status
- giti --> git commit
- git lg --> git log
- fa --> git fetch --all
- git br --> git branch
- git dy --> git diff (I use Meld for comparision tool)



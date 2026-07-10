# transfer-market-platform
Crud operations for football players information.

Initally decided to create Repo on GitHub first, then clone locally:
- `Remote exists immediately:` No extra steps to link local repo to GitHub
- `Automatic setup:` GitHub creates README, .gitignore, and license templates if you want them
- `No manuel remote configuration:` git clone automatically sets up the remote origin
- `Easier collaboration:` Repository is accessible to others from the start
- `No push errors:` Repository is accessible to others from the start


From my computer I have created the solution without front end:
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


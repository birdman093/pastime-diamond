# pastime-diamond
Game/Chat application powered by .NET/SignalR/Typescript

## Current Status
In Development

## technology description
TBD

## Techologies Used
.NET 7.0<br>
ASP.NET<br>
SignalR<br>
Typescript<br>
OAuth<br>

## Build Instructions
0 - Download npm packages
```
npm install
```
1 - Bundle/ Transpile w/ Webpack
```
npm run release
```
2 - Run Server
```
From VS Studio: 
Run Debug

Terminal:
dotnet restore 
dotnet build
dotnet build --configuration Debug
dotnet run --project SignalRWebpack.csproj
```

## Next steps
### Authentication, Authorization
- [ ] Set up OAuth Login controller connections
- [ ] OAuth Access tokens
- [ ] Verify Chats/ Games are “private”

### Groups
- [ ] Creating groups, adding users to groups
- [ ] DB storage of groups (SqLite, Entity Framework) - Railway?

### Game Chat
- [ ] Initialize game from chat/ or button
- [ ] Modify web page for all users at that point? 
- [ ] DB storage of chats (see above)

### Game Dev
- [ ] Strategy TBD

# Finança Simples

This API must provide all features for any front-end or mobile app to help people control, map, and manage their financial life. This a simple way to know where and when you waste your precious money!

## RUN MIGRATIONS AND APPLY 

_cd Financas-Simples-Backend/src/backend/FS.Infrastructure_ 

_dotnet ef migrations add Initial --project FS.Data.csproj --startup-project ../FS.Api/FS.Api.csproj_

_dotnet ef database update --project FS.Data.csproj --startup-project ../FS.Api/FS.Api.csproj_


## BUILD AND RUN

  _dotnet restore_

  _dotnet build_

  _dotnet run_


### Features (UNDER CONSTRUCTION)

- [x] Create and update.
- [x] Create a account.
- [x] Add updatde and remove single expenses

**COMMING SOON**
- [ ] Create recurrent expenses
- [ ] Create on credit expenses


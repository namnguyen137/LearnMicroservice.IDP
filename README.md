
## Create migration 
dotnet ef migrations add "init" -c NameOfContext -o Persistence/Migrations

## Identity has 2 context by system: 
dotnet ef migrations add "init" -c ConfigurationDbContext -o Persistence/Migrations/ConfigurationDb
dotnet ef migrations add "init" -c PersistedGrantDbContext -o Persistence/Migrations/PersistedGrantDb


## *****Update databse
dotnet ef database update -c TeduIdentityContext


## Create Procedure in SQL 


CREATE PROCEDURE TenProcedure
    @ThamSo1 KieuDuLieu,
    @ThamSo2 KieuDuLieu
AS
BEGIN
    -- Logic ở đây
    SELECT * FROM TenBang WHERE Cot1 = @ThamSo1;
END

## Excuce
EXEC Get_Permission_ByRoleId @roleId = 'admin';
if not exists(select * from sys.schemas where [name] = '#schema#')
begin
    EXEC ('CREATE SCHEMA [#schema#] AUTHORIZATION [dbo]')
end

if not exists(select object_id from sys.tables where [name] = 'Apps')
begin
    create table #schema#.Apps (
        Id integer not null primary key identity,
        Slug nvarchar(128) not null,
        DisplayText nvarchar(1024) not null
    )
end
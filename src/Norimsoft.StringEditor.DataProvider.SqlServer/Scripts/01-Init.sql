if not exists(select *
              from sys.schemas
              where [name] = '#schema#')
    begin
        EXEC ('CREATE SCHEMA [#schema#] AUTHORIZATION [dbo]')
    end

if not exists(select object_id
              from sys.tables
              where [name] = 'Apps')
    begin
        create table #schema#.Apps
        (
            Id          integer        not null primary key identity,
            Slug        nvarchar(128)  not null,
            DisplayText nvarchar(1024) not null
        )
    end

if not exists(select object_id
              from sys.tables
              where [name] = 'Releases')
    begin
        create table #schema#.Releases
        (
            Id        integer        not null primary key identity,
            AppFk     int            not null,
            Version   varchar(24)    not null,
            CreatedAt datetimeoffset not null,
            constraint FK_Releases_App foreign key (AppFk)
                references #schema#.Apps (Id),
            constraint IX_Releases_Version unique (AppFk, Version)
        )
    end

if not exists(select object_id
              from sys.tables
              where [name] = 'Environments')
    begin
        create table #schema#.Environments
        (
            Id               integer        not null primary key identity,
            AppFk            int            not null,
            Slug             nvarchar(128)  not null,
            DisplayText      nvarchar(1024) not null,
            Position         int            not null,
            CurrentReleaseFk int            null,
            constraint FK_Environments_App foreign key (AppFk)
                references #schema#.Apps (Id),
            constraint FK_Environments_Release foreign key (CurrentReleaseFk)
                references #schema#.Releases (Id)
        )
    end

if not exists(select object_id
              from sys.tables
              where [name] = 'Languages')
    begin
        create table #schema#.Languages
        (
            Id          integer        not null primary key identity,
            Code        varchar(5)     not null,
            EnglishName nvarchar(1024) not null,
            NativeName  nvarchar(1024) not null
        )
    end

if not exists(select object_id
              from sys.tables
              where [name] = 'StringKeys')
    begin
        create table #schema#.StringKeys
        (
            Id    integer        not null primary key identity,
            AppFk int            not null,
            [Key] nvarchar(1024) not null,
            constraint FK_StringKeys_App foreign key (AppFk)
                references #schema#.Apps (Id)
        )
    end

if not exists(select *
              from sys.tables
              where [name] = 'StringTexts')
    begin
        create table #schema#.StringTexts
        (
            Id          integer       not null primary key identity,
            StringKeyFk int           not null,
            LanguageFk  int           not null,
            [Text]      nvarchar(max) not null,
            constraint FK_StringKeys_StringKey foreign key (StringKeyFk)
                references #schema#.StringKeys (Id),
            constraint FK_StringKeys_Language foreign key (LanguageFk)
                references #schema#.Languages (Id),
        )
    end

if not exists(select object_id
              from sys.tables
              where [name] = 'ReleaseStringTexts')
    begin
        create table #schema#.ReleaseStringTexts
        (
            ReleaseFk    int not null,
            StringTextFk int not null,
            constraint PK_ReleaseStringTexts primary key (ReleaseFk, StringTextFk),
            constraint FK_ReleaseStringTexts_Release foreign key (ReleaseFk)
                references #schema#.Releases (Id),
            constraint FK_ReleaseStringTexts_StringText foreign key (StringTextFk)
                references #schema#.StringTexts (Id)
        )
    end

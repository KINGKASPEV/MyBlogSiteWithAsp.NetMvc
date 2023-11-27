IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231014194255_Initial Migration')
BEGIN
    CREATE TABLE [BlogPosts] (
        [Id] uniqueidentifier NOT NULL,
        [Heading] nvarchar(max) NOT NULL,
        [PageTitle] nvarchar(max) NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [ShortDescription] nvarchar(max) NOT NULL,
        [FeaturedImageUrl] nvarchar(max) NOT NULL,
        [UrlHandle] nvarchar(max) NOT NULL,
        [publishedDate] datetime2 NOT NULL,
        [Author] nvarchar(max) NOT NULL,
        [Visible] bit NOT NULL,
        CONSTRAINT [PK_BlogPosts] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231014194255_Initial Migration')
BEGIN
    CREATE TABLE [Tags] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [DisplayName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Tags] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231014194255_Initial Migration')
BEGIN
    CREATE TABLE [BlogPostTag] (
        [BlogPostsId] uniqueidentifier NOT NULL,
        [TagsId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_BlogPostTag] PRIMARY KEY ([BlogPostsId], [TagsId]),
        CONSTRAINT [FK_BlogPostTag_BlogPosts_BlogPostsId] FOREIGN KEY ([BlogPostsId]) REFERENCES [BlogPosts] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_BlogPostTag_Tags_TagsId] FOREIGN KEY ([TagsId]) REFERENCES [Tags] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231014194255_Initial Migration')
BEGIN
    CREATE INDEX [IX_BlogPostTag_TagsId] ON [BlogPostTag] ([TagsId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231014194255_Initial Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231014194255_Initial Migration', N'7.0.12');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231018114952_Adding Like functionality')
BEGIN
    EXEC sp_rename N'[BlogPosts].[publishedDate]', N'PublishedDate', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231018114952_Adding Like functionality')
BEGIN
    CREATE TABLE [BlogPostLike] (
        [Id] uniqueidentifier NOT NULL,
        [BlogPostId] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_BlogPostLike] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_BlogPostLike_BlogPosts_BlogPostId] FOREIGN KEY ([BlogPostId]) REFERENCES [BlogPosts] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231018114952_Adding Like functionality')
BEGIN
    CREATE INDEX [IX_BlogPostLike_BlogPostId] ON [BlogPostLike] ([BlogPostId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231018114952_Adding Like functionality')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231018114952_Adding Like functionality', N'7.0.12');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231018150923_Adding comments functionality')
BEGIN
    CREATE TABLE [BlogPostComment] (
        [Id] uniqueidentifier NOT NULL,
        [Descripption] nvarchar(max) NOT NULL,
        [BlogPostId] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [DateAdded] datetime2 NOT NULL,
        CONSTRAINT [PK_BlogPostComment] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_BlogPostComment_BlogPosts_BlogPostId] FOREIGN KEY ([BlogPostId]) REFERENCES [BlogPosts] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231018150923_Adding comments functionality')
BEGIN
    CREATE INDEX [IX_BlogPostComment_BlogPostId] ON [BlogPostComment] ([BlogPostId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231018150923_Adding comments functionality')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231018150923_Adding comments functionality', N'7.0.12');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231019173431_SuperAdmin')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231019173431_SuperAdmin', N'7.0.12');
END;
GO

COMMIT;
GO


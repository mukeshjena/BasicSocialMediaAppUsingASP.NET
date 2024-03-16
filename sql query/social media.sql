-- Create User table
CREATE TABLE [User] (
    user_id INT PRIMARY KEY IDENTITY(1,1),
    username VARCHAR(50) NOT NULL,
    password VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL,
    full_name VARCHAR(100) NOT NULL,
    registration_date DATETIME NOT NULL DEFAULT GETDATE()
);

select * from [dbo].[User]
-- Create Post table
CREATE TABLE Post (
    post_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT NOT NULL,
    post_content VARCHAR(MAX) NOT NULL,
    post_time DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES [User](user_id)
);

select * from Post

-- Stored procedure to register a new user
CREATE PROCEDURE RegisterUser(
    @username VARCHAR(50),
    @password VARCHAR(50),
    @email VARCHAR(100),
    @full_name VARCHAR(100))
AS
BEGIN
    INSERT INTO [dbo].[User] (username, password, email, full_name, registration_date)
    VALUES (@username, @password, @email, @full_name, GETDATE());
END;

exec RegisterUser 'muk3shjena','1234567890','muk3shjena@gmail.com','Mukesh Jena';

-- Stored procedure to login a user
CREATE PROCEDURE LoginUser(
    @username VARCHAR(50),
    @password VARCHAR(50))
AS
BEGIN
    IF EXISTS (SELECT 1 FROM [dbo].[User] WHERE username = @username AND password = @password)
    BEGIN
        SELECT user_id FROM [dbo].[User] WHERE username = @username;
    END
END

exec LoginUser 'muk3shjena','1234567890';

-- Stored procedure to create a new post
CREATE PROCEDURE CreatePost
    @user_id INT,
    @post_content VARCHAR(MAX)
AS
BEGIN
    INSERT INTO Post (user_id, post_content, post_time)
    VALUES (@user_id, @post_content, GETDATE());
END;

exec CreatePost 6,'this is my first post, me millan gandu achhi';
-- Stored procedure to get posts for the home screen
CREATE PROCEDURE GetHomePagePosts
AS
BEGIN
    SELECT p.post_id, u.username, p.post_content, p.post_time
    FROM Post p
    INNER JOIN [dbo].[User] u ON p.user_id = u.user_id
    ORDER BY p.post_time DESC;
END;

exec GetHomePagePosts

-- Stored procedure to get user profile details and posts
CREATE PROCEDURE GetUserProfile
    @user_id INT
AS
BEGIN
    SELECT u.username, u.full_name, u.email,
           p.post_id, p.post_content, p.post_time
    FROM [dbo].[User] u
    LEFT JOIN Post p ON u.user_id = p.user_id
    WHERE u.user_id = @user_id
    ORDER BY p.post_time DESC;
END;

exec GetUserProfile 6;

CREATE PROCEDURE DeletePost
    @post_id INT
AS
BEGIN
    DELETE FROM Post
    WHERE post_id = @post_id;
END;

exec DeletePost 6;
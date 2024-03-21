--use database
use PracticeCRUD

--create user table
create table Users(
	user_id int primary key identity(1,1),
	username nvarchar(50) not null,
	password nvarchar(100) not null,
	email nvarchar(100) not null,
	full_name nvarchar(100) not null,
	registration_date datetime not null default getdate()
);

select * from Users;

--create post table
create table Post(
	post_id int primary key identity(1,1),
	user_id int not null,
	post_content nvarchar(max) not null,
	post_time datetime not null default getdate(),
	foreign key(user_id) references Users(user_id)
);

select * from Post;

--stored procedure to register a new user
create proc RegisterUser(
	@username nvarchar(50),
	@password nvarchar(50),
	@email nvarchar(100),
	@full_name nvarchar(100))
as
begin
	insert into Users (username, password, email, full_name, registration_date)
	values (@username, @password, @email, @full_name, getdate());
end

exec RegisterUser 'muk3shjena','1234567890','muk3shjena@gmail.com','Mukesh Jena';

--stored procedure to login a user
create proc LoginUser(
	@username nvarchar(50),
	@password nvarchar(50))
as
begin
	if exists (select 1 from Users where username = @username and password = @password)
	begin
		select user_id from Users where username = @username;
	end
end

exec LoginUser 'muk3shjena','1234567890';


--stored procedure to create a new post
create proc CreatePost(
	@user_id int,
	@post_content nvarchar(max))
as
begin
	insert into Post (user_id, post_content, post_time)
	values (@user_id, @post_content, getdate());
end

exec CreatePost 1,'this is my second post';

--stored procedure to get posts for the homepage
create proc GetHomePagePosts
as
begin
	select p.post_id, u.username, u.user_id, p.post_content, p.post_time
	from Post p
	inner join Users u  on p.user_id = u.user_id
	order by p.post_time desc;
end

exec GetHomePagePosts;

--stored proc for user profile details and posts
create proc GetUserProfile(
	@user_id int)
as
begin
	select u.username, u.full_name, u.email,
			p.post_id, p.post_content, p.post_time
	from Users u
	left join Post p on u.user_id = p.user_id
	where u.user_id = @user_id
	order by p.post_time desc;
end

exec GetUserProfile 1;

--delete a post from homepage
create proc DeletePostFromHomePage(
	@post_id int,
	@user_id int)
as
begin
	delete from Post
	where post_id = @post_id
	and user_id = @user_id;
end;

exec DeletePostFromHomePage 2,1;

--get user by id
create proc GetUserByID(
	@user_id int)
as
begin
	select * from Users
	where user_id = @user_id;
end

exec GetUserByID 1;
if exists(select * from sys.objects where name='td_Answers')
drop table td_Answers;

create table td_Answers(
	Lang_Name varchar(20),
	Prob_Type nvarchar(20),
	Answer_Title nvarchar(50) primary key
)

insert into td_Answers(Lang_Name ,Prob_Type ,Answer_Title ) values('Java','JDBC+Servlet','Answer_Title JDBC+Servlet one'),
('Java','JDBC+Servlet','Answer_Title JDBC+Servlet two'),
('Java','JDBC+Servlet','Answer_Title JDBC+Servlet three'),
('Java','JDBC+Servlet','Answer_Title JDBC+Servlet four');

insert into td_Answers(Lang_Name ,Prob_Type ,Answer_Title ) values('Java','SpringBoot','Answer_Title SpringBoot one'),
('Java','SpringBoot','Answer_Title SpringBoot two'),
('Java','SpringBoot','Answer_Title SpringBoot three'),
('Java','SpringBoot','Answer_Title SpringBoot four');


insert into td_Answers(Lang_Name ,Prob_Type ,Answer_Title ) values('Java','JSP','Answer_Title JSP one'),
('Java','JSP','Answer_Title JSP two'),
('Java','JSP','Answer_Title JSP three'),
('Java','JSP','Answer_Title JSP four');

insert into td_Answers(Lang_Name ,Prob_Type ,Answer_Title ) values('Java','Maven','Answer_Title Maven one'),
('Java','Maven','Answer_Title Maven two'),
('Java','Maven','Answer_Title Maven three'),
('Java','Maven','Answer_Title Maven four');

select * from td_Answers;
select * from td_Content;

select distinct td_Answers.Prob_Type  from td_Answers  where Lang_Name = 'Java';
select td_Answers.Answer_Title from td_Answers where Lang_Name = 'Java' And Prob_Type = 'JSP';

select * from td_Content C inner join td_Answers A on  C.Title = A.Answer_Title where A.Lang_Name = 'Java' And A.Prob_Type = 'JDBC+Servlet' And A.Answer_Title = 'Answer_Title JDBC+Servlet one';

insert into td_Content(Title, Article1, Article2, Article3, PhotoPath1, PhotoPath2, PhotoPath3) values
	('Answer_Title JSP one', 'article1', 'article2', 'article3', 'photoPath1', 'photoPath2', 'photoPath3'),
	('Answer_Title Maven two', 'article1', 'article2', 'article3', 'photoPath1', 'photoPath2', 'photoPath3'),
	('Answer_Title SpringBoot three', 'article1', 'article2', 'article3', 'photoPath1', 'photoPath2', 'photoPath3'),
	('Answer_Title JSP four', 'article1', 'article2', 'article3', 'photoPath1', 'photoPath2', 'photoPath3'),
	('Answer_Title JDBC+Servlet five', 'article1', 'article2', 'article3', 'photoPath1', 'photoPath2', 'photoPath3');

if exists(select * from sys.objects where name='td_Content')
drop table td_Content;

create table td_Content(
	Title nvarchar(50) primary key,
	Article1 nvarchar(200),
	Article2 nvarchar(200),
	Article3 nvarchar(200),
	PhotoPath1 varchar(50),
	PhotoPath2 varchar(50),
	PhotoPath3 varchar(50),
	AddTime datetime default(getdate())
);

if exists(select * from sys.objects where name='PR_GetAnswer')
drop procedure PR_GetAnswer

if exists(select * from sys.objects where name='PR_SetAnswer')
drop procedure PR_SetAnswer

if exists(select * from sys.objects where name='PR_GetContent')
drop procedure PR_GetContent

if exists(select * from sys.objects where name='PR_SetContent')
drop procedure PR_SetContent

CREATE PROCEDURE PR_GetAnswer(
	@lang_name varchar(20),
	@prob_type nvarchar(20)
)
as
begin
	select td_Answers.Answer_Title from td_Answers where Lang_Name = @lang_name And Prob_Type = @prob_type;
end
go

CREATE PROCEDURE PR_SetAnswer(
	@lang_name varchar(20),
	@prob_type nvarchar(20),
	@answer_title nvarchar(50)
)
as
begin
	insert into td_Answers(Lang_Name ,Prob_Type ,Answer_Title ) values(@lang_name ,@prob_type,@answer_title);
end
go

CREATE PROCEDURE PR_GetContent(
	@lang_name varchar(20),
	@prob_type nvarchar(20),
	@answer_title nvarchar(50)
)
as
begin
	select * from td_Content C inner join td_Answers A on  C.Title = A.Answer_Title where A.Lang_Name = @lang_name And A.Prob_Type = @prob_type And A.Answer_Title = @answer_title;
end
go

CREATE PROCEDURE PR_SetContent(
	@title nvarchar(20),
	@article1 nvarchar(200),
	@article2 nvarchar(200),
	@article3 nvarchar(200),
	@photoPath1 varchar(50),
	@photoPath2 varchar(50),
	@photoPath3 varchar(50)
)
as
begin
	insert into td_Content(Title, Article1, Article2, Article3, PhotoPath1, PhotoPath2, PhotoPath3) values(
	@title, @article1, @article2, @article3, @photoPath1, @photoPath2, @photoPath3);
end
go
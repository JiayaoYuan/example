/*2.1网站管理表*/
if exists (select * from sys.objects where name = 'tb_options')
drop table tb_options

CREATE TABLE tb_options (
   option_id int NOT NULL primary key identity,
   option_name varchar(225) NOT NULL unique,
   option_values text NOT NULL,
)

/*用户表*/
if exists (select * from sys.objects where name = 'tb_users')
drop table tb_users

CREATE TABLE tb_users (
   user_id bigint NOT NULL primary key identity,
   user_ip varchar(20) NOT NULL,
   user_name varchar(20) NOT NULL unique,
   user_password varchar(15) NOT NULL,
   user_email varchar(30) NOT NULL unique,
   user_profile_photo varchar(255) NOT NULL,
   user_level varchar(20) NOT NULL,
   user_rights varchar(20) NOT NULL,
   user_registration_time datetime NOT NULL,
   user_birthday date DEFAULT NULL,
   user_age int DEFAULT NULL,
   user_telephone_number int NOT NULL unique,
   user_nickname varchar(20) NOT NULL unique,
)

/*用户好友表*/
if exists (select * from sys.objects where name = 'tb_user_friends')
drop table tb_user_friends

CREATE TABLE tb_user_friends (
  Id bigint NOT NULL primary key identity,
  user_id bigint NOT NULL unique,
  user_friends_id bigint NOT NULL,
  user_note varchar(20) NOT NULL,
  user_status varchar(20) NOT NULL
)

/*博文表*/
if exists (select * from sys.objects where name = 'tb_articles')
drop table tb_articles

CREATE TABLE tb_articles (
  article_id bigint NOT NULL primary key identity,
  user_id bigint FOREIGN KEY REFERENCES tb_users(user_id),
  article_title text NOT NULL,
  article_content text NOT NULL,
  article_description text,
  article_label_img varchar(200),
  article_views bigint NOT NULL,
  article_comment_count bigint NOT NULL,
  article_date datetime NOT NULL,
  article_like_count bigint NOT NULL
) 

/*文章评论表*/
if exists (select * from sys.objects where name = 'tb_comments')
drop table tb_comments

CREATE TABLE tb_comments (
  comment_id bigint NOT NULL primary key identity,
  user_id bigint FOREIGN KEY REFERENCES tb_users(user_id),
  article_id bigint NOT NULL,
  comment_like_count bigint NOT NULL,
  comment_date datetime NOT NULL,
  comment_content text NOT NULL,
  parent_comment_id bigint NOT NULL
)

/*公告表 announcement 字段*/
	select * from tb_announcement
/**/
if exists (select * from sys.objects where name = 'tb_announcement')
drop table tb_announcement

CREATE TABLE tb_announcement (
  announce_id bigint NOT NULL primary key identity,
  announce_title text,
  announce_content text,
  announce_publish_time varchar(100)
)

/* 分类表*/
if exists (select * from sys.objects where name = 'tb_sorts')
drop table tb_sorts

CREATE TABLE tb_sorts (
  sort_id bigint NOT NULL primary key identity,
  sort_name varchar(50) NOT NULL,
  sort_alias varchar(15) NOT NULL,
  sort_description text NOT NULL,
  parent_sort_id bigint NOT NULL
)

/*test sort*/
select * from tb_articles where article_title like '%从客户端%'
select * from tb_set_article_sort where sort_id = 3
select * from tb_users
select top 10 * from tb_articles order by NEWID()
select * from tb_sorts
delete from tb_labels where label_id = 20
select * from tb_comments
select * from tb_set_article_sort
select * from tb_articles
select * from tb_set_article_label
select * from tb_labels
insert into tb_sorts(sort_name, sort_alias, sort_description, parent_sort_id) values 
('前端技术','web','前端开发是创建Web页面或app等前端界面呈现给用户的过程，通过HTML，CSS及JavaScript以及衍生出来的各种技术、框架、解决方案，来实现互联网产品的用户界面交互 [1]  。它从网页制作演变而来，名称上有很明显的时代特征。在互联网的演化进程中，网页制作是Web1.0时代的产物，早期网站主要内容都是静态，以图片和文字为主，用户使用网站的行为也以浏览为主。随着互联网技术的发展和HTML5、CSS3的应用，现代网页更加美观，交互效果显著，功能更加强大。',0),
('后端程序','program','后端开发就是开发人员编写的不能直接看到的代码。',0),
('管理系统','cms','在中国互联网的发展历程中，一直以来默默地为中国站长提供动力的CMS厂商作出的贡献尤其巨大，而与之成反比的是CMS厂商的生存状态依然令人担忧，由于国内站长对于免费和开源的FreeEIM尤为热衷，用户的版权意识低加之用户误将开源认 为就是免费的，使得一些获得资本注入的CMS厂商无法达到预期的目标，导致PHPCMS创始人淡淡风和DEDECMS创始人IT柏拉图相继离职事件，亦是2010年CMS行业最大的新闻，交流中，感受更多的是他们只是成功的技术狂热者，还不算真正的商人，CMS行业之路对他们还任重道远。',0),
('授人以鱼','tutorial','授人以鱼不如授人以渔 --语出《淮南子·说林训》，原文说：“临河而羡鱼，不如归家织网。”《汉书·董仲舒传》，书中说：“故汉得天下以来，常欲治而至今不可善治者，失之于当更化而不更化也。古人有言 曰：‘临渊羡鱼，不如退而结网。”中国有句古话叫“授人以鱼不如授人以渔”，说的是传授给人既有知识，不如传授给人学习知识的方法。道理其实很简单，鱼是目的，钓鱼是手段，一条鱼能解一时之饥，却不能解长久之饥，如果想永远有鱼吃，那就要学会钓鱼的方法。',0),
('程序人生','code','程序员的人生，不断学习与总结',0)
insert into tb_set_article_sort(article_id, sort_id) values
(1,1),
(2,1),
(3,1),
(4,1)
insert into tb_labels(label_name, label_alias, label_description) values
('html','前端技术',''),
('css','前端技术',''),
('javascript','前端技术',''),
('java','后端程序',''),
('c#','后端程序',''),
('c','后端程序',''),
('c++','后端程序',''),
('','程序人生',''),
('cms','管理系统',''),
('erp','管理系统',''),
('soa','管理系统',''),
('saas','管理系统',''),
('八鱼','授人以鱼',''),
('asp.net','前端技术',''),
('git','前端技术',''),
('sql','前端技术',''),
('php','后端程序','')

exec  sp_helpconstraint 'tb_sorts';
/*test sort*/

/*文章设置分类表*/
if exists (select * from sys.objects where name = 'tb_set_article_sort')
drop table tb_set_article_sort

CREATE TABLE tb_set_article_sort (
  set_article_sort_id bigint primary key identity,
  article_id bigint,
  sort_id bigint,
)

/* 文章设置标签表*/
if exists (select * from sys.objects where name = 'tb_set_article_label')
drop table tb_set_article_label

CREATE TABLE tb_set_article_label (
  set_article_label_id bigint primary key identity,
  article_id bigint,
  label_id bigint
)

/* 标签表*/
if exists (select * from sys.objects where name = 'tb_labels')
drop table tb_labels

CREATE TABLE tb_labels (
  label_id bigint NOT NULL primary key identity,
  label_name varchar(20) NOT NULL,
  label_alias varchar(15) NOT NULL,
  label_description text NOT NULL
)

/* 论坛表*/
if exists (select * from sys.objects where name = 'tb_forums')
drop table tb_forums

CREATE TABLE tb_forums (
  forum_id bigint NOT NULL primary key identity,
  forum_name varchar(20) NOT NULL unique,
  forum_description text NOT NULL,
  forum_logo varchar(255) NOT NULL,
  forum_post_count bigint NOT NULL,
  parent_forum_id bigint NOT NULL unique
)

/* 主贴表*/
if exists (select * from sys.objects where name = 'tb_posts')
drop table tb_posts

CREATE TABLE tb_posts (
  post_id bigint NOT NULL primary key identity,
  forum_id bigint NOT NULL,
  user_id bigint NOT NULL,
  post_title text NOT NULL,
  post_views bigint NOT NULL,
  post_content text NOT NULL,
  post_date datetime NOT NULL,
  post_status varchar(20) NOT NULL,
  post_comment_count bigint NOT NULL
)

/* 回贴表*/
if exists (select * from sys.objects where name = 'tb_floors')
drop table tb_floors

CREATE TABLE tb_floors (
  floor_id bigint NOT NULL primary key identity,
  user_id bigint NOT NULL unique,
  post_id bigint NOT NULL unique,
  floor_content text NOT NULL,
  floor_date datetime NOT NULL,
  parent_floor_id bigint NOT NULL unique, 
)

/* 版主表*/
if exists (select * from sys.objects where name = 'tb_moderator')
drop table tb_moderator

CREATE TABLE tb_moderator (
  moderator_id bigint NOT NULL identity,
  forum_id bigint NOT NULL,
  moderator_level varchar(20) NOT NULL,
  PRIMARY KEY ( moderator_id , forum_id )
)

/* 总菜单表*/
if exists (select * from sys.objects where name = 'tb_menus')
drop table tb_menus

CREATE TABLE tb_menus (
  menu_id bigint NOT NULL primary key identity,
  menu_name varchar(20) NOT NULL
)

/* 子菜单表*/
if exists (select * from sys.objects where name = 'tb_submenus')
drop table tb_submenus

CREATE TABLE tb_submenus (
  link_id bigint NOT NULL primary key identity,
  menu_id bigint NOT NULL,
  link_name varchar(255) NOT NULL,
  link_target varchar(255) NOT NULL,
  link_open_way varchar(20) NOT NULL,
  parent_link_id bigint NOT NULL
)

/* 友链表*/
if exists (select * from sys.objects where name = 'tb_friend_links')
drop table tb_friend_links

CREATE TABLE tb_friend_links (
  friend_link_id bigint NOT NULL primary key identity,
  friend_links varchar(255) NOT NULL,
  friend_link_name varchar(20) NOT NULL,
  friend_link_description text NOT NULL,
  friend_link_logo varchar(255) NOT NULL
)

/*测试*/
select * from tb_users
select * from tb_articles
select top 5 * from tb_articles order by article_views desc
select * from tb_set_article_sort
select * from tb_set_article_label
select * from tb_sorts
select * from tb_labels
select * from tb_articles where article_id >= (select floor(RAND() * (select max(article_id) from tb_articles))) order by article_id
select * from tb_set_article_label where article_id = 4
select top 8 * from tb_articles order by NEWID()
select * from tb_comments
select * from tb_comments a join tb_users b on a.user_id = b.user_id where a.article_id = 2 
select top 1 * from tb_comments a join tb_users b on a.user_id = b.user_id where a.article_id = 2 order by a.comment_id desc
select * from tb_sorts where sort_name = '前端技术'
select * from tb_friend_links
delete from tb_set_article_label where set_article_label_id = 1
update tb_set_article_label set set_article_label_id = 2 where set_article_label_id = 200
/*测试*/
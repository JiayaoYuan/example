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
  user_id bigint NOT NULL unique,
  article_title text NOT NULL,
  article_content text NOT NULL,
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
  user_id bigint NOT NULL,
  article_id bigint NOT NULL unique,
  comment_like_count bigint NOT NULL,
  comment_date datetime NOT NULL unique,
  comment_content text NOT NULL,
  parent_comment_id bigint NOT NULL unique
)

/* 分类表*/
if exists (select * from sys.objects where name = 'tb_sorts')
drop table tb_sorts

CREATE TABLE tb_sorts (
  sort_id bigint NOT NULL primary key,
  sort_name varchar(50) NOT NULL unique,
  sort_alias varchar(15) NOT NULL unique,
  sort_description text NOT NULL,
  parent_sort_id bigint NOT NULL
)

/*文章设置分类表*/
if exists (select * from sys.objects where name = 'tb_set_artitle_sort')
drop table tb_set_artitle_sort

CREATE TABLE tb_set_artitle_sort (
  article_id bigint NOT NULL,
  sort_id bigint NOT NULL unique,
  PRIMARY KEY ( article_id , sort_id )
)

/* 标签表*/
if exists (select * from sys.objects where name = 'tb_labels')
drop table tb_labels

CREATE TABLE tb_labels (
  label_id bigint NOT NULL primary key identity,
  label_name varchar(20) NOT NULL unique,
  label_alias varchar(15) NOT NULL unique,
  label_description text NOT NULL
)

/* 文章设置标签表*/
if exists (select * from sys.objects where name = 'tb_set_artitle_label')
drop table tb_set_artitle_label

CREATE TABLE tb_set_artitle_label (
  article_id bigint NOT NULL primary key identity,
  label_id bigint NOT NULL unique
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
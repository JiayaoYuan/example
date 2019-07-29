/*2.1��վ�����*/
if exists (select * from sys.objects where name = 'tb_options')
drop table tb_options

CREATE TABLE tb_options (
   option_id int NOT NULL primary key identity,
   option_name varchar(225) NOT NULL unique,
   option_values text NOT NULL,
)

/*�û���*/
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

/*�û����ѱ�*/
if exists (select * from sys.objects where name = 'tb_user_friends')
drop table tb_user_friends

CREATE TABLE tb_user_friends (
  Id bigint NOT NULL primary key identity,
  user_id bigint NOT NULL unique,
  user_friends_id bigint NOT NULL,
  user_note varchar(20) NOT NULL,
  user_status varchar(20) NOT NULL
)

/*���ı�*/
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

/*�������۱�*/
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

/*����� announcement �ֶ�*/
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

/* �����*/
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
select * from tb_articles where article_title like '%�ӿͻ���%'
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
('ǰ�˼���','web','ǰ�˿����Ǵ���Webҳ���app��ǰ�˽�����ָ��û��Ĺ��̣�ͨ��HTML��CSS��JavaScript�Լ����������ĸ��ּ�������ܡ������������ʵ�ֻ�������Ʒ���û����潻�� [1]  ��������ҳ�����ݱ�������������к����Ե�ʱ���������ڻ��������ݻ������У���ҳ������Web1.0ʱ���Ĳ��������վ��Ҫ���ݶ��Ǿ�̬����ͼƬ������Ϊ�����û�ʹ����վ����ΪҲ�����Ϊ�������Ż����������ķ�չ��HTML5��CSS3��Ӧ�ã��ִ���ҳ�������ۣ�����Ч�����������ܸ���ǿ��',0),
('��˳���','program','��˿������ǿ�����Ա��д�Ĳ���ֱ�ӿ����Ĵ��롣',0),
('����ϵͳ','cms','���й��������ķ�չ�����У�һֱ����ĬĬ��Ϊ�й�վ���ṩ������CMS���������Ĺ�������޴󣬶���֮�ɷ��ȵ���CMS���̵�����״̬��Ȼ���˵��ǣ����ڹ���վ��������ѺͿ�Դ��FreeEIM��Ϊ���ԣ��û��İ�Ȩ��ʶ�ͼ�֮�û��󽫿�Դ�� Ϊ������ѵģ�ʹ��һЩ����ʱ�ע���CMS�����޷��ﵽԤ�ڵ�Ŀ�꣬����PHPCMS��ʼ�˵������DEDECMS��ʼ��IT����ͼ�����ְ�¼�������2010��CMS��ҵ�������ţ������У����ܸ����������ֻ�ǳɹ��ļ��������ߣ����������������ˣ�CMS��ҵ֮·�����ǻ����ص�Զ��',0),
('��������','tutorial','�������㲻���������� --����������ӡ�˵��ѵ����ԭ��˵�����ٺӶ����㣬������֯�����������顤�����洫��������˵�����ʺ������������������ζ����񲻿������ߣ�ʧ֮�ڵ�������������Ҳ���������� Ի������Ԩ���㣬�����˶����������й��о�Ż��С��������㲻���������桱��˵���Ǵ��ڸ��˼���֪ʶ�����紫�ڸ���ѧϰ֪ʶ�ķ�����������ʵ�ܼ򵥣�����Ŀ�ģ��������ֶΣ�һ�����ܽ�һʱ֮����ȴ���ܽⳤ��֮�����������Զ����ԣ��Ǿ�Ҫѧ�����ķ�����',0),
('��������','code','����Ա������������ѧϰ���ܽ�',0)
insert into tb_set_article_sort(article_id, sort_id) values
(1,1),
(2,1),
(3,1),
(4,1)
insert into tb_labels(label_name, label_alias, label_description) values
('html','ǰ�˼���',''),
('css','ǰ�˼���',''),
('javascript','ǰ�˼���',''),
('java','��˳���',''),
('c#','��˳���',''),
('c','��˳���',''),
('c++','��˳���',''),
('','��������',''),
('cms','����ϵͳ',''),
('erp','����ϵͳ',''),
('soa','����ϵͳ',''),
('saas','����ϵͳ',''),
('����','��������',''),
('asp.net','ǰ�˼���',''),
('git','ǰ�˼���',''),
('sql','ǰ�˼���',''),
('php','��˳���','')

exec  sp_helpconstraint 'tb_sorts';
/*test sort*/

/*�������÷����*/
if exists (select * from sys.objects where name = 'tb_set_article_sort')
drop table tb_set_article_sort

CREATE TABLE tb_set_article_sort (
  set_article_sort_id bigint primary key identity,
  article_id bigint,
  sort_id bigint,
)

/* �������ñ�ǩ��*/
if exists (select * from sys.objects where name = 'tb_set_article_label')
drop table tb_set_article_label

CREATE TABLE tb_set_article_label (
  set_article_label_id bigint primary key identity,
  article_id bigint,
  label_id bigint
)

/* ��ǩ��*/
if exists (select * from sys.objects where name = 'tb_labels')
drop table tb_labels

CREATE TABLE tb_labels (
  label_id bigint NOT NULL primary key identity,
  label_name varchar(20) NOT NULL,
  label_alias varchar(15) NOT NULL,
  label_description text NOT NULL
)

/* ��̳��*/
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

/* ������*/
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

/* ������*/
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

/* ������*/
if exists (select * from sys.objects where name = 'tb_moderator')
drop table tb_moderator

CREATE TABLE tb_moderator (
  moderator_id bigint NOT NULL identity,
  forum_id bigint NOT NULL,
  moderator_level varchar(20) NOT NULL,
  PRIMARY KEY ( moderator_id , forum_id )
)

/* �ܲ˵���*/
if exists (select * from sys.objects where name = 'tb_menus')
drop table tb_menus

CREATE TABLE tb_menus (
  menu_id bigint NOT NULL primary key identity,
  menu_name varchar(20) NOT NULL
)

/* �Ӳ˵���*/
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

/* ������*/
if exists (select * from sys.objects where name = 'tb_friend_links')
drop table tb_friend_links

CREATE TABLE tb_friend_links (
  friend_link_id bigint NOT NULL primary key identity,
  friend_links varchar(255) NOT NULL,
  friend_link_name varchar(20) NOT NULL,
  friend_link_description text NOT NULL,
  friend_link_logo varchar(255) NOT NULL
)

/*����*/
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
select * from tb_sorts where sort_name = 'ǰ�˼���'
select * from tb_friend_links
delete from tb_set_article_label where set_article_label_id = 1
update tb_set_article_label set set_article_label_id = 2 where set_article_label_id = 200
/*����*/
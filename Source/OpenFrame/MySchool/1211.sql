create database MySchool
go
use MySchool
go
create table SexInfo --�Ա���Ϣ
(
	SexId int primary key not null,
	SexName varchar(4) not null
)

insert into sexInfo values(0,'��')
insert into sexInfo values(1,'Ů')

create table IdentityInfo --������Ϣ	
(
	IdentityId int identity(1,1)primary key not null,
	IdentityName varchar(10) not null
)
insert into IdentityInfo values('ѧ��')
insert into IdentityInfo values('��ʦ')
insert into IdentityInfo values('������')
insert into IdentityInfo values('����Ա')
insert into IdentityInfo values('һ������Ա')


create table TeacherInfo--��ʦ��Ϣ
(	
	TeacherId int identity(1,1) primary key not null,
	TeacherLoginName varchar(20)  not null,
	TeacherLoginPassWord varchar(10) not null,
	TeacherEnterSchoolTime varchar(20) not null,
	TeacherLeaveSchoolTime varchar(20) default('��ְ'),	
	TeacherName varchar(20) not null,
	TeacherIdCard varchar(18) check(TeacherIdCard like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9,x]') not null,
	TeacherAge int not null,
	TeacherBrithday varchar(20) not null,
	TeacherPhone int check(TeacherPhone like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]' or TeacherPhone like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]') not null,
	FKSexId int references SexInfo(SexId) not null,
	TeacherAddress varchar(60) not null,
	TeacherRemoveTime varchar(20) default('��'),
	TeacherRemoveClass varchar(20) default('��'),
	FKIdentityId int references IdentityInfo(IdentityId) not null,
	TeacherIsExist bit default(1)
)



insert into teacherInfo values('hahaaa','123','2008-7-1',default,'����ʦ','310226198311150063',31,'2000-1-1','57191145',1,'�Ϻ�',default,default,2,default)
insert into teacherInfo values('zhaojin','123','2008-8-1',default,'����ʦ','310226198311150013',25,'2000-7-1','57191155',1,'�Ϻ�',default,default,2,default)
insert into teacherInfo values('hahaaa1','123','2008-7-1',default,'aa','310226198311150063',31,'2000-1-1','57191145',1,'�Ϻ�',default,default,2,default)
insert into teacherInfo values('zhaojin1','123','2008-8-1',default,'bb','310226198311150013',25,'2000-7-1','57191155',1,'�Ϻ�',default,default,2,default)

create table ClassTeacherInfo--��������Ϣ
(	
	ClassTeacherId int identity(1,1) primary key not null,
	ClassTeacherLoginName varchar(20)  not null,
	ClassTeacherLoginPassWord varchar(10) not null,
	ClassTeacherEnterSchoolTime varchar(20) not null,
	ClassTeacherLeaveSchoolTime varchar(20) default('��ְ'),	
	ClassTeacherName varchar(20) not null,
	ClassTeacherIdCard varchar(18) check(ClassTeacherIdCard like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9,x]') not null,
	ClassTeacherAge int not null,
	ClassTeacherBrithday varchar(20) not null,
	ClassTeacherPhone int check(ClassTeacherPhone like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]' or ClassTeacherPhone like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]') not null,
	ClassFKSexId int references SexInfo(SexId) not null,
	ClassTeacherAddress varchar(60) not null,
	ClassTeacherRemoveTime varchar(20) default('��'),
	ClassTeacherRemoveClass varchar(20) default('��'),
	FKIdentityId int references IdentityInfo(IdentityId) not null,
	ClassTeacherIsExist bit default(1)
)

insert into ClassTeacherInfo values('haha','123','2008-6-1',default,'����ʦ','310226198311150023',35,'2000-5-1','57191115',1,'�Ϻ�',default,default,3,default)
insert into ClassTeacherInfo values('hahaa','123','2008-4-1',default,'����ʦ','310226198311150033',25,'2000-3-1','57191125',1,'�Ϻ�',default,default,3,default)
insert into ClassTeacherInfo values('haha1','123','2008-6-1',default,'aa','310226198311150023',35,'2000-5-1','57191115',1,'�Ϻ�',default,default,3,default)
insert into ClassTeacherInfo values('hahaa2','123','2008-4-1',default,'bb','310226198311150033',25,'2000-3-1','57191125',1,'�Ϻ�',default,default,3,default)


create table ManagerInfo--����Ա��Ϣ
(
	ManagerId int identity(1,1) primary key not null,
	ManagerLoginName varchar(20)  not null,
	ManagerLoginPassWord varchar(10) not null,
	ManagerEnterSchoolTime varchar(20) not null,
	ManagerLeaveSchoolTime varchar(20) default('��ְ'),	
	ManagerName varchar(20) not null,
	ManagerIdCard varchar(18) check(ManagerIdCard like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9,x]') not null,
	ManagerAge int not null,
	ManagerBrithday varchar(20) not null,
	ManagerPhone int check(ManagerPhone like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]' or ManagerPhone like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]') not null,
	FKSexId int references SexInfo(SexId) not null,
	ManagerAddress varchar(60) not null,
	FKIdentityId int references IdentityInfo(IdentityId) not null,
	ManagerIsExist bit default(1)
)


insert into ManagerInfo
values('wt','123','2008-8-7',default,'����','310226198311150019',25,'1983-11-15',57191187,0,'�Ϻ�',5,default)
insert into managerInfo
values('wh','123','2008-8-7',default,'����','310226198311150019',25,'1984-10-31',57191187,1,'�Ϻ�',4,default)
insert into ManagerInfo
values('aa','123','2008-8-7',default,'����','310226198311150019',25,'1983-11-15',57191187,0,'�Ϻ�',5,default)
insert into managerInfo
values('bb','123','2008-8-7',default,'����','310226198311150019',25,'1984-10-31',57191187,1,'�Ϻ�',4,default)



create table ClassInfo --�༶��Ϣ
(
	ClassId int identity(1,1)primary key not null,
	ClassName varchar(10) unique not null,
	ClassStartTime varchar(20) not null,
	ClassFinishTime varchar(20),
	ClassStuNum int not null,
	FKClassTeacherId int references ClassTeacherInfo(ClassTeacherId) not null,
	FKTeacherId int references TeacherInfo(TeacherId) not null,
	ClassIsExist bit default(1)
)
insert into classInfo values('S1-63t','2008��8��10��','������',11,1,2,default)
insert into classInfo values('S1-62t','2008��7��1��','������',15,2,1,default)
insert into classInfo values('S1-61t','2008��7��1��','������',15,2,2,default)


create table StuInfo --ѧ����Ϣ
(
	StuLoginName varchar(20) primary key not null,
	StuLoginPassWord varchar(10) not null,
	StuEnterSchoolTime varchar(20) not null,
	StuLeaveSchoolTime varchar(20) default('��У'),	
	StuName varchar(20) not null,
	StuIdCard varchar(18) check (StuIdCard like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9,x]') not null,
	StuAge int check (StuAge>18 and StuAge<40) not null,
	StuBrithday varchar(20) not null,
	StuPhone int check(StuPhone like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]' or StuPhone like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]') not null,
	FKSexId int references SexInfo(SexId) not null,
	FKClassId int references ClassInfo(ClassId) not null,
	StuId int not null,
	StuAddress varchar(60) not null,	
	StuRemoveTime varchar(20) default('��'),
	StuRemoveClass varchar(20) default('��'),
	FKIdentityId int references IdentityInfo(IdentityId) not null,
	StuIsExist bit default(1),
	StuInitializationClass varchar(20) default('��')
	
)
insert into stuInfo values('wj1','123','2008-5-1',default,'����','310226198311150012',25,'1983-11-15',57191177,1,1,5,'�Ϻ�',default,default,1,default,default)
insert into stuInfo values('wh1','123','2008-5-1',default,'����','310226198311150019',25,'1983-11-15',57191187,1,1,6,'�Ϻ�',default,default,1,default,default)



create table LessonInfo --�γ���Ϣ
(
	LessonId int identity(1,1) primary key not null,
	LessonName varchar(20) not null,
	LessonIsExist bit default(1)
)
insert into LessonInfo values('C#',default)
insert into LessonInfo values('Sql',default)

create table ChoiceInfo --ѡ������Ϣ
(
	ChoiceId int identity(1,1) primary key not null,
	FKlessonId int references LessonInfo(LessonId) not null,
	ChoiceSubject varchar(80) unique not null ,
	ChoiceContentA varchar(50) not null,
	ChoiceContentB varchar(50) not null,
	ChoiceContentC varchar(50) not null,
	ChoiceContentD varchar(50) not null,
	ChoiceContentE varchar(50) not null,
	ChoiceRightAnswer varchar(4) not null,
	ChoiceIsExist bit default(1)
)
insert into ChoiceInfo values(1,'b','bb','bb','bSb','bb','bb','AD',default)
insert into ChoiceInfo values(1,'c','bab','bsb','bDb','bb','bb','AC',default)
insert into ChoiceInfo values(1,'d','bab','bdb','bSb','bb','bb','BC',default)
insert into ChoiceInfo values(1,'e','bcb','bsb','bFSDb','bFSD','bbF','CD',default)
insert into ChoiceInfo values(1,'f','bb','1bb','bCXb','FSb','bb11','AC',default)
insert into ChoiceInfo values(1,'g','bb','2bb','bVb','DZb1','b23b','AB',default)
insert into ChoiceInfo values(1,'h','bb','3bb','bCXb','SMb1','b2b','AE',default)



create table EssayQuestionInfo --�������Ϣ
(
	FKlessonId int references LessonInfo(LessonId) not null,
	EssayQuestionSubject varchar(200) not null,
	EssayQuestionAnswer varchar(200) not null,
	EssayQuestionIsExist bit default(1)
)


insert into EssayQuestionInfo values(1,'aa','bfasbbb',default)
insert into EssayQuestionInfo values(1,'ab','bbbbdasb',default)
insert into EssayQuestionInfo values(1,'ac','bbdasbbb',default)
insert into EssayQuestionInfo values(1,'ad','bbbbfasb',default)
insert into EssayQuestionInfo values(1,'ae','bbfasbbb',default)
insert into EssayQuestionInfo values(1,'af','bbfabbb',default)
insert into EssayQuestionInfo values(1,'ag','bbbfasdfsbb',default)



create table StuMarkInfo --ѧ���ɼ���Ϣ
(
	FKStuLoginName varchar(20) references StuInfo(StuLoginName) not null,
	FKlessonId int references LessonInfo(LessonId) not null,
	StuChoiceMark int not null,
	StuEssayQuestionMark int not null,
	StuMark int not null
)
create table ExamQuestionInfo --����⿼��ʹ�
(
	id int identity(1,1) primary key,
	FKlessonId int references LessonInfo(LessonId) not null,
	FKStuLoginName varchar(20) references StuInfo(StuLoginName) not null,
	EssayQuestionSubject varchar(200) not null,--��Ŀ
	EssayQuestionAnswer varchar(200) not null,--��׼��
	StuEssayQuestionAnswer varchar(200)		--ѧ���ش�Ĵ�
)

/*
select LessonName as ��Ŀ,StuMark as �ɼ�,StuName as ����,ClassName as �༶ from StuMarkInfo inner join StuInfo
on(FKStuLoginName=StuLoginName)
inner join ClassInfo
on(FKClassId=ClassId)
inner join LessonInfo
on(FKlessonId=lessonId)
where FKlessonId=1 and ClassId=1 and StuName like '%%'
*/

create table BBS --���԰�
(
	UserLoginName varchar(20) not null,
	UserMessage varchar(100) not null,
	UserLeaveMessageTime varchar(20),
)
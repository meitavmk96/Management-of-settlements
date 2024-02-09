
Create Table Settlements (
	[id] smallint identity (1, 1) NOT NULL ,
    [name] nvarchar (30) NOT NULL Unique,
	Primary key (id)
)

Select * from Settlements

Insert into [Settlements]([name]) values(N'אחיהוד')
Insert into [Settlements]([name]) values(N'גאולי תימן')
Insert into [Settlements]([name]) values(N'אלישיב')
Insert into [Settlements]([name]) values(N'אריאל')
Insert into [Settlements]([name]) values(N'עמיעד')
Insert into [Settlements]([name]) values(N'עתלית')
Insert into [Settlements]([name]) values(N'אפיקים')
Insert into [Settlements]([name]) values(N'עדי')
Insert into [Settlements]([name]) values(N'אבו גוש')
Insert into [Settlements]([name]) values(N'חברון')
Insert into [Settlements]([name]) values(N'מטולה')

----עדכון
--Update [Settlements] set name = 'תל אביב'
--where id = 3

----מחיקה
--Delete from [Settlements]
--where id = 3
--DROP TABLE Settlements


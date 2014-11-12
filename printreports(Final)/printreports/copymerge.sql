delete from chloritech_db.dbo.report1_temp
insert into chloritech_db.dbo.report1_temp (category,Opening,Receipt,Issue,closing)
select r2.category , r1.closing as Opening , r2.Receipt - r1.Receipt as Receipt , r2.Issue - r1.Issue as Issue , r2.closing  
from chloritech_db.dbo.report2 r2 left outer join chloritech_db.dbo.report1 r1 on r2.category = r1.category   
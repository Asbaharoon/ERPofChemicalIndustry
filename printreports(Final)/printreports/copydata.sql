insert into chloritech_db.dbo.report1_temp (rt.category,rt.Opening,rt.Receipt,rt.Issue,rt.closing)
select rt.category,rt.Opening,rt.Receipt,rt.Issue,rt.closing
from chloritech_db.dbo.report1 as rt;
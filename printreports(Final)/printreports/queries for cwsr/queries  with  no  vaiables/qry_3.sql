drop table chloritech_d.dbo.cwsr_final
select r2.category, r1.closing as opning ,r2.rcpt-r1.rcpt as receipt ,r2.issue-r1.issue as issue , r2.closing as  closing 
into chloritech_d.dbo.cwsr_final
from chloritech_d.dbo.rcpt2 r2 left outer  join chloritech_d.dbo.rcpt1 r1 on r2.category = r1.category 
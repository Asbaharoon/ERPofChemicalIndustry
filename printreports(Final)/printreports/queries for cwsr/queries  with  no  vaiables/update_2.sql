

update  r1
set r1.issue =0
from chloritech_d.dbo.rcpt1 r1
where r1.issue is  null

update  r1
set r1.closing = r1.opening 
from chloritech_d.dbo.rcpt1 r1
where r1.closing is  null

update  r1
set r1.closing = r1.opening
from chloritech_d.dbo.rcpt2 r1
where r1.closing is  null

update  r1
set r1.issue =0
from chloritech_d.dbo.rcpt2 r1
where r1.issue is  null


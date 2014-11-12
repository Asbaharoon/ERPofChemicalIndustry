update cw
set cw.opning = 0
from chloritech_d.dbo.cwsr_final cw
where cw.opning is null

update cw
set cw.receipt = r2.rcpt
from chloritech_d.dbo.cwsr_final cw , chloritech_d.dbo.rcpt2 r2
where cw.category = r2.category and cw.receipt is  null

update cw
set cw.issue = r2.issue
from chloritech_d.dbo.cwsr_final cw , chloritech_d.dbo.rcpt2 r2
where cw.category = r2.category and cw.issue is  null


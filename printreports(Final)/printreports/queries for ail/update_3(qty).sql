update cw
set cw.opning = 0
from chloritech_d.dbo.ail_final cw
where cw.opning is null

update cw
set cw.accepted = r2.acpqty
from chloritech_d.dbo.ail_final cw , chloritech_d.dbo.rcp2 r2
where cw.category = r2.category and cw.accepted is  null

update cw
set cw.issue = r2.issued
from chloritech_d.dbo.ail_final cw , chloritech_d.dbo.rcp2 r2
where cw.category = r2.category and cw.issue is  null


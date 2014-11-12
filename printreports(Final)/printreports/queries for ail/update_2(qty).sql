

update  r1
set r1.issued =0
from chloritech_d.dbo.rcp1 r1
where r1.issued is  null;

update  r1
set r1.closing = r1.oq 
from chloritech_d.dbo.rcp1 r1
where r1.closing is  null;


update  r1
set r1.issued =0
from chloritech_d.dbo.rcp2 r1
where r1.issued is  null;

update  r1
set r1.closing = r1.oq
from chloritech_d.dbo.rcp2 r1
where r1.closing is  null;


--update  r1
--set r1.issued = 0 ,r1.closing = r1.oq+r1.acpqty
--from chloritech_d.dbo.rcp1 r1
--where (r1.oq+r1.acpqty)<r1.issued;


--update  r1
--set r1.issued = 0 ,r1.closing = r1.oq+r1.acpqty
--from chloritech_d.dbo.rcp2 r1
--where (r1.oq+r1.acpqty)<r1.issued;

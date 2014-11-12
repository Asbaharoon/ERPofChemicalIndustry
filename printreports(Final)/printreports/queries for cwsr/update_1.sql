update t3
set f_rate = (ti.openingval/ti.openingqty)
from chloritech_d.dbo.temptbl3 t3 , chloritech_d.dbo.tblitemmaster ti
where t3.id = ti.id and f_rate is  null 

update t3
set t3.recpt = 0
from chloritech_d.dbo.temptbl3 t3
where t3.recpt is null
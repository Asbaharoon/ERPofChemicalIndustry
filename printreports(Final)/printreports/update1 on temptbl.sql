update t3
set f_rate = ti.rate
from chloritech_db.dbo.temptable t3 , chloritech_db.dbo.tblitemmaster ti
where t3.id = ti.id and f_rate is  null 



update t3
set t3.acpqty = 0
from chloritech_d.dbo.iwsr_temp t3
where t3.acpqty is null
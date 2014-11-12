

-- '2012-04-01''2012-04-30'

drop table  chloritech_d.dbo.iwsr_temp

select  op.id   , op.oq , rec.acpqty ,titm.category
into chloritech_d.dbo.iwsr_temp
from 
(select ti.id,SUM(ti.openingqty) as  oq ,SUM(ti.openingval)as ov 
from chloritech_d.dbo.tblitemmaster ti

 group by ti.id) op full OUTER JOIN 

(select ti.itemdescription ,SUM(accepted) as acpqty

from chloritech_d.dbo.tblgrndetails ti 

where ti.recpdate >=$start and ti.recpdate<=$end 
 
 group  by  ti.itemdescription) rec    
 
 ON op.id =rec.itemdescription,
 chloritech_d.dbo.tblitemmaster titm
 
 where ( op.oq!=0 or rec.acpqty!=0)and (op.id =titm.id)
 order by op.id
 
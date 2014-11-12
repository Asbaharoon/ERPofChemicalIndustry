select  op.id , (op.ov+rec.recpt)/(op.oq+rec.acpqty) as f_rate , rec.recpt ,op.ov , titm.category
into chloritech_db.dbo.temptable
from 
(select ti.id,SUM(ti.openingqty) as  oq ,SUM(ti.openingval)as ov 
from chloritech_db.dbo.tblitemmaster ti

 group by ti.id) op full OUTER JOIN 

(select ti.itemdescription ,SUM(accepted) as acpqty,(SUM(amout)+SUM(addon))-(SUM(modvat)+SUM(vat)+SUM(dnote))as  recpt

from chloritech_db.dbo.tblgrndetails ti 

where ti.grndate >=$start and ti.grndate<=$end 
 
 group  by  ti.itemdescription) rec    
 
 
 ON op.id =rec.itemdescription,
 chloritech_db.dbo.tblitemmaster titm
 
 
 where ( op.oq!=0 or rec.acpqty!=0)and (op.id =titm.id)
 order by op.id
 
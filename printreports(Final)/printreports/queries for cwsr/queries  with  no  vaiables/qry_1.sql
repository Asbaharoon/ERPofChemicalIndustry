


drop table chloritech_d.dbo.temptbl3

select  op.id , (op.ov+rec.recpt)/(op.oq+rec.acpqty) as f_rate , rec.recpt ,op.ov , op.oq,rec.acpqty,titm.category
into chloritech_d.dbo.temptbl3
from 
(select ti.id,SUM(ti.openingqty) as  oq ,SUM(ti.openingval)as ov 
from chloritech_d.dbo.tblitemmaster ti

 group by ti.id) op full OUTER JOIN 

(select ti.itemdescription ,SUM(accepted) as acpqty,(SUM(amout)+SUM(addon))-(SUM(modvat)+SUM(vat)+SUM(dnote))as  recpt

from chloritech_d.dbo.tblgrndetails ti 

where ti.dor >='2012-03-01' and ti.dor<='2012-03-01' 
 
 group  by  ti.itemdescription) rec    
 
 
 ON op.id =rec.itemdescription,
 chloritech_d.dbo.tblitemmaster titm
 
 
 where ( op.oq!=0 or rec.acpqty!=0)and (op.id =titm.id)
 order by op.id
 
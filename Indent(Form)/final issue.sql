select cm.category , SUM(az.issued)
from(select a.itemname ,a.category,pure_rate*b.total_qty as issued
from 
(select ti.id,ti.itemname,ti.category,(ld.rate+ti.rate)/2 as pure_rate   
from 
(select itemdescription , SUM(amout)as avgg_amt,SUM(accepted) as avgg_qty , SUM(amout)/SUM(accepted) as rate  
from chloritech_db.dbo.tblgrndetails 
where  grndate >= '2012-04-01' and grndate<='2012-12-01'and accepted!=0 group by itemdescription) as ld , chloritech_db.dbo.tblitemmaster as ti

where ld.itemdescription=ti.id) a ,
(select item , SUM(qtyissued) as total_qty
from chloritech_db.dbo.tblissuedetails tis 
where tis.date  >= '2012-04-01' and tis.date<='2012-12-01'
group by item) b
where a.id=b.item) as az , chloritech_db.dbo.tblcategorymasters cm
where cm.catid=az.category
group by cm.category;
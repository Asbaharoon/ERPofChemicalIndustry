drop table chloritech_db.dbo.$var
select t1.itemname,t1.abbrunits,t1.openingqty,t1.grn,iss.issue,(t1.openingqty+t1.grn-iss.issue) as closing
into chloritech_db.dbo.$var
from
(select op.id,op.itemname ,op.abbrunits ,op.openingqty ,grn.grn
from
(select ti.id,ti.itemname , ti.openingqty , tu.abbrunits
from chloritech_db.dbo.tblitemmaster ti , chloritech_db.dbo.tblunitsmaster tu
where  ti.units=tu.id and ti.category = $id ) as op left outer join 
(select itemdescription,SUM(tgd.accepted) as grn
from chloritech_db.dbo.tblgrndetails tgd
where tgd.category= $id and tgd.grndate >= $start and tgd.grndate <= $end
group by itemdescription) as grn on op.id=grn.itemdescription
where op.openingqty!=0 or grn.grn!=0) as t1 left outer join

(select  ti.id,sum(tis.qtyissued) as issue
from chloritech_db.dbo.tblissuedetails tis , chloritech_db.dbo.tblitemmaster ti
where tis.category=1 and tis.date >= $start and tis.date <= $end and ti.itemname=itemdesc
group by ti.id) as iss on t1.id = iss.id


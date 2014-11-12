select tgd.grndate , tgd.grnno , tgd.accepted , tgd.amout
from (select ti.id , ti.itemname , tu.abbrunits
from chloritech_db.dbo.tblitemmaster ti , chloritech_db.dbo.tblunitsmaster tu
where ti.itemname=$itm and  ti.units=tu.id) as itm_id 
,    (select SUM(ti.openingqty) as Opening
from chloritech_db.dbo.tblitemmaster ti
where ti.itemname=$itm
group by ti.itemname) as itm_opn
,    chloritech_db.dbo.tblgrndetails tgd
where itm_id.id=tgd.itemdescription and tgd.grndate >= $start and tgd.grndate <= $end


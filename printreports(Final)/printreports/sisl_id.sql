select distinct itm_id.itemname,itm_opn.Opening,itm_id.abbrunits
from (select ti.id , ti.itemname , tu.abbrunits
from chloritech_db.dbo.tblitemmaster ti , chloritech_db.dbo.tblunitsmaster tu
where ti.itemname='BEARING 3206' and  ti.units=tu.id) as itm_id 
,    (select SUM(ti.openingqty) as Opening
from chloritech_db.dbo.tblitemmaster ti
where ti.itemname='BEARING 3206'
group by ti.itemname) as itm_opn
,    chloritech_db.dbo.tblgrndetails tgd
where itm_id.id=tgd.itemdescription 
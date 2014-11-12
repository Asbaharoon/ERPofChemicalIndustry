select st.itemname ,(st.openingqty+grn.grn-iss.issue) as Opening,grn.grn , iss.issue ,st.abbrunits
from  
(select ti.itemname , ti.openingqty , tu.abbrunits
from chloritech_db.dbo.tblitemmaster ti , chloritech_db.dbo.tblunitsmaster tu
where ti.itemname='ADVANI OVERCARD-S 3.15 * 4.50' and ti.units=tu.id
) as st , 

(select SUM(tgd.accepted) as grn
from (select ti.id 
from chloritech_db.dbo.tblitemmaster ti 
where ti.itemname= 'ADVANI OVERCARD-S 3.15 * 4.50' ) as itm_id 
,    chloritech_db.dbo.tblgrndetails tgd
where itm_id.id=tgd.itemdescription 
and tgd.grndate >= '2012-04-01' and tgd.grndate <= '2012-07-30') as grn ,

(select  sum(tis.qtyissued) as issue
from chloritech_db.dbo.tblissuedetails tis
where tis.itemdesc= 'ADVANI OVERCARD-S 3.15 * 4.50' and tis.date >= '2012-04-01' and tis.date <= '2012-07-30') as iss





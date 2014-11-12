select st.itemname ,(st.openingqty+grn.grn-iss.issue) as Opening,st.abbrunits
from  
(select ti.itemname , ti.openingqty , tu.abbrunits
from chloritech_db.dbo.tblitemmaster ti , chloritech_db.dbo.tblunitsmaster tu
where ti.itemname=$itm and ti.units=tu.id
) as st , 

(select SUM(tgd.accepted) as grn
from (select ti.id 
from chloritech_db.dbo.tblitemmaster ti 
where ti.itemname= $itm ) as itm_id 
,    chloritech_db.dbo.tblgrndetails tgd
where itm_id.id=tgd.itemdescription 
and tgd.grndate >= $start and tgd.grndate <= $end) as grn ,

(select  sum(tis.qtyissued) as issue
from chloritech_db.dbo.tblissuedetails tis
where tis.itemdesc= $itm and tis.date >= $start and tis.date <= $end) as iss





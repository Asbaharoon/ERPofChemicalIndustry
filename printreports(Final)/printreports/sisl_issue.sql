select  tis.date,tis.issueslipno,tis.qtyissued
from chloritech_db.dbo.tblissuedetails tis
where tis.itemdesc=$itm and tis.date >=$start and tis.date <= $end
ORDER BY tis.issueslipno
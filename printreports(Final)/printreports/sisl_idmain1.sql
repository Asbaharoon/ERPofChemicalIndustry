select ti.itemname , ti.openingqty as Opening, tu.abbrunits
from chloritech_db.dbo.tblitemmaster ti , chloritech_db.dbo.tblunitsmaster tu
where ti.itemname=$itm and ti.units=tu.id






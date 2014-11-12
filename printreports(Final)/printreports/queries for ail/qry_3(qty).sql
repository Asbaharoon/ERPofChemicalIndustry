drop table chloritech_d.dbo.ail_final;

select r2.id,r2.category, r1.closing as opning ,r2.acpqty-r1.acpqty  as accepted ,r2.issued-r1.issued as issue , r2.closing as  closing 
into  chloritech_d.dbo.ail_final
from chloritech_d.dbo.rcp2 r2 left outer  join chloritech_d.dbo.rcp1 r1 on r2.id= r1.id 
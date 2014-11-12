drop  table chloritech_d.dbo.rcpt2
select ti.category ,SUM(iss.recpt)as rcpt ,SUM(iss.ov)as opening  ,SUM(iss.issued)as issue , SUM(iss.ov)+SUM(iss.recpt)-SUM(iss.issued) as closing 
into chloritech_d.dbo.rcpt2
 from chloritech_d.dbo.tblitemmaster ti , 

(select  t2.ov,t2.recpt,t2.id,qtyissue*t2.f_rate as  issued 
from
chloritech_d.dbo.temptbl3 t2 left outer  join
 
(select idet.item , SUM(idet.qtyissued) as qtyissue
from chloritech_d.dbo.tblissuedetails idet
where idet.dois >='2012-04-01' and idet.dois<='2012-12-01'  
group by idet.item)  qi
on t2.id = qi.item
) iss

where ti.id = iss.id
group by  ti.category
order by ti.category


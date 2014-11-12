
select tc.category,SUM(iss.ov) as Opening , SUM(iss.recpt) as Receipt , SUM(iss.issued) as Issue,SUM(iss.ov)+SUM(iss.recpt)-SUM(iss.issued) as closing 
into chloritech_db.dbo.report1 
from chloritech_db.dbo.tblitemmaster ti, 
(select  t2.ov,t2.recpt,t2.id,qtyissue*t2.f_rate as issued 
from
chloritech_db.dbo.temptable t2 left outer  join
(select idet.item , SUM(idet.qtyissued) as qtyissue
from chloritech_db.dbo.tblissuedetails as idet
where idet.date >=$start and idet.date<=$end  
group by idet.item) qi
on t2.id = qi.item
) as iss , 
chloritech_db.dbo.tblcategorymasters tc
where tc.catid=ti.category and ti.id = iss.id 
group by  tc.category
order by tc.category

update r
set r.closing = r.Opening + r.Receipt , r.Issue=0
from chloritech_db.dbo.report1 as r
where r.closing is null
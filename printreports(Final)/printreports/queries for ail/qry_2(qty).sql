

drop  table chloritech_d.dbo.rcp1;

select  t2.id,t2.category,t2.oq,t2.acpqty, qi.qtyissue as  issued , t2.oq + t2.acpqty - qi.qtyissue as  closing
into chloritech_d.dbo.rcp1
from
chloritech_d.dbo.iwsr_temp t2 left outer  join
 
 (select idet.item , SUM(idet.qtyissued) as qtyissue
from chloritech_d.dbo.tblissuedetails idet
where idet.date >=$start and idet.date<=$end
group by idet.item)  qi
on t2.id = qi.item

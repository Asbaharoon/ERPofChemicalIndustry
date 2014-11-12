delete from chloritech_db.dbo.report_cwr
insert into chloritech_db.dbo.report_cwr (Itemname,Unit,Opening,Accepted,Issued,Closing)
select *
from
chloritech_db.dbo.cwrtmp1 tbl1 
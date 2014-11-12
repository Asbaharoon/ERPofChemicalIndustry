delete from chloritech_db.dbo.report_cwr
insert into chloritech_db.dbo.report_cwr (Itemname,Unit,Opening,Accepted,Issued,Closing)
select tbl1.itemname as Itemname ,tbl1.abbrunits as Unit,tbl1.closing as Opening,(tbl2.grn-tbl1.grn) as Accepted ,(tbl2.issue-tbl1.issue) as Issued ,tbl1.closing+((tbl2.grn-tbl1.grn)-(tbl2.issue-tbl1.issue)) as Closing
from
chloritech_db.dbo.cwrtmp1 as tbl1 , chloritech_db.dbo.cwrtmp2 as tbl2
where tbl1.itemname=tbl2.itemname
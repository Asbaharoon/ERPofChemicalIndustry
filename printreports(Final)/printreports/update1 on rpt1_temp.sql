update rpt1
set rpt1.Issue=0 , rpt1.closing=0
from chloritech_db.dbo.report1_temp rpt1
where rpt1.Issue is null or rpt1.closing is null 
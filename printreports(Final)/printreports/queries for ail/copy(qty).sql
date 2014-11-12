
delete from chloritech_d.dbo.ail_rcpt;
insert into chloritech_d.dbo.ail_rcpt(item,category,opning,accepted,issued,closing)
select ti.itemname , tc.category , af.opning , af.accepted , af.issue , af.closing from chloritech_d.dbo.ail_final af, chloritech_d.dbo.tblitemmaster ti ,  chloritech_d.dbo.tblcategorymasters tc
where ti.id = af.id and  af.category = tc.catid and (af.opning+af.accepted)>=af.issue
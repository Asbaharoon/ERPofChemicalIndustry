
delete from chloritech_d.dbo.report1_temp
INSERT  INTO chloritech_d.dbo.report1_temp (category,Opening,Receipt,Issue,closing)
select tc.category , cwsr.opning ,cwsr.receipt,cwsr.issue,cwsr.closing from  chloritech_d.dbo.cwsr_final cwsr , chloritech_d.dbo.tblcategorymasters tc  where cwsr.category = tc.catid 

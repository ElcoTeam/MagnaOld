SELECT co.[co_id]
                                  ,[co_no]
	                              ,corel.all_id
	                              ,corel.co_count
	                              ,p.part_no
	                              ,p.part_name
	                              ,p.part_categoryid
                              FROM [mg_CustomerOrder] co
                              left join mg_cusOrder_Allpart_rel corel on co.co_id = corel.co_id
                              left join mg_allpart_part_rel aprel on corel.all_id = aprel.all_id
                              left join mg_part p  on aprel.partid_id = p.part_id
                              where co_isCutted = 0 and co.co_id=3
                            order by co_order,all_id




							
--DELETE FROM mg_Order_FSD
--DELETE FROM mg_Order_FSDB
--DELETE FROM mg_Order_FSDC
--DELETE FROM mg_Order_FSP
--DELETE FROM mg_Order_FSPB
--DELETE FROM mg_Order_FSPC
--DELETE FROM mg_Order_RSB40
--DELETE FROM mg_Order_RSB60
--DELETE FROM mg_Order_RSC


--select * from mg_Order_FSD
--select * from mg_Order_FSDB
--select * from mg_Order_FSDC
--select * from mg_Order_FSP
--select * from mg_Order_FSPB
--select * from mg_Order_FSPC
--select * from mg_Order_RSB40
--select * from mg_Order_RSB60
--select * from mg_Order_RSC

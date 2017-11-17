
--select ap.all_id,ap.all_no,p.part_id,p.part_no,p.part_categoryid,prop.prop_name from mg_allpart ap
--inner join mg_allpart_part_rel aprel on ap.all_id = aprel.all_id
--inner join mg_part p on aprel.partid_id = p.part_id
--left join mg_Property prop on p.part_categoryid=prop.prop_id and prop.Prop_type=5 

--order by all_id

if exists(select * from tempdb..sysobjects where id=object_id('tempdb..#temp')) 
	                            drop table #temp;


                            with data as
                            (
                            SELECT 
                                  step.[fl_id]
	                              ,fl.fl_name

	                             ,prop.Prop_type
	                             ,p.part_categoryid 
	                             ,prop.prop_name
	                             ,step.[part_id] 
	                             ,p.part_no
	                             ,p.part_name 

	                               ,step.[st_id]
	                              ,st.st_no
	                              ,st.st_name
	                              ,st.st_typeid
								  ,st_order
	                              ,prop1.prop_name st_typename

                                  ,step.[bom_id]
	                              ,bom.bom_storeid
								  ,bom.bom_isCustomerPN
								  ,bom.bom_PN
								  ,bom.bom_customerPN
								  ,case bom.bom_isCustomerPN
								  when 1 then bom.bom_PN
								  when 0 then  bom.bom_customerPN
								  end bom_barcode
	                              ,prop2.prop_name bom_storename

	                              ,step.bom_count
	                              ,[step_id]
                                  ,[step_name]
	                              ,[step_plccode]
                                  ,[step_order]
                              FROM [mg_step] step
                              left join mg_FlowLine fl on step.fl_id = fl.fl_id
                              left join mg_station st on st.st_id=step.st_id
                              left join mg_part p on p.part_id=step.part_id
                              left join mg_Property prop on prop.prop_id = p.part_categoryid
                              left join mg_Property prop1 on prop1.prop_id = st.st_typeid 
                              left join mg_BOM bom on bom.bom_id = step.bom_id
                              left join mg_Property prop2 on prop2.prop_id = bom.bom_storeid
                              )select * into #temp from data  


                             --  select distinct part_categoryid,part_id,part_name from #temp order by part_categoryid,part_id;
                              -- select distinct part_categoryid,part_id,part_name,st_id,st_no,st_typeid,st_order from #temp order by part_categoryid,part_id,st_order;
                               --select distinct part_categoryid,part_id,part_name,st_id,st_no,st_typeid,st_order,step_id,step_order  from #temp order by part_categoryid,part_id,st_order,step_order;

                               select * from #temp order by part_categoryid,part_id,st_order,step_order;

                              drop table #temp

    --select * from #temp where part_categoryid=19
  --select * from #temp where part_categoryid=43
  --select * from #temp where part_categoryid=44
  --select * from #temp where part_categoryid=45
  --select * from #temp where part_categoryid=46
  --select * from #temp where part_categoryid=47
  --select * from #temp where part_categoryid=48
  --select * from #temp where part_categoryid=49
  --select * from #temp where part_categoryid=50

  --order by fl_id,part_categoryid,step_order	

     --FS_Drive = 19,
   --     FSB_Drive = 43,
   --     FSC_Drive = 44,
   --     RSB40 = 45,
   --     RSB60 = 46,
   --     RSC = 47,
   --     FS_Passenger = 48,
   --     FSB_Passenger = 49,
   --     FSC_Passenger = 50

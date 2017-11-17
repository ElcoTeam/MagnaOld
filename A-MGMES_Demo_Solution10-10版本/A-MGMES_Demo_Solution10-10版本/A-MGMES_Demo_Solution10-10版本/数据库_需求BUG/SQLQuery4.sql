

SELECT  co.co_no
	,fsd.ofsd_id
	  --,fsd.[ofsd_pre]+fsd.[part_no]+fsd.[ofsd_cdstr]+right('0000'+ltrim(fsd.[ofsd_id]),4)fsd_no
	  --,fsp.[ofsp_pre]+fsp.[part_no]+fsp.[ofsp_cdstr]+right('0000'+ltrim(fsp.[ofsp_id]),4)ofsp_no
	  --,fsdb.[ofsdb_pre]+fsdb.[part_no]+fsdb.[ofsdb_cdstr]+right('0000'+ltrim(fsdb.[ofsdb_id]),4)ofsdb_no
	  --,fspb.[ofspb_pre]+fspb.[part_no]+fspb.[ofspb_cdstr]+right('0000'+ltrim(fspb.[ofspb_id]),4)ofspb_no
	  ,fsdc.[ofsdc_pre]+fsdc.[part_no]+fsdc.[ofsdc_cdstr]+right('0000'+ltrim(fsdc.[ofsdc_id]),4)ofsdc_no
	  ,fspc.[ofspc_pre]+fspc.[part_no]+fspc.[ofspc_cdstr]+right('0000'+ltrim(fspc.[ofspc_id]),4)ofspc_no
	  ,rsb40.[orsb40_pre]+rsb40.[part_no]+rsb40.[orsb40_cdstr]+right('0000'+ltrim(rsb40.[orsb40_id]),4)orsb40_no
	  ,rsb60.[orsb60_pre]+rsb60.[part_no]+rsb60.[orsb60_cdstr]+right('0000'+ltrim(rsb60.[orsb60_id]),4)orsb60_no
	  ,rsc.[orsc_pre]+rsc.[part_no]+rsc.[orsc_cdstr]+right('0000'+ltrim(rsc.[orsc_id]),4)orsc_no
	 
  FROM [mg_Order_FSD] fsd
  inner join mg_CustomerOrder co on fsd.co_id = co.co_id and co.co_isCutted=1 and fsd.ofsd_unproducing=1
  inner join mg_Order_FSDB fsdb on fsd.co_id =fsdb.co_id and fsd.ofsd_id = fsdb.ofsdb_id
  inner join mg_Order_FSDC fsdc on fsd.co_id =fsdc.co_id and fsd.ofsd_id = fsdc.ofsdc_id
  inner join mg_Order_FSP fsp on fsd.co_id =fsp.co_id and fsd.ofsd_id = fsp.ofsp_id
  inner join mg_Order_FSPB fspb on fsd.co_id =fspb.co_id and fsd.ofsd_id = fspb.ofspb_id
  inner join mg_Order_FSPC fspc on fsd.co_id =fspc.co_id and fsd.ofsd_id = fspc.ofspc_id
  inner join mg_Order_RSB40 rsb40 on fsd.co_id =rsb40.co_id and fsd.ofsd_id = rsb40.orsb40_id
  inner join mg_Order_RSB60 rsb60 on fsd.co_id =rsb60.co_id and fsd.ofsd_id = rsb60.orsb60_id
  inner join mg_Order_RSC rsc on fsd.co_id =rsc.co_id and fsd.ofsd_id = rsc.orsc_id
order by co.co_order,fsd.ofsd_id






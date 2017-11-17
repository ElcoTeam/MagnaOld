
SELECT top 1 fsd.[ofsd_pre]+fsd.[part_no]+fsd.[ofsd_cdstr]+right('0000'+ltrim(fsd.[ofsd_id]),4)orderno
,[ofsd_id] id,fsd.co_id coid
     
  FROM [mg_Order_FSD] fsd 
  inner join mg_CustomerOrder co on fsd.co_id = co.co_id
  where (ofsd_isPLC is null or  ofsd_isPLC <>1)
  order by co.co_order,fsd.ofsd_id


  
SELECT top 1 fsp.[ofsp_pre]+fsp.[part_no]+fsp.[ofsp_cdstr]+right('0000'+ltrim(fsp.[ofsp_id]),4)orderno
,[ofsp_id] id,fsp.co_id coid
     
  FROM mg_Order_FSP fsp 
  inner join mg_CustomerOrder co on fsp.co_id = co.co_id
  where (ofsp_isPLC is null or  ofsp_isPLC <>1)
  order by co.co_order,fsp.ofsp_id


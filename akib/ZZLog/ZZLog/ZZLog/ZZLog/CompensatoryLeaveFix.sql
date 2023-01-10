update [HRM_CompensatoryLeave]
  set ManagerId = 
  (select top 1 Id
  from HRM_Employee
  where Name = [HRM_CompensatoryLeave].AuthorizedBy)
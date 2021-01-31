select *
from Sistema
where (rOUND(POSx,2), Round(posY,2), Round(posZ,2)) in (
	SELECT rOUND(POSx,2), Round(posY,2), Round(posZ,2)
	FROM SISTEMA
	GROUP BY rOUND(POSx,2), Round(posY,2), Round(posZ,2)
	having count(1) > 1)
order by 6,7,8
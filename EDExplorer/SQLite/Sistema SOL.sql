Select *
from Cuerpo
join sistema on Sistema.Id = SistemaId
where sistema.nombre = "Sol"
order by cuerpo.id
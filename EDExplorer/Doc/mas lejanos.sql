select *, (posx*posx+posy*posy+posz*posz) dd
from Sistema
order by (posx*posx+posy*posy+posz*posz) desc
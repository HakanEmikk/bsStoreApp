using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DataTransferObjets
{
    public record BookDtoForUpdate(int Id,string Title,decimal Price);

}

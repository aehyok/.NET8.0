using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.RabbitMQ
{
    public interface ICF
    {
        public void Publish();
 
        public void Subscrber();
    }
}

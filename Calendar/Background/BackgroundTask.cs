using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Background
{
    class BackgroundTask
    {
        static BackgroundTask Ins=null;

        private BackgroundTask()
        {

        }

        static BackgroundTask getInstance()
        {
            if(Ins == null)
            {
                Ins = new BackgroundTask();
            }

            return Ins;
        }



    }
}

using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.Helpers
{
    class ExceptionHandler
    {
        public static void LogException(Exception ex)
        {
            ILog logger = LogManager.GetLogger("logger");

            logger.Error("Ocurrió un error.", ex);
        }
    }
}

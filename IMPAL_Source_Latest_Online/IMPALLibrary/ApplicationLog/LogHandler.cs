using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using log4net;

namespace IMPALLibrary 
{
    public  sealed class Log
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       
        // All methods are static, so this can be private 
        private Log()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="MethodName"></param>
        /// <param name="Message"></param>
        public static void WriteLog(Type Source, string MethodName, string Message)
        {
            ILog logger = log4net.LogManager.GetLogger(Source);
            logger.Info("MethodName: " + MethodName + " Message Details: " + Message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="Ex"></param>
        public static void WriteException(Type Source, Exception exp)
        {
            ILog logger = log4net.LogManager.GetLogger(Source);
            log.Error(Source, exp);

            //Added for redirecting to Custom Error Page
            if (exp != null)
                throw exp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="Ex"></param>
        public static void WriteExceptionCustom(Type Source, Exception exp)
        {
            ILog logger = log4net.LogManager.GetLogger(Source);
            log.Error(Source, exp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="Ex"></param>
        public static void WriteLoginException(Type Source, Exception exp)
        {
            ILog logger = log4net.LogManager.GetLogger(Source);
            log.Error(Source, exp);
        }

        internal static void WriteException(Type source, string v)
        {
            throw new NotImplementedException();
        }
    }
}

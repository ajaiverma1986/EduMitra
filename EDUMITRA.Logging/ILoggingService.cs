using EDUMITRA.Datamodel.Interfaces;
using System;
using System.Threading.Tasks;


namespace EDUMITRA.Logging
{
    public interface ILoggingService
    {
        Task LogError(Exception exception, Log log, object moreInfo = null);
        Task LogError(Exception exception, object loggerClass, object moreInfo = null);
        Task LogError(Exception exception, Log log, object loggerClass, object moreInfo = null);
        Task LogError(Exception exception, object loggerClass, IEDUMITRAServiceUser serviceUser, object moreInfo = null);
        Task LogError(Exception exception, object loggerClass, string moduleName, string title, object moreInfo = null);
        Task LogMessage(string message, IEDUMITRAServiceUser serviceUser, object moreInfo = null);
        Task LogMessage(Log log, object loggerClass, object moreInfo = null);
        Task LogMessage(string message, IEDUMITRAServiceUser spineUser, string moduleName, string title, object moreInfo = null);
    }
}

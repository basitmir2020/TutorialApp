using System.Reflection;
using log4net;

namespace TutorialApp.Business.Common.Middleware.Exception;

public class LogManager : ILogManager
{
    private static readonly ILog Logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

    public void LogInfo(string message)
    {
        Logger.Info(message);
    }

    public void LogWarn(string message)
    {
        Logger.Warn(message);
    }

    public void LogDebug(string message)
    {
        Logger.Debug(message);
    }

    public void LogError(string message)
    {
        Logger.Error(message);
    }
}
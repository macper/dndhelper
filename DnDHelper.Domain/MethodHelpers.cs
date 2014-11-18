using System;

namespace DnDHelper.Domain
{
    public static class MethodHelpers
    {
        public static void ThrowAndLog(Exception exc, ILogger logger)
        {
            logger.Error(exc.Message, exc);
            throw exc;
        }
    }
}

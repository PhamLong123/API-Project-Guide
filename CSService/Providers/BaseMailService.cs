using CRMProfileShared;
using CSService.Models;

namespace CSService.Providers
{
    /// <summary>
    /// Base Mail Service
    /// </summary>
    public abstract class BaseMailService
    {
        /// <summary>
        /// Send mail to an email address
        /// </summary>
        /// <param name="messageInfo">Message Info</param>
        /// <returns>Result Status</returns>
        public abstract Result Send(MessageInfo messageInfo);
    }
}

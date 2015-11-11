#region Usings
    using System.Diagnostics;
#endregion

namespace Pawelby.Services
{
    /// <summary>
    /// Debug mail service class
    /// </summary>
    public class DebugMailService : IMailService
    {
        /// <summary>
        /// Sending Mail 
        /// </summary>
        /// <param name="to">To whom</param>
        /// <param name="from">From</param>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <returns>True if success</returns>
        public bool SendMail(string to, string from, string subject, string body)
        {
            Debug.WriteLine($"Sending mail: To: {to}, Subject: {subject}");
            return true;
        }
    }
}

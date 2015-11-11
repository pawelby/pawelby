namespace Pawelby.Services
{
    /// <summary>
    /// Interface of mail service
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// Sending mail
        /// </summary>
        /// <param name="to">To whom</param>
        /// <param name="from">From</param>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <returns>True if success</returns>
        bool SendMail(string to, string from, string subject, string body);
    }
}

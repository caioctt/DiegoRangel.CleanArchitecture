namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Email
{
    public class EmailSettings
    {
        /// <summary>
        /// SMTP Host name/IP.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// SMTP Port.
        /// </summary>
        public short Port { get; set; }

        /// <summary>
        /// User name to login to SMTP server.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password to login to SMTP server.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Is SSL enabled?
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// Use default credentials?
        /// </summary>
        public bool UseDefaultCredentials { get; set; }

        /// <summary>
        /// The no-reply email address.
        /// </summary>
        public string NoReplyMail { get; set; }
    }
}
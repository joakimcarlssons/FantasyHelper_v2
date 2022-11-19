using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyHelper.Shared.Config
{
    public class EmailOptions
    {
        public const string Key = "Email";
        public string SenderEmail { get; set; } = default!;
        public string SenderName { get; set; } = default!;
        public string ReceiverEmail { get; set; } = default!;
        public string ReceiverName { get; set; } = default!;
    }

    public class SmtpOptions
    {
        public const string Key = "Smtp";
        public string Host { get; set; } = default!;
        public short Port { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}

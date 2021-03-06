using System;
using System.Collections.Generic;
using System.Net;

namespace ConvertFromIISLogFile
{
    /// <summary>
    /// W3C Extended Log File Format
    /// http://www.w3.org/TR/WD-logfile.html
    /// 
    /// Table 10.1 W3C Extended Log File Fields
    /// http://www.microsoft.com/technet/prodtechnol/WindowsServer2003/Library/IIS/676400bc-8969-4aa7-851a-9319490a9bbb.mspx?mfr=true
    /// </summary>
    public class LogEntry : ILogEntry
    {

        /// <summary>
        /// The date and time (UTC) on which the activity occurred.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// The date and time (Local) on which the activity occurred.
        /// </summary>
        public DateTime DateTimeLocalTime => DateTime.SpecifyKind(this.DateTime, DateTimeKind.Utc).ToLocalTime();

        /// <summary>
        ///     Field: s-ip
        /// </summary>
        public string SourceIpAddress { get; set; }

        /// <summary>
        /// The requested action, for example, a GET method.
        /// 
        ///     Field: cs-method
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// The target of the action, for example, Default.htm.
        /// 
        ///     Field: cs-uri-stem
        /// </summary>
        public string UriStem { get; set; }

        /// <summary>
        /// The query, if any, that the client was trying to perform. A Universal Resource Identifier (URI) query is necessary only for dynamic pages.
        /// 
        ///     Field: cs-uri-query
        /// </summary>
        public string UriQuery { get; set; }

        /// <summary>
        /// The server port number that is configured for the service.
        /// 
        ///     Field: s-port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// The name of the authenticated user who accessed your server. Anonymous users are indicated by a hyphen.
        /// 
        ///     Field: cs-username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The IP address of the client that made the request.
        /// 
        ///     Field: c-ip
        /// </summary>
        public string ClientIpAddress { get; set; }

        /// <summary>
        /// The browser type that the client used.
        /// 
        ///     Field: cs(User-Agent)
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// The site that the user last visited. This site provided a link to the current site. 
        /// 
        ///     Field: cs(Referrer)
        /// </summary>
        public string Referrer { get; set; }

        /// <summary>
        /// The HTTP status code.
        /// 
        ///     Field: sc-status
        /// </summary>
        public string HttpStatus { get; set; }

        /// <summary>
        /// The substatus error code. 
        /// 
        ///     Field: sc-substatus
        /// </summary>
        public string ProtocolSubstatus { get; set; }

        /// <summary>
        /// Win32 error code
        ///     Field: sc-win32-status
        /// 
        /// https://msdn.microsoft.com/en-us/library/ms681381.aspx
        /// </summary>
        public string SystemErrorCodes { get; set; }

        /// <summary>
        /// The number of bytes that the server sent.
        /// 
        ///     Field: sc-bytes
        /// </summary>
        public int ServerSentBytes { get; set; }

        /// <summary>
        /// The number of bytes that the server received.
        /// 
        ///     Field: cs-bytes
        /// </summary>
        public int ServerReceivedBytes { get; set; }

        /// <summary>
        /// The length of time that the action took, in milliseconds. 
        /// 
        ///     Field: time-taken
        /// </summary>
        public int TimeTaken { get; set; }


        /// <summary>
        /// The Internet service name and instance number that was running on the client.
        /// 
        ///     Field: s-sitename
        /// </summary>
        public string SiteName { get; set; }


        /// <summary>
        /// The host geader
        /// 
        ///     Field: cs-host
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// The name of the server on which the log file entry was generated.
        /// 
        ///     Field: s-computername
        /// </summary>
        public string ComputerName { get; set; }

        public string LogFile { get; set; }
        public string LogFileRootFolder { get; set; }

        public Dictionary<string,string> NotedProperties { get; set; }
    }

  
}
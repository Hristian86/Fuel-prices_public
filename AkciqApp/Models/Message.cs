namespace AkciqApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// Used for non error status.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <param name="message">Message for the given task.</param>
        public Message(int statusCode, string message)
        {
            this.message = message;
            this.StatusCode = statusCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// Send array of errors.
        /// </summary>
        /// <param name="statusCode">Status of the request.</param>
        /// <param name="errors">Collection of errors.</param>
        public Message(int statusCode, List<string> errors)
        {
            this.errors = errors;
            this.StatusCode = statusCode;
        }

        public string message { get; set; }

        public int StatusCode { get; set; }

        public IEnumerable<string> errors { get; set; }
    }
}
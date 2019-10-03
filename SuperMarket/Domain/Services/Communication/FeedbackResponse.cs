using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SuperMarket.API.Domain.Models;

namespace SuperMarket.API.Domain.Services.Communication
{
    public class FeedbackResponse : BaseResponse
    {
        public Feedback Feedback { get; protected set; }
        
        private FeedbackResponse(bool success, string message, Feedback feedback) : base(success, message)
        {
            Feedback = feedback;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="feedback">Saved Feedback</param>
        /// <return>Response.</return>
        public FeedbackResponse(Feedback feedback) : this(true, string.Empty, feedback)
        { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <return>Response.</return>
        public FeedbackResponse(string message) : this(false, message, null)
        { }
    }
}

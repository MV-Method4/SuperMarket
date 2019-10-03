using SuperMarket.API.Domain.Models;
using SuperMarket.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMarket.API.Domain.Services
{
    public interface IFeedbackService
    {
        Task<FeedbackResponse> AddFeedback(Feedback feedback);
    }
}

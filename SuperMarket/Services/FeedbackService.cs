using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperMarket.API.Domain.Models;
using SuperMarket.API.Domain.Repositories;
using SuperMarket.API.Domain.Services;
using SuperMarket.API.Domain.Services.Communication;

namespace SuperMarket.API.Services
{
    public class FeedbackService :IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IUnitOfWork _unitOfWork;
        public FeedbackService(IFeedbackRepository feedbackRepository, IUnitOfWork unitOfWork)
        {
            _feedbackRepository = feedbackRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<FeedbackResponse> AddFeedback(Feedback feedback)
        {
            try
            {
                await _feedbackRepository.AddAsync(feedback);
                await _unitOfWork.CompleteAsync();

                return new FeedbackResponse(feedback);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new FeedbackResponse($"An error occurred when saving the Feedback: {ex.Message}");
            }
        }

        
    }
}

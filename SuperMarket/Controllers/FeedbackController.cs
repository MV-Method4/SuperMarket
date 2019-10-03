using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SuperMarket.API.Domain.Models;
using SuperMarket.API.Domain.Services;
using SuperMarket.API.Extensions;
using SuperMarket.API.Resources;

namespace SuperMarket.API.Controllers
{
    [Produces("application/json")]
    [Route("/api/[controller]")]
    [ApiController]
    public class FeedbackController : Controller
    {

        private readonly IFeedbackService _feedbackService;
        private readonly IMapper _mapper;

        public FeedbackController(IFeedbackService feedbackService, IMapper mapper)
        {
            _feedbackService = feedbackService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveFeedbackResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var feedback = _mapper.Map<SaveFeedbackResource, Feedback>(resource);
            var result = await _feedbackService.AddFeedback(feedback);

            if (!result.Success)
                return BadRequest(result.Message);

            var productResource = _mapper.Map<Feedback, FeedbackResource>(result.Feedback);
            return Ok(productResource);
        }

    }
}

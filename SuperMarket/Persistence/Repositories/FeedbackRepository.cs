using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SuperMarket.API.Domain.Models;
using SuperMarket.API.Domain.Repositories;
using SuperMarket.API.Persistence.Contexts;

namespace SuperMarket.API.Persistence.Repositories
{
    public class FeedbackRepository : BaseRepository, IFeedbackRepository
    {
        public FeedbackRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Feedback feedback)
        {
            var result = await _context.Feedbacks.AddAsync(feedback);
        }
    }
}

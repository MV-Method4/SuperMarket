using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SuperMarket.API.Domain.Models;

namespace SuperMarket.API.Domain.Repositories
{
    public interface IFeedbackRepository
    {
        Task AddAsync(Feedback feedback);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectNASA.Services
{
    public interface IApodService
    {
        Task<Apod> GetAstronomyPictureoftheDayAsync();

    }
}

using LoginForm.Data.Repositorys;
using LoginForm.Services.Dto.Requests;
using LoginForm.Services.Dto.Responses;
using LoginForm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginForm.Services
{

    public class UserActivityService : IUserActivityService
    {
        private readonly IUserActivityRepository _userActivityRepository;
        public UserActivityService(IUserActivityRepository userActivityRepository)
        {
            _userActivityRepository = userActivityRepository;
        }

        public CreateUserActivityResponse Create(CreateUserActivityRequest CreateUserActivityRequest)
        {
            try
            {
                _userActivityRepository.Add(new Domain.UserActivity(CreateUserActivityRequest.IP,
                                                                    CreateUserActivityRequest.UserAgent,
                                                                    CreateUserActivityRequest.Cookie,
                                                                    CreateUserActivityRequest.Url,
                                                                    CreateUserActivityRequest.Body));
                _userActivityRepository.SaveChanges();

                return new CreateUserActivityResponse()
                {
                    StatusCode = Dto.StatusCode.Created,
                    Data = new UserActivityViewModel(CreateUserActivityRequest.IP,
                                                     CreateUserActivityRequest.UserAgent,
                                                     CreateUserActivityRequest.Cookie,
                                                     CreateUserActivityRequest.Url,
                                                     CreateUserActivityRequest.Body),

                    Message = "UserActivity Created Succesfull",
                };
            }
            catch
            {
                return new CreateUserActivityResponse()
                {
                    StatusCode = Dto.StatusCode.InternullServerError,
                    Data = new UserActivityViewModel(CreateUserActivityRequest.IP,
                                                     CreateUserActivityRequest.UserAgent,
                                                     CreateUserActivityRequest.Cookie,
                                                     CreateUserActivityRequest.Url,
                                                     CreateUserActivityRequest.Body),

                    Message = "Internull Server Error Are happend",
                };
            }
        }
    }

    public interface IUserActivityService
    {
        CreateUserActivityResponse Create(CreateUserActivityRequest CreateUserActivityRequest);
    }
}

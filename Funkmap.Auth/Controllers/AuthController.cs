﻿using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Funkmap.Auth.Data.Abstract;
using Funkmap.Auth.Data.Entities;
using Funkmap.Common.Filters;
using Funkmap.Common.Notification.Abstract;
using Funkmap.Module.Auth.Confirmation;
using Funkmap.Module.Auth.Models;

namespace Funkmap.Module.Auth.Controllers
{
    [RoutePrefix("api/auth")]
    [ValidateRequestModel]
    public class AuthController : ApiController
    {
        private readonly IAuthRepository _authRepository;
        private readonly INotificationService _notificationService;

        private static readonly ConcurrentDictionary<string, UserConfirmationModel> _usersConfirmationCache = new ConcurrentDictionary<string, UserConfirmationModel>();
        public AuthController(IAuthRepository authRepository, INotificationService notificationService)
        {
            _authRepository = authRepository;
            _notificationService = notificationService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IHttpActionResult> Register(RegistrationRequest creds)
        {
            var isExist = await _authRepository.CheckIfExist(creds.Login);
            isExist = isExist || _usersConfirmationCache.ContainsKey(creds.Login);

            var response = new RegistrationResponse()
            {
                Success = !isExist
            };

            if (!isExist)
            {
                _usersConfirmationCache[creds.Login] = new UserConfirmationModel()
                {
                    User = new UserEntity() {Login = creds.Login, Password = creds.Password}
                };
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("sendEmail")]
        public async Task<IHttpActionResult> SendEmail(RegistrationRequest request)
        {
            var response = new RegistrationResponse();
            if (_usersConfirmationCache.ContainsKey(request.Login))
            {
                _usersConfirmationCache[request.Login].User.Email = request.Email;

                var code = new Random().Next(100000, 999999).ToString();

                var notification = new ConfirmationNotification
                {
                    Receiver = request.Email
                };
                notification.BuildMessageText(request.Login, code);
                var sendResult = await _notificationService.SendNotification(notification);
                _usersConfirmationCache[request.Login].Code = code;
                response.Success = sendResult;
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("confirm")]
        public async Task<IHttpActionResult> Confirm(ConfirmationRequest request)
        {
            var response = new RegistrationResponse();
            if (_usersConfirmationCache.ContainsKey(request.Login) && _usersConfirmationCache[request.Login].Code == request.Code)
            {
                try
                {
                    var userConfirm = _usersConfirmationCache[request.Login];
                    _authRepository.Add(userConfirm.User);
                    await _authRepository.SaveAsync();
                    response.Success = true;
                }
                catch (Exception e)
                {
                    response.Success = false;
                }
            }

            return Ok(response);
        }
    }
}

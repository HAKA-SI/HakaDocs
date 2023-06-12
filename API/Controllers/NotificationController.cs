using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(LogUserActivity))]

    public class NotificationController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }


        [HttpGet("NotificationThread")]
        public async Task<ActionResult> NotificationThread()
        {
            var notifications = await _unitOfWork.NotificationRepository.UserNotificationTread(User.GetUserId());
            return Ok(notifications);
        }

        [HttpPost("DeleteNotifications")]
        public async Task<ActionResult> DeleteNotifications(List<int> ids)
        {
            var deleted = await _unitOfWork.NotificationRepository.DeleteUserNotifications(User.GetUserId(), ids);
            if (deleted) return Ok();
            return BadRequest("impossible de supprimer ces notifications");
        }

        [HttpPost("MarkNotificationsAsReaded")]
        public async Task<ActionResult> MarkNotificationsAsReaded(List<int> ids)
        {
            bool saved = await _unitOfWork.NotificationRepository.MarkNotificationsAsRead(User.GetUserId(), ids);
            if (saved) return Ok();
            return BadRequest("impossible de modifier ces notifications");
        }
    }
}
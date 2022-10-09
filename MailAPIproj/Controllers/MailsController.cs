namespace MonqLabTest.Controllers
{
    using MailAPI.Settings;
    using MailCore.Controllers;
    using MailCore.Infrastructure;
    using MailCore.Log;
    using MailCore.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;


    [ApiController]
    [Route("/api/[controller]")]
    public class MailsController : ControllerBase
    {
        private EmailSettings _settings;

        public MailsController(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        /// <summary>
        /// Обработка POST запроса
        /// </summary>
        /// <param Параметры сообщения="message"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        public ActionResult Post([FromBody] Message message)
        {
            try
            {
                var controller = new LoggerController(new ControllerBuilder()
                                              .SetModel(new Model(new Sending(
                                                      _settings.SMTPHost,
                                                      _settings.SMTPPort,
                                                      _settings.SMTPEmail,
                                                      _settings.SMTPPassword,
                                                      _settings.EmailFrom), message))
                                              .Build(), new DatabaseLog());
                controller.SendMessage();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }


        /// <summary>
        /// Возвращение записей об отправке сообщений
        /// </summary>
        /// <returns>JSON объекты</returns>
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            try
            {
                using (SendingContext context = new SendingContext())
                {
                    var records = await context.RecordsSending.Join(context.Messages,
                                                                                    record => record.MessageId,
                                                                                    message => message.Id,
                                                                                    (record, message) => new
                                                                                    {
                                                                                        DateSending = record.timeSending,
                                                                                        Result = record.Result.ToString(),
                                                                                        Status = record.FailedMessage,
                                                                                        Subject = message.Subject,
                                                                                        Body = message.Body,
                                                                                        Recipients = message.Recipients
                                                                                    }).ToArrayAsync();
                    return new JsonResult(records);
                }
            }
            catch (Exception ex)
            {
                return new JsonResult($"{ex.Message}");
            }
        }
    }
}

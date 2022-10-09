namespace MailCore.Controllers
{
    using MailCore.Log;
    using MailCore.Models;
    using System.Threading.Tasks;

    public abstract class BaseController
    {
        public virtual IModel Model { get; set; }
        public abstract Task<RecordSending> SendMessage();
    }

    public class Controller : BaseController
    {
        /// <summary>
        /// Выполнение отправки
        /// </summary>
        /// <returns>Запись о результате отправки</returns>
        public override Task<RecordSending> SendMessage()
        {
            return Model.Execute();
        }
    }

    public class LoggerController : BaseController
    {
        BaseController baseController;
        ILog log;

        public LoggerController(BaseController controller, ILog log = null)
        {
            baseController = controller;
            this.log = new VoluntaryLog(log);
        }

        /// <summary>
        /// Передача записи о результате отправки методу логирования
        /// </summary>
        /// <returns></returns>
        public override Task<RecordSending> SendMessage()
        {
            var result = baseController.SendMessage();
            log.Log(result.Result);
            return result;
        }
    }
}

using MailCore.Infrastructure;
using System.Threading.Tasks;

namespace MailCore.Models
{
    public interface IModel
    {
        Task<RecordSending> Execute();
    }
    public class Model : IModel
    {
        private readonly IAction action;
        private readonly Message message;

        public Model (IAction action, Message message)
        {
            this.action = action;
            this.message = message;
        }

        /// <summary>
        /// Выполняет указанное действие
        /// </summary>
        /// <returns>Результат отправки</returns>
        public Task<RecordSending> Execute()
        {
            return action.Result(message);
        }
    }
}

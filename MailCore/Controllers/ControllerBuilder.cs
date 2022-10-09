using MailCore.Models;

namespace MailCore.Controllers
{
    public class ControllerBuilder
    {
        Controller controller;
        public ControllerBuilder() => controller = new Controller();
        
        /// <summary>
        /// Построение контроллера
        /// </summary>
        /// <returns>Контроллера</returns>
        public Controller Build() => controller;

        /// <summary>
        /// Присвоение модели
        /// </summary>
        /// <param Модель взаимодействия="model"></param>
        /// <returns>Строитель контроллера</returns>
        public ControllerBuilder SetModel(IModel model)
        {
            controller.Model = model;
            return this;
        }
    }
}

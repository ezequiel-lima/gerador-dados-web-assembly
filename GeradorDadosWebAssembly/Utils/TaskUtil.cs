namespace GeradorDadosWebAssembly.Utils
{
    public static class TaskUtil
    {
        /// <summary>
        /// Executa uma tarefa com limite de tempo.
        /// </summary>
        /// <typeparam name="T">Tipo do retorno da tarefa.</typeparam>
        /// <param name="taskFunc">Função da tarefa assíncrona a ser executada.</param>
        /// <param name="timeout">Tempo limite para execução.</param>
        /// <returns>O resultado da tarefa ou default(T) se o tempo limite for excedido.</returns>
        public static async Task<T?> ExecuteWithTimeout<T>(Func<Task<T>> taskFunc, TimeSpan timeout)
        {
            var task = taskFunc();
            var delay = Task.Delay(timeout);

            var completedTask = await Task.WhenAny(task, delay);
            return completedTask == task ? await task : default;
        }
    }
}

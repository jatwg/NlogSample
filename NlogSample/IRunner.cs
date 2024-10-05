    namespace NLogSample
    {
        /// <summary>
        /// Defines the contract for a runner that can perform asynchronous actions
        /// </summary>
        public interface IRunner
        {
            /// <summary>
            /// Performs an asynchronous action with the given name
            /// </summary>
            /// <param name="name">The name of the action to perform</param>
            /// <returns>A Task representing the asynchronous operation</returns>
            Task DoActionAsync(string name);
        }
    }
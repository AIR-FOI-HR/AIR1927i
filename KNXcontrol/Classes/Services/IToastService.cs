namespace Model.Services
{
    public interface IToastService
    {
        /// <summary>
        /// Interface that defines a method that should be implemented for notifying the user
        /// </summary>
        /// <param name="message"></param>
        void ShowToast(string message);
    }
}

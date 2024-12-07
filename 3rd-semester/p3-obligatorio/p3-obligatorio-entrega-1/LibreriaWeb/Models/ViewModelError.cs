namespace LibreriaWeb.Models
{
    public class ViewModelError
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}

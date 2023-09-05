namespace WEB_153504_SIVY.Domain.Models
{
    public class ResponseData<T>
    {
        // Запрашиваемые данные
        public T Data { get; set; }
        // признак успешного завершения запроса
        public bool Success { get; set; } = true;
        // сообщение в случае неуспешного завершения
        public string? ErrorMessage { get; set; }
    }
}

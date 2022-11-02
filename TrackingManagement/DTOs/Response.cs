namespace TrackingManagement.DTO
{
    public class Response<T>
    {
        public int StatusCode { get; set; }
        public T Data { get; set; }
    }
}

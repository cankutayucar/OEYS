namespace OEYS.WEB.Utilities.Responses
{
    public class CustomResponse<T>
    {
        public T? Data { get; set; }
        public bool IsSuccessfull { get; set; }
        public List<string>? Errors { get; set; }

        public static CustomResponse<T> Success()
        {
            return new CustomResponse<T> { Data = default, IsSuccessfull = true, Errors = default };
        }

        public static CustomResponse<T> Success(T data)
        {
            return new CustomResponse<T> { Data = data, IsSuccessfull = true, Errors = default };
        }

        public static CustomResponse<T> Fail(List<string> errors)
        {
            return new CustomResponse<T> { Errors = errors, IsSuccessfull = false, Data = default };
        }

        public static CustomResponse<T> Fail(string error)
        {
            var errors = new List<string>() { error };
            return new CustomResponse<T> { Errors = errors, IsSuccessfull = false, Data = default };
        }
    }
}

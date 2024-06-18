using ShopingCart.Models;

namespace ShopingCart.Helper
{
    public class PasswordValidation
    {
        public static Response IsvalidPassword(User users)
        {
            var response = new Response();
            if (users.Password != users.Cpassword)
            {
                response.Status = -1;
                response.Message = "Generated password does not match the provided password.";
                return response;
            }


            response.Status = 1;
            response.Message = "Password is valid.";
            return response;
          }
    }
}

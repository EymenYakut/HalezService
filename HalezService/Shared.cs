using HalezService.Model.Dtos.User;
using HalezService.Model.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HalezService
{
    public class Shared
    {
        public string SecretKey { get { return "9769aa4b-f346-450d-93da-d1f20a1b2650"; } }

        public string GenerateToken(LoginResult loginResult)
        {
            var claims = new[]
                        {
                new Claim("Id", loginResult.Id.ToString()),
                new Claim("NameSurname", loginResult.NameSurname.ToString()),
                new Claim("SecurityLevel", loginResult.SecurityLevel.ToString()),
                new Claim("UserType", loginResult.UserTypes.ToString())
            };

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(SecretKey));
            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                null, //_config["Jwt:Issuer"],
                null, //_config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddYears(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal DecodeToken(string token)
        {
            token = token.Replace("Bearer ", "");
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            var jwtSecurityToken = new JwtSecurityTokenHandler().ValidateToken(token, validations, out var tokenSecure);
            return jwtSecurityToken;
        }

        public int GetUserId(string token)
        {
            ClaimsPrincipal jwtSecurityToken = DecodeToken(token: token);
            return int.Parse(jwtSecurityToken.Claims.FirstOrDefault(r => r.Type == "Id").Value);
        }

        public UserTypes GetUserMode(string token)
        {
            ClaimsPrincipal jwtSecurityToken = DecodeToken(token: token);
            return (UserTypes)Enum.Parse(typeof(UserTypes), jwtSecurityToken.Claims.FirstOrDefault(r => r.Type == "UserType").Value.ToString());
        }

        public int GetCustomerId(string token)
        {
            ClaimsPrincipal jwtSecurityToken = DecodeToken(token: token);
            string x = jwtSecurityToken.Claims.FirstOrDefault(r => r.Type == "CustomerId").Value;
            return x == "" ? 0 : int.Parse(x);
        }

        public string GenerateFileName(string fileName)
        {
            return Guid.NewGuid().ToString() + Path.GetExtension(fileName);
        }

        public void UploadFile(string directoryAddress, string fileName, string file)
        {
            try
            {
                if (!Directory.Exists(directoryAddress))
                    Directory.CreateDirectory(directoryAddress);
                string[] fileBytesStringFormat = file.Split(',');

                byte[] fileByte = new byte[fileBytesStringFormat.Length];
                for (int i = 0; i < fileBytesStringFormat.Length; i++)
                    fileByte[i] = Convert.ToByte(fileBytesStringFormat[i]);
                File.WriteAllBytes(directoryAddress + "/" + fileName, fileByte);
            }
            catch
            {
                throw new Exception("AnUnexpectedErrorOccurredWhileUploadingFile");
            }
        }

        public void DeleteFile(string directoryAddress, string fileName)
        {
            if (File.Exists(directoryAddress + fileName))
                File.Delete(directoryAddress + fileName);
        }

        public string ImageToBase64String(string imageFilePath)
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(imageFilePath);
            return Convert.ToBase64String(imageArray);
        }
    }
}

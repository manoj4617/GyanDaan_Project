using GyanDyan.DataAccess;
using GyanDyan.Exceptions;
using GyanDyan.Services.Interfaces;
using GyanDyan.Utils;
using GyanDyan.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using static GyanDyan.Models.Domain;

namespace GyanDyan.Services
{
    public class UserServices : IUserService
    {
        private readonly Context _user;
        private readonly IConfiguration _configuration;

        //Dependency Injectione
        public UserServices(Context user, IConfiguration configuration)
        {
            _user = user;
            _configuration = configuration;
        }


        //REGISTERING NEW VOLUNTEER
        public async Task VolunteerRegister(VolunteerRegisterViewModel volunteerRegisterView)
        {
            //checks if the volunteer already exists
            if (VolunteerExists(volunteerRegisterView.Email))
            {
                throw new DuplicateUserException($"Volunteer with {volunteerRegisterView.Email} already exists");
            }

            //Creating password Hash
            CreatePasswordHash(volunteerRegisterView.Password, out byte[] passwordHash, out byte[] passwordSalt);

            //Storing data in database
            var volunteer = new VolunteerProfile()
            {
                FirstName = volunteerRegisterView.FirstName,
                LastName = volunteerRegisterView.LastName,
                //this is to convert string type to enum
                Gender = (Gender)Enum.Parse(typeof(Gender), volunteerRegisterView.Gender),
                MobileNumber = volunteerRegisterView.MobileNumber,
                JoinedOn = DateTime.Now,
                DateOfBirth = volunteerRegisterView.DateOfBirth,
                Email = volunteerRegisterView.Email,
                Street = volunteerRegisterView.Street,
                State = volunteerRegisterView.State,
                City = volunteerRegisterView.City,
                Pin = volunteerRegisterView.Pin,
                PassowrdSalt = passwordSalt,
                PasswordHash = passwordHash,
                //this is to convert string type to enum
                EducationQualification = (EducationQualification)Enum.Parse(typeof(EducationQualification),volunteerRegisterView.EducationQualification)
            };

            await _user.VolunteerProfiles.AddAsync(volunteer);
            SaveChangesToDB();
        }

        //REGISTERING NEW STUDENT
        public async Task StudentRegister(StudentRegisterViewModel studentRegisterView)
        {
            //checks if the student already exists
            if (StudentExists(studentRegisterView.Email))
            {
                throw new DuplicateUserException($"Student with {studentRegisterView.Email} already exists");
            }

            //Creating password Hash
            CreatePasswordHash(studentRegisterView.Password, out byte[] passwordHash, out byte[] passwordSalt);

            //Storing data in database
            var student = new StudentProfile()
            {
                FirstName = studentRegisterView.FirstName,
                LastName = studentRegisterView.LastName,
                //this is to convert string type to enum
                Gender = (Gender)Enum.Parse(typeof(Gender), studentRegisterView.Gender),
                MobileNumber = studentRegisterView.MobileNumber,
                JoinedOn = DateTime.Now,
                DateOfBirth = studentRegisterView.DateOfBirth,
                Email = studentRegisterView.Email,
                Street = studentRegisterView.Street,
                State = studentRegisterView.State,
                City = studentRegisterView.City,
                Pin = studentRegisterView.Pin,
                PassowrdSalt = passwordSalt,
                PasswordHash = passwordHash,
                IsVolunteer = false,
                //this is to convert string type to enum
                EducationQualification = (EducationQualification)Enum.Parse(typeof(EducationQualification), studentRegisterView.EducationQualification)
            };

            await _user.StudentProfiles.AddAsync(student);
            SaveChangesToDB();
        }

        //SIGNING USER
        public async Task<TokenViewModel> StudentLogin(LoginViewModel userLogin)
        {
            var student = _user.StudentProfiles.FirstOrDefault(e => e.Email == userLogin.Email);

            if(student == null)
            {
                throw new LoginFailedException($"Student with {userLogin.Email} doesn't exists");
            }
            var checkPassword = VerifyPassoword(userLogin.Password, student.PasswordHash, student.PassowrdSalt);
            if (!checkPassword)
            {
                throw new LoginFailedException($"Incorrect Password");
            }
            return GenerateStudentToken(student);
        }

        //SIGNING VOLUNTEER
        public async Task<TokenViewModel> VolunteerLogin(LoginViewModel userLogin)
        {
            var volunteer =  _user.VolunteerProfiles.FirstOrDefault(e => e.Email == userLogin.Email);

            if (volunteer == null)
            {
                throw new LoginFailedException($"Volunteer with {userLogin.Email} doesn't exists");
            }
            var checkPassword = VerifyPassoword(userLogin.Password, volunteer.PasswordHash, volunteer.PassowrdSalt);
            if (!checkPassword)
            {
                throw new LoginFailedException($"Incorrect Password");
            }
            return GenerateVolunteerToken(volunteer);
        }


        #region PRIVATE METHODS
        //Returns true if the Volunteer is already registered in VolunteerProfile Table
        private  bool VolunteerExists(string email)
        {
            return _user.VolunteerProfiles.Any(e => e.Email == email);
        }

        //Returns true if the Student is already registered in StudentProfile Table
        private bool StudentExists(string email)
        {
            return _user.StudentProfiles.Any(e => e.Email == email);
                }

        //out keyword stores the calculated passwordHash and passwordSalt in the variables passed
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            //Uses HMACSHA512 Algorithm for Hashing
            using (var hmac = new HMACSHA512())
            {
                //Salt adds addition charecters to the password to give it more security
                passwordSalt = hmac.Key;
                //actual hashed password
                passwordHash = hmac.ComputeHash(GetBytes(password));
            }
        }

        //Verifies if the passoword is correct
        private bool VerifyPassoword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var newHash = hmac.ComputeHash(GetBytes(password));
                for (var i = 0; i < newHash.Length; ++i)
                {
                    if (newHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        
        //Generates token for Student
        private TokenViewModel GenerateStudentToken(StudentProfile student)
        {
            var claims = GetClaims(student.Email,student.FirstName,student.LastName,StaticProvider.StudentPolicy);
            return GetToken(claims);
        }

        //Generates token for Volunteer
        private TokenViewModel GenerateVolunteerToken(VolunteerProfile volunteer)
        {
            var claims = GetClaims(volunteer.Email, volunteer.FirstName, volunteer.LastName, StaticProvider.VolunteerPolicy);
            return GetToken(claims);
        }

        //This creates the token using secrect key and the claims
        private TokenViewModel GetToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(GetBytes(_configuration.GetSection("AuthConfig:ServerSecrects").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescreptor = new SecurityTokenDescriptor
            {
                //Adding claims to the token
                //claims will make to token specific to the user with his/her name and email
                Subject = new ClaimsIdentity(claims),
                //Tells when the token should expire
                Expires = DateTime.Now.AddDays(1),
                //Sigining the token 
                SigningCredentials = credentials
            };

            var tokenHanlder = new JwtSecurityTokenHandler();

            return new TokenViewModel
            {
                Jwt = tokenHanlder.WriteToken(tokenHanlder.CreateToken(tokenDescreptor))
            };
        }

        //This generates the claims
        private List<Claim> GetClaims(string email, string firstName, string lastName, string policy)
        {
            return new List<Claim>()
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name,$"{firstName} {lastName}"),
                new Claim("Roles",$"{policy}")
            };
        }

        //Converts string password to byte type because
        //the hashing algorithm deals with byte data type not string
        private byte[] GetBytes(string rawPassword)
        {
            return System.Text.Encoding.Default.GetBytes(rawPassword);
        }

        //this method saves changes to database
        private void SaveChangesToDB()
        {
            _user.SaveChanges();
        }
        #endregion
    }
}

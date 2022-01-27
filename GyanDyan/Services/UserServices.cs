using GyanDyan.DataAccess;
using GyanDyan.Exceptions;
using GyanDyan.Services.Interfaces;
using GyanDyan.Utils;
using GyanDyan.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
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
        public async Task<string> VolunteerRegister(VolunteerRegisterViewModel volunteerRegisterView)
        {
            //checks if the volunteer already exists
            if (VolunteerExists(volunteerRegisterView.Email))
            {
                return $"Volunteer with {volunteerRegisterView.Email} email id already exists";
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
                Email = volunteerRegisterView.Email,
                //this is to convert string type to enum
                EducationQualification = (EducationQualification)Enum.Parse(typeof(EducationQualification),volunteerRegisterView.EducationQualification)
            };
            await _user.VolunteerProfiles.AddAsync(volunteer);
            SaveChangesToDB();

            var voluneerAccount = new VolunteerAccount()
            {
                VolunteerProfileId = volunteer.Id,
                JoinedOn = DateTime.Now,
                DateOfBirth = volunteerRegisterView.DateOfBirth,
                Street = volunteerRegisterView.Street,
                State = volunteerRegisterView.State,
                City = volunteerRegisterView.City,
                Pin = volunteerRegisterView.Pin,
                PassowrdSalt = passwordSalt,
                PasswordHash = passwordHash,
            };

           
            await _user.VolunteerAccounts.AddAsync(voluneerAccount);
            SaveChangesToDB();
            return $"Account Creation Successful.";
        }

        //REGISTERING NEW STUDENT
        public async Task<string> StudentRegister(StudentRegisterViewModel studentRegisterView)
        {
            //checks if the student already exists
            if (StudentExists(studentRegisterView.Email))
            {
                return $"Student with {studentRegisterView.Email} email id already exists";
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
                Email = studentRegisterView.Email,
                //this is to convert string type to enum
                EducationQualification = (EducationQualification)Enum.Parse(typeof(EducationQualification), studentRegisterView.EducationQualification)
            };
            await _user.StudentProfiles.AddAsync(student);
            SaveChangesToDB();

            var studnetAccount = new StudentAccount()
            {
                StudentProfileId = student.Id,
                JoinedOn = DateTime.Now,
                DateOfBirth = studentRegisterView.DateOfBirth,
                Street = studentRegisterView.Street,
                State = studentRegisterView.State,
                City = studentRegisterView.City,
                Pin = studentRegisterView.Pin,
                PassowrdSalt = passwordSalt,
                PasswordHash = passwordHash,
                IsVolunteer = false
            };

            //try
            //{
            //    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProfileImages", studentRegisterView.FileName);
            //    using (Stream stream = new FileStream(path, FileMode.Create))
            //    {
            //        studentRegisterView.FormFile.CopyTo(stream);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}

            await _user.StudentAccounts.AddAsync(studnetAccount);
            SaveChangesToDB();
            return $"Account Creation Successful.";
        }

        //SIGNING USER
        public async Task<TokenViewModel> StudentLogin(LoginViewModel userLogin)
        {
            var student = await _user.StudentProfiles.FirstOrDefaultAsync(e => e.Email == userLogin.Email);
            var studentAccout = await GetStudentAccount(student.Id);
            if (student == null)
            {
                throw new LoginFailedException($"Student with {userLogin.Email} doesn't exists");
            }
            var checkPassword = VerifyPassoword(userLogin.Password, studentAccout.PasswordHash, studentAccout.PassowrdSalt);
            if (!checkPassword)
            {
                throw new LoginFailedException($"Incorrect Password");
            }
            return GenerateStudentToken(student);
        }

        //SIGNING VOLUNTEER
        public async Task<TokenViewModel> VolunteerLogin(LoginViewModel userLogin)
        {
            var volunteer = await  _user.VolunteerProfiles.FirstOrDefaultAsync(e => e.Email == userLogin.Email);
            var volunteerAccount = await GetVolunteerAccount(volunteer.Id);

            if (volunteer == null)
            {
                throw new LoginFailedException($"Volunteer with {userLogin.Email} doesn't exists");
            }
            var checkPassword = VerifyPassoword(userLogin.Password, volunteerAccount.PasswordHash, volunteerAccount.PassowrdSalt);
            if (!checkPassword)
            {
                throw new LoginFailedException($"Incorrect Password");
            }
            return GenerateVolunteerToken(volunteer);
        }

        //UPDATING STUDENT PROFILE
        public async Task<string> UpdateStudentProfile(int studentId, ProfileUpdateViewModel studentProfile)
        {
            var student = await _user.StudentProfiles.FirstOrDefaultAsync(id=>id.Id == studentId);
            var studentAccout = await GetStudentAccount(student.Id);

            if (student == null)
            {
                return $"Student doesn't exist";
            }


            student.FirstName = studentProfile.FirstName;
            student.LastName = studentProfile.LastName;
            student.MobileNumber = studentProfile.MobileNumber;
            student.Email = studentProfile.Email;
            studentAccout.Street = studentProfile.Street;
            studentAccout.State = studentProfile.State;
            studentAccout.City = studentProfile.City;
            studentAccout.Pin = studentProfile.Pin;
            student.EducationQualification = (EducationQualification)Enum.Parse(typeof(EducationQualification), studentProfile.EducationQualification);
            

             SaveChangesToDB();
             return $"Profile Updated";
        }


        //UPDATE VOLUNTEER PROFILE
        public async Task<string> UpdateVolunteerProfile(int volunteerId, ProfileUpdateViewModel volunteerProfile)
        {
            var volunteer = await _user.VolunteerProfiles.FirstOrDefaultAsync(id => id.Id == volunteerId);
            var volunteerAccount = await GetVolunteerAccount(volunteer.Id);

            if (volunteer == null)
            {
                return $"Volunteer doesn't exist";
            }

            volunteer.FirstName = volunteerProfile.FirstName;
            volunteer.LastName = volunteerProfile.LastName;
            volunteer.MobileNumber = volunteerProfile.MobileNumber;
            volunteer.Email = volunteerProfile.Email;
            volunteerAccount.Street = volunteerProfile.Street;
            volunteerAccount.State = volunteerProfile.State;
            volunteerAccount.City = volunteerProfile.City;
            volunteerAccount.Pin = volunteerProfile.Pin;
            volunteer.EducationQualification = (EducationQualification)Enum.Parse(typeof(EducationQualification), volunteerProfile.EducationQualification);
            

            SaveChangesToDB();
            return $"Profile Updated";
        }

        public async Task<StudentProfile> GetStudentDetails(int studentId)
        {
            return await _user.StudentProfiles.FirstOrDefaultAsync(id => id.Id == studentId);
        }

        public async Task<VolunteerProfile> GetVolunteerDetails(int volunteerId)
        {
            return await _user.VolunteerProfiles.FirstOrDefaultAsync(id => id.Id == volunteerId);
        }


        #region PRIVATE METHODS
        //Returns true if the Volunteer is already registered in VolunteerProfile Table

        private async Task<StudentAccount> GetStudentAccount(int id)
        {
            return await _user.StudentAccounts.Where(sid => sid.StudentProfileId == id).FirstOrDefaultAsync();
        }

        private async Task<VolunteerAccount> GetVolunteerAccount(int id)
        {
            return await _user.VolunteerAccounts.Where(vid => vid.VolunteerProfileId == id).FirstOrDefaultAsync();
        }

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
            var claims = GetClaims(student.Id,student.Email,student.FirstName,student.LastName,StaticProvider.StudentPolicy);
            return GetToken(claims);
        }

        //Generates token for Volunteer
        private TokenViewModel GenerateVolunteerToken(VolunteerProfile volunteer)
        {
            var claims = GetClaims(volunteer.Id,volunteer.Email, volunteer.FirstName, volunteer.LastName, StaticProvider.VolunteerPolicy);
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
        private List<Claim> GetClaims(int id,string email, string firstName, string lastName, string policy)
        {
            return new List<Claim>()
            {
                new Claim("Id",$"{id}"),
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

using OEYS.WEB.Models.Contexts;
using OEYS.WEB.Models.Entities;
using OEYS.WEB.Utilities.Password;

namespace OEYS.WEB.Configurations.SeedData
{
    public static class MockData
    {
        static byte[] passwordhash, passwordsalt;

        static MockData()
        {
            HashingHelper.CreatePasswordHash("123", out passwordhash, out passwordsalt);
        }


        public static async Task AddData()
        {
            foreach (var item in GetRoles())
            {
                await DapperDatabaseConnection.InsertAsync(item);
            }

            foreach (var item in GetUsers())
            {
                await DapperDatabaseConnection.InsertAsync(item);
            }

            foreach (var item in GetActivities())
            {
                await DapperDatabaseConnection.InsertAsync(item);
            }


            UserRole userRole = new UserRole()
            {
                RoleId = 1,
                UserId = 1,
                CreatedDate = DateTime.Now,
                Id = 1,
                IsActive = true,
                IsDeleted = false,
                UpdatedDate = DateTime.Now
            };

            await DapperDatabaseConnection.InsertAsync(userRole);
        }


        static List<Role> GetRoles() => new List<Role>()
        {
            new Role(){Id=1,Name="Admin",IsActive=true,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Role(){Id=2,Name="Participant",IsActive=true,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now}
        };

        static List<User> GetUsers() => new List<User>()
        {
            new User(){Id=1,Name="Admin",Surname="Admin",Email="admin@admin.com",UserName="Admin",CreatedDate=DateTime.Now,IsActive=true,IsDeleted=false,
            PhoneNumber="0541254545",UpdatedDate=DateTime.Now,PasswordHash=passwordhash,PasswordSalt=passwordsalt}
        };

        static List<Activity> GetActivities() => new List<Activity>()
        {
            new Activity(){Id=1,Name="Activity 1",Description="Description 1",IsActive=true,ActivityTime=DateTime.Now,ActivityType=1,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=2,Name="Activity 2",Description="Description 2",IsActive=true,ActivityTime=DateTime.Now,ActivityType=2,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=3,Name="Activity 3",Description="Description 3",IsActive=true,ActivityTime=DateTime.Now,ActivityType=3,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=4,Name="Activity 4",Description="Description 4",IsActive=true,ActivityTime=DateTime.Now,ActivityType=4,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=5,Name="Activity 5",Description="Description 5",IsActive=true,ActivityTime=DateTime.Now,ActivityType=5,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=6,Name="Activity 6",Description="Description 6",IsActive=true,ActivityTime=DateTime.Now,ActivityType=6,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=7,Name="Activity 7",Description="Description 7",IsActive=true,ActivityTime=DateTime.Now,ActivityType=7,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=8,Name="Activity 8",Description="Description 8",IsActive=true,ActivityTime=DateTime.Now,ActivityType=8,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=9,Name="Activity 9",Description="Description 9",IsActive=true,ActivityTime=DateTime.Now,ActivityType=4,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=10,Name="Activity 10",Description="Description 10",IsActive=true,ActivityTime=DateTime.Now,ActivityType=2,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=11,Name="Activity 11",Description="Description 11",IsActive=true,ActivityTime=DateTime.Now,ActivityType=8,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=12,Name="Activity 12",Description="Description 12",IsActive=true,ActivityTime=DateTime.Now,ActivityType=7,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=13,Name="Activity 13",Description="Description 13",IsActive=true,ActivityTime=DateTime.Now,ActivityType=1,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=14,Name="Activity 14",Description="Description 14",IsActive=true,ActivityTime=DateTime.Now,ActivityType=2,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=15,Name="Activity 15",Description="Description 15",IsActive=true,ActivityTime=DateTime.Now,ActivityType=4,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=16,Name="Activity 16",Description="Description 16",IsActive=true,ActivityTime=DateTime.Now,ActivityType=6,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=17,Name="Activity 17",Description="Description 17",IsActive=true,ActivityTime=DateTime.Now,ActivityType=7,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=18,Name="Activity 18",Description="Description 18",IsActive=true,ActivityTime=DateTime.Now,ActivityType=7,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=19,Name="Activity 19",Description="Description 19",IsActive=true,ActivityTime=DateTime.Now,ActivityType=2,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=20,Name="Activity 20",Description="Description 20",IsActive=true,ActivityTime=DateTime.Now,ActivityType=2,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=21,Name="Activity 21",Description="Description 21",IsActive=true,ActivityTime=DateTime.Now,ActivityType=3,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=22,Name="Activity 22",Description="Description 22",IsActive=true,ActivityTime=DateTime.Now,ActivityType=4,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=23,Name="Activity 23",Description="Description 23",IsActive=true,ActivityTime=DateTime.Now,ActivityType=8,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=24,Name="Activity 24",Description="Description 24",IsActive=true,ActivityTime=DateTime.Now,ActivityType=1,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=25,Name="Activity 25",Description="Description 25",IsActive=true,ActivityTime=DateTime.Now,ActivityType=2,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=26,Name="Activity 26",Description="Description 26",IsActive=true,ActivityTime=DateTime.Now,ActivityType=6,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=27,Name="Activity 27",Description="Description 27",IsActive=true,ActivityTime=DateTime.Now,ActivityType=3,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=28,Name="Activity 28",Description="Description 28",IsActive=true,ActivityTime=DateTime.Now,ActivityType=7,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=29,Name="Activity 29",Description="Description 29",IsActive=true,ActivityTime=DateTime.Now,ActivityType=8,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=30,Name="Activity 30",Description="Description 30",IsActive=true,ActivityTime=DateTime.Now,ActivityType=5,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=31,Name="Activity 31",Description="Description 31",IsActive=true,ActivityTime=DateTime.Now,ActivityType=3,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=32,Name="Activity 32",Description="Description 32",IsActive=true,ActivityTime=DateTime.Now,ActivityType=7,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=33,Name="Activity 33",Description="Description 33",IsActive=true,ActivityTime=DateTime.Now,ActivityType=8,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=34,Name="Activity 34",Description="Description 34",IsActive=true,ActivityTime=DateTime.Now,ActivityType=3,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now},
            new Activity(){Id=35,Name="Activity 35",Description="Description 35",IsActive=true,ActivityTime=DateTime.Now,ActivityType=2,CreatedDate=DateTime.Now,IsDeleted=false,UpdatedDate=DateTime.Now}
        };










    }
}

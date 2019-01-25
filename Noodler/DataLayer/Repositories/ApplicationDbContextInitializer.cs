using DataLayer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.IO;

namespace DataLayer.Repositories {
    public class ApplicationDbContextInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext> {
        protected override void Seed(ApplicationDbContext context) {
            base.Seed(context);
            SeedUsers(context);
        }
        public static void SeedUsers(ApplicationDbContext context) {
            // Create the users
            UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(context);
            UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(store);

            string directory = AppDomain.CurrentDomain.BaseDirectory;

            ApplicationUser eliasU = new ApplicationUser {
                UserName = "elias@noodler.com",
                Email = "elias@noodler.com"
            };
            manager.Create(eliasU, "#MyPassword123");
            ApplicationUser nicoU = new ApplicationUser {
                UserName = "nico@noodler.com",
                Email = "nico@noodler.com"
            };
            manager.Create(nicoU, "#MyPassword123");
            ApplicationUser oskarU = new ApplicationUser {
                UserName = "oskar@noodler.com",
                Email = "oskar@noodler.com"
            };
            manager.Create(oskarU, "#MyPassword123");
            ApplicationUser randomU = new ApplicationUser {
                UserName = "random@noodler.com",
                Email = "random@noodler.com"
            };
            manager.Create(randomU, "#MyPassword123");
            ApplicationUser corazonU = new ApplicationUser {
                UserName = "corazon@noodler.com",
                Email = "corazon@noodler.com"
            };
            manager.Create(corazonU, "#MyPassword123");

            // Setting default avatar for all profiles
            string path = directory + "/Content/Images/defaultAvatar.png";
            FileStream file = new FileStream(path, FileMode.Open);
            byte[] defaultAvatar = null;
            using(var binary = new BinaryReader(file)) {
                defaultAvatar = binary.ReadBytes((int)file.Length);
            }

            // Define Profiles
            ProfileModels eliasP = new ProfileModels {
                Id = eliasU.Id,
                FirstName = "Elias",
                LastName = "Stagg",
                Gender = Gender.Man,
                Biography = "This is an example biography of the profile for Elias Stagg.",
                BirthDate = new DateTime(1998, 04, 22),
                ProfileImage = defaultAvatar
            };
            ProfileModels nicoP = new ProfileModels {
                Id = nicoU.Id,
                FirstName = "Nicolas",
                LastName = "Björkefors",
                Gender = Gender.Man,
                Biography = "This is an example biography of the profile for Nicolas Björkefors.",
                BirthDate = new DateTime(1998, 01, 05),
                ProfileImage = defaultAvatar
            };
            ProfileModels oskarP = new ProfileModels {
                Id = oskarU.Id,
                FirstName = "Oskar",
                LastName = "Olofsson",
                Gender = Gender.Man,
                Biography = "This is an example biography of the profile for Oskar Olofsson.",
                BirthDate = new DateTime(1982, 09, 15),
                ProfileImage = defaultAvatar
            };
            ProfileModels randomP = new ProfileModels {
                Id = randomU.Id,
                FirstName = "Random",
                LastName = "Svensson",
                Gender = Gender.Attackhelicopter,
                Biography = "This is an example biography of the profile for Random Svensson.",
                BirthDate = new DateTime(1980, 08, 14),
                ProfileImage = defaultAvatar
            };
            ProfileModels corazonP = new ProfileModels {
                Id = corazonU.Id,
                FirstName = "Corazon",
                LastName = "D'amico",
                Gender = Gender.Man,
                Biography = "Este es un ejemplo de biografía del perfil de Corazon D'amico.",
                BirthDate = new DateTime(1971, 07, 15),
                ProfileImage = defaultAvatar
            };

            // Define Posts
            PostModels post1 = new PostModels {
                PostFromId = eliasU.Id,
                PostToId = randomU.Id,
                PostDateTime = new DateTime(2019, 01, 12, 14, 44, 24),
                Text = "This is a post from Elias to Random!"
            };
            PostModels post2 = new PostModels {
                PostFromId = oskarU.Id,
                PostToId = randomU.Id,
                PostDateTime = new DateTime(2019, 01, 13, 23, 45, 02),
                Text = "This is a post from Oskar to Random!"
            };
            PostModels post3 = new PostModels {
                PostFromId = nicoU.Id,
                PostToId = randomU.Id,
                PostDateTime = new DateTime(2019, 01, 14, 19, 01, 08),
                Text = "This is a post from Nico to Random!"
            };
            PostModels post4 = new PostModels {
                PostFromId = randomU.Id,
                PostToId = randomU.Id,
                PostDateTime = new DateTime(2019, 01, 15, 17, 33, 48),
                Text = "This is a post from Random his own profile!"
            };
            PostModels post5 = new PostModels {
                PostFromId = nicoU.Id,
                PostToId = corazonU.Id,
                PostDateTime = new DateTime(2019, 01, 15, 07, 27, 33),
                Text = "I'm your biggest fan!"
            };
            PostModels post6 = new PostModels {
                PostFromId = corazonU.Id,
                PostToId = corazonU.Id,
                PostDateTime = new DateTime(2019, 01, 15, 08, 03, 17),
                Text = "Gracias."
            };
            PostModels post7 = new PostModels {
                PostFromId = corazonU.Id,
                PostToId = eliasU.Id,
                PostDateTime = new DateTime(2019, 01, 16, 17, 28, 51),
                Text = "Hola señor."
            };
            PostModels post8 = new PostModels {
                PostFromId = oskarU.Id,
                PostToId = nicoU.Id,
                PostDateTime = new DateTime(2019, 01, 18, 22, 04, 55),
                Text = "Mate, get stuck in there. Scrum!"
            };
            PostModels post9 = new PostModels {
                PostFromId = eliasU.Id,
                PostToId = randomU.Id,
                PostDateTime = new DateTime(2019, 01, 20, 19, 05, 33),
                Text = "Fett avis på din randomness."
            };

            // Define requests
            RequestModels request1 = new RequestModels {
                RequestDateTime = DateTime.Now,
                RequestFromId = nicoU.Id,
                RequestToId = corazonU.Id
            };
            RequestModels request2 = new RequestModels {
                RequestDateTime = DateTime.Now,
                RequestFromId = oskarU.Id,
                RequestToId = corazonU.Id
            };
            RequestModels request3 = new RequestModels {
                RequestDateTime = DateTime.Now,
                RequestFromId = oskarU.Id,
                RequestToId = eliasU.Id
            };
            RequestModels request4 = new RequestModels {
                RequestDateTime = DateTime.Now,
                RequestFromId = randomU.Id,
                RequestToId = eliasU.Id
            };
            RequestModels request5 = new RequestModels {
                RequestDateTime = DateTime.Now,
                RequestFromId = randomU.Id,
                RequestToId = nicoU.Id
            };

            // Define FriendCategories
            FriendCategoryModels category1 = new FriendCategoryModels {
                CategoryName = "Default"
            };
            FriendCategoryModels category2 = new FriendCategoryModels {
                CategoryName = "Acquaintances"
            };
            FriendCategoryModels category3 = new FriendCategoryModels {
                CategoryName = "Best Friends Forever"
            };

            context.Profiles.AddRange(new[] { eliasP, nicoP, oskarP, randomP, corazonP }); // Add profiles
            context.Posts.AddRange(new[] { post1, post2, post3, post4, post5, post6, post7, post8, post9 }); // Add posts
            context.Requests.AddRange(new[] { request1, request2, request3, request4, request5 }); // Add requests
            context.Categories.AddRange(new[] { category1, category2, category3 }); // Add friend categories
            context.SaveChanges(); // We need to save the friend categories into the database to be able to access their IDs for the creation of the friends.
            context.Database.Connection.Open(); // Connection closes after SaveChanges() so we have to re-open it.
            
            // Define friendships
            FriendModels friends1 = new FriendModels {
                UserId = nicoU.Id,
                FriendId = oskarU.Id,
                FriendCategory = category1.Id
            };
            FriendModels friends2 = new FriendModels {
                UserId = nicoU.Id,
                FriendId = eliasU.Id,
                FriendCategory = category2.Id
            };
            FriendModels friends3 = new FriendModels {
                UserId = eliasU.Id,
                FriendId = corazonU.Id,
                FriendCategory = category3.Id
            };
            FriendModels friends4 = new FriendModels {
                UserId = oskarU.Id,
                FriendId = randomU.Id,
                FriendCategory = category1.Id
            };

            context.Friends.AddRange(new[] { friends1, friends2, friends3, friends4 }); // Add friendships
            context.SaveChanges(); // Save database once more to finally add the friends into the database.
        }
    }
}

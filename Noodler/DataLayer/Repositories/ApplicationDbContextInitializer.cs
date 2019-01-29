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
        public static byte[] setInitializerProfilePicture(string endPath) {

            // Setting default avatar for all profiles
            string path = AppDomain.CurrentDomain.BaseDirectory + endPath;
            FileStream file = new FileStream(path, FileMode.Open);
            byte[] avatar = null;
            using (var binary = new BinaryReader(file)) {
                avatar = binary.ReadBytes((int)file.Length);
            }
            return avatar;
        }
        public static void SeedUsers(ApplicationDbContext context) {
            // Create the users
            UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(context);
            UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(store);

            ApplicationUser eliasU = new ApplicationUser {
                UserName = "elias@noodler.com",
                Email = "elias@noodler.com"
            };
            manager.Create(eliasU, "password");
            ApplicationUser nicoU = new ApplicationUser {
                UserName = "nico@noodler.com",
                Email = "nico@noodler.com"
            };
            manager.Create(nicoU, "password");
            ApplicationUser oskarU = new ApplicationUser {
                UserName = "oskar@noodler.com",
                Email = "oskar@noodler.com"
            };
            manager.Create(oskarU, "password");
            ApplicationUser randomU = new ApplicationUser {
                UserName = "random@noodler.com",
                Email = "random@noodler.com"
            };
            manager.Create(randomU, "password");
            ApplicationUser corazonU = new ApplicationUser {
                UserName = "corazon@noodler.com",
                Email = "corazon@noodler.com"
            };
            manager.Create(corazonU, "password");
            ApplicationUser andreasU = new ApplicationUser {
                UserName = "andreas@noodler.com",
                Email = "andreas@noodler.com"
            };
            manager.Create(andreasU, "password");
            ApplicationUser mathiasU = new ApplicationUser {
                UserName = "mathias@noodler.com",
                Email = "mathias@noodler.com"
            };
            manager.Create(mathiasU, "password");
            ApplicationUser lightU = new ApplicationUser {
                UserName = "light@noodler.com",
                Email = "light@noodler.com"
            };
            manager.Create(lightU, "password");
            ApplicationUser hakU = new ApplicationUser {
                UserName = "hak@noodler.com",
                Email = "hak@noodler.com"
            };
            manager.Create(hakU, "password");
            ApplicationUser alfonsU = new ApplicationUser {
                UserName = "alfons@noodler.com",
                Email = "alfons@noodler.com"
            };
            manager.Create(alfonsU, "password");

            
            // Define Profiles
            ProfileModels eliasP = new ProfileModels {
                Id = eliasU.Id,
                FirstName = "Elias",
                LastName = "Stagg",
                Gender = Gender.Man,
                Biography = "This is an example biography of the profile for Elias Stagg.",
                BirthDate = new DateTime(1998, 04, 22),
                Desperation = 10,
                Loneliness = 10,
                Horniness = 4,
                Pride = 6,
                ProfileImage = setInitializerProfilePicture("/Content/Images/stagg.jpg")
            };
            ProfileModels nicoP = new ProfileModels {
                Id = nicoU.Id,
                FirstName = "Nicolas",
                LastName = "Björkefors",
                Gender = Gender.Man,
                Biography = "This is an example biography of the profile for Nicolas Björkefors.",
                BirthDate = new DateTime(1998, 01, 05),
                Desperation = 9,
                Loneliness = 10,
                Horniness = 3,
                Pride = 8,
                ProfileImage = setInitializerProfilePicture("/Content/Images/smugOkuu.png")
            };
            ProfileModels oskarP = new ProfileModels {
                Id = oskarU.Id,
                FirstName = "Oskar",
                LastName = "Olofsson",
                Gender = Gender.Man,
                Biography = "This is an example biography of the profile for Oskar Olofsson.",
                BirthDate = new DateTime(1982, 09, 15),
                Desperation = 9,
                Loneliness = 10,
                Horniness = 10,
                Pride = 1,
                ProfileImage = setInitializerProfilePicture("/Content/Images/pickadoller.jpg")
            };
            ProfileModels randomP = new ProfileModels {
                Id = randomU.Id,
                FirstName = "Random",
                LastName = "Svensson",
                Gender = Gender.Attackhelicopter,
                Biography = "This is an example biography of the profile for Random Svensson.",
                BirthDate = new DateTime(1980, 08, 14),
                Desperation = 5,
                Loneliness = 6,
                Horniness = 10,
                Pride = 9,
                ProfileImage = setInitializerProfilePicture("/Content/Images/random.png")
            };
            ProfileModels corazonP = new ProfileModels {
                Id = corazonU.Id,
                FirstName = "Corazon",
                LastName = "D'amico",
                Gender = Gender.Man,
                Biography = "Este es un ejemplo de biografía del perfil de Corazon D'amico.",
                BirthDate = new DateTime(1971, 07, 15),
                Desperation = 10,
                Loneliness = 10,
                Horniness = 7,
                Pride = 3,
                ProfileImage = setInitializerProfilePicture("/Content/Images/corazon.png")
            };
            ProfileModels andreasP = new ProfileModels {
                Id = andreasU.Id,
                FirstName = "Andreas",
                LastName = "Ask",
                Gender = Gender.Man,
                Biography = "Ja, nu har du hittat min profil. Diskutera vad detta innebär sakligt i par eller nåt. Och förresten har någon sett min fez?",
                BirthDate = new DateTime(1900, 01, 25),
                Desperation = 8,
                Loneliness = 10,
                Horniness = 7,
                Pride = 5,
                ProfileImage = setInitializerProfilePicture("/Content/Images/ask.jpg")
            };
            ProfileModels mathiasP = new ProfileModels {
                Id = mathiasU.Id,
                FirstName = "Mathias",
                LastName = "Hatakka",
                Gender = Gender.Man,
                Biography = "Jag har en katt, och katten. Har flera katter. Mitt hem är praktiskt taget ett katthem.",
                BirthDate = new DateTime(1900, 05, 11),
                Desperation = 5,
                Loneliness = 10,
                Horniness = 7,
                Pride = 8,
                ProfileImage = setInitializerProfilePicture("/Content/Images/katt.jpg")
            };
            ProfileModels lightP = new ProfileModels {
                Id = lightU.Id,
                FirstName = "月",
                LastName = "夜神",
                Gender = Gender.Man,
                Biography = "を魚取り、ヌードリングする",
                BirthDate = new DateTime(1996, 03, 22),
                Desperation = 3,
                Loneliness = 10,
                Horniness = 7,
                Pride = 10,
                ProfileImage = setInitializerProfilePicture("/Content/Images/light.png")
            };
            ProfileModels hakP = new ProfileModels {
                Id = hakU.Id,
                FirstName = "Hak",
                LastName = "Son",
                Gender = Gender.Man,
                Biography = "Don't come near me or the princess. I'll kill you.",
                BirthDate = new DateTime(1982, 09, 29),
                Desperation = 2,
                Loneliness = 10,
                Horniness = 8,
                Pride = 10,
                ProfileImage = setInitializerProfilePicture("/Content/Images/hak.png")
            };
            ProfileModels alfonsP = new ProfileModels {
                Id = alfonsU.Id,
                FirstName = "Alfons",
                LastName = "Åberg",
                Gender = Gender.Man,
                Biography = "Någon som vill med på brajbaxningsäventyr?",
                BirthDate = new DateTime(2002, 12, 11),
                Desperation = 6,
                Loneliness = 10,
                Horniness = 4,
                Pride = 10,
                ProfileImage = setInitializerProfilePicture("/Content/Images/braj.png")
            };

            // Define Posts
            PostModels post1 = new PostModels {
                PostFromId = eliasU.Id,
                PostToId = randomU.Id,
                PostDateTime = new DateTime(2019, 01, 12, 14, 44, 24),
                Text = "Praise RNGesus!"
            };
            PostModels post2 = new PostModels {
                PostFromId = oskarU.Id,
                PostToId = randomU.Id,
                PostDateTime = new DateTime(2019, 01, 13, 23, 45, 02),
                Text = "Shit asså."
            };
            PostModels post3 = new PostModels {
                PostFromId = nicoU.Id,
                PostToId = randomU.Id,
                PostDateTime = new DateTime(2019, 01, 14, 19, 01, 08),
                Text = "Söker efter någon att spela Pokémon Reborn med!"
            };
            PostModels post4 = new PostModels {
                PostFromId = randomU.Id,
                PostToId = randomU.Id,
                PostDateTime = new DateTime(2019, 01, 15, 17, 33, 48),
                Text = "Does this thing work?"
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
            PostModels post10 = new PostModels {
                PostFromId = lightU.Id,
                PostToId = alfonsU.Id,
                PostDateTime = new DateTime(2019, 01, 22, 21, 09, 53),
                Text = "死ね、 人間のくず"
            };
            PostModels post11 = new PostModels {
                PostFromId = alfonsU.Id,
                PostToId = mathiasU.Id,
                PostDateTime = new DateTime(2019, 01, 22, 21, 05, 18),
                Text = "Hej jag vill klappa katter."
            };
            PostModels post12 = new PostModels {
                PostFromId = andreasU.Id,
                PostToId = eliasU.Id,
                PostDateTime = new DateTime(2019, 01, 23, 07, 48, 32),
                Text = "Kom ihåg att evaluera dina hattalternativ noga varje morgon. Idag valde jag min fez."
            };
            PostModels post13 = new PostModels {
                PostFromId = mathiasU.Id,
                PostToId = oskarU.Id,
                PostDateTime = new DateTime(2019, 01, 24, 11, 39, 33),
                Text = "En av mina katter har tappat sitt hår. Får jag låna lite av ditt?"
            };
            PostModels post14 = new PostModels {
                PostFromId = nicoU.Id,
                PostToId = lightU.Id,
                PostDateTime = new DateTime(2019, 01, 24, 22, 52, 04),
                Text = "私はLですwwww."
            };
            PostModels post15 = new PostModels {
                PostFromId = oskarU.Id,
                PostToId = alfonsU.Id,
                PostDateTime = new DateTime(2019, 01, 25, 10, 41, 52),
                Text = "Har svårt att stå emot en sådan lockande inbjudan! Hänger med på studs."
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
                RequestFromId = nicoU.Id,
                RequestToId = lightU.Id
            };
            RequestModels request6 = new RequestModels {
                RequestDateTime = DateTime.Now,
                RequestFromId = lightU.Id,
                RequestToId = mathiasU.Id
            };
            RequestModels request7 = new RequestModels {
                RequestDateTime = DateTime.Now,
                RequestFromId = randomU.Id,
                RequestToId = alfonsU.Id
            };
            RequestModels request8 = new RequestModels {
                RequestDateTime = DateTime.Now,
                RequestFromId = alfonsU.Id,
                RequestToId = eliasU.Id
            };
            RequestModels request9 = new RequestModels {
                RequestDateTime = DateTime.Now,
                RequestFromId = hakU.Id,
                RequestToId = eliasU.Id
            };
            RequestModels request10 = new RequestModels {
                RequestDateTime = DateTime.Now,
                RequestFromId = andreasU.Id,
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
                CategoryName = "BFFs"
            };

            context.Profiles.AddRange(new[] { eliasP, nicoP, oskarP, randomP, corazonP, andreasP, mathiasP, lightP, hakP, alfonsP }); // Add profiles
            context.Posts.AddRange(new[] { post1, post2, post3, post4, post5, post6, post7, post8, post9, post10, post11, post12, post13, post14, post15 }); // Add posts
            context.Requests.AddRange(new[] { request1, request2, request3, request4, request5, request6, request7, request8, request9, request10 }); // Add requests
            context.Categories.AddRange(new[] { category1, category2, category3 }); // Add friend categories
            context.SaveChanges(); // We need to save the friend categories into the database to be able to access their IDs for the creation of the friends.

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
            FriendModels friends5 = new FriendModels {
                UserId = oskarU.Id,
                FriendId = alfonsU.Id,
                FriendCategory = category1.Id
            };
            FriendModels friends6 = new FriendModels {
                UserId = eliasU.Id,
                FriendId = lightU.Id,
                FriendCategory = category1.Id
            };
            FriendModels friends7 = new FriendModels {
                UserId = andreasU.Id,
                FriendId = eliasU.Id,
                FriendCategory = category1.Id
            };
            FriendModels friends8 = new FriendModels {
                UserId = nicoU.Id,
                FriendId = hakU.Id,
                FriendCategory = category1.Id
            };

            context.Friends.AddRange(new[] { friends1, friends2, friends3, friends4, friends5, friends6, friends7, friends8 }); // Add friendships
            context.SaveChanges();
        }
    }
}
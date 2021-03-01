using LibShare.Api.Data.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibShare.Api.Data.Entities
{
    public class SeederDB
    {
        #region Seed Users
        public static void SeedData(UserManager<DbUser> userManager, RoleManager<DbRole> roleManager)
        {
            var adminRoleName = Roles.Admin;
            var userRoleName = Roles.User;

            var roleResult = roleManager.FindByNameAsync(adminRoleName).Result;
            if (roleResult == null)
            {
                var roleresult = roleManager.CreateAsync(new DbRole
                {
                    Name = adminRoleName

                }).Result;
            }

            roleResult = roleManager.FindByNameAsync(userRoleName).Result;
            if (roleResult == null)
            {
                var roleresult = roleManager.CreateAsync(new DbRole
                {
                    Name = userRoleName

                }).Result;
            }

            var email = "admin@gmail.com";
            var findUser = userManager.FindByEmailAsync(email).Result;
            if (findUser == null)
            {
                var user = new DbUser
                {
                    Email = email,
                    UserName = email,
                };

                user.UserProfile = new UserProfile()
                {
                    Name = "Petro",
                    Surname = "Petunchik",
                    DateOfBirth = new DateTime(1980, 5, 20),
                    Phone = "+380978515659",
                    RegistrationDate = DateTime.Now,
                    Photo = "person_1.jpg"
                };

                var result = userManager.CreateAsync(user, "Qwerty1-").Result;
                result = userManager.AddToRoleAsync(user, adminRoleName).Result;
            }

            email = "user@gmail.com";
            findUser = userManager.FindByEmailAsync(email).Result;
            if (findUser == null)
            {
                var user = new DbUser
                {
                    Email = email,
                    UserName = email,
                };

                user.UserProfile = new UserProfile()
                {
                    Name = "Natalya",
                    Surname = "Pupenko",
                    DateOfBirth = new DateTime(1982, 10, 7),
                    Phone = "+380670015009",
                    RegistrationDate = DateTime.Now,
                    Photo = "person_2.jpg"
                };

                var result = userManager.CreateAsync(user, "Qwerty1-").Result;
                result = userManager.AddToRoleAsync(user, userRoleName).Result;
            }
        }
        #endregion

        #region Categories
        public static void SeedCategories(ApplicationDbContext context)
        {
            var categories = new List<Category>();
            categories.Add(
                new Category
                {
                    Id = "0",
                    Name = "ЗАГАЛЬНИЙ ВІДДІЛ",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = null
                });

            categories.Add(
                new Category
                {
                    Id = "00",
                    Name = "Загальні питання науки та культури",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "0"
                });

            categories.Add(
                new Category
                {
                    Id = "01",
                    Name = "Бібліографія та бібліографічні покажчики. Каталоги",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "0"
                });

            categories.Add(
                new Category
                {
                    Id = "02",
                    Name = "Бібліотечна справа",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "0"
                });


            categories.Add(
                new Category
                {
                    Id = "03",
                    Name = "Довідкові видання загального типу (енциклопедії, словники)",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "0"
                });

            categories.Add(
                new Category
                {
                    Id = "04",
                    Name = "Серійні публікації. Періодика (щорічники, альманахи, календарі)",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "0"
                });

            categories.Add(
                new Category
                {
                    Id = "05",
                    Name = "Організація та інші типи об'єднання (співробітництва)",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "0"
                });

            categories.Add(
                new Category
                {
                    Id = "06",
                    Name = "Газети. Преса",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "0"
                });

            categories.Add(
                new Category
                {
                    Id = "07",
                    Name = "Видання змішаного змісту. Праці. Збірники",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "0"
                });
            categories.Add(
                new Category
                {
                    Id = "08",
                    Name = "Рукописи. Раритети та рідкісні видання",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "0"
                });

            categories.Add(
                new Category
                {
                    Id = "1",
                    Name = "ФІЛОСОФІЯ. ПСИХОЛОГІЯ",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = null
                });

            categories.Add(
                new Category
                {
                    Id = "10",
                    Name = "Сутність і роль філософії",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "1"
                });

            categories.Add(
                new Category
                {
                    Id = "11",
                    Name = "Метафізика",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "1"
                });

            categories.Add(
                new Category
                {
                    Id = "12",
                    Name = "Окремі проблеми та категорії філософії",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "1"
                });

            categories.Add(
                new Category
                {
                    Id = "13",
                    Name = "Філософія розуму та духу. Метафізика духовного життя",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "1"
                });

            categories.Add(
                new Category
                {
                    Id = "14",
                    Name = "Філософські системи та погляди",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "1"
                });

            categories.Add(
                new Category
                {
                    Id = "15",
                    Name = "Психологія",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "1"
                });

            categories.Add(
                new Category
                {
                    Id = "16",
                    Name = "Логіка. Епістемологія. Теорія пізнання. Методологія логіки",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "1"
                });

            categories.Add(
                new Category
                {
                    Id = "17",
                    Name = "Філософія моралі. Етика. Практична філософія",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "1"
                });

            categories.Add(
                new Category
                {
                    Id = "2",
                    Name = "РЕЛІГІЯ. ТЕОЛОГІЯ (БОГОСЛОВ'Я)",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = null
                });

            categories.Add(
                new Category
                {
                    Id = "20",
                    Name = "Історія релігії",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "2"
                });

            categories.Add(
                new Category
                {
                    Id = "21",
                    Name = "Природна теологія. Теодицея. Бог. Раціональна теологія. Релігійна філософія",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "2"
                });

            categories.Add(
                new Category
                {
                    Id = "22",
                    Name = "Біблія. Святе письмо",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "2"
                });

            categories.Add(
                new Category
                {
                    Id = "23",
                    Name = "Догматична теологія",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "2"
                });

            categories.Add(
                new Category
                {
                    Id = "24",
                    Name = "Практична теологія",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "2"
                });

            categories.Add(
                new Category
                {
                    Id = "25",
                    Name = "Пасторська теологія (богослов'я)",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "2"
                });

            categories.Add(
                new Category
                {
                    Id = "26",
                    Name = "Християнська церква в цілому",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "2"
                });

            categories.Add(
                new Category
                {
                    Id = "27",
                    Name = "Загальна історія християнської церкви",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "2"
                });

            categories.Add(
                new Category
                {
                    Id = "28",
                    Name = "Християнські церкви, секти, деномінації",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "2"
                });

            categories.Add(
                new Category
                {
                    Id = "29",
                    Name = "Нехристиянські релігії",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "2"
                });

            categories.Add(
                new Category
                {
                    Id = "3",
                    Name = "СУСПІЛЬНІ НАУКИ",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = null
                });

            categories.Add(
                new Category
                {
                    Id = "30",
                    Name = "Теорія, методолія та методи суспільних наук. Соціографія",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "3"
                });

            categories.Add(
                new Category
                {
                    Id = "31",
                    Name = "Демографія, соціологія, статистика",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "3"
                });

            categories.Add(
                new Category
                {
                    Id = "32",
                    Name = "Політика",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "3"
                });

            categories.Add(
                new Category
                {
                    Id = "33",
                    Name = "Економіка. Економічна наука",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "3"
                });

            categories.Add(
                new Category
                {
                    Id = "34",
                    Name = "Право. Юриспруденція",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "3"
                });

            categories.Add(
                new Category
                {
                    Id = "35",
                    Name = "Державне адміністративне управління. Військова справа",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "3"
                });

            categories.Add(
                new Category
                {
                    Id = "36",
                    Name = "Забезпечення духовних і матеріальних життєвих потреб",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "3"
                });

            categories.Add(
                new Category
                {
                    Id = "37",
                    Name = "Освіта. Виховання. Навчання. Дозвілля",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "3"
                });

            categories.Add(
                new Category
                {
                    Id = "38",
                    Name = "Етнологія. Етнографія. Звичаї. Традиції. Спосіб життя. Фольклор",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "3"
                });

            categories.Add(
                new Category
                {
                    Id = "4",
                    Name = "МАТЕМАТИКА. ПРИРОДНИЧІ НАУКИ",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = null
                });

            categories.Add(
                new Category
                {
                    Id = "40",
                    Name = "Загальні відомості про математичні та природничі науки",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "4"
                });

            categories.Add(
                new Category
                {
                    Id = "41",
                    Name = "Математика",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "4"
                });

            categories.Add(
                new Category
                {
                    Id = "42",
                    Name = "Астрономія. Астрофізика. Космічні дослідження. Геодезія",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "4"
                });

            categories.Add(
                new Category
                {
                    Id = "43",
                    Name = "Фізика",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "4"
                });

            categories.Add(
                new Category
                {
                    Id = "44",
                    Name = "Хімія. Кристалографія. Мінералогія",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "4"
                });

            categories.Add(
                new Category
                {
                    Id = "45",
                    Name = "Геологія. Науки про землю",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "4"
                });

            categories.Add(
                new Category
                {
                    Id = "46",
                    Name = "Палеонтологія",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "4"
                });

            categories.Add(
                new Category
                {
                    Id = "47",
                    Name = "Біологічні науки в цілому",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "4"
                });

            categories.Add(
                new Category
                {
                    Id = "48",
                    Name = "Ботаніка",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "4"
                });

            categories.Add(
                new Category
                {
                    Id = "49",
                    Name = "Зоологія",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "4"
                });

            categories.Add(
                new Category
                {
                    Id = "5",
                    Name = "ІНФОРМАЦІЙНІ ТЕХНОЛОГІЇ ТА КОМП'ЮТЕРНА ІНЖЕНЕРІЯ",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = null
                });

            categories.Add(
                new Category
                {
                    Id = "50",
                    Name = "Мови програмування",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "5"
                });

            categories.Add(
                new Category
                {
                    Id = "500",
                    Name = "C++",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "50"
                });

            categories.Add(
                new Category
                {
                    Id = "501",
                    Name = "C#",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "50"
                });

            categories.Add(
                new Category
                {
                    Id = "502",
                    Name = "Java",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "50"
                });

            categories.Add(
                new Category
                {
                    Id = "6",
                    Name = "ПРИКЛАДНІ НАУКИ. МЕДИЦИНА. ТЕХНІКА. СІЛЬСЬКЕ ГОСПОДАРСТВО",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = null
                });

            categories.Add(
                new Category
                {
                    Id = "60",
                    Name = "Загальні питання прикладних наук",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "6"
                });

            categories.Add(
                new Category
                {
                    Id = "61",
                    Name = "Медичні науки",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "6"
                });

            categories.Add(
                new Category
                {
                    Id = "62",
                    Name = "Машинобудування. Техніка в цілому",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "6"
                });

            categories.Add(
                new Category
                {
                    Id = "63",
                    Name = "Сільське господарство. Лісове господарство. Мисливство. Рибне господарство",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "6"
                });

            categories.Add(
                new Category
                {
                    Id = "64",
                    Name = "Домоведення. Комунальне господарство. Служба побуту",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "6"
                });

            categories.Add(
                new Category
                {
                    Id = "65",
                    Name = "Керування підприємствами. Менеджмент. Організація виробництва, торгівлі, транспорту, зв'язку, поліграфії",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "6"
                });

            categories.Add(
                new Category
                {
                    Id = "66",
                    Name = "Хімічна технологія. Хімічна промисловість і споріднені галузі",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "6"
                });

            categories.Add(
                new Category
                {
                    Id = "67",
                    Name = "Різні галузі промисловості та ремесла",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "6"
                });

            categories.Add(
                new Category
                {
                    Id = "68",
                    Name = "Галузі промисловості та ремесла, що виробляють готову продукцію",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "6"
                });

            categories.Add(
                new Category
                {
                    Id = "69",
                    Name = "Будівельна промисловість. Будівельні матеріали. Будівельно-монтажні роботи",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "6"
                });

            categories.Add(
                new Category
                {
                    Id = "7",
                    Name = "МИСТЕЦТВО. АРХІТЕКТУРА. ІГРИ. СПОРТ",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = null
                });

            categories.Add(
                new Category
                {
                    Id = "70",
                    Name = "Історія мистецтва. Художні стилі, напрямки, школи",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "7"
                });

            categories.Add(
                new Category
                {
                    Id = "71",
                    Name = "Планування у межах адміністративно-територіальних одиниць. Ландшафти, парки, сади",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "7"
                });

            categories.Add(
                new Category
                {
                    Id = "72",
                    Name = "Архітектура",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "7"
                });

            categories.Add(
                new Category
                {
                    Id = "73",
                    Name = "Пластичні мистецтва",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "7"
                });

            categories.Add(
                new Category
                {
                    Id = "74",
                    Name = "Малювання та креслення. Дизайн. Декоративно-прикладне мистецтво. Художні промисли",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "7"
                });

            categories.Add(
                new Category
                {
                    Id = "75",
                    Name = "Живопис",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "7"
                });

            categories.Add(
                new Category
                {
                    Id = "76",
                    Name = "Графічні мистецтва. Графіка",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "7"
                });

            categories.Add(
                new Category
                {
                    Id = "77",
                    Name = "Фотографія, кінематографія та подібні процеси",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "7"
                });

            categories.Add(
                new Category
                {
                    Id = "78",
                    Name = "Музика",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "7"
                });

            categories.Add(
                new Category
                {
                    Id = "79",
                    Name = "Видовищні мистецтва. Розваги. Ігри. Спорт",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "7"
                });

            categories.Add(
                new Category
                {
                    Id = "8",
                    Name = "МОВА. МОВОЗНАВСТВО. ХУДОЖНЯ ЛІТЕРАТУРА. ЛІТЕРАТУРОЗНАВСТВО",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = null
                });

            categories.Add(
                new Category
                {
                    Id = "80",
                    Name = "Загальні питання лінгвістики та літератури. Філологія",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "8"
                });

            categories.Add(
                new Category
                {
                    Id = "81",
                    Name = "Лінгвістика. Мовознавство. Мови",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "8"
                });

            categories.Add(
                new Category
                {
                    Id = "82",
                    Name = "Художня література. Літературознавство",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "8"
                });

            categories.Add(
                new Category
                {
                    Id = "9",
                    Name = "ГЕОГРАФІЯ. БІОГРАФІЇ. ІСТОРІЯ",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = null
                });

            categories.Add(
                new Category
                {
                    Id = "90",
                    Name = "Археологія",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "9"
                });

            categories.Add(
                new Category
                {
                    Id = "91",
                    Name = "Передісторія. Доісторичні залишки. Знаряддя праці. Старожитності",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "9"
                });

            categories.Add(
                new Category
                {
                    Id = "92",
                    Name = "Археологічні пам'ятки історичних часів",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "9"
                });

            categories.Add(
                new Category
                {
                    Id = "93",
                    Name = "Краєзнавство",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "9"
                });

            categories.Add(
                new Category
                {
                    Id = "94",
                    Name = "Географія. Географічні дослідження Землі та окремих країн",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "9"
                });

            categories.Add(
                new Category
                {
                    Id = "95",
                    Name = "Біографічні та подібні дослідження",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "9"
                });

            categories.Add(
                new Category
                {
                    Id = "96",
                    Name = "Історична наука. Допоміжні історичні науки",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "9"
                });

            categories.Add(
                new Category
                {
                    Id = "97",
                    Name = "Загальна історія",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "9"
                });

            categories.Add(
                new Category
                {
                    Id = "98",
                    Name = "Історія України",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "9"
                });

            categories.Add(
                new Category
                {
                    Id = "X",
                    Name = "ІНШІ МАТЕРІАЛИ",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = null
                });

            categories.Add(
                new Category
                {
                    Id = "X1",
                    Name = "Навчальні матеріали",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "X"
                });

            categories.Add(
                new Category
                {
                    Id = "X2",
                    Name = "Дисертації",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "X"
                });

            categories.Add(
                new Category
                {
                    Id = "X3",
                    Name = "Дипломні роботи",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = "X"
                });

            context.Categories.AddRange(categories);
            context.SaveChanges();
        }
        #endregion

        public static void SeedBooks(UserManager<DbUser> userManager, ApplicationDbContext context)
        {
            var book = new Book
            {
                Title = "",
                Author = "",
                CategoryId = null,
                DateCreate = DateTime.Now,
                UserId = userManager.FindByEmailAsync("admin@gmail.com").Result.Id,
                Description = "",
                Image = "",
                IsEbook = true,
                File = "",
                Language = "",
                Publisher = "",
                Year = ""
            };
        }

        public static void SeedDataByAS(IServiceProvider services)
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var manager = scope.ServiceProvider.GetRequiredService<UserManager<DbUser>>();
                var managerRole = scope.ServiceProvider.GetRequiredService<RoleManager<DbRole>>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                Console.WriteLine("Appling migrations ... ");
                context.Database.Migrate();
                Console.WriteLine("Database migrated");

                if (!context.Users.Any())
                {
                    Console.WriteLine("Adding data - seeding ... ");
                    SeedData(manager, managerRole);
                }

                if (!context.Categories.Any())
                {
                    Console.WriteLine("Adding categories - seeding ... ");
                    SeedCategories(context);
                }

                Console.WriteLine("Database seeded.");
            }
        }
    }
}

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

        #region Seed Categories
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

            categories.Add(
                new Category
                {
                    Id = "сhild",
                    Name = "Дитяча література",
                    Image = null,
                    DateCreate = DateTime.Now,
                    ParentId = null
                });

            context.Categories.AddRange(categories);
            context.SaveChanges();
        }
        #endregion

        #region Seed Books
        public static void SeedBooks(UserManager<DbUser> userManager, ApplicationDbContext context)
        {
            var books = new List<Book>();
            books.Add(new Book
            {
                Id = "book1",
                Title = "Barron's IELTS 4th edition",
                Author = "Dr. Lin Loughleed",
                CategoryId = "81",
                DateCreate = DateTime.Now,
                UserId = userManager.FindByEmailAsync("admin@gmail.com").Result.Id,
                Description = "The IELTS test is used as a measure of English language proficiency by over 7,000 " +
                "educational institutions, government departments and agencies, and professional organizations in 135 countries. " +
                "This updated manual for ESL students covers all parts of the IELTS and all of its question types: multiple-choice, " +
                "short answer, sentence completion, flowchart completion, graphs, tables, note taking, summarizing, labeling diagrams " +
                "and maps, classification, matching, and selecting from a list. Students will find: Four practice Academic tests " +
                "reflective of the most recent exams. Two practice General Training tests An MP3 CD containing audio for all tests" +
                " and activities. Explanatory answers for all test questions",
                Image = "book1.jpg",
                IsEbook = true,
                File = "book1.pdf",
                Language = "English",
                Publisher = "Barron's Educational Series, Inc.",
                Year = "2016",
                LookedRate = 1
            });

            books.Add(new Book
            {
                Id = "book2",
                Title = "Липпман, Лажойе, Му - Язык программирования С++. Базовый курс, 5-е изд. - 2014",
                Author = "Липпман Стенли Б., Лажойе Жози, Му Барбара Э.",
                CategoryId = "500",
                DateCreate = DateTime.Now,
                UserId = userManager.FindByEmailAsync("admin@gmail.com").Result.Id,
                Description = "Книга \"Язык программирования C++.Базовый курс\" — новое издание популярного и исчерпывающего " +
                "бестселлера по языку программирования C++, которое было полностью пересмотрено и обновлено под стандарт C++11. " +
                "Оно поможет вам быстро изучить язык и использовать его весьма эффективными и передовыми способами. " +
                "С самого начала книги Книга «Язык программирования C++.Базовый курс» читатель знакомится со стандартной " +
                "библиотекой C++, ее самыми популярными функциями и средствами, что позволяет сразу же приступить к написанию" +
                " полезных программ, еще не овладев всеми нюансами языка.Большинство примеров из книги было пересмотрено так, " +
                "чтобы использовать новые средства языка и продемонстрировать их наилучшие способы применения.Книга Книга «Язык " +
                "программирования C++.Базовый курс» — не только проверенное руководство для новичков в C++, " +
                "она содержит также авторитетное обсуждение базовых концепций и методик языка C++ и является ценным ресурсом для опытных программистов, " +
                "особенно желающих побыстрей узнать об усовершенствованиях C++11. Узнайте, как использовать новые средства языка С++11 " +
                "и стандартной библиотеки для быстрого создания надежных программ, а также ознакомьтесь с высокоуровневым " +
                "программированием; Учитесь на примерах, в которых показаны передовые стили программирования и методики " +
                "проектирования; Изучите принципы и узнайте почему язык С++11 работает именно так; Воспользуйтесь множеством " +
                "перекрестных ссылок, способных помочь вам объединить взаимосвязанные концепции и проникнуть в суть; Ознакомьтесь " +
                "с современными методиками обучения и извлеките пользу из упражнений, в которых подчеркиваются ключевые моменты, позволяющие избежать проблем; " +
                "Освойте лучшие методики программирования и закрепите на практике изученный материал.",
                Image = "book2.jpg",
                IsEbook = true,
                File = "book2.pdf",
                Language = "Пер. с англ. и ред. В.А. Коваленко.",
                Publisher = "Издательский дом \"Вильяме\"",
                Year = "2014",
                LookedRate = 2
            });

            books.Add(new Book
            {
                Id = "book3",
                Title = "Страуструп - Программирование. Принципы и практика с использованием C++, 2-е изд. - 2016",
                Author = "Бьярне Страуструп",
                CategoryId = "500",
                DateCreate = DateTime.Now,
                UserId = userManager.FindByEmailAsync("admin@gmail.com").Result.Id,
                Description = "Эта книга — курс программирования, написанный автором языка С++ Бьярном Страуструпом. Книга " +
                "\"Программирование: принципы и практика с использованием C++, второе издание\" не просто учебник по языку C++, " +
                "это учебник по программированию.Несмотря на то что ее автор — автор языка С++, " +
                "книга посвящена не только этому языку программирования(при этом книга представляет собой прекрасное введение в язык С++); язык C++играет в книге сугубо иллюстративную роль.Автор задумал данную книгу как вводный курс по программированию.Поскольку теория без практики совершенно бессмысленна, " +
                "такой учебник должен изобиловать примерами программных решений, и неудивительно, что автор языка C++использовал в книге свое детище. " +
                "В книге в первую очередь описан широкий круг понятий и приемов программирования, необходимых для того, чтобы стать профессиональным " +
                "программистом, и в гораздо меньшей степени — возможности языка программирования C++. " +
                "Книга предназначена в основном для людей, никогда ранее не программировавших.Она опробована более чем тысячей студентов университета. " +
                "Однако опытные программисты и студенты, уже изучившие основы программирования, также найдут в книге много полезной информации, " +
                "которая позволит им перейти на более высокий уровень мастерства.",
                Image = "book3.jpg",
                IsEbook = true,
                File = "book3.pdf",
                Language = "Русский язык",
                Publisher = "Издательский дом \"Вильяме\"",
                Year = "2016",
                LookedRate = 3
            });

            books.Add(new Book
            {
                Id = "book4",
                Title = "Richter, Bospoort - Windows Runtime via C# - 2013",
                Author = "Richter, Bospoort",
                CategoryId = "501",
                DateCreate = DateTime.Now,
                UserId = userManager.FindByEmailAsync("admin@gmail.com").Result.Id,
                Description = "Delve inside the Windows Runtime - and learn best ways to design and build Windows Store apps. Guided by Jeffrey Richter, " +
                "a recognized expert in Windows and .NET programming, along with principal Windows consultant Maarten van de Bospoort, you'll master essential " +
                "concepts. And you'll gain practical insights and tips for how to architect, design, optimize, and debug your apps.",
                Image = "book4.jpg",
                IsEbook = true,
                File = "book4.pdf",
                Language = "English",
                Publisher = "Microsoft Press",
                Year = "2013",
                LookedRate = 4
            });

            books.Add(new Book
            {
                Id = "book5",
                Title = "Troelsen, Japikse - Pro C# 7. With .NET and .NET Core, 8th ed. - 2017",
                Author = "Troelsen, Japikse",
                CategoryId = "501",
                DateCreate = DateTime.Now,
                UserId = userManager.FindByEmailAsync("admin@gmail.com").Result.Id,
                Description = "This essential classic title provides a comprehensive foundation in the C# programming language and the frameworks it lives in. Now in its 8th " +
                "edition, you’ll find all the very latest C# 7.1 and .NET 4.7 features here, along with four brand new chapters on Microsoft’s " +
                "lightweight, cross-platform framework, .NET Core, up to and including .NET Core 2.0. Coverage of ASP.NET Core, Entity Framework (EF) " +
                "Core, and more, sits alongside the latest updates to .NET, including Windows Presentation Foundation (WPF), Windows Communication " +
                "Foundation (WCF), and ASP.NET MVC.",
                Image = "book5.jpg",
                IsEbook = true,
                File = "book5.pdf",
                Language = "English",
                Publisher = "apress",
                Year = "2017",
                LookedRate = 5
            });

            books.Add(new Book
            {
                Id = "book6",
                Title = "Рихтер - CLR via C#. Программирование на платформе Microsoft .NET Framework 4.5 на языке C#, 4-е изд. - 2013",
                Author = "Рихтер",
                CategoryId = "501",
                DateCreate = DateTime.Now,
                UserId = userManager.FindByEmailAsync("admin@gmail.com").Result.Id,
                Description = "Эта книга, выходящая в четвертом издании и уже ставшая классическим учебником по программированию, подробно " +
                "описывает внутреннее устройство и функционирование общеязыковой исполняющей среды (CLR) Microsoft .NET Framework версии 4.5. " +
                "Написанная признанным экспертом в области программирования Джеффри Рихтером, много лет являющимся консультантом команды " +
                "разработчиков .NET Framework компании Microsoft, книга научит вас создавать по-настоящему надежные приложения любого вида, " +
                "в том числе с использованием Microsoft Silverlight, ASP.NET, Windows Presentation Foundation и т.д. Четвертое издание полностью " +
                "обновлено в соответствии со спецификацией платформы.NET Framework 4.5, а также среды Visual Studio 2012 и C# 5.0.",
                Image = "book6.jpg",
                IsEbook = true,
                File = "book6.pdf",
                Language = "Русский язык",
                Publisher = "Питер",
                Year = "2013",
                LookedRate = 6
            });

            books.Add(new Book
            {
                Id = "book7",
                Title = "Троелсен, Джепикс - Язык программирования C# 7 и платформы .NET и .NET Core - 2018",
                Author = "Троелсен, Джепикс",
                CategoryId = "501",
                DateCreate = DateTime.Now,
                UserId = userManager.FindByEmailAsync("admin@gmail.com").Result.Id,
                Description = "Эта классическая книга представляет собой всеобъемлющий источник сведений о языке программирования C# и о связанной " +
                "с ним инфраструктуре. В 8-м издании книги вы найдете описание функциональных возможностей самых последних версий C# 7.0 и 7.1 и .NET 4.7, " +
                "а также совершенно новые главы о легковесной межплатформенной инфраструктуре Microsoft .NET Core, включая версию .NET Core 2.0. Книга " +
                "охватывает ASP.NET Core, Entity Framework (EF) Core и т.д. наряду с последними обновлениями платформы .NET, в том числе внесенными в " +
                "Windows Presentation Foundation (WPF), Windows Communication Foundation (WCF) и ASP.NET MVC.",
                Image = "book7.jpg",
                IsEbook = true,
                File = "book7.pdf",
                Language = "Русский язык",
                Publisher = "Компьютерное издательство \"Диалектика\"",
                Year = "2018",
                LookedRate = 7
            });

            books.Add(new Book
            {
                Id = "book8",
                Title = "Richter - Applied Microsoft .NET Framework Programming - 2002",
                Author = "Richter",
                CategoryId = "501",
                DateCreate = DateTime.Now,
                UserId = userManager.FindByEmailAsync("admin@gmail.com").Result.Id,
                Description = "The Microsoft® .NET Framework allows developers to quickly build robust, secure ASP.NET Web Forms and XML Web service applications, " +
                "Windows® Forms applications, tools, and types. Find out all about its common language runtime and learn how to leverage its power to build, package, " +
                "and deploy any kind of application or component. APPLIED MICROSOFT .NET FRAMEWORK PROGRAMMING is ideal for anyone who understands object-oriented " +
                "programming concepts such as data abstraction, inheritance, and polymorphism. The book carefully explains the extensible type system of the .NET " +
                "Framework, examines how the runtime manages the behavior of types, and explores how an application manipulates types. While focusing on C#, it " +
                "presents concepts applicable to all programming languages that target the .NET Framework.",
                Image = "book8.jpg",
                IsEbook = true,
                File = "book8.pdf",
                Language = "English",
                Publisher = "Wintellect",
                Year = "2002",
                LookedRate = 8
            });

            books.Add(new Book
            {
                Id = "book9",
                Title = "Burd - Beginning Programming with Java For Dummies, 4th ed. - 2014",
                Author = "Burd",
                CategoryId = "502",
                DateCreate = DateTime.Now,
                UserId = userManager.FindByEmailAsync("admin@gmail.com").Result.Id,
                Description = "Beginning Programming with Java For Dummies, 4 Edition is a comprehensive guide to " +
                "learning one of the most popular programming languages worldwide. This book covers basic development " +
                "concepts and techniques through a Java lens. You'll learn what goes into a program, how to put the " +
                "pieces together, how to deal with challenges, and how to make it work. The new Fourth Edition " +
                "has been updated to align with Java 8, and includes new options for the latest tools and techniques. " +
                "Java is the predominant language used to program Android and cloud apps, and its popularity is surging " +
                "as app demand rises.Whether you're just tooling around, or embarking on a career, Beginning Programming " +
                "with Java For Dummies, 4 Edition is a great place to start. Step-by-step instruction, easy-to-read language, " +
                "and quick navigation make this book the perfect resource for new programmers. You'll begin with the basics " +
                "before moving into code, with simple, yet detailed explanations every step of the way.",
                Image = "book9.jpg",
                IsEbook = true,
                File = "book9.pdf",
                Language = "English",
                Publisher = "John Wiley & Sons, Inc",
                Year = "2014",
                LookedRate = 9
            });

            books.Add(new Book
            {
                Id = "book10",
                Title = "Cosmina - Java for Absolute Beginners - 2018",
                Author = "Cosmina",
                CategoryId = "502",
                DateCreate = DateTime.Now,
                UserId = userManager.FindByEmailAsync("admin@gmail.com").Result.Id,
                Description = "Write your first code in Java using simple, step-by-step examples that model real-word objects and " +
                "events, making learning easy. With this book you’ll be able to pick up the concepts without fuss. Java for Absolute " +
                "Beginners teaches Java development in language anyone can understand, giving you the best possible start. You’ll see " +
                "clear code descriptions and layout so that you can get your code running as soon as possible. After reading this book, " +
                "you'll come away with the basics to get started writing programs in Java.",
                Image = "book10.jpg",
                IsEbook = true,
                File = "book10.pdf",
                Language = "English",
                Publisher = "apress",
                Year = "2018",
                LookedRate = 10
            });

            books.Add(new Book
            {
                Id = "book11",
                Title = "Вязовик - Программирование на Java - 2016",
                Author = "Вязовик",
                CategoryId = "502",
                DateCreate = DateTime.Now,
                UserId = userManager.FindByEmailAsync("admin@gmail.com").Result.Id,
                Description = "Курс лекций посвящен современному и мощному языку программирования Java. В его " +
                "рамках дается вводное изложение принципов ООП, необходимое для разработки на Java, основы языка, " +
                "библиотеки для работы с файлами, сетью, для построения оконного интерфейса пользователя (GUI) и др. " +
                "Java изначально появилась на свет как язык для создания небольших приложений для Интернета(апплетов), " +
                "но со временем развилась как универсальная платформа для создания программного обеспечения, " +
                "которое работает буквально везде – от мобильных устройств и смарт - карт до мощных серверов.",
                Image = "book11.jpg",
                IsEbook = true,
                File = "book11.pdf",
                Language = "Русский язык",
                Publisher = "ИНТУИТ",
                Year = "2016",
                LookedRate = 11
            });

            books.Add(new Book
            {
                Id = "book12",
                Title = "The Callan ® Method - 2013",
                Author = "Robin Callan, Duncan McLeay",
                CategoryId = "81",
                DateCreate = DateTime.Now,
                UserId = userManager.FindByEmailAsync("admin@gmail.com").Result.Id,
                Description = "Learning English with the Callan™ Method is fast and effective! " +
                "The Callan Method is a teaching method created specifically to improve your English " +
                "in an intensive atmosphere.The teacher is constantly asking questions, so you are hearing and using the " +
                "language as much as possible.When you speak in the lesson, the teacher corrects your grammar and " +
                "pronunciation mistakes, and you learn a lot from this correction.",
                Image = "book12.jpg",
                IsEbook = true,
                File = "book12.pdf",
                Language = "English",
                Publisher = "Callan Method Organisation Limited",
                Year = "2013",
                LookedRate = 12
            });

            context.Books.AddRange(books);
            context.SaveChanges();

            //Random random = new Random();
            //List<Book> randomList = new List<Book>();
            //for (int i = 0; i < 50; i++)
            //{
            //    int randNumber = random.Next(0, 12);
            //    randomList.Add(new Book()
            //    {
            //        Id = Guid.NewGuid().ToString("D"),
            //        Title = books[randNumber].Title,
            //        Author = books[randNumber].Author,
            //        CategoryId = books[randNumber].CategoryId,
            //        DateCreate = DateTime.Now,
            //        UserId = userManager.FindByEmailAsync("admin@gmail.com").Result.Id,
            //        Description = books[randNumber].Description,
            //        Image = books[randNumber].Image,
            //        IsEbook = books[randNumber].IsEbook,
            //        File = books[randNumber].File,
            //        Language = books[randNumber].Language,
            //        Publisher = books[randNumber].Publisher,
            //        Year = books[randNumber].Year,
            //        LookedRate = randNumber
            //    });
            //}
            //context.Books.AddRange(randomList);
            //context.SaveChanges();
        }
        #endregion

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

                if (!context.Books.Any())
                {
                    Console.WriteLine("Adding books - seeding ... ");
                    SeedBooks(manager, context);
                }

                Console.WriteLine("Database seeded.");
            }
        }
    }
}

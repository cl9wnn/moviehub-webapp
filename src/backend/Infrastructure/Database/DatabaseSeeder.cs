using Infrastructure.Database.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Database;

public static class MovieSeeder
{
    public static void SeedMovies(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var genres = db.Genres.ToList();

        // ===== АКТЁРЫ  =====
        var actors = new List<ActorEntity>();

        // "Начало" (Inception)
        var leo = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Леонардо",
            LastName = "Ди Каприо",
            Biography = "Леонардо Вильгельм Ди Каприо — американский актёр и кинопродюсер.",
            BirthDate = new DateOnly(1974, 11, 11),
            IsDeleted = false
        };
        actors.Add(leo);

        var joseph = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Джозеф",
            LastName = "Гордон-Левитт",
            Biography = "Джозеф Леонард Гордон-Левитт — американский актёр и кинорежиссёр.",
            BirthDate = new DateOnly(1981, 2, 17),
            IsDeleted = false
        };
        actors.Add(joseph);

        var elliot = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Эллиот",
            LastName = "Пейдж",
            Biography = "Эллиот Пейдж — канадский актёр и продюсер.",
            BirthDate = new DateOnly(1987, 2, 21),
            IsDeleted = false
        };
        actors.Add(elliot);

        var tom = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Том",
            LastName = "Харди",
            Biography = "Эдвард Томас Харди — английский актёр и продюсер.",
            BirthDate = new DateOnly(1977, 9, 15),
            IsDeleted = false
        };
        actors.Add(tom);

        var ken = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Кэн",
            LastName = "Ватанабэ",
            Biography = "Кэн Ватанабэ — японский актёр, известный по ролям в американских и японских фильмах.",
            BirthDate = new DateOnly(1959, 10, 21),
            IsDeleted = false
        };
        actors.Add(ken);

        // "Тёмный рыцарь" (The Dark Knight)
        var christian = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Кристиан",
            LastName = "Бейл",
            Biography = "Кристиан Чарльз Филип Бейл — британский актёр.",
            BirthDate = new DateOnly(1974, 1, 30),
            IsDeleted = false
        };
        actors.Add(christian);

        var heath = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Хит",
            LastName = "Леджер",
            Biography = "Хитклифф Эндрю Леджер — австралийский актёр.",
            BirthDate = new DateOnly(1979, 4, 4),
            IsDeleted = false
        };
        actors.Add(heath);

        var aaron = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Аарон",
            LastName = "Экхарт",
            Biography = "Аарон Эдвард Экхарт — американский актёр и продюсер.",
            BirthDate = new DateOnly(1968, 3, 12),
            IsDeleted = false
        };
        actors.Add(aaron);

        var garyOldman = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Гэри",
            LastName = "Олдмен",
            Biography = "Гэри Леонард Олдмен — английский актёр, режиссёр и сценарист.",
            BirthDate = new DateOnly(1958, 3, 21),
            IsDeleted = false
        };
        actors.Add(garyOldman);

        var michaelCaine = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Майкл",
            LastName = "Кейн",
            Biography = "Сэр Майкл Кейн — английский актёр, двукратный обладатель премии «Оскар».",
            BirthDate = new DateOnly(1933, 3, 14),
            IsDeleted = false
        };
        actors.Add(michaelCaine);

        // "Форрест Гамп" (Forrest Gump)
        var tomHanks = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Том",
            LastName = "Хэнкс",
            Biography = "Томас Джеффри Хэнкс — американский актёр, режиссёр и продюсер.",
            BirthDate = new DateOnly(1956, 7, 9),
            IsDeleted = false
        };
        actors.Add(tomHanks);

        var robin = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Робин",
            LastName = "Райт",
            Biography = "Робин Гейл Райт — американская актриса и режиссёр.",
            BirthDate = new DateOnly(1966, 4, 8),
            IsDeleted = false
        };
        actors.Add(robin);

        var garySinise = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Гэри",
            LastName = "Синиз",
            Biography = "Гэри Алан Синиз — американский актёр, режиссёр и музыкант.",
            BirthDate = new DateOnly(1955, 3, 17),
            IsDeleted = false
        };
        actors.Add(garySinise);

        var sally = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Салли",
            LastName = "Филд",
            Biography = "Салли Маргарет Филд — американская актриса, режиссёр и продюсер.",
            BirthDate = new DateOnly(1946, 11, 6),
            IsDeleted = false
        };
        actors.Add(sally);

        var mykelti = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Майкелти",
            LastName = "Уильямсон",
            Biography = "Майкелти Уильямсон — американский актёр.",
            BirthDate = new DateOnly(1957, 3, 4),
            IsDeleted = false
        };
        actors.Add(mykelti);

        // "Криминальное чтиво" (Pulp Fiction)
        var john = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Джон",
            LastName = "Траволта",
            Biography = "Джон Джозеф Траволта — американский актёр, танцор и певец.",
            BirthDate = new DateOnly(1954, 2, 18),
            IsDeleted = false
        };
        actors.Add(john);

        var samuel = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Сэмюэл",
            LastName = "Джексон",
            Biography = "Сэмюэл Лерой Джексон — американский актёр и продюсер.",
            BirthDate = new DateOnly(1948, 12, 21),
            IsDeleted = false
        };
        actors.Add(samuel);

        var uma = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Ума",
            LastName = "Турман",
            Biography = "Ума Карруна Турман — американская актриса и модель.",
            BirthDate = new DateOnly(1970, 4, 29),
            IsDeleted = false
        };
        actors.Add(uma);

        var bruce = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Брюс",
            LastName = "Уиллис",
            Biography = "Уолтер Брюс Уиллис — американский актёр, продюсер и музыкант.",
            BirthDate = new DateOnly(1955, 3, 19),
            IsDeleted = false
        };
        actors.Add(bruce);

        var harvey = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Харви",
            LastName = "Кейтель",
            Biography = "Харви Кейтель — американский актёр и продюсер.",
            BirthDate = new DateOnly(1939, 5, 13),
            IsDeleted = false
        };
        actors.Add(harvey);
        
        // "Волк с Уолл-стрит" (The wolf of Wall Street)
        var jonah = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Джона",
            LastName = "Хилл",
            Biography = "Джона Хилл Фельдштейн — американский актёр, продюсер и комик.",
            BirthDate = new DateOnly(1983, 12, 20),
            IsDeleted = false
        };
        actors.Add(jonah);

        var margot = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Марго",
            LastName = "Робби",
            Biography = "Марго Элис Робби — австралийская актриса и продюсер.",
            BirthDate = new DateOnly(1990, 7, 2),
            IsDeleted = false
        };
        actors.Add(margot);

        var kyle = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Кайл",
            LastName = "Чандлер",
            Biography = "Кайл Мартин Чандлер — американский актёр и продюсер.",
            BirthDate = new DateOnly(1965, 9, 17),
            IsDeleted = false
        };
        actors.Add(kyle);

        var matthew = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Мэттью",
            LastName = "Макконахи",
            Biography = "Мэттью Дэвид Макконахи — американский актёр и продюсер.",
            BirthDate = new DateOnly(1969, 11, 4),
            IsDeleted = false
        };
        actors.Add(matthew);
        
        // "Заклятие" (The Conjuring)
        var patrick = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Патрик",
            LastName = "Уилсон",
            Biography = "Патрик Джозеф Уилсон — американский актёр и певец, известный по ролям в фильмах ужасов.",
            BirthDate = new DateOnly(1973, 7, 3),
            IsDeleted = false
        };
        actors.Add(patrick);

        var vera = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Вера",
            LastName = "Фармига",
            Biography = "Вера Энн Фармига — американская актриса, режиссёр и продюсер украинского происхождения.",
            BirthDate = new DateOnly(1973, 8, 6),
            IsDeleted = false
        };
        actors.Add(vera);

        var lili = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Лили",
            LastName = "Тейлор",
            Biography = "Лили Тейлор — американская актриса, известная своими ролями в независимых фильмах и ужасах.",
            BirthDate = new DateOnly(1967, 2, 20),
            IsDeleted = false
        };
        actors.Add(lili);

        var ron = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Рон",
            LastName = "Ливингстон",
            Biography = "Рон Ливингстон — американский актёр, снявшийся в фильмах ужасов и драмах.",
            BirthDate = new DateOnly(1967, 6, 5),
            IsDeleted = false
        };
        actors.Add(ron);

        var shanley = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Шанли",
            LastName = "Касвелл",
            Biography = "Шанли Касвелл — американская актриса, снималась в жанре ужасов и подростковых фильмах.",
            BirthDate = new DateOnly(1991, 12, 3),
            IsDeleted = false
        };
        actors.Add(shanley);
        
        // Властелин колец: Братство кольца" (The Lord of the Rings: The Fellowship of the Ring)
        var elijah = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Элайджа",
            LastName = "Вуд",
            Biography = "Элайджа Вуд — американский актёр, наиболее известный по роли Фродо Бэггинса.",
            BirthDate = new DateOnly(1981, 1, 28),
            IsDeleted = false
        };
        actors.Add(elijah);

        var ian = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Иэн",
            LastName = "Маккеллен",
            Biography = "Сэр Иэн Маккеллен — британский актёр театра и кино, исполнивший роль Гэндальфа.",
            BirthDate = new DateOnly(1939, 5, 25),
            IsDeleted = false
        };
        actors.Add(ian);

        var viggo = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Вигго",
            LastName = "Мортенсен",
            Biography = "Вигго Мортенсен — американский актёр и поэт, исполнивший роль Арагорна.",
            BirthDate = new DateOnly(1958, 10, 20),
            IsDeleted = false
        };
        actors.Add(viggo);

        var orlando = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Орландо",
            LastName = "Блум",
            Biography = "Орландо Блум — британский актёр, сыгравший эльфа Леголаса.",
            BirthDate = new DateOnly(1977, 1, 13),
            IsDeleted = false
        };
        actors.Add(orlando);

        var sean = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Шон",
            LastName = "Эстин",
            Biography = "Шон Эстин — американский актёр, наиболее известный по роли Сэма.",
            BirthDate = new DateOnly(1971, 2, 25),
            IsDeleted = false
        };
        actors.Add(sean);
        
        // "Остров проклятых" (Shutter Island)
        var mark = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Марк",
            LastName = "Руффало",
            Biography = "Марк Руффало — американский актёр, сыгравший Чака в 'Острове проклятых'.",
            BirthDate = new DateOnly(1967, 11, 22),
            IsDeleted = false
        };
        actors.Add(mark);

        var ben = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Бен",
            LastName = "Кингсли",
            Biography = "Сэр Бен Кингсли — британский актёр, известный по ролям в психологических триллерах.",
            BirthDate = new DateOnly(1943, 12, 31),
            IsDeleted = false
        };
        actors.Add(ben);

        var michelle = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Мишель",
            LastName = "Уильямс",
            Biography = "Мишель Уильямс — американская актриса, снявшаяся в драме 'Остров проклятых'.",
            BirthDate = new DateOnly(1980, 9, 9),
            IsDeleted = false
        };
        actors.Add(michelle);

        var emily = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Эмили",
            LastName = "Мортимер",
            Biography = "Эмили Мортимер — британская актриса, сыгравшая психически нестабильного пациента.",
            BirthDate = new DateOnly(1971, 12, 1),
            IsDeleted = false
        };
        actors.Add(emily);
        
        // "Король Лев" (The Lion King
        var matthewBro = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Мэттью",
            LastName = "Бродерик",
            Biography = "Мэттью Бродерик — американский актёр, озвучивший взрослого Симбу в 'Короле Льве'.",
            BirthDate = new DateOnly(1962, 3, 21),
            IsDeleted = false
        };
        actors.Add(matthewBro);

        var jeremy = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Джереми",
            LastName = "Айронс",
            Biography = "Джереми Айронс — британский актёр, озвучивший Шрама в оригинальной версии.",
            BirthDate = new DateOnly(1948, 9, 19),
            IsDeleted = false
        };
        actors.Add(jeremy);

        var james = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Джеймс",
            LastName = "Джонс",
            Biography = "Джеймс Эрл Джонс — американский актёр, озвучивший Муфасу.",
            BirthDate = new DateOnly(1931, 1, 17),
            IsDeleted = false
        };
        actors.Add(james);

        var nathan = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Нэйтан",
            LastName = "Лейн",
            Biography = "Нэйтан Лейн — американский актёр и комик, озвучивший Тимуна.",
            BirthDate = new DateOnly(1956, 2, 3),
            IsDeleted = false
        };
        actors.Add(nathan);

        var ernie = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Эрни",
            LastName = "Сабелла",
            Biography = "Эрни Сабелла — американский актёр, озвучивший Пумбу.",
            BirthDate = new DateOnly(1949, 9, 19),
            IsDeleted = false
        };
        actors.Add(ernie);
        
        // "Индиана Джонс: В поисках утраченного ковчега" (Indiana Jones: Raiders of the Lost Ark
        var harrison = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Харрисон",
            LastName = "Форд",
            Biography = "Харрисон Форд — американский актёр, прославившийся ролями в приключенческом и научно-фантастическом кино.",
            BirthDate = new DateOnly(1942, 7, 13),
            IsDeleted = false
        };
        actors.Add(harrison);

        var karen = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Карен",
            LastName = "Аллен",
            Biography = "Карен Джейн Аллен — американская актриса, известная по роли Мэрион в фильмах об Индиане Джонсе.",
            BirthDate = new DateOnly(1951, 10, 5),
            IsDeleted = false
        };
        actors.Add(karen);

        var paul = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Пол",
            LastName = "Фриман",
            Biography = "Пол Фриман — британский актёр, известный по роли антагониста Беллока.",
            BirthDate = new DateOnly(1943, 1, 18),
            IsDeleted = false
        };
        actors.Add(paul);

        var ronald = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Рональд",
            LastName = "Лейси",
            Biography = "Рональд Лейси — английский актёр, сыгравший нацистского агента в 'Индиане Джонсе'.",
            BirthDate = new DateOnly(1935, 9, 28),
            IsDeleted = false
        };
        actors.Add(ronald);

        var johnRhys = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Джон",
            LastName = "Рис-Дэвис",
            Biography = "Джон Рис-Дэвис — валлийский актёр, сыгравший Саллу в приключенческой серии фильмов.",
            BirthDate = new DateOnly(1944, 5, 5),
            IsDeleted = false
        };
        actors.Add(johnRhys);
        
        
        // ===== ФИЛЬМЫ =====
        var movies = new List<MovieEntity>();

        // Начало (Inception)
        var nolanDirector = new MovieDirectorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Кристофер",
            LastName = "Нолан"
        };

        var nolanWriter = new MovieWriterEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Кристофер",
            LastName = "Нолан"
        };

        var inception = new MovieEntity
        {
            Id = Guid.NewGuid(),
            Title = "Начало",
            Description = "Вор, крадущий корпоративные секреты с помощью технологии совместного использования снов, получает задание внедрить идею в подсознание наследника бизнес-империи.",
            Year = 2010,
            DurationAtMinutes = 148,
            AgeRating = "PG-13",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { nolanDirector },
            Writers = new List<MovieWriterEntity> { nolanWriter },
            Genres = genres.Where(g => g.Name == "Action" || g.Name == "Sci-Fi" || g.Name == "Thriller").ToList(),
            MovieActors = new List<MovieActorEntity>
            {
                new() { Actor = leo, CharacterName = "Дом Кобб" },
                new() { Actor = joseph, CharacterName = "Артур" },
                new() { Actor = elliot, CharacterName = "Ариадна" },
                new() { Actor = tom, CharacterName = "Имс" },
                new() { Actor = ken, CharacterName = "Сайто" }
            }
        };
        movies.Add(inception);

        // Тёмный рыцарь (The Dark Knight)
        var jonathanNolanWriter = new MovieWriterEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Джонатан",
            LastName = "Нолан"
        };

        var darkKnight = new MovieEntity
        {
            Id = Guid.NewGuid(),
            Title = "Тёмный рыцарь",
            Description = "Бэтмен, Джеймс Гордон и Харви Дент объединяются против хаоса, сеемого Джокером в Готэм-сити.",
            Year = 2008,
            DurationAtMinutes = 152,
            AgeRating = "PG-13",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { nolanDirector },
            Writers = new List<MovieWriterEntity> { jonathanNolanWriter },
            Genres = genres.Where(g => g.Name == "Action" || g.Name == "Crime" || g.Name == "Drama").ToList(),
            MovieActors = new List<MovieActorEntity>
            {
                new() { Actor = christian, CharacterName = "Брюс Уэйн / Бэтмен" },
                new() { Actor = heath, CharacterName = "Джокер" },
                new() { Actor = aaron, CharacterName = "Харви Дент" },
                new() { Actor = garyOldman, CharacterName = "Джеймс Гордон" },
                new() { Actor = michaelCaine, CharacterName = "Альфред Пенниуорт" }
            }
        };
        movies.Add(darkKnight);

        // Форрест Гамп (Forrest Gump)
        var zemekisDirector = new MovieDirectorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Роберт",
            LastName = "Земекис"
        };

        var ericRothWriter = new MovieWriterEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Эрик",
            LastName = "Рот"
        };

        var forrestGump = new MovieEntity
        {
            Id = Guid.NewGuid(),
            Title = "Форрест Гамп",
            Description = "История человека с низким IQ, который неосознанно влияет на ключевые события истории США во второй половине XX века.",
            Year = 1994,
            DurationAtMinutes = 142,
            AgeRating = "PG-13",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { zemekisDirector },
            Writers = new List<MovieWriterEntity> { ericRothWriter },
            Genres = genres.Where(g => g.Name == "Drama" || g.Name == "Romance").ToList(),
            MovieActors = new List<MovieActorEntity>
            {
                new() { Actor = tomHanks, CharacterName = "Форрест Гамп" },
                new() { Actor = robin, CharacterName = "Дженни Каррен" },
                new() { Actor = garySinise, CharacterName = "Лейтенант Дэн Тейлор" },
                new() { Actor = sally, CharacterName = "Миссис Гамп" },
                new() { Actor = mykelti, CharacterName = "Бубба" }
            }
        };
        movies.Add(forrestGump);

        // Криминальное чтиво (Pulp Fiction)
        var tarantinoDirector = new MovieDirectorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Квентин",
            LastName = "Тарантино"
        };

        var tarantinoWriter = new MovieWriterEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Квентин",
            LastName = "Тарантино"
        };

        var pulpFiction = new MovieEntity
        {
            Id = Guid.NewGuid(),
            Title = "Криминальное чтиво",
            Description = "Переплетённые истории бандитов, наёмного убийцы, боксёра и жены гангстера в криминальном мире Лос-Анджелеса.",
            Year = 1994,
            DurationAtMinutes = 154,
            AgeRating = "R",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { tarantinoDirector },
            Writers = new List<MovieWriterEntity> { tarantinoWriter },
            Genres = genres.Where(g => g.Name == "Crime" || g.Name == "Drama").ToList(),
            MovieActors = new List<MovieActorEntity>
            {
                new() { Actor = john, CharacterName = "Винсент Вега" },
                new() { Actor = samuel, CharacterName = "Джулс Виннфилд" },
                new() { Actor = uma, CharacterName = "Миа Уоллес" },
                new() { Actor = bruce, CharacterName = "Бутч Кулидж" },
                new() { Actor = harvey, CharacterName = "Уинстон Вулф" }
            }
        };
        movies.Add(pulpFiction);

        // Волк с Уолл-стрит (The Wolf of Wall Street)
        var scorseseDirector = new MovieDirectorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Мартин",
            LastName = "Скорсезе"
        };

        var terenceWinterWriter = new MovieWriterEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Теренс",
            LastName = "Уинтер"
        };

        var wolfOfWallStreet = new MovieEntity
        {
            Id = Guid.NewGuid(),
            Title = "Волк с Уолл-стрит",
            Description = "Основанная на реальных событиях история брокера Джордана Белфорта, чьи невероятные успехи и эксцентричный образ жизни привели к расследованию ФБР.",
            Year = 2013,
            DurationAtMinutes = 180,
            AgeRating = "R",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { scorseseDirector },
            Writers = new List<MovieWriterEntity> { terenceWinterWriter },
            Genres = genres.Where(g => g.Name == "Biography" || g.Name == "Comedy" || g.Name == "Crime").ToList(),
            MovieActors = new List<MovieActorEntity>
            {
                new() { Actor = leo, CharacterName = "Джордан Белфорт" },
                new() { Actor = jonah, CharacterName = "Донни Азофф" },
                new() { Actor = margot, CharacterName = "Наоми Лапалья" },
                new() { Actor = kyle, CharacterName = "Агент Патрик Дэнэм" },
                new() { Actor = matthew, CharacterName = "Марк Ханна" },
            }
        };
        movies.Add(wolfOfWallStreet);
        
        // "Заклятие" (The Conjuring)
        var wanDirector = new MovieDirectorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Джеймс",
            LastName = "Ван"
        };

        var hayesWriter = new MovieWriterEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Чад",
            LastName = "Хэйс"
        };

        var conjuring = new MovieEntity
        {
            Id = Guid.NewGuid(),
            Title = "Заклятие",
            Description = "Паранормальные исследователи помогают семье, страдающей от демонической одержимости в их доме на ферме.",
            Year = 2013,
            DurationAtMinutes = 112,
            AgeRating = "R",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { wanDirector },
            Writers = new List<MovieWriterEntity> { hayesWriter },
            Genres = genres.Where(g => g.Name == "Horror" || g.Name == "Thriller").ToList(),
            MovieActors = new List<MovieActorEntity>
            {
                new() { Actor = patrick, CharacterName = "Эд Уоррен" },
                new() { Actor = vera, CharacterName = "Лоррейн Уоррен" },
                new() { Actor = lili, CharacterName = "Кэролин Перрон" },
                new() { Actor = ron, CharacterName = "Роджер Перрон" },
                new() { Actor = shanley, CharacterName = "Андреа Перрон" }
            }
        };
        movies.Add(conjuring);
        
        // "Властелин колец: Братство кольца" (The Lord of the Rings: The Fellowship of the Ring)
        var jacksonDirector = new MovieDirectorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Питер",
            LastName = "Джексон"
        };

        var tolkienWriter = new MovieWriterEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Дж. Р. Р.",
            LastName = "Толкин"
        };

        var lotr = new MovieEntity
        {
            Id = Guid.NewGuid(),
            Title = "Властелин колец: Братство кольца",
            Description = "Молодой хоббит получает могущественное кольцо и отправляется в путешествие, чтобы уничтожить его.",
            Year = 2001,
            DurationAtMinutes = 178,
            AgeRating = "PG-13",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { jacksonDirector },
            Writers = new List<MovieWriterEntity> { tolkienWriter },
            Genres = genres.Where(g => g.Name == "Fantasy").ToList(),
            MovieActors = new List<MovieActorEntity>
            {
                new() { Actor = elijah, CharacterName = "Фродо Бэггинс" },
                new() { Actor = ian, CharacterName = "Гэндальф" },
                new() { Actor = viggo, CharacterName = "Арагорн" },
                new() { Actor = orlando, CharacterName = "Леголас" },
                new() { Actor = sean, CharacterName = "Сэм Гэмджи" }
            }
        };
        movies.Add(lotr);
        
        // "Остров проклятых" (Shutter Island)
        var lehaneWriter = new MovieWriterEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Деннис",
            LastName = "Лихэйн"
        };

        var shutter = new MovieEntity
        {
            Id = Guid.NewGuid(),
            Title = "Остров проклятых",
            Description = "Детектив расследует исчезновение пациентки с психиатрической клиники на удалённом острове.",
            Year = 2010,
            DurationAtMinutes = 138,
            AgeRating = "R",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { scorseseDirector },
            Writers = new List<MovieWriterEntity> { lehaneWriter },
            Genres = genres.Where(g => g.Name == "Mystery" || g.Name == "Thriller").ToList(),
            MovieActors = new List<MovieActorEntity>
            {
                new() { Actor = leo, CharacterName = "Тедди Дэниелс" },
                new() { Actor = mark, CharacterName = "Чак Аул" },
                new() { Actor = ben, CharacterName = "Доктор Коли" },
                new() { Actor = michelle, CharacterName = "Долорес Чанал" },
                new() { Actor = emily, CharacterName = "Рэйчел Соландо" }
            }
        };
        movies.Add(shutter);
        
        // "Король Лев" (The Lion King
        var allersDirector = new MovieDirectorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Роджер",
            LastName = "Аллерс"
        };

        var minkerWriter = new MovieWriterEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Айрин",
            LastName = "Мекки"
        };

        var lionKing = new MovieEntity
        {
            Id = Guid.NewGuid(),
            Title = "Король Лев",
            Description = "Молодой лев по имени Симба должен найти своё место в круге жизни после гибели отца.",
            Year = 1994,
            DurationAtMinutes = 88,
            AgeRating = "G",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { allersDirector },
            Writers = new List<MovieWriterEntity> { minkerWriter },
            Genres = genres.Where(g => g.Name == "Animation" || g.Name == "Adventure").ToList(),
            MovieActors = new List<MovieActorEntity>
            {
                new() { Actor = matthewBro, CharacterName = "Симба" },
                new() { Actor = jeremy, CharacterName = "Шрам" },
                new() { Actor = james, CharacterName = "Муфаса" },
                new() { Actor = nathan, CharacterName = "Тимон" },
                new() { Actor = ernie, CharacterName = "Пумба" }
            }
        };
        movies.Add(lionKing);
        
        // "Индиана Джонс: В поисках утраченного ковчега" (Indiana Jones: Raiders of the Lost Ark
        var spielbergDirector = new MovieDirectorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Стивен",
            LastName = "Спилберг"
        };

        var lucasWriter = new MovieWriterEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Джордж",
            LastName = "Лукас"
        };

        var raiders = new MovieEntity
        {
            Id = Guid.NewGuid(),
            Title = "Индиана Джонс: В поисках утраченного ковчега",
            Description = "Археолог Индиана Джонс пытается обогнать нацистов в поисках Ковчега Завета.",
            Year = 1981,
            DurationAtMinutes = 115,
            AgeRating = "PG",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { spielbergDirector },
            Writers = new List<MovieWriterEntity> { lucasWriter },
            Genres = genres.Where(g => g.Name == "Adventure").ToList(),
            MovieActors = new List<MovieActorEntity>
            {
                new() { Actor = harrison, CharacterName = "Индиана Джонс" },
                new() { Actor = karen, CharacterName = "Мэрион Рэйвенвуд" },
                new() { Actor = paul, CharacterName = "Рене Беллок" },
                new() { Actor = ronald, CharacterName = "Тот" },
                new() { Actor = johnRhys, CharacterName = "Салла" }
            }
        };
        movies.Add(raiders);
        
        // Заполнение
        foreach (var actor in actors)
        {
            if (!db.Actors.Any(a => a.FirstName == actor.FirstName && a.LastName == actor.LastName && a.BirthDate == actor.BirthDate))
            {
                db.Actors.Add(actor);
            }
        }
        
        foreach (var movie in movies)
        {
            if (!db.Movies.Any(m => m.Title == movie.Title))
            {
                db.Movies.Add(movie);
            }
        }
        
        db.SaveChanges();
    }
}
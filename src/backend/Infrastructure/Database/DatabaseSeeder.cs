using Infrastructure.Database.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Database;

public static class MovieSeeder
{
    public static void SeedMovies(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (db.Movies.Any())
            return;

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

        // ===== ФИЛЬМЫ =====
        var movies = new List<MovieEntity>();

        // Начало (Inception)
        var nolanDirector1 = new MovieDirectorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Кристофер",
            LastName = "Нолан"
        };

        var nolanWriter1 = new MovieWriterEntity
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
            Directors = new List<MovieDirectorEntity> { nolanDirector1 },
            Writers = new List<MovieWriterEntity> { nolanWriter1 },
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
        var nolanDirector2 = new MovieDirectorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Кристофер",
            LastName = "Нолан"
        };

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
            Directors = new List<MovieDirectorEntity> { nolanDirector2 },
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
        
        db.Actors.AddRange(actors);
        db.Movies.AddRange(movies);
        db.SaveChanges();
    }
}
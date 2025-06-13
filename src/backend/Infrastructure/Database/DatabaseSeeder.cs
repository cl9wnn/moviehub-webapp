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

        // Documentary — "Free Solo"
        var alex = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Алекс",
            LastName = "Хоннольд",
            Biography = "Алекс Хоннольд — американский скалолаз, главный герой документального фильма 'Free Solo'.",
            BirthDate = new DateOnly(1985, 8, 17),
            IsDeleted = false
        };
        actors.Add(alex);

        var tommy = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Томми",
            LastName = "Колдуэлл",
            Biography = "Томми Колдуэлл — американский скалолаз, друг Алекса Хоннольда, выступает в фильме 'Free Solo'.",
            BirthDate = new DateOnly(1978, 8, 11),
            IsDeleted = false
        };
        actors.Add(tommy);

        var jimmy = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Джимми",
            LastName = "Чин",
            Biography = "Джимми Чин — режиссёр и альпинист, снявший 'Free Solo'.",
            BirthDate = new DateOnly(1973, 10, 12),
            IsDeleted = false
        };
        actors.Add(jimmy);

        var elizabeth = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Элизабет",
            LastName = "Чай",
            Biography = "Элизабет Чай Васархели — режиссёр документального фильма 'Free Solo'.",
            BirthDate = new DateOnly(1976, 3, 15),
            IsDeleted = false
        };
        actors.Add(elizabeth);

        var sany = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Санни",
            LastName = "МакКэндлесс",
            Biography = "Санни МакКэндлесс — партнёр Алекса Хоннольда, также показана в фильме.",
            BirthDate = new DateOnly(1989, 6, 5),
            IsDeleted = false
        };
        actors.Add(sany);

        // Western — "The Good, the Bad and the Ugly"
        var clint = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Клинт",
            LastName = "Иствуд",
            Biography = "Клинт Иствуд — американский актёр, сыграл Безымянного в 'Хороший, плохой, злой'.",
            BirthDate = new DateOnly(1930, 5, 31),
            IsDeleted = false
        };
        actors.Add(clint);

        var eli = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Элай",
            LastName = "Уоллак",
            Biography = "Элай Уоллак — американский актёр, исполнил роль Туко.",
            BirthDate = new DateOnly(1915, 12, 7),
            IsDeleted = false
        };
        actors.Add(eli);

        var lee = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Ли",
            LastName = "Ван Клиф",
            Biography = "Ли Ван Клиф — американский актёр, сыграл Ангела Глаза.",
            BirthDate = new DateOnly(1925, 1, 9),
            IsDeleted = false
        };
        actors.Add(lee);

        var mario = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Марио",
            LastName = "Брега",
            Biography = "Марио Брега — итальянский актёр, сыграл Маленького Джона.",
            BirthDate = new DateOnly(1923, 3, 25),
            IsDeleted = false
        };
        actors.Add(mario);

        var aldo = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Альдо",
            LastName = "Джуффре",
            Biography = "Альдо Джуффре — итальянский актёр, сыграл капитана армии Союза.",
            BirthDate = new DateOnly(1924, 4, 10),
            IsDeleted = false
        };
        actors.Add(aldo);

        // Comedy — "The Grand Budapest Hotel"
        var ralph = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Рэйф",
            LastName = "Файнс",
            Biography = "Рэйф Файнс — британский актёр, сыграл Густава в 'Отеле 'Гранд Будапешт'.",
            BirthDate = new DateOnly(1962, 12, 22),
            IsDeleted = false
        };
        actors.Add(ralph);

        var tony = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Тони",
            LastName = "Револори",
            Biography = "Тони Револори — американский актёр, сыграл Зеро Мустафу.",
            BirthDate = new DateOnly(1996, 4, 28),
            IsDeleted = false
        };
        actors.Add(tony);

        var adrien = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Эдриен",
            LastName = "Броуди",
            Biography = "Эдриен Броуди — американский актёр, исполнил роль Дмитрия.",
            BirthDate = new DateOnly(1973, 4, 14),
            IsDeleted = false
        };
        actors.Add(adrien);

        var willem = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Уиллем",
            LastName = "Дефо",
            Biography = "Уиллем Дефо — американский актёр, сыграл Джоплин.",
            BirthDate = new DateOnly(1955, 7, 22),
            IsDeleted = false
        };
        actors.Add(willem);

        var saoirse = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Сирша",
            LastName = "Ронан",
            Biography = "Сирша Ронан — ирландская актриса, исполнила Агату.",
            BirthDate = new DateOnly(1994, 4, 12),
            IsDeleted = false
        };
        actors.Add(saoirse);

        // Horror — "Get Out"
        var daniel = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Даниэл",
            LastName = "Калуя",
            Biography = "Даниэл Калуя — британский актёр, сыграл Криса в фильме 'Прочь'.",
            BirthDate = new DateOnly(1989, 2, 24),
            IsDeleted = false
        };
        actors.Add(daniel);

        var allison = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Эллисон",
            LastName = "Уильямс",
            Biography = "Эллисон Уильямс — американская актриса, сыграла Роуз.",
            BirthDate = new DateOnly(1988, 4, 13),
            IsDeleted = false
        };
        actors.Add(allison);

        var bradley = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Брэдли",
            LastName = "Уитфорд",
            Biography = "Брэдли Уитфорд — американский актёр, сыграл отца Роуз.",
            BirthDate = new DateOnly(1959, 10, 10),
            IsDeleted = false
        };
        actors.Add(bradley);

        var catherine = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Кэтрин",
            LastName = "Кинер",
            Biography = "Кэтрин Кинер — актриса, исполнила мать Роуз.",
            BirthDate = new DateOnly(1959, 3, 23),
            IsDeleted = false
        };
        actors.Add(catherine);

        var lilrel = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Лил Рел",
            LastName = "Хаури",
            Biography = "Лил Рел Хаури — актёр, сыграл Рода, друга Криса.",
            BirthDate = new DateOnly(1979, 12, 17),
            IsDeleted = false
        };
        actors.Add(lilrel);

        // Sci-Fi — "Interstellar"
        var anne = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Энн",
            LastName = "Хэтэуэй",
            Biography = "Энн Хэтэуэй — актриса, сыграла доктора Бренд.",
            BirthDate = new DateOnly(1982, 11, 12),
            IsDeleted = false
        };
        actors.Add(anne);

        var jessica = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Джессика",
            LastName = "Честейн",
            Biography = "Джессика Честейн — актриса, сыграла взрослую Мёрф.",
            BirthDate = new DateOnly(1977, 3, 24),
            IsDeleted = false
        };
        actors.Add(jessica);
        
        var casey = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Кейси",
            LastName = "Аффлек",
            Biography = "Кейси Аффлек — актёр, сыграл Тома, сына Купера.",
            BirthDate = new DateOnly(1975, 8, 12),
            IsDeleted = false
        };
        actors.Add(casey);
        
        // "Гарри Поттер и философский камень"
        var danielRed = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Дэниел",
            LastName = "Рэдклифф",
            Biography = "Дэниел Рэдклифф — британский актёр, сыгравший главную роль Гарри Поттера в одноимённой серии фильмов.",
            BirthDate = new DateOnly(1989, 7, 23),
            IsDeleted = false
        };
        actors.Add(daniel);

        var rupert = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Руперт",
            LastName = "Гринт",
            Biography = "Руперт Гринт — британский актёр, исполнивший роль Рона Уизли, лучшего друга Гарри Поттера.",
            BirthDate = new DateOnly(1988, 8, 24),
            IsDeleted = false
        };
        actors.Add(rupert);

        var emma = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Эмма",
            LastName = "Уотсон",
            Biography = "Эмма Уотсон — британская актриса, сыгравшая Гермиону Грейнджер, одну из главных героинь серии о Гарри Поттере.",
            BirthDate = new DateOnly(1990, 4, 15),
            IsDeleted = false
        };
        actors.Add(emma);

        var alan = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Алан",
            LastName = "Рикман",
            Biography = "Алан Рикман — британский актёр, исполнивший роль профессора Северуса Снегга.",
            BirthDate = new DateOnly(1946, 2, 21),
            IsDeleted = false
        };
        actors.Add(alan);

        var richard = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Ричард",
            LastName = "Харрис",
            Biography = "Ричард Харрис — ирландский актёр, сыгравший роль профессора Альбуса Дамблдора в первых двух фильмах о Гарри Поттере.",
            BirthDate = new DateOnly(1930, 10, 1),
            IsDeleted = false
        };
        actors.Add(richard);
        
        // Титаник
        var kate = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Кейт",
            LastName = "Уинслет",
            Biography = "Кейт Уинслет — британская актриса, исполнившая роль Розы Дьюитт Бьюкейтер.",
            BirthDate = new DateOnly(1975, 10, 5),
            IsDeleted = false
        };
        actors.Add(kate);

        var billy = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Билли",
            LastName = "Зейн",
            Biography = "Билли Зейн — американский актёр, сыгравший Кэла Хокли, жениха Розы.",
            BirthDate = new DateOnly(1966, 2, 24),
            IsDeleted = false
        };
        actors.Add(billy);

        var kathy = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Кэти",
            LastName = "Бейтс",
            Biography = "Кэти Бейтс — американская актриса, сыгравшая Молли Браун, пассажирку первого класса.",
            BirthDate = new DateOnly(1948, 6, 28),
            IsDeleted = false
        };
        actors.Add(kathy);

        var francis = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Фрэнсис",
            LastName = "Фишер",
            Biography = "Фрэнсис Фишер — американская актриса, сыгравшая Рут, мать Розы.",
            BirthDate = new DateOnly(1952, 5, 11),
            IsDeleted = false
        };
        actors.Add(francis);
        
        // Унесенные призраками
        var rumi = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Руми",
            LastName = "Хиираги",
            Biography = "Руми Хиираги — японская актриса, озвучившая Чихиро в оригинальной версии фильма.",
            BirthDate = new DateOnly(1987, 8, 1),
            IsDeleted = false
        };
        actors.Add(rumi);

        var miyu = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Мию",
            LastName = "Ирино",
            Biography = "Мию Ирино — японский актёр, озвучивший Хаку.",
            BirthDate = new DateOnly(1988, 2, 19),
            IsDeleted = false
        };
        actors.Add(miyu);

        var mari = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Мари",
            LastName = "Нацуки",
            Biography = "Мари Нацуки — японская актриса, озвучившая Юбабу и Зенибабу.",
            BirthDate = new DateOnly(1952, 5, 2),
            IsDeleted = false
        };
        actors.Add(mari);

        var takashi = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Такаши",
            LastName = "Наито",
            Biography = "Такаши Наито — японский актёр, озвучивший отца Чихиро.",
            BirthDate = new DateOnly(1955, 5, 27),
            IsDeleted = false
        };
        actors.Add(takashi);

        var bunta = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Бунта",
            LastName = "Суговара",
            Biography = "Бунта Суговара — японский актёр, озвучивший Камадзи.",
            BirthDate = new DateOnly(1933, 8, 16),
            IsDeleted = false
        };
        actors.Add(bunta);
        
        // Социальная дилема
        var tristan = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Тристан",
            LastName = "Харрис",
            Biography = "Тристан Харрис — бывший специалист Google по этике дизайна, один из центральных участников фильма.",
            BirthDate = new DateOnly(1984, 1, 1),
            IsDeleted = false
        };
        actors.Add(tristan);

        var jaron = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Джарон",
            LastName = "Ланье",
            Biography = "Джарон Ланье — пионер виртуальной реальности, философ и автор, выступающий в фильме.",
            BirthDate = new DateOnly(1960, 5, 3),
            IsDeleted = false
        };
        actors.Add(jaron);

        var shoshana = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Шошана",
            LastName = "Зубофф",
            Biography = "Шошана Зубофф — профессор Гарварда и эксперт в области цифрового капитализма.",
            BirthDate = new DateOnly(1951, 11, 18),
            IsDeleted = false
        };
        actors.Add(shoshana);

        var aza = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Аза",
            LastName = "Раскин",
            Biography = "Аза Раскин — сооснователь Center for Humane Technology, выступает как эксперт в фильме.",
            BirthDate = new DateOnly(1984, 2, 1),
            IsDeleted = false
        };
        actors.Add(aza);

        var randy = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Рэнди",
            LastName = "Фернер",
            Biography = "Рэнди Фернер — бывший инженер Facebook, делится опытом изнутри индустрии.",
            BirthDate = new DateOnly(1980, 6, 15),
            IsDeleted = false
        };
        actors.Add(randy);
        
        // Безумный Макс
        var charlize = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Шарлиз",
            LastName = "Терон",
            Biography = "Шарлиз Терон — южноафриканская актриса, исполнившая роль Фуриосы.",
            BirthDate = new DateOnly(1975, 8, 7),
            IsDeleted = false
        };
        actors.Add(charlize);

        var nicholas = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Николас",
            LastName = "Холт",
            Biography = "Николас Холт — британский актёр, сыгравший Накса, воителя.",
            BirthDate = new DateOnly(1989, 12, 7),
            IsDeleted = false
        };
        actors.Add(nicholas);

        var hugh = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Хью",
            LastName = "Кияс-Бёрн",
            Biography = "Хью Кияс-Бёрн — австралийский актёр, исполнивший роль Несмертного Джо.",
            BirthDate = new DateOnly(1947, 5, 18),
            IsDeleted = false
        };
        actors.Add(hugh);

        var zoe = new ActorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Зои",
            LastName = "Кравиц",
            Biography = "Зои Кравиц — американская актриса, сыгравшая Тост — одну из жён Несмертного Джо.",
            BirthDate = new DateOnly(1988, 12, 1),
            IsDeleted = false
        };
        actors.Add(zoe);
        
        // Animation — "Toy Story"
        var tim = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Тим",
            LastName = "Эллен",
            Biography = "Тим Эллен — американский актёр, озвучил Базза Лайтера в «Toy Story».",
            BirthDate = new DateOnly(1953, 6, 13),
            IsDeleted = false
        };
        actors.Add(tim);

        var don = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Дон",
            LastName = "Риклз",
            Biography = "Дон Риклз — американский комик и актёр, озвучил мистера Картофанчика в «Toy story».",
            BirthDate = new DateOnly(1926, 5, 8),
            IsDeleted = false
        };
        actors.Add(don);

        var jim = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Джим",
            LastName = "Варни",
            Biography = "Джим Варни — американский актёр, озвучил Слинки Дога в «Toy Story».",
            BirthDate = new DateOnly(1949, 6, 15),
            IsDeleted = false
        };
        actors.Add(jim);

        var wallace = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Уоллес",
            LastName = "Шон",
            Biography = "Уоллес Шон — американский актёр, озвучил Рекса, нервозного динозавра в «Toy story».",
            BirthDate = new DateOnly(1943, 11, 12),
            IsDeleted = false
        };
        actors.Add(wallace);

        // Animation — "Finding Nemo"
        var albert = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Альберт",
            LastName = "Брукс",
            Biography = "Альберт Брукс — американский актёр, озвучил Марлина, отца Немо в «В поисках Немо».",
            BirthDate = new DateOnly(1947, 7, 22),
            IsDeleted = false
        };
        actors.Add(albert);

        var ellen = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Эллен",
            LastName = "Дедженерес",
            Biography = "Эллен Дедженерес — американская актриса и комедийная актриса, озвучила Дори в «В поисках Немо».",
            BirthDate = new DateOnly(1958, 1, 26),
            IsDeleted = false
        };
        actors.Add(ellen);

        var alexandr = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Александер",
            LastName = "Гулд",
            Biography = "Александер Гулд — канадский актёр, озвучил Немо в «В поисках Немо».",
            BirthDate = new DateOnly(1994, 11, 28),
            IsDeleted = false
        };
        actors.Add(alex);

        var will = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Уилл",
            LastName = "Рэндл",
            Biography = "Вилл Рэндл — американский актёр, озвучил Бруно, рыбку-ангела в «В поисках Немо».",
            BirthDate = new DateOnly(1953, 12, 13),
            IsDeleted = false
        };
        actors.Add(will);

        var geoff = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Джефф",
            LastName = "Голдблюм",
            Biography = "Джефф Голдблюм — американский актёр, озвучил Джека Хэд Вейла – маску монахоморя в «В поисках Немо».",
            BirthDate = new DateOnly(1952, 10, 22),
            IsDeleted = false
        };
        actors.Add(geoff);

        // Animation — "Shrek"
        var mike = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Майк",
            LastName = "Майерс",
            Biography = "Майк Майерс — канадско-американский актёр, озвучил Шрека в «Шреке».",
            BirthDate = new DateOnly(1963, 5, 25),
            IsDeleted = false
        };
        actors.Add(mike);

        var eddy = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Эдди",
            LastName = "Мерфи",
            Biography = "Эдди Мерфи — американский актёр и комик, озвучил Осла в «Шреке».",
            BirthDate = new DateOnly(1961, 4, 3),
            IsDeleted = false
        };
        actors.Add(eddy);

        var cameron = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Кэмерон",
            LastName = "Диас",
            Biography = "Кэмерон Диас — американская актриса, озвучила Фиону в «Шреке».",
            BirthDate = new DateOnly(1972, 8, 30),
            IsDeleted = false
        };
        actors.Add(cameron);

        var johnLitgo = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Джон",
            LastName = "Литгоу",
            Biography = "Джон Литгоу — американский актёр, озвучил Лорда Фаркуада в «Шреке».",
            BirthDate = new DateOnly(1947, 10, 7),
            IsDeleted = false
        };
        actors.Add(john);

        var vincent = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Винсент",
            LastName = "Кассель",
            Biography = "Винсент Кассель — французский актёр, озвучил короля Гарольда во французской версии «Шрека».",
            BirthDate = new DateOnly(1966, 11, 23),
            IsDeleted = false
        };
        actors.Add(vincent);

        // Horror — "A Nightmare on Elm Street"
        var robert = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Роберт",
            LastName = "Энглунд",
            Biography = "Роберт Энглунд — американский актёр, сыграл Фредди Крюгера в «Кошмаре на улице Вязов».",
            BirthDate = new DateOnly(1947, 6, 6),
            IsDeleted = false
        };
        actors.Add(robert);

        var heather = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Хизер",
            LastName = "Лангкенп",
            Biography = "Хизер Лэнгенкамп — американская актриса, сыграла Нэнси Томпсон.",
            BirthDate = new DateOnly(1964, 7, 17),
            IsDeleted = false
        };
        actors.Add(heather);

        var johnS = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Джон",
            LastName = "Саксон",
            Biography = "Джон Саксон — американский актёр, сыграл лейтенанта Дональда Томпсона.",
            BirthDate = new DateOnly(1936, 8, 10),
            IsDeleted = false
        };
        actors.Add(johnS);

        var ronee = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Рони",
            LastName = "Блэкли",
            Biography = "Рони Блэкли — американская актриса, сыграла Мардж Томпсон.",
            BirthDate = new DateOnly(1945, 4, 16),
            IsDeleted = false
        };
        actors.Add(ronee);

        var johnny = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Джонни",
            LastName = "Депп",
            Biography = "Джонни Депп — американский актёр, сыграл Глена Лэнца, дебют в кино.",
            BirthDate = new DateOnly(1963, 6, 9),
            IsDeleted = false
        };
        actors.Add(johnny);

        // Horror — "Halloween" (1978)
        var jamie = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Джейми",
            LastName = "Ли Кёртис",
            Biography = "Джейми Ли Кёртис — американская актриса, сыграла Лори Строуд в «Хэллоуине» (1978).",
            BirthDate = new DateOnly(1958, 11, 22),
            IsDeleted = false
        };
        actors.Add(jamie);

        var donald = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Дональд",
            LastName = "Плезенс",
            Biography = "Дональд Плезенс — британский актёр, сыграл доктора Лумиса.",
            BirthDate = new DateOnly(1919, 10, 5),
            IsDeleted = false
        };
        actors.Add(donald);

        var tonyMorana = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Тони",
            LastName = "Морана",
            Biography = "Тони Морана — американский актёр, сыграл Лэнса Бостона.",
            BirthDate = new DateOnly(1953, 7, 14),
            IsDeleted = false
        };
        actors.Add(tony);

        var nudelman = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Николас",
            LastName = "Рудельман",
            Biography = "Николас Рудельман — американский актёр, сыграл студента Шермана.",
            BirthDate = new DateOnly(1950, 5, 1),
            IsDeleted = false
        };
        actors.Add(nudelman);

        var ira = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Ира",
            LastName = "Хорли",
            Biography = "Ира Хорли — американский актёр, сыграл Пэта Роджерса.",
            BirthDate = new DateOnly(1950, 3, 17),
            IsDeleted = false
        };
        actors.Add(ira);

        // Comedy — "The Big Lebowski"
        var jeff = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Джефф",
            LastName = "Бриджес",
            Biography = "Джефф Бриджес — американский актёр, сыграл Джеффри „Чувака“ Лебовски в «The Big Lebowski».",
            BirthDate = new DateOnly(1949, 12, 4),
            IsDeleted = false
        };
        actors.Add(jeff);

        var johnTurturro = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Джон",
            LastName = "Туртурро",
            Biography = "Джон Туртурро — американский актёр, сыграл Уолтера Собчака в «The Big Lebowski».",
            BirthDate = new DateOnly(1957, 2, 28),
            IsDeleted = false
        };
        actors.Add(johnTurturro);

        var julianne = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Джулианна",
            LastName = "Мур",
            Biography = "Джулианна Мур — американская актриса, сыграла Лорен Лебовски в «The Big Lebowski».",
            BirthDate = new DateOnly(1960, 12, 3),
            IsDeleted = false
        };
        actors.Add(julianne);

        var steveBuscemi = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Стив",
            LastName = "Бушеми",
            Biography = "Стив Бушеми — американский актёр, сыграл Ти-Боун в «The Big Lebowski».",
            BirthDate = new DateOnly(1957, 12, 13),
            IsDeleted = false
        };
        actors.Add(steveBuscemi);

        var philip = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Филип",
            LastName = "Сеймур Хоффман",
            Biography = "Филипп Сеймур Хоффман — американский актёр, сыграл Brandt в «The Big Lebowski».",
            BirthDate = new DateOnly(1967, 7, 23),
            IsDeleted = false
        };
        actors.Add(philip);

        // Comedy — "Office Space"
        var jenniferAniston = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Дженнифер",
            LastName = "Энистон",
            Biography = "Дженнифер Энистон — американская актриса, сыграла Доанна в «Office Space».",
            BirthDate = new DateOnly(1969, 2, 11),
            IsDeleted = false
        };
        actors.Add(jenniferAniston);

        var gary = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Гэри",
            LastName = "Коул",
            Biography = "Гэри Коул — американский актёр, сыграл Билла Ламбча в «Office Space».",
            BirthDate = new DateOnly(1956, 2, 20),
            IsDeleted = false
        };
        actors.Add(gary);

        var stephenRoot = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Стивен",
            LastName = "Рут",
            Biography = "Стивен Рут — американский актёр, сыграл Майкла Болтона в «Office Space».",
            BirthDate = new DateOnly(1955, 7, 22),
            IsDeleted = false
        };
        actors.Add(stephenRoot);

        var jason = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Джейсон",
            LastName = "Бирд",
            Biography = "Джейсон Бирд — американский актёр, сыграл Сэмира Нату в «Office Space».",
            BirthDate = new DateOnly(1969, 1, 23),
            IsDeleted = false
        };
        actors.Add(jason);

        // Comedy — "Shaun of the Dead"
        var simon = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Саймон",
            LastName = "Пегг",
            Biography = "Саймон Пегг — британский актёр, сыграл Шона в «Shaun of the Dead».",
            BirthDate = new DateOnly(1970, 2, 14),
            IsDeleted = false
        };
        actors.Add(simon);

        var nick = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Ник",
            LastName = "Фрост",
            Biography = "Ник Фрост — британский актёр, сыграл Эда в «Shaun of the Dead».",
            BirthDate = new DateOnly(1972, 3, 28),
            IsDeleted = false
        };
        actors.Add(nick);

        var kateAshfield = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Кейт",
            LastName = "Эшфилд",
            Biography = "Кейт Эшфилд — британская актриса, сыграла Диану в «Shaun of the Dead».",
            BirthDate = new DateOnly(1972, 10, 17),
            IsDeleted = false
        };
        actors.Add(kate);

        var lucy = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Люси",
            LastName = "Дэвис",
            Biography = "Люси Давис — британская актриса, сыграла Люси в «Shaun of the Dead».",
            BirthDate = new DateOnly(1966, 6, 13),
            IsDeleted = false
        };
        actors.Add(lucy);

        var billNighy = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Билл",
            LastName = "Найи",
            Biography = "Билл Найи — британский актёр, сыграл Филиппа в «Shaun of the Dead».",
            BirthDate = new DateOnly(1949, 12, 12),
            IsDeleted = false
        };
        actors.Add(billNighy);

        // Drama — "The Godfather"
        var marlon = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Марлон",
            LastName = "Брандо",
            Biography = "Марлон Брандо — американский актёр, сыграл Вито Корлеоне в «The Godfather».",
            BirthDate = new DateOnly(1924, 4, 3),
            IsDeleted = false
        };
        actors.Add(marlon);

        var alPacino = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Аль",
            LastName = "Пачино",
            Biography = "Аль Пачино — американский актёр, сыграл Майкла Корлеоне в «The Godfather».",
            BirthDate = new DateOnly(1940, 4, 25),
            IsDeleted = false
        };
        actors.Add(alPacino);

        var jamesCaan = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Джеймс",
            LastName = "Каан",
            Biography = "Джеймс Каан — американский актёр, сыграл Сона Корлеоне в «The Godfather».",
            BirthDate = new DateOnly(1940, 3, 26),
            IsDeleted = false
        };
        actors.Add(jamesCaan);

        var robertDuvall = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Роберт",
            LastName = "Дувалль",
            Biography = "Роберт Дувалль — американский актёр, сыграл Брана в «The Godfather».",
            BirthDate = new DateOnly(1931, 1, 5),
            IsDeleted = false
        };
        actors.Add(robertDuvall);

        var diane = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Диана",
            LastName = "Кийтон",
            Biography = "Диана Кийтон — американская актриса, сыграла Кей Адамс в «The Godfather».",
            BirthDate = new DateOnly(1946, 1, 5),
            IsDeleted = false
        };
        actors.Add(diane);

        // Drama — "Schindler's List"
        var liam = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Лиам",
            LastName = "Нисон",
            Biography = "Лиам Нисон — ирландский актёр, сыграл Оскара Шиндлера в «Schindler's List».",
            BirthDate = new DateOnly(1952, 6, 7),
            IsDeleted = false
        };
        actors.Add(liam);
        
        var carolineGoodall = new ActorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Кэролайн",
            LastName = "Гудолл",
            Biography = "Кэролайн Гудолл — британская актриса, сыграла Эмили Шиндлер.",
            BirthDate = new DateOnly(1959, 11, 13),
            IsDeleted = false
        };
        actors.Add(carolineGoodall);
        
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
        
        // Documentary — "Free Solo"
        var chinDirector = new MovieDirectorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Джимми",
            LastName = "Чин"
        };

        var elizWriter = new MovieWriterEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Элизабет",
            LastName = "Васархели"
        };

        var freeSolo = new MovieEntity
        {
            Id = Guid.NewGuid(),
            Title = "Фри-соло",
            Description = "История Алекса Хоннольда, который без страховки покоряет вершину Эль-Капитан.",
            Year = 2018,
            DurationAtMinutes = 100,
            AgeRating = "PG-13",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { chinDirector },
            Writers = new List<MovieWriterEntity> { elizWriter },
            Genres = genres.Where(g => g.Name == "Documentary").ToList(),
            MovieActors = new List<MovieActorEntity>
            {
                new() { Actor = alex, CharacterName = "Сам себя" },
                new() { Actor = tommy, CharacterName = "Сам себя" },
                new() { Actor = jimmy, CharacterName = "Сам себя" },
                new() { Actor = elizabeth, CharacterName = "Сама себя" },
                new() { Actor = sany, CharacterName = "Сама себя" }
            }
        };
        movies.Add(freeSolo);

        // Western — "The Good, the Bad and the Ugly"
        var leoneDirector = new MovieDirectorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Серджо",
            LastName = "Леоне"
        };

        var vincenzoniWriter = new MovieWriterEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Лучано",
            LastName = "Винченцони"
        };

        var goodBadUgly = new MovieEntity
        {
            Id = Guid.NewGuid(),
            Title = "Хороший, плохой, злой",
            Description = "Трое стрелков в поисках клада времён Гражданской войны в США.",
            Year = 1966,
            DurationAtMinutes = 161,
            AgeRating = "R",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { leoneDirector },
            Writers = new List<MovieWriterEntity> { vincenzoniWriter },
            Genres = genres.Where(g => g.Name == "Western").ToList(),
            MovieActors = new List<MovieActorEntity>
            {
                new() { Actor = clint, CharacterName = "Безымянный (Блонди)" },
                new() { Actor = eli, CharacterName = "Туко" },
                new() { Actor = lee, CharacterName = "Ангел Глаза" },
                new() { Actor = mario, CharacterName = "Маленький Джон" },
                new() { Actor = aldo, CharacterName = "Капитан Союза" }
            }
        };
        movies.Add(goodBadUgly);

        // Comedy — "The Grand Budapest Hotel"
        var wesDirector = new MovieDirectorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Уэс",
            LastName = "Андерсон"
        };

        var hugoWriter = new MovieWriterEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Хьюго",
            LastName = "Гиннесс"
        };

        var grandBudapest = new MovieEntity
        {
            Id = Guid.NewGuid(),
            Title = "Отель 'Гранд Будапешт'",
            Description = "Консьерж и юный портье оказываются в центре интригующего наследственного спора.",
            Year = 2014,
            DurationAtMinutes = 99,
            AgeRating = "R",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { wesDirector },
            Writers = new List<MovieWriterEntity> { hugoWriter },
            Genres = genres.Where(g => g.Name == "Comedy").ToList(),
            MovieActors = new List<MovieActorEntity>
            {
                new() { Actor = ralph, CharacterName = "Густав" },
                new() { Actor = tony, CharacterName = "Зеро Мустафа" },
                new() { Actor = adrien, CharacterName = "Дмитрий" },
                new() { Actor = willem, CharacterName = "Джоплин" },
                new() { Actor = saoirse, CharacterName = "Агата" }
            }
        };
        movies.Add(grandBudapest);

        // Horror — "Get Out"
        var jordanDirector = new MovieDirectorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Джордан",
            LastName = "Пил"
        };

        var jordanWriter = new MovieWriterEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Джордан",
            LastName = "Пил"
        };

        var getOut = new MovieEntity
        {
            Id = Guid.NewGuid(),
            Title = "Прочь",
            Description = "Чернокожий парень узнаёт зловещую правду о семье своей девушки.",
            Year = 2017,
            DurationAtMinutes = 104,
            AgeRating = "R",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { jordanDirector },
            Writers = new List<MovieWriterEntity> { jordanWriter },
            Genres = genres.Where(g => g.Name == "Horror").ToList(),
            MovieActors = new List<MovieActorEntity>
            {
                new() { Actor = daniel, CharacterName = "Крис Вашингтон" },
                new() { Actor = allison, CharacterName = "Роуз Армитаж" },
                new() { Actor = bradley, CharacterName = "Дин Армитаж" },
                new() { Actor = catherine, CharacterName = "Мисси Армитаж" },
                new() { Actor = lilrel, CharacterName = "Род Уильямс" }
            }
        };
        movies.Add(getOut);

        // Sci-Fi — "Interstellar"
        var interstellar = new MovieEntity
        {
            Id = Guid.NewGuid(),
            Title = "Интерстеллар",
            Description = "Группа астронавтов отправляется через червоточину в поисках нового дома для человечества.",
            Year = 2014,
            DurationAtMinutes = 169,
            AgeRating = "PG-13",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { nolanDirector },
            Writers = new List<MovieWriterEntity> { jonathanNolanWriter },
            Genres = genres.Where(g => g.Name == "Sci-Fi").ToList(),
            MovieActors = new List<MovieActorEntity>
            {
                new() { Actor = matthew, CharacterName = "Купер" },
                new() { Actor = anne, CharacterName = "Амелия Бренд" },
                new() { Actor = jessica, CharacterName = "Мёрф Купер" },
                new() { Actor = michaelCaine, CharacterName = "Профессор Бренд" },
                new() { Actor = casey, CharacterName = "Том Купер" }
            }
        };
        movies.Add(interstellar);
        
        // Fantasy — "Гарри Поттер и философский камень"
        var chrisDirector = new MovieDirectorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Крис",
            LastName = "Коламбус"
        };

        var steveWriter = new MovieWriterEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Стив",
            LastName = "Кловз"
        };

        var harryPotter = new MovieEntity
        {
            Id = Guid.NewGuid(),
            Title = "Гарри Поттер и философский камень",
            Description = "Юный волшебник Гарри Поттер поступает в школу Хогвартс, где узнаёт правду о своём прошлом и сражается с тёмными силами.",
            Year = 2001,
            DurationAtMinutes = 152,
            AgeRating = "PG",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { chrisDirector },
            Writers = new List<MovieWriterEntity> { steveWriter },
            Genres = genres.Where(g => g.Name == "Fantasy").ToList(),
            MovieActors = new List<MovieActorEntity>
            {
                new() { Actor = danielRed, CharacterName = "Гарри Поттер" },
                new() { Actor = rupert, CharacterName = "Рон Уизли" },
                new() { Actor = emma, CharacterName = "Гермиона Грейнджер" },
                new() { Actor = alan, CharacterName = "Северус Снегг" },
                new() { Actor = richard, CharacterName = "Альбус Дамблдор" }
            }
        };
        movies.Add(harryPotter); 
        
        // Romance — "Титаник"
        var cameronDirector = new MovieDirectorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Джеймс",
            LastName = "Кэмерон"
        };

        var cameronWriter = new MovieWriterEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Джеймс",
            LastName = "Кэмерон"
        };
        
        var titanic = new MovieEntity
        {
            Id = Guid.NewGuid(),
            Title = "Титаник",
            Description = "История любви Джека и Роуз на борту обречённого лайнера.",
            Year = 1997,
            DurationAtMinutes = 195,
            AgeRating = "PG-13",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { cameronDirector },
            Writers = new List<MovieWriterEntity> { cameronWriter },
            Genres = genres.Where(g => g.Name == "Romance").ToList(),
            MovieActors = new List<MovieActorEntity>
            {
                new() { Actor = leo, CharacterName = "Джек Доусон" },
                new() { Actor = kate, CharacterName = "Роуз Дьюитт Бьюкейтер" },
                new() { Actor = billy, CharacterName = "Кэл Хокли" },
                new() { Actor = francis, CharacterName = "Рут Дьюитт" },
                new() { Actor = kathy, CharacterName = "Молли Браун" }
            }
        };
        movies.Add(titanic);

        // Animation — "Унесённые призраками"
        var miyazakiDirector = new MovieDirectorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Хаяо",
            LastName = "Миядзаки"
        };

        var hayaoWriter = new MovieWriterEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Хаяо",
            LastName = "Миядзаки"
        };

        var spiritedAway = new MovieEntity
        {
            Id = Guid.NewGuid(),
            Title = "Унесённые призраками",
            Description = "Девочка Чихиро попадает в волшебный мир духов, где ей предстоит спасти своих родителей.",
            Year = 2001,
            DurationAtMinutes = 125,
            AgeRating = "PG",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { miyazakiDirector },
            Writers = new List<MovieWriterEntity> { hayaoWriter },
            Genres = genres.Where(g => g.Name == "Animation").ToList(),
            MovieActors = new List<MovieActorEntity>
            {
                new() { Actor = rumi, CharacterName = "Юбаба (озвучка)" },
                new() { Actor = miyu, CharacterName = "Чихиро (озвучка)" },
                new() { Actor = mari, CharacterName = "Мама (озвучка)" },
                new() { Actor = takashi, CharacterName = "Отец (озвучка)" },
                new() { Actor = bunta, CharacterName = "Камадзи (озвучка)" }
            }
        };
        movies.Add(spiritedAway);

        // Documentary — "The Social Dilemma"
        var jeffDirector = new MovieDirectorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Джефф",
            LastName = "Орловски"
        };

        var davisWriter = new MovieWriterEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Дэвис",
            LastName = "Кумб"
        };

        var socialDilemma = new MovieEntity
        {
            Id = Guid.NewGuid(),
            Title = "Социальная дилемма",
            Description = "Документальный фильм о том, как соцсети манипулируют поведением людей.",
            Year = 2020,
            DurationAtMinutes = 94,
            AgeRating = "PG-13",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { jeffDirector },
            Writers = new List<MovieWriterEntity> { davisWriter },
            Genres = genres.Where(g => g.Name == "Documentary").ToList(),
            MovieActors = new List<MovieActorEntity>
            {
                new() { Actor = tristan, CharacterName = "Сам себя (эксперт)" },
                new() { Actor = jaron, CharacterName = "Сам себя (теоретик)" },
                new() { Actor = shoshana, CharacterName = "Сама себя (экономист)" },
                new() { Actor = aza, CharacterName = "Сам себя (UX-дизайнер)" },
                new() { Actor = randy, CharacterName = "Сам себя (Facebook engineer)" }
            }
        };
        movies.Add(socialDilemma);

        // Action — "Безумный Макс: Дорога ярости"
        var georgeDirector = new MovieDirectorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Джордж",
            LastName = "Миллер"
        };

        var brendanWriter = new MovieWriterEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Брендан",
            LastName = "Маккарти"
        };

        var furyRoad = new MovieEntity
        {
            Id = Guid.NewGuid(),
            Title = "Безумный Макс: Дорога ярости",
            Description = "Макс и Императрица Фуриоза бегут от тирана через постапокалиптическую пустошь.",
            Year = 2015,
            DurationAtMinutes = 120,
            AgeRating = "R",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { georgeDirector },
            Writers = new List<MovieWriterEntity> { brendanWriter },
            Genres = genres.Where(g => g.Name == "Action").ToList(),
            MovieActors = new List<MovieActorEntity>
            {
                new() { Actor = tom, CharacterName = "Макс Рокатански" },
                new() { Actor = charlize, CharacterName = "Фуриоса" },
                new() { Actor = nicholas, CharacterName = "Накс" },
                new() { Actor = hugh, CharacterName = "Несмертный Джо" },
                new() { Actor = zoe, CharacterName = "Тост" }
            }
        };
        movies.Add(furyRoad);

        // Animation — "Toy Story"
        var toystoryDirector = new MovieDirectorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Джон",
            LastName = "Лассетер"
        };
        var toystoryWriter = new MovieWriterEntity {
            Id = Guid.NewGuid(),
            FirstName = "Эндрю",
            LastName = "Стантон"
        };
        var toyStory = new MovieEntity {
            Id = Guid.NewGuid(),
            Title = "История игрушек",
            Description = "История игрушек, оживающих, когда никто не видит.",
            Year = 1995,
            DurationAtMinutes = 81,
            AgeRating = "G",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { toystoryDirector },
            Writers = new List<MovieWriterEntity> { toystoryWriter },
            Genres = genres.Where(g => g.Name == "Animation").ToList(),
            MovieActors = new List<MovieActorEntity> {
                new() { Actor = tom, CharacterName = "Вуди (долликовый ковбой)" },
                new() { Actor = tim, CharacterName = "Базз Лайтер" },
                new() { Actor = don, CharacterName = "Мистер Картофанчик" },
                new() { Actor = jim, CharacterName = "Слинки Дог" },
                new() { Actor = wallace, CharacterName = "Рекс" }
            }
        };
        movies.Add(toyStory);

        // Animation — "Finding Nemo"
        var findingDirector = new MovieDirectorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Эндрю",
            LastName = "Стэнтон"
        };
        var findingWriter = new MovieWriterEntity {
            Id = Guid.NewGuid(),
            FirstName = "Боб",
            LastName = "Питерсон"
        };
        var findingNemo = new MovieEntity {
            Id = Guid.NewGuid(),
            Title = "В поисках Немо",
            Description = "Отец-рыба ищет своего сына Немо через океан.",
            Year = 2003,
            DurationAtMinutes = 100,
            AgeRating = "G",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { findingDirector },
            Writers = new List<MovieWriterEntity> { findingWriter },
            Genres = genres.Where(g => g.Name == "Animation").ToList(),
            MovieActors = new List<MovieActorEntity> {
                new() { Actor = albert, CharacterName = "Марлин" },
                new() { Actor = ellen, CharacterName = "Дори" },
                new() { Actor = alexandr, CharacterName = "Немо" },
                new() { Actor = will, CharacterName = "Бруно" },
                new() { Actor = geoff, CharacterName = "Джек-Хэд" }
            }
        };
        movies.Add(findingNemo);

        // Animation — "Shrek"
        var shrekDirector = new MovieDirectorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Эндрю",
            LastName = "Адамсон"
        };
        var shrekWriter = new MovieWriterEntity {
            Id = Guid.NewGuid(),
            FirstName = "Тед",
            LastName = "Эллиот"
        };
        var shrek = new MovieEntity {
            Id = Guid.NewGuid(),
            Title = "Шрек",
            Description = "Ог Шрек отправляется спасать принцессу Фиону.",
            Year = 2001,
            DurationAtMinutes = 90,
            AgeRating = "PG",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { shrekDirector },
            Writers = new List<MovieWriterEntity> { shrekWriter },
            Genres = genres.Where(g => g.Name == "Animation").ToList(),
            MovieActors = new List<MovieActorEntity> {
                new() { Actor = mike, CharacterName = "Шрек" },
                new() { Actor = eddy, CharacterName = "Осёл" },
                new() { Actor = cameron, CharacterName = "Фиона" },
                new() { Actor = john, CharacterName = "Лорд Фаркуад" },
                new() { Actor = vincent, CharacterName = "Король Гарольд (франц. версия)" }
            }
        };
        movies.Add(shrek);

        // Horror — "A Nightmare on Elm Street"
        var cravenDirector = new MovieDirectorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Уэс",
            LastName = "Крейвен"
        };
        var cravenWriter = new MovieWriterEntity {
            Id = Guid.NewGuid(),
            FirstName = "Уэс",
            LastName = "Крейвен"
        };
        var nightmare = new MovieEntity {
            Id = Guid.NewGuid(),
            Title = "Кошмар на улице Вязов",
            Description = "Фредди Крюгер убивает подростков во сне.",
            Year = 1984,
            DurationAtMinutes = 91,
            AgeRating = "R",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { cravenDirector },
            Writers = new List<MovieWriterEntity> { cravenWriter },
            Genres = genres.Where(g => g.Name == "Horror").ToList(),
            MovieActors = new List<MovieActorEntity> {
                new() { Actor = robert, CharacterName = "Фредди Крюгер" },
                new() { Actor = heather, CharacterName = "Нэнси Томпсон" },
                new() { Actor = johnS, CharacterName = "Лейтенант Дон Томпсон" },
                new() { Actor = ronee, CharacterName = "Мардж Томпсон" },
                new() { Actor = johnny, CharacterName = "Глен Лэнц" }
            }
        };
        movies.Add(nightmare);

        // Horror — "Halloween"
        var carpenterDirector = new MovieDirectorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Джон",
            LastName = "Карпентер"
        };
        var carpenterWriter = new MovieWriterEntity {
            Id = Guid.NewGuid(),
            FirstName = "Джон",
            LastName = "Карпентер"
        };
        var halloween = new MovieEntity {
            Id = Guid.NewGuid(),
            Title = "Хэллоуин",
            Description = "Майкл Майерс возвращается в Хэддонфилд на Хэллоуин.",
            Year = 1978,
            DurationAtMinutes = 91,
            AgeRating = "R",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { carpenterDirector },
            Writers = new List<MovieWriterEntity> { carpenterWriter },
            Genres = genres.Where(g => g.Name == "Horror").ToList(),
            MovieActors = new List<MovieActorEntity> {
                new() { Actor = jamie, CharacterName = "Лори Строуд" },
                new() { Actor = donald, CharacterName = "доктор Лумис" },
                new() { Actor = tonyMorana, CharacterName = "Лэнс Бостон" },
                new() { Actor = nudelman, CharacterName = "студент Шерман" },
                new() { Actor = ira, CharacterName = "Пэт Роджерс" }
            }
        };
        movies.Add(halloween);

        // Comedy — "The Big Lebowski"
        var lebowskiDirector = new MovieDirectorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Джоэл",
            LastName = "Коэн"
        };
        var lebowskiWriter = new MovieWriterEntity {
            Id = Guid.NewGuid(),
            FirstName = "Итан",
            LastName = "Коэн"
        };
        var bigLebowski = new MovieEntity {
            Id = Guid.NewGuid(),
            Title = "Большой Лебовски",
            Description = "„Чувак“ ввязывается в абсурдное дело из-за чужого коврика.",
            Year = 1998,
            DurationAtMinutes = 117,
            AgeRating = "R",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { lebowskiDirector },
            Writers = new List<MovieWriterEntity> { lebowskiWriter },
            Genres = genres.Where(g => g.Name == "Comedy").ToList(),
            MovieActors = new List<MovieActorEntity> {
                new() { Actor = jeff, CharacterName = "Джеффри «Чувак» Лебовски" },
                new() { Actor = johnTurturro, CharacterName = "Уолтер Собчак" },
                new() { Actor = julianne, CharacterName = "Лорен Лебовски" },
                new() { Actor = steveBuscemi, CharacterName = "Ти‑Боун" },
                new() { Actor = philip, CharacterName = "Брандт" }
            }
        };
        movies.Add(bigLebowski);

        // Comedy — "Office Space"
        var officeDirector = new MovieDirectorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Майк",
            LastName = "Джадж"
        };
        var officeWriter = new MovieWriterEntity {
            Id = Guid.NewGuid(),
            FirstName = "Майк",
            LastName = "Джадж"
        };
        var officeSpace = new MovieEntity {
            Id = Guid.NewGuid(),
            Title = "Офисное пространство",
            Description = "Антиутопичная комедия о нелюбимой офисной работе.",
            Year = 1999,
            DurationAtMinutes = 89,
            AgeRating = "R",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { officeDirector },
            Writers = new List<MovieWriterEntity> { officeWriter },
            Genres = genres.Where(g => g.Name == "Comedy").ToList(),
            MovieActors = new List<MovieActorEntity> {
                new() { Actor = ron, CharacterName = "Питер Гиббонс" },
                new() { Actor = jenniferAniston, CharacterName = "Доанна" },
                new() { Actor = gary, CharacterName = "Билл Ламбч" },
                new() { Actor = stephenRoot, CharacterName = "Майкл Болтон" },
                new() { Actor = jason, CharacterName = "Самир На'ту" }
            }
        };
        movies.Add(officeSpace);

        // Comedy — "Shaun of the Dead"
        var shaunDirector = new MovieDirectorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Эдгар",
            LastName = "Райт"
        };
        var shaunWriter = new MovieWriterEntity {
            Id = Guid.NewGuid(),
            FirstName = "Эдгар",
            LastName = "Райт"
        };
        var shaun = new MovieEntity {
            Id = Guid.NewGuid(),
            Title = "Зомби по имени Шон",
            Description = "Британская зомби-комедия о том, как спасают мир после апокалипсиса.",
            Year = 2004,
            DurationAtMinutes = 99,
            AgeRating = "15",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { shaunDirector },
            Writers = new List<MovieWriterEntity> { shaunWriter },
            Genres = genres.Where(g => g.Name == "Comedy").ToList(),
            MovieActors = new List<MovieActorEntity> {
                new() { Actor = simon, CharacterName = "Шон" },
                new() { Actor = nick, CharacterName = "Эд" },
                new() { Actor = kate, CharacterName = "Диана" },
                new() { Actor = lucy, CharacterName = "Люси" },
                new() { Actor = billNighy, CharacterName = "Филипп" }
            }
        };
        movies.Add(shaun);

        // Drama — "The Godfather"
        var godfatherDirector = new MovieDirectorEntity {
            Id = Guid.NewGuid(),
            FirstName = "Фрэнсис",
            LastName = "Форд Коппола"
        };
        var godfatherWriter = new MovieWriterEntity {
            Id = Guid.NewGuid(),
            FirstName = "Марио",
            LastName = "Пьюзо"
        };
        var godfather = new MovieEntity {
            Id = Guid.NewGuid(),
            Title = "Крёстный отец",
            Description = "Сага о семье Корлеоне и власти в криминальном мире.",
            Year = 1972,
            DurationAtMinutes = 175,
            AgeRating = "R",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { godfatherDirector },
            Writers = new List<MovieWriterEntity> { godfatherWriter },
            Genres = genres.Where(g => g.Name == "Drama").ToList(),
            MovieActors = new List<MovieActorEntity> {
                new() { Actor = marlon, CharacterName = "Вито Корлеоне" },
                new() { Actor = alPacino, CharacterName = "Майкл Корлеоне" },
                new() { Actor = jamesCaan, CharacterName = "Сонни Корлеоне" },
                new() { Actor = robertDuvall, CharacterName = "Бранч Корлеоне" },
                new() { Actor = diane, CharacterName = "Кей Адамс" }
            }
        };
        movies.Add(godfather);

        // Drama — "Schindler's List"
        var schindlerWriter = new MovieWriterEntity {
            Id = Guid.NewGuid(),
            FirstName = "Стивен",
            LastName = "Заилян"
        };
        var schindler = new MovieEntity {
            Id = Guid.NewGuid(),
            Title = "Список Шиндлера",
            Description = "История Оскара Шиндлера, спасшего сотни евреев во время Холокоста.",
            Year = 1993,
            DurationAtMinutes = 195,
            AgeRating = "R",
            RatingCount = 0,
            RatingSum = 0,
            IsDeleted = false,
            Directors = new List<MovieDirectorEntity> { spielbergDirector },
            Writers = new List<MovieWriterEntity> { schindlerWriter },
            Genres = genres.Where(g => g.Name == "Drama").ToList(),
            MovieActors = new List<MovieActorEntity> {
                new() { Actor = liam, CharacterName = "Оскар Шиндлер" },
                new() { Actor = ben, CharacterName = "Ицхак Штерн" },
                new() { Actor = ralph, CharacterName = "Амон Гот" },
                new() { Actor = carolineGoodall, CharacterName = "Эмили Шиндлер" },
                new() { Actor = jonah, CharacterName = "заполнитель" }
            }
        };
        movies.Add(schindler);
        
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
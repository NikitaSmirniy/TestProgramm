using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Good iPhone12 = new Good("IPhone 12");
            Good iPhone11 = new Good("IPhone 11");

            Warehouse warehouse = new Warehouse();

            Shop shop = new Shop(warehouse);

            warehouse.Delive(iPhone12, 10);
            warehouse.Delive(iPhone11, 1);

            warehouse.ShowGoods();

            Cart cart = shop.CreateCart();
            cart.Add(iPhone12, 4);
            cart.Add(iPhone11, 3);

            cart.ShowGoods();

            Console.WriteLine(cart.Order().Paylink);

            cart.Add(iPhone12, 9);

            Console.ReadLine();
        }
    }

    public struct Order
    {
        public Order(string paylink)
        {
            Paylink = paylink;
        }

        public string Paylink { get; private set; }
    }

    public class Cart
    {
        private readonly IWarehouse _warehouse;
        private readonly Dictionary<Good, int> _goods = new Dictionary<Good, int>();

        public Cart(IWarehouse warehouse)
        {
            _warehouse = warehouse;
        }

        public void Add(Good good, int count)
        {
            if (_warehouse.Contains(good, count))
            {
                _goods.Add(good, count);
                _warehouse.WriteOffGood(good, count);
            }
        }

        public void ShowGoods()
        {
            foreach (KeyValuePair<Good, int> good in _goods)
            {
                Console.WriteLine($"{good.Key.Name}: {good.Value}");
            }
        }

        public Order Order()
        {
            int paylink = 0;

            foreach (KeyValuePair<Good, int> good in _goods)
                paylink += good.Value;

            return new Order($"Paylink: {paylink}");
        }
    }

    public class Shop
    {
        private readonly Warehouse _warehouse;

        public Shop(Warehouse warehouse)
        {
            _warehouse = warehouse;
        }

        public Cart CreateCart()
        {
            return new Cart(_warehouse);
        }
    }

    public class Warehouse : IWarehouse
    {
        private Dictionary<Good, int> _goods = new Dictionary<Good, int>();

        public void Delive(Good good, int count)
        {
            if (good == null)
                throw new ArgumentNullException(nameof(good));

            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            _goods.Add(good, count);
        }

        public bool Contains(Good good, int count)
        {
            if (_goods.ContainsKey(good) == false)
                throw new ArgumentNullException(nameof(good));

            if (_goods.Count < count)
                throw new ArgumentOutOfRangeException(nameof(count));

            return true;
        }

        public void WriteOffGood(Good good, int count)
        {
            int goodCountInKey = _goods[good];

            if (goodCountInKey >= count)
                _goods[good] -= count;
            else
                _goods.Remove(good);
        }

        public void ShowGoods()
        {
            foreach (KeyValuePair<Good, int> good in _goods)
            {
                Console.WriteLine($"{good.Key.Name}: {good.Value}");
            }
        }
    }

    public interface IWarehouse
    {
        bool Contains(Good good, int count);
        void WriteOffGood(Good good, int count);
    }

    public class Good
    {
        public string Name { get; private set; }

        public Good(string name)
        {
            Name = name;
        }
    }

    public class FF
    {
    }

    static class Randomizer
    {
        private static Random s_random = new Random();

        public static int GenerateRandomValue(int minRandomValue, int maxRandomValue)
        {
            return s_random.Next(minRandomValue, maxRandomValue);
        }

        public static int GenerateRandomValue(int maxRandomValue)
        {
            return s_random.Next(maxRandomValue);
        }
    }

    static class StringDelimiter
    {
        public static void DrawLine(int lineRange = 20)
        {
            Console.WriteLine(new string('-', lineRange));
        }
    }

    //static void Shuffle(int[] array, Random random)
    //{
    //    int secondElementOfArray = 1;

    //    for (int i = array.Length - 1; i > secondElementOfArray; i--)
    //    {
    //        int randomNumber = random.Next(array.Length);
    //        int temp = array[randomNumber];

    //        array[randomNumber] = array[i];
    //        array[i] = temp;
    //    }
    //}

    //static void ShowArray(int[] array)
    //{
    //    for (int i = 0; i < array.Length; i++)
    //    {
    //        if (i < array.Length - 1)
    //            Console.Write($"{array[i]}, ");
    //        else
    //            Console.Write($"{array[i]}");
    //    }
    //}

    //static int[] GetDirection(ConsoleKeyInfo pressedKey)
    //{
    //    const ConsoleKey MoveUpCommand = ConsoleKey.UpArrow;
    //    const ConsoleKey MoveDownCommand = ConsoleKey.DownArrow;
    //    const ConsoleKey MoveLeftCommand = ConsoleKey.LeftArrow;
    //    const ConsoleKey MoveRightCommand = ConsoleKey.RightArrow;

    //    int[] direction = { 0, 0 };

    //    switch (pressedKey.Key)
    //    {
    //        case MoveUpCommand:
    //            direction[0] = -1;
    //            break;

    //        case MoveDownCommand:
    //            direction[0] = 1;
    //            break;

    //        case MoveLeftCommand:
    //            direction[1] = -1;
    //            break;

    //        case MoveRightCommand:
    //            direction[1] = 1;
    //            break;
    //    }

    //    return direction;
    //}

    //static void TryMovePlayer(ref int playerPositionX, ref int playerPositionY, char[,] map, ref int[] direction, char space)
    //{
    //    int nextPlayerPositionX = playerPositionX + direction[0];
    //    int nextPlayerPositionY = playerPositionY + direction[1];

    //    if (map[nextPlayerPositionX, nextPlayerPositionY] == space)
    //    {
    //        playerPositionX = nextPlayerPositionX;
    //        playerPositionY = nextPlayerPositionY;
    //    }
    //}

    //static bool TryGameOver(ref int playerPositionX, ref int playerPositionY, char[,] map, ref int[] direction, char exitPoint)
    //{
    //    int nextPlayerPositionX = playerPositionX + direction[0];
    //    int nextPlayerPositionY = playerPositionY + direction[1];

    //    return map[nextPlayerPositionX, nextPlayerPositionY] != exitPoint;
    //}

    //static void ShowMap(char[,] map)
    //{
    //    Console.SetCursorPosition(0, 0);

    //    for (int x = 0; x < map.GetLength(0); x++)
    //    {
    //        for (int y = 0; y < map.GetLength(1); y++)
    //        {
    //            Console.Write(map[x, y]);
    //        }

    //        Console.WriteLine();
    //    }
    //}

    ////static int GetMaxLengthOfLine(string[] lines)
    ////{
    ////    int maxLenght = lines[0].Length;

    ////    foreach (var line in lines)
    ////        if (line.Length > maxLenght)
    ////            maxLenght = line.Length;

    ////    return maxLenght;
    ////}

    //static void Main(string[] args)
    //{
    //    int playerPositionX = 5;
    //    int playerPositionY = 1;
    //    char playerSymbol = '@';

    //    char space = ' ';
    //    char exitPoint = '%';

    //    bool isOpen = true;

    //    char[,] map = 
    //    {
    //        {'#', '#', '#','#','#','#','#','#','#','#','#','#','#','#','#'},
    //        {'#', '#', '#','#','#','#','#','#','#','#','#','#','#','#','#'},
    //        {'#', '#', '#','#','#','#','#','#','#','#','#','#','#','#','#'},
    //        {'#', '#', '#','#','#','#','#','#','#','#','#','#','#','#','#'},
    //        {'#', ' ', '#','#','#','#','#','#','#','#','#','#','#','#','#'},
    //        {'#', ' ', ' ',' ',' ',' ',' ','#','#','#','#','#','#','#','#'},
    //        {'#', '#', ' ',' ','#','#','#','#','#','#','#','#','#','#','#'},
    //        {'#', '#', '#',' ','#','#','#','#',' ',' ',' ',' ','#','#','#'},
    //        {'#', '#', '#',' ','#','#','#','#',' ','#','#',' ','#','#','#'},
    //        {'#', '#', '#',' ','#','#','#','#',' ','#','#',' ','#',' ','#'},
    //        {'#', '#', '#',' ','#','#','#',' ',' ',' ','#',' ','#',' ','#'},
    //        {'#', '#', '#',' ','#','#','#',' ',' ',' ','#',' ',' ',' ','%'},
    //        {'#', '#', '#',' ','#','#','#',' ',' ',' ','#','#','#',' ','#'},
    //        {'#', '#', '#',' ',' ',' ',' ',' ',' ','#','#','#','#',' ','#'},
    //        {'#', '#', '#','#','#','#','#','#','#','#','#','#','#','#','#'}
    //    };

    //    Console.CursorVisible = false;

    //    while (isOpen)
    //    {
    //        ShowMap(map);

    //        ConsoleKeyInfo pressedKey;
    //        Console.SetCursorPosition(playerPositionY, playerPositionX);
    //        Console.Write(playerSymbol);
    //        pressedKey = Console.ReadKey();

    //        int[] direction = GetDirection(pressedKey);

    //        isOpen = TryGameOver(ref playerPositionX, ref playerPositionY, map, ref direction, exitPoint);

    //        TryMovePlayer(ref playerPositionX, ref playerPositionY, map, ref direction, space);
    //    }

    //Console.ReadLine();

    //static void DrawBar(float value, int maxValue, ConsoleColor color, char symbolBar = ' ', int cursorPositionX = 0, int cursorPositionY = 0)
    //{
    //    ConsoleColor defaulteColor = Console.BackgroundColor;

    //    int percentage = 100;
    //    string bar = "";

    //    if (value > percentage)
    //        value = maxValue;
    //    else if (value <= 0)
    //        value = 0;
    //    else
    //        value = (float)maxValue / percentage * value;

    //    bar = FillBar(0, (int)value, bar, symbolBar);

    //    Console.SetCursorPosition(cursorPositionX, cursorPositionY);
    //    Console.Write('[');
    //    Console.BackgroundColor = color;
    //    Console.Write(bar);
    //    Console.BackgroundColor = defaulteColor;

    //    bar = "";

    //    bar = FillBar((int)value, maxValue, bar);

    //    Console.Write(bar + ']');
    //}

    //static string FillBar(int value, int maxValue, string bar, char symbol = '_')
    //{
    //    for (int i = value; i < maxValue; i++)
    //    {
    //        bar += symbol;
    //    }

    //    return bar;
    //}
    //const string HealthText = "Здоровье: ";
    //const string ManaText = "Мана: ";

    //int health = 100;
    //int maxHealth = 20;
    //int mana = 85;
    //int maxMana = 10;

    //Console.Write(HealthText);
    //DrawBar(health, maxHealth, ConsoleColor.Green, '$', HealthText.Length);

    //Console.Write($"\n{ManaText}");
    //DrawBar(mana, maxMana, ConsoleColor.Blue, '#', ManaText.Length, 1);


    //string userInput = "(()(()))";
    //int maxDepth = 0;
    //int depth = 0;
    //char rightStaple = '(';
    //char leftStaple = ')';

    //Console.WriteLine("Введите скобочное выражение");
    //userInput = Console.ReadLine();

    //foreach (var symbol in userInput)
    //{
    //    if (symbol == rightStaple)
    //    {
    //        depth++;

    //        if (depth > maxDepth)
    //            maxDepth = depth;
    //    }
    //    else if (symbol == leftStaple)
    //    {
    //        depth--;

    //        if(depth < 0)
    //        {
    //            break;
    //        }
    //    }
    //}

    //if(depth != 0)
    //    Console.WriteLine("Выражение некорректно");
    //else
    //    Console.WriteLine($"Корректно \nМаксимальная глубина {maxDepth}");


    //const string CommandExit = "exit";

    //int[] numbers = new int[10];
    //int firstElement = 0;
    //int lastElement = numbers.Length - 1;

    //Random random = new Random();
    //int maxRandomNumber = 100;

    //bool isOpen = true;

    //for (int i = 0; i < numbers.Length; i++)
    //{
    //    numbers[i] = random.Next(0, maxRandomNumber + 1);

    //    Console.Write($"{numbers[i]}, ");
    //}

    //while (isOpen)
    //{
    //    Console.WriteLine($"\n\nКоманда {CommandExit} закрыть программу");

    //    Console.WriteLine("\nНа сколько элеметов хотите произвести сдвиг: ");

    //    string userInput = Console.ReadLine();

    //    int shift;
    //    bool isSuccess = int.TryParse(userInput, out shift);

    //    if (userInput == CommandExit)
    //    {
    //        break;
    //    }
    //    else if (isSuccess == false)
    //    {
    //        Console.WriteLine("Неверная комманда!");
    //        Console.ReadLine();
    //        continue;
    //    }

    //    int shiftResult = shift % numbers.Length;

    //    for (int i = 0; i < shiftResult; i++)
    //    {
    //        int temp = numbers[numbers.Length - 1];

    //        numbers[numbers.Length - 1] = numbers[firstElement];

    //        for (int j = 0; j < lastElement - 1; j++)
    //        {
    //            numbers[j] = numbers[j + 1];
    //        }

    //        numbers[lastElement - 1] = temp;
    //    }

    //    for (int i = 0; i < numbers.Length; i++)
    //    {
    //        Console.Write($"{numbers[i]}, ");
    //    }
    //}


    //string text = "hgfdh hfdks .nfjd kkdfk";
    //char[] seporates = new char[] { ' ', '.' };

    //string[] words = text.Split(seporates, StringSplitOptions.RemoveEmptyEntries);

    //foreach (var word in words)
    //{
    //    Console.WriteLine(word);
    //}


    //int[] numbers = new int[30];

    //Random random = new Random();

    //int maxRandomNumber = 100;

    //Console.Write("Не отсортированный массив: ");

    //for (int i = 0; i < numbers.Length; i++)
    //{
    //    numbers[i] = random.Next(0, maxRandomNumber + 1);

    //    Console.Write(numbers[i] + " ");
    //}

    //for (int i = 0; i < numbers.Length; i++)
    //{
    //    for (int j = 0; j < numbers.Length - 1 - i; j++)
    //    {
    //        int temp;

    //        if (numbers[j] > numbers[j + 1])
    //        {
    //            temp = numbers[j];
    //            numbers[j] = numbers[j + 1];
    //            numbers[j + 1] = temp;
    //        }
    //    }
    //}

    //Console.WriteLine("\n");
    //Console.Write("Отсортированный массив: ");

    //for (int i = 0; i < numbers.Length; i++)
    //{
    //    Console.Write(numbers[i] + " ");
    //}


    //int[] array = new int[30];

    //Random random = new Random();

    //int maxRandomNumber = 9;

    //int repeatedNumber = 0;
    //int repeatedAmount = 0;
    //int repeatedCheck = 0;

    //for (int i = 0; i < array.Length; i++)
    //{
    //    array[i] = random.Next(0, maxRandomNumber + 1);

    //    Console.Write(array[i] + " ");
    //}

    //for (int i = 0; i < array.Length - 1; i++)
    //{
    //    if (array[i] == array[i + 1])
    //    {
    //        repeatedCheck++;

    //        if (repeatedCheck > repeatedAmount)
    //        {
    //            repeatedAmount++;
    //            repeatedNumber = array[i];
    //        }
    //    }
    //    else
    //    {
    //        repeatedCheck = 0;
    //    }
    //}

    //Console.WriteLine($"\nЦифра с большим повторением подряд: {repeatedNumber}\nОно повторилось {repeatedAmount + 1} раз");


    //const string CommandSum = "sum";
    //const string CommandExit = "exit";

    //int[] numbers = new int[0];
    //bool isOpen = true;

    //while (isOpen)
    //{
    //    string userInput;

    //    Console.Clear();

    //    Console.Write("Все элемнты массива: ");

    //    for (int i = 0; i < numbers.Length; i++)
    //    {
    //        Console.Write($"{numbers[i]} ");
    //    }

    //    Console.WriteLine("\n");

    //    Console.WriteLine($"Команда: {CommandSum} - находит сумму всего массива");
    //    Console.WriteLine($"Команда: {CommandExit} - выходит из программы");
    //    Console.WriteLine($"Команда: {CommandSum} - находит сумму всего массива");

    //    Console.Write("Введите комманду: ");

    //    userInput = Console.ReadLine();

    //    switch (userInput)
    //    {
    //        case CommandSum:
    //            int sum = 0;

    //            for (int i = 0; i < numbers.Length; i++)
    //            {
    //                sum += numbers[i];
    //            }

    //            Console.WriteLine($"Сумма чисел всех элементов массива: {sum}");
    //            break;

    //        case CommandExit:
    //            isOpen = false;
    //            Console.WriteLine("Программа завершена");
    //            break;

    //        default:
    //            int[] tempArray;

    //            tempArray = numbers;
    //            numbers = new int[numbers.Length + 1];

    //            for (int i = 0; i < tempArray.Length; i++)
    //            {
    //                numbers[i] = tempArray[i];
    //            }

    //            Console.Write($"Вы добавили элемент массива: {userInput}");
    //            numbers[numbers.Length - 1] = Convert.ToInt32(userInput);
    //            break;
    //    }

    //    Console.ReadLine();
    //}
    //Random random = new Random();

    //int minNumber = 10;
    //int maxNumber = 100;
    //int[,] array = new int[10, 10];

    //int lineIndex = 0;
    //int lastLargeNumber = 0;

    //for (int i = 0; i < array.GetLength(0); i++)
    //{
    //    for (int j = 0; j < array.GetLength(1); j++)
    //    {
    //        array[i, j] = random.Next(minNumber + 1, maxNumber);

    //        if (array[i, j] > lastLargeNumber)
    //        {
    //            lastLargeNumber = array[i, j];
    //            lineIndex = i;
    //        }

    //        Console.Write(array[i, j] + " ");
    //    }

    //    Console.WriteLine();
    //}

    //Console.WriteLine();

    //for (int i = 0; i < array.GetLength(0); i++)
    //{
    //    for (int j = 0; j < array.GetLength(1); j++)
    //    {
    //        if (i == lineIndex)
    //        {
    //            array[i, j] = 0;
    //        }

    //        Console.Write(array[i, j] + " ");
    //    }

    //    Console.WriteLine();
    //}

    //Console.ReadLine();
    ////Кол-во строк массива
    //Console.WriteLine(array.GetLength(0));
    ////Кол-во элем-ов массива
    //Console.WriteLine(array.GetLength(1));

    //const string CommandDamageAttack = "1";
    //const string CommandFireBallReload = "2";
    //const string CommandFireBallActive = "3";
    //const string CommandHealing = "4";

    //int playerMaxHealth = 100;
    //int playerHealth = playerMaxHealth;
    //int playerMaxMana = 100;
    //int playerMana = playerMaxMana;
    //int fireBallCost = 50;
    //int healingAmount = 3;

    //int playerDamage;
    //int playerMinDamage = 50;
    //int playerMaxDamage = 125;
    //int fireBallDamage = 300;
    //bool isHaveFireBall = false;

    //int enemyMaxHealth = 1000;
    //int enemyHealth = enemyMaxHealth;
    //int enemyDamage;
    //int enemyMinDamage = 20;
    //int enemyMaxDamage = 40;

    //Random random = new Random();

    //while (playerHealth > 0 && enemyHealth > 0)
    //{
    //    Console.Clear();

    //    Console.WriteLine($"Здоровье героя {playerHealth} / {playerMaxHealth}\nМана героя {playerMana} / {playerMaxMana}\n");


    //    Console.WriteLine($"Здоровье Босса: {enemyHealth} / {enemyMaxHealth}\n");

    //    Console.WriteLine("Ваш ход, выберите способность:");

    //    Console.WriteLine($"Способность {CommandDamageAttack} - наносит урон врагу от {playerMinDamage} до {playerMaxDamage}");

    //    if (isHaveFireBall == false)
    //    {
    //        Console.WriteLine($"Способность {CommandFireBallReload} - создает огненный шар");
    //    }
    //    else
    //    {
    //        Console.WriteLine($"Способность {CommandFireBallReload} - Вы уже создали огненный шар, осталось только взорвать его способностью - {CommandFireBallActive}");
    //    }

    //    if (isHaveFireBall)
    //    {
    //        Console.WriteLine($"Способность {CommandFireBallActive} - взрывает огненный шар и наносит {fireBallDamage} урона врагу");
    //    }
    //    else
    //    {
    //        Console.WriteLine($"Способность {CommandFireBallActive} - взрывает огненный шар(!сначала воспользуйтесь способностью {CommandFireBallReload}) и наносит {fireBallDamage} урона врагу");
    //    }
    //    Console.WriteLine($"Способность {CommandHealing} - полность восстанавливает здоровье и ману герою(у вас осталось {healingAmount} применений)");

    //    string userInput = Console.ReadLine();

    //    switch (userInput)
    //    {
    //        case CommandDamageAttack:
    //            playerDamage = random.Next(playerMinDamage, playerMaxDamage + 1);
    //            enemyHealth -= playerDamage;
    //            Console.WriteLine($"Вы нанесли - {playerDamage} урона\nЗдоровье врага: {enemyHealth} / {enemyMaxHealth}");
    //            break;

    //        case CommandFireBallReload:
    //            if (playerMana >= fireBallCost)
    //            {
    //                isHaveFireBall = true;
    //                playerMana -= fireBallCost;
    //                Console.WriteLine($"Вы создали огненный шар и потратили ману!\nМана {playerMana} / {playerMaxMana}");
    //            }
    //            else
    //            {
    //                Console.WriteLine("У вас не хватает маны");
    //            }
    //            break;

    //        case CommandFireBallActive:
    //            if (isHaveFireBall)
    //            {
    //                isHaveFireBall = false;
    //                enemyHealth -= fireBallDamage;
    //                Console.WriteLine($"Вы взорвали огненный шар и нанесли - {fireBallDamage} урона\nЗдоровье врага: {enemyHealth} / {enemyMaxHealth}");
    //            }
    //            else
    //            {
    //                Console.WriteLine("Сначала создайте огненный шар");
    //            }
    //            break;

    //        case CommandHealing:
    //            if (healingAmount > 0)
    //            {
    //                healingAmount--;
    //                playerHealth = playerMaxHealth;
    //                playerMana = playerMaxMana;
    //                Console.WriteLine($"Вы восстановили себе ману и здоровье, а так же потратили одно восстановление\nЗдоровье {playerHealth} / {playerMaxHealth}\nМана {playerMana} / {playerMaxMana}");
    //            }
    //            else
    //            {
    //                Console.WriteLine("У вас закончились восстановления!");
    //            }
    //            break;

    //        default:
    //            Console.WriteLine("Вы промахнулись!");
    //            break;
    //    }

    //    Console.ReadLine();

    //    Console.WriteLine("Теперь ход врага");

    //    enemyDamage = random.Next(enemyMinDamage, enemyMaxDamage + 1);
    //    playerHealth -= enemyDamage;
    //    Console.Write($"Враг использовал атаку наносящую урон от {enemyMinDamage} до {enemyMaxDamage}\nВраг нанес вам {enemyDamage} урона\nЗдоровье героя {playerHealth} / {playerMaxHealth}");

    //    Console.ReadLine();
    //}

    //if (playerHealth <= 0 && enemyHealth <= 0)
    //{
    //    Console.ForegroundColor = ConsoleColor.Yellow;
    //    Console.WriteLine("Ничья!!!");
    //}
    //else if (playerHealth <= 0)
    //{
    //    Console.ForegroundColor = ConsoleColor.Red;
    //    Console.WriteLine("Вы проиграли!!!");
    //}
    //else if (enemyHealth <= 0)
    //{
    //    Console.ForegroundColor = ConsoleColor.Green;
    //    Console.WriteLine("Вы одержали победу над боссом!!!");
    //}

    //Console.ReadLine();

    //Console.WriteLine(new string (userSymbol, userName.Length));


    //char userSymbol;
    //string userName;
    //string frame = "";

    //Console.Write("Введите ваш символ: ");
    //userSymbol = Console.ReadKey(true).KeyChar;
    //Console.Write(userSymbol);

    //Console.Write("\nВведите ваше имя: ");
    //userName = Console.ReadLine();

    //for (int i = 0; i < userName.Length; i++)
    //{
    //    frame += userSymbol;
    //}

    //Console.WriteLine($"{userSymbol}{frame}{userSymbol}");
    //Console.WriteLine($"{userSymbol}{userName}{userSymbol}");
    //Console.WriteLine($"{userSymbol}{frame}{userSymbol}");


    //const string CommandRubToUsd = "1";
    //const string CommandRubToEur = "2";
    //const string CommandUsdToRub = "3";
    //const string CommandUsdToEur = "4";
    //const string CommandEurToRub = "5";
    //const string CommandEurToUsd = "6";
    //const string CommandExit = "0";

    //const string CurrencyUsd = "Usd";
    //const string CurrencyRub = "Rub";
    //const string CurrencyEur = "Eur";

    //float rublesInWallent;
    //float dollarsInWallent;
    //float euroInWallent;

    //float usdToRub = 85;
    //float rubToUsd = 1 / usdToRub;
    //float eurToRub = 92;
    //float rubToEur = 1 / eurToRub;
    //float usdToEur = 1.5f;
    //float eurToUsd = 1.5f;
    //float exchangeCurrencyCount;

    //bool isOpen = true;
    //string userInput;

    //Console.Write("Введите ваш балланс рублей: ");
    //rublesInWallent = Convert.ToSingle(Console.ReadLine());
    //Console.Write("Введите ваш балланс долларов: ");
    //dollarsInWallent = Convert.ToSingle(Console.ReadLine());
    //Console.Write("Введите ваш балланс евро: ");
    //euroInWallent = Convert.ToSingle(Console.ReadLine());

    //while (isOpen)
    //{
    //    Console.WriteLine($"\nКурс доллара составляет {rubToUsd} рублей\nКурс евро составляет {rubToEur} рублей\nКурс евро составляет {usdToEur} долларов\n");

    //    Console.Write($"Команда {CommandRubToUsd} сконвертировать валюту {CurrencyRub} в валюту {CurrencyUsd}\n");
    //    Console.Write($"Команда {CommandRubToEur} сконвертировать валюту {CurrencyRub} в валюту {CurrencyEur}\n");
    //    Console.Write($"Команда {CommandUsdToRub} сконвертировать валюту {CurrencyUsd} в валюту {CurrencyRub}\n");
    //    Console.Write($"Команда {CommandUsdToEur} сконвертировать валюту {CurrencyUsd} в валюту {CurrencyEur}\n");
    //    Console.Write($"Команда {CommandEurToRub} сконвертировать валюту {CurrencyEur} в валюту {CurrencyRub}\n");
    //    Console.Write($"Команда {CommandEurToUsd} сконвертировать валюту {CurrencyEur} в валюту {CurrencyUsd}\n");
    //    Console.Write($"Команда {CommandExit} выйти из программы!\n\n");

    //    Console.Write("Введина команда: ");
    //    userInput = Console.ReadLine();

    //    switch (userInput)
    //    {
    //        case CommandRubToUsd:
    //            Console.WriteLine("Обмен рублей на доллары.");
    //            Console.Write("Сколько вы хотите обменять? ");
    //            exchangeCurrencyCount = Convert.ToSingle(Console.ReadLine());

    //            if (rublesInWallent >= exchangeCurrencyCount)
    //            {
    //                rublesInWallent -= exchangeCurrencyCount;
    //                dollarsInWallent += exchangeCurrencyCount * rubToUsd;
    //            }
    //            else
    //            {
    //                Console.WriteLine("Средств для операции не достаточно!");
    //            }

    //            break;

    //        case CommandRubToEur:
    //            Console.WriteLine("Обмен рублей на евры.");
    //            Console.Write("Сколько вы хотите обменять? ");
    //            exchangeCurrencyCount = Convert.ToSingle(Console.ReadLine());

    //            if (rublesInWallent >= exchangeCurrencyCount)
    //            {
    //                rublesInWallent -= exchangeCurrencyCount;
    //                euroInWallent += exchangeCurrencyCount * rubToEur;
    //            }
    //            else
    //            {
    //                Console.WriteLine("Средств для операции не достаточно!");
    //            }

    //            break;

    //        case CommandUsdToRub:
    //            Console.WriteLine("Обмен долларов на рубли.");
    //            Console.Write("Сколько вы хотите обменять? ");
    //            exchangeCurrencyCount = Convert.ToSingle(Console.ReadLine());

    //            if (dollarsInWallent >= exchangeCurrencyCount)
    //            {
    //                dollarsInWallent -= exchangeCurrencyCount;
    //                rublesInWallent += exchangeCurrencyCount * usdToRub;
    //            }
    //            else
    //            {
    //                Console.WriteLine("Средств для операции не достаточно!");
    //            }

    //            break;

    //        case CommandUsdToEur:
    //            Console.WriteLine("Обмен долларов на евро.");
    //            Console.Write("Сколько вы хотите обменять? ");
    //            exchangeCurrencyCount = Convert.ToSingle(Console.ReadLine());

    //            if (dollarsInWallent >= exchangeCurrencyCount)
    //            {
    //                dollarsInWallent -= exchangeCurrencyCount;
    //                euroInWallent += exchangeCurrencyCount * usdToEur;
    //            }
    //            else
    //            {
    //                Console.WriteLine("Средств для операции не достаточно!");
    //            }

    //            break;

    //        case CommandEurToRub:
    //            Console.WriteLine("Обмен евро на рубли.");
    //            Console.Write("Сколько вы хотите обменять? ");
    //            exchangeCurrencyCount = Convert.ToSingle(Console.ReadLine());

    //            if (euroInWallent >= exchangeCurrencyCount)
    //            {
    //                euroInWallent -= exchangeCurrencyCount;
    //                rublesInWallent += exchangeCurrencyCount * eurToRub;
    //            }
    //            else
    //            {
    //                Console.WriteLine("Средств для операции не достаточно!");
    //            }

    //            break;

    //        case CommandEurToUsd:
    //            Console.WriteLine("Обмен евро на доллараы.");
    //            Console.Write("Сколько вы хотите обменять? ");
    //            exchangeCurrencyCount = Convert.ToSingle(Console.ReadLine());

    //            if (euroInWallent >= exchangeCurrencyCount)
    //            {
    //                euroInWallent -= exchangeCurrencyCount;
    //                dollarsInWallent += exchangeCurrencyCount * eurToUsd;
    //            }
    //            else
    //            {
    //                Console.WriteLine("Средств для операции не достаточно!");
    //            }

    //            break;

    //        case CommandExit:
    //            isOpen = false;
    //            Console.WriteLine("Вы вышли из программы");
    //            break;

    //        default:
    //            Console.WriteLine("Такой команды не существует!\n");
    //            break;
    //    }

    //    Console.WriteLine($"\nВаш балланс рублей: {rublesInWallent}");
    //    Console.WriteLine($"Ваш балланс долларов: {dollarsInWallent}");
    //    Console.WriteLine($"Ваш балланс евро: {euroInWallent}");
    //}

    //Console.ReadLine();

    //const string CommandShowText1 = "1";
    //const string CommandShowText2 = "2";
    //const string CommandRandomNumber = "3";
    //const string CommandConsoleClear = "4";
    //const string CommandExit = "5";

    //Random random = new Random();
    //int number;

    //string userInput;
    //bool isOpen = true;

    //Console.WriteLine("Добро пожаловать в программу!\n");

    //while (isOpen)
    //{
    //    Console.Write($"Команда {CommandShowText1} вывести текст документа {CommandShowText1}\n");
    //    Console.Write($"Команда {CommandShowText2} вывести текст документа {CommandShowText2}\n");
    //    Console.Write($"Команда {CommandRandomNumber} вывести рандомное число\n");
    //    Console.Write($"Команда {CommandConsoleClear} очистить консоль!\n");
    //    Console.WriteLine($"Команда {CommandExit} выйти из консоли!");

    //    userInput = Console.ReadLine();

    //    switch (userInput)
    //    {
    //        case CommandShowText1:
    //            Console.WriteLine("Документ 1\n\n");
    //            break;
    //        case CommandShowText2:
    //            Console.WriteLine("Докумет 2\n\n");
    //            break;
    //        case CommandRandomNumber:
    //            number = random.Next(0, 101);
    //            Console.WriteLine($"Вы вывели рандомное число и оно равно {number}\n\n");
    //            break;
    //        case CommandConsoleClear:
    //            Console.Clear();
    //            Console.Write("Консоль очищена\n\n");
    //            break;
    //        case CommandExit:
    //            Console.Write("Вы вышли из прогараммы");
    //            isOpen = false;
    //            break;
    //        default:
    //            Console.Write("Такой команды не существует!\n\n");
    //            break;
    //    }
    //}
}
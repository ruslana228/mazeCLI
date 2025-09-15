using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maze
{
    internal class Program
    {
        // Фиксированный лабиринт (0 - проход, 1 - стена)
        private static int[,] maze = {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,1},
            {1,0,1,0,1,1,1,0,1,1,1,1,1,1,1,1,1,0,1,0,1,0,1,1,1,0,1,1,1,0,1,0,1,0,1,0,1},
            {1,0,1,0,0,0,0,0,0,0,1,0,1,0,0,0,0,0,1,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,1},
            {1,0,1,0,1,1,1,1,1,0,1,0,1,1,1,1,1,0,1,1,1,0,1,1,1,1,1,0,1,0,1,1,1,0,1,0,1},
            {1,0,1,0,1,0,1,0,1,0,1,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,1,0,0,0,1,0,1},
            {1,0,1,1,1,0,1,0,1,0,1,0,1,1,1,0,1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,0,1,1,1,0,1},
            {1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,1,0,1},
            {1,0,1,0,1,0,1,0,1,1,1,0,1,1,1,1,1,0,1,1,1,1,1,0,1,1,1,1,1,0,1,0,1,0,1,0,1},
            {1,0,1,0,1,0,1,0,1,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,1,0,1,0,1,0,0,0,1},
            {1,0,1,0,1,1,1,1,1,0,1,0,1,1,1,0,1,0,1,1,1,0,1,0,1,0,1,1,1,0,1,1,1,1,1,0,1},
            {1,0,1,0,1,0,1,0,1,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,1,0,1,0,1,0,1,0,0,0,0,0,1},
            {1,0,1,1,1,0,1,0,1,1,1,0,1,1,1,1,1,0,1,1,1,0,1,1,1,0,1,0,1,0,1,0,1,0,1,0,1},
            {1,0,1,0,1,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,0,0,1,0,1,0,1,0,1},
            {1,1,1,0,1,0,1,1,1,0,1,0,1,1,1,0,1,0,1,1,1,1,1,0,1,1,1,0,1,0,1,0,1,1,1,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,0,0,1},
            {1,0,1,1,1,1,1,1,1,0,1,0,1,0,1,1,1,0,1,1,1,0,1,1,1,0,1,1,1,0,1,1,1,0,1,0,1},
            {1,0,1,0,0,0,0,0,0,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,1},
            {1,1,1,1,1,0,1,0,1,1,1,1,1,0,1,1,1,0,1,0,1,0,1,1,1,1,1,0,1,0,1,0,1,1,1,0,1},
            {1,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
        };

        // Начальные координаты игрока
        private static int playerX = 1;
        private static int playerY = 1;

        // Координаты выхода 
        private static int exitX = 35;
        private static int exitY = 19;

        private static bool[,] pathDisplayed; // Массив для отслеживания отображения подсказывающего пути
        private static bool isPathShown = false; // Флаг для отслеживания состояния подсказки
        static void Main()
        {
            pathDisplayed = new bool[21, 37]; // Размеры лабиринта: 21 строка, 37 столбцов

            Console.WriteLine("Генерация лабиринта...");
            Console.WriteLine("Нажмите любую клавишу для начала игры");
            Console.ReadKey();

            Console.Clear(); // Очистка консоли

            while (true)
            {
                DrawMaze(); // Отрисовка лабиринта
                var key = Console.ReadKey(true).Key; // Нажатая клавиша

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        MovePlayer(0, -1); // Веерх
                        break;
                    case ConsoleKey.DownArrow:
                        MovePlayer(0, 1); // Вниз
                        break;
                    case ConsoleKey.LeftArrow:
                        MovePlayer(-1, 0); // Влево
                        break;
                    case ConsoleKey.RightArrow:
                        MovePlayer(1, 0); // Вправо
                        break;
                    case ConsoleKey.Spacebar:
                        TogglePath(); // Переключение отображения пути подсказки
                        break;
                }

                // Если игрок дошел до выхода, то игра завершена
                if (playerX == exitX && playerY == exitY)
                {
                    Console.Clear();
                    Console.WriteLine("Победа! Вы вышли из лабиринта!");
                    Console.WriteLine("Нажмите любую клавишу для выхода.");
                    Console.ReadKey();
                    break;
                }

            }
        }

        /// <summary>
        /// Структура для хранения координат точки
        /// </summary>
        struct Point
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        /// <summary>
        /// Метод для отрисовки лабиринта
        /// </summary>
        static void DrawMaze()
        {
            Console.SetCursorPosition(0, 0);

            for (int y = 0; y < 21; y++) // По строкам
            {
                for (int x = 0; x < 37; x++) // По столбцам
                {
                    if (x == playerX && y == playerY)
                    {
                        Console.Write('P'); // Игрок
                    }
                    else if (x == exitX && y == exitY)
                    {
                        Console.Write('E'); // Выход
                    }
                    else if (maze[y, x] == 1)
                    {
                        Console.Write('█'); // Стена
                    }
                    else if (pathDisplayed[y, x])
                    {
                        Console.Write('.'); // Подсказка пути к выходу
                    }
                    else
                    {
                        Console.Write(' '); // Проход
                    }
                }
                Console.WriteLine();
            }

            // Информационная строка
            Console.WriteLine();
            Console.WriteLine($"Используйте клавиши со стрелками для передвижения.");
            Console.WriteLine($"Нажмите пробел, чтобы получить подсказку!");

        }

        /// <summary>
        /// Метод для передвижения игрока
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        static void MovePlayer(int dx, int dy)
        {
            // Новые координаты игрока
            int newX = playerX + dx;
            int newY = playerY + dy;

            if (newX >= 0 && newX < 37 && // Находятся в пределах лабиринта
                newY >= 0 && newY < 21 &&
                maze[newY, newX] == 0) // Являются проходом
            {
                // Обновление позиции игрока
                playerX = newX;
                playerY = newY;

                // Удаление точки пути, если она была на этой позиции
                if (pathDisplayed[playerY, playerX])
                {
                    pathDisplayed[playerY, playerX] = false;
                }
            }
        }

        /// <summary>
        ///  Метод для поиска пути к выходу от текущего местоположения игрока (Алгоритм поиска в ширину)
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <returns></returns>
        static List<Point> FindPath(int startX, int startY, int endX, int endY)
        {
            var queue = new Queue<Point>(); // Очередь из точек, которые нужно обработать
            var visited = new bool[21, 37]; // Массик посещенных точек
            var parent = new Point?[21, 37]; // Массив, в котором для каждый точки записано, из какой точки в нее пришли

            queue.Enqueue(new Point(startX, startY)); // Добавляем стартовую точку в очередь
            visited[startY, startX] = true; // Отмечаем ее, как посещенную

            // Направление движения
            int[] dx = { 0, 0, -1, 1 };
            int[] dy = { -1, 1, 0, 0 };

            while (queue.Count > 0) // Пока в очереди есть точки для проверки
            {
                var current = queue.Dequeue(); // Точка, которая извлекается из очереди

                if (current.X == endX && current.Y == endY) // Если текущая точка - это координаты выхода
                {
                    // Восстанавливаем путь
                    var path = new List<Point>();
                    var point = current;

                    while (point.X != startX || point.Y != startY) // Пока текущая точка не стала начальной точкой
                    {
                        path.Add(point);
                        if (!parent[point.Y, point.X].HasValue)
                            break;
                        point = parent[point.Y, point.X].Value;
                    }

                    path.Reverse(); // Переворачиваем весь путь
                    return path; // Возвращаем путь от местоположения игрока до выхода
                }

                for (int i = 0; i < 4; i++) // Цикл по четырем направлениям
                {
                    // Определение координат соседних точек
                    int newX = current.X + dx[i];
                    int newY = current.Y + dy[i];

                    if (newX >= 0 && newX < 37 && // В пределах лабиринта
                        newY >= 0 && newY < 21 &&
                        !visited[newY, newX] && // Не посещена
                        maze[newY, newX] == 0) // Проход (не стена)
                    {
                        visited[newY, newX] = true; // Отмечаем точку, как посещенную
                        parent[newY, newX] = current; // Запоминаем, точку из которой пришли
                        queue.Enqueue(new Point(newX, newY)); // Добавляем точку в очередь
                    }
                }
            }

            return null; // Путь не найден
        }

        /// <summary>
        /// Метод для отображения пути подсказки
        /// </summary>
        static void ShowPath()
        {
            // Находим путь с помощью алгоритма поиска в ширину
            var currentPath = FindPath(playerX, playerY, exitX, exitY);

            // Отображаем путь точками
            if (currentPath != null)
            {
                foreach (var point in currentPath)
                {
                    if (!(point.X == playerX && point.Y == playerY) &&
                        !(point.X == exitX && point.Y == exitY))
                    {
                        pathDisplayed[point.Y, point.X] = true;
                    }
                }
            }
        }

        /// <summary>
        /// Метод для очистки пути подскзки
        /// </summary>
        static void ClearPath()
        {
            for (int y = 0; y < 21; y++)
            {
                for (int x = 0; x < 37; x++)
                {
                    pathDisplayed[y, x] = false; // Очищаем весь массив
                }
            }
        }

        /// <summary>
        /// Метод для переключение пути подсказки
        /// </summary>
        static void TogglePath()
        {
            if (isPathShown)
            {
                // Если путь показан, скрываем его
                ClearPath();
                isPathShown = false;
            }
            else
            {
                // Если путь скрыт, показываем его
                ShowPath();
                isPathShown = true;
            }
        }

    }
}

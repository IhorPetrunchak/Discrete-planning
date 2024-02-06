using System;

class BranchAndBound
{
    // Ваша функція, яку потрібно мінімізувати
    static int ObjectiveFunction(int[] values)
    {
        // Приклад: сума елементів масиву
        int sum = 0;
        foreach (int value in values)
        {
            sum += value;
        }
        return sum;
    }

    // Перевірка, чи може вирішення задачі бути кращим за поточний найкращий результат
    static bool CanBeBetter(int[] currentValues, int currentValue, int[] bestValues, int bestValue)
    {
        // Якщо є поточний результат менший за найкращий, тоді можливо знайшли кращий
        return currentValue < bestValue;
    }

    // Метод гілок і меж
    static void BranchAndBound(int[] values, int[] currentValues, int currentIndex, ref int bestValue, int[] bestValues)
    {
        // Перевірка, чи варто розглядати гілку
        if (currentIndex == values.Length)
        {
            // Перевірка, чи можливо знайшли краще рішення
            int currentValue = ObjectiveFunction(currentValues);
            if (CanBeBetter(currentValues, currentValue, bestValues, bestValue))
            {
                // Зберігаємо новий найкращий результат та його значення
                Array.Copy(currentValues, bestValues, currentValues.Length);
                bestValue = currentValue;
            }
        }
        else
        {
            // Вибір гілки: включити чи не включити елемент у поточну комбінацію
            currentValues[currentIndex] = 0;
            BranchAndBound(values, currentValues, currentIndex + 1, ref bestValue, bestValues);

            currentValues[currentIndex] = values[currentIndex];
            BranchAndBound(values, currentValues, currentIndex + 1, ref bestValue, bestValues);
        }
    }

    static void Main(string[] args)
    {
        // Приклад даних
        int[] values = { 3, 5, 2, 7 };
        int[] currentValues = new int[values.Length];
        int[] bestValues = new int[values.Length];
        int bestValue = int.MaxValue;

        // Запуск методу гілок і меж
        BranchAndBound(values, currentValues, 0, ref bestValue, bestValues);

        // Вивід результату
        Console.WriteLine("Best value: " + bestValue);
        Console.Write("Best combination: ");
        foreach (int value in bestValues)
        {
            Console.Write(value + " ");
        }
        Console.WriteLine();
    }
}
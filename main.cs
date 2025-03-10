using System;

// класс сам для работы с дробями
// сделал везде коменты что бы быстрее было читать на паре
class RationalNumber
{
    private int _numerator;   // числитель
    private int _denominator; // знаменатель

    // конструктор (проверяет знаменатель на 0)
    public RationalNumber(int numerator, int denominator)
    {
        if (denominator == 0)
            throw new ArgumentException("знаменатель не может быть равен 0");

        _numerator = numerator;
        _denominator = denominator;
        Simplify(); // упрощаем дробь при создании
    }

    // свойство для числителя
    public int Numerator
    {
        get => _numerator;
        private set => _numerator = value;
    }

    // свойство для знаменателя с проверкой на 0 которая была в задании
    public int Denominator
    {
        get => _denominator;
        private set
        {
            if (value == 0)
                throw new ArgumentException("знаменатель не может быть равен 0");
            _denominator = value;
        }
    }

    // свойство для получения десятичной дроби (только чтение)
    public double DecimalValue => (double)Numerator / Denominator;

    // сложение 
    public RationalNumber Add(RationalNumber other)
    {
        // числитель типо (a*d + b*c)
        int newNumerator = Numerator * other.Denominator + other.Numerator * Denominator;

        // знаменатель типо b*d
        int newDenominator = Denominator * other.Denominator;

        // и создаем новую дробь она автоматически упростится
        return new RationalNumber(newNumerator, newDenominator);
    }

    // вычитание 
    public RationalNumber Subtract(RationalNumber other)
    {
        int newNumerator = Numerator * other.Denominator - other.Numerator * Denominator;
        int newDenominator = Denominator * other.Denominator;
        return new RationalNumber(newNumerator, newDenominator);
    }

    // умножение 
    public RationalNumber Multiply(RationalNumber other)
    {
        int newNumerator = Numerator * other.Numerator;
        int newDenominator = Denominator * other.Denominator;
        return new RationalNumber(newNumerator, newDenominator);
    }

    // деление дробей
    public RationalNumber Divide(RationalNumber other)
    {
        int newNumerator = Numerator * other.Denominator;
        int newDenominator = Denominator * other.Numerator;
        return new RationalNumber(newNumerator, newDenominator);
    }

    // упрощение дроби (наибольшего общего делимого)
    private void Simplify()
    {
        // находим НОД для числителя и знаменателя. 
        int gcd = GCD(Math.Abs(Numerator), Math.Abs(Denominator));

        // делим оба числа на НОД
        Numerator /= gcd;   // например: 12 / 4 = 3
        Denominator /= gcd; // например: 8 / 4 = 2
    }

    // Нахождение НОД (алгоритм Евклида)
    private int GCD(int a, int b)
    {
        while (b != 0) // пока второе число не станет нулем
        {
            int temp = b; // запоминаем второе число
            b = a % b;    // заменяем его на остаток от деления a на b
            a = temp;     // теперь a = старое b
        }
        return a; // ну и когда b = 0, возвращаем a (это и есть НОД)
    }

    // переопределение все в ToString для красивого вывода
    public override string ToString() => $"{Numerator}/{Denominator}";
}

// кастомное исключение (задание 6*)
class NotCorrectlyDenominatorException : Exception
{
    public NotCorrectlyDenominatorException(string message) : base(message) { }
}

class Program
{
    static void Main()
    {
        try
        {
            
            RationalNumber a = new RationalNumber(3, 4);
            RationalNumber b = new RationalNumber(1, 2);

            Console.WriteLine($"a = {a}, десятичное: {a.DecimalValue}");
            Console.WriteLine($"b = {b}, десятичное: {b.DecimalValue}\n");

            Console.WriteLine($"a + b = {a.Add(b)}");
            Console.WriteLine($"a - b = {a.Subtract(b)}");
            Console.WriteLine($"a * b = {a.Multiply(b)}");
            Console.WriteLine($"a / b = {a.Divide(b)}");

            // тут исключение. Анкоментите его.
            // RationalNumber error = new RationalNumber(1, 0);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"ошибка: {ex.Message}");
        }
    }
}

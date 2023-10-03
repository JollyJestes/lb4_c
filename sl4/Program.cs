namespace Fractions
{
    class Fraction
    {
        public int Numerator { get; set; }
        public int Denominator { get; set; }

        public Fraction(int numerator, int denominator)
        {
            this.Numerator = numerator;
            this.Denominator = denominator;
        }

        public static Fraction Add(Fraction a, Fraction b)
        {
            if (a.Denominator == 0 || b.Denominator == 0)
            {
                throw new DivideByZeroException("Деление на ноль");
            }

            return new Fraction(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator * b.Denominator);
        }

        public static Fraction Subtract(Fraction a, Fraction b)
        {
            if (a.Denominator == 0 || b.Denominator == 0)
            {
                throw new DivideByZeroException("Деление на ноль");
            }

            return new Fraction(a.Numerator * b.Denominator - b.Numerator * a.Denominator, a.Denominator * b.Denominator);
        }

        public static Fraction Multiply(Fraction a, Fraction b)
        {
            if (a.Denominator == 0 || b.Denominator == 0)
            {
                throw new DivideByZeroException("Деление на ноль");
            }

            return new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
        }

        public static Fraction Divide(Fraction a, Fraction b)
        {
            if (b.Denominator == 0)
            {
                throw new DivideByZeroException("Деление на ноль");
            }

            return new Fraction(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
        }

        public static bool Equals(Fraction a, Fraction b)
        {
            if (a.Denominator != b.Denominator)
            {
                return false;
            }

            return a.Numerator * b.Denominator == b.Numerator * a.Denominator;
        }

        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }

        public string ToDecimal()
        {
            return $"{Numerator / Denominator:F5}";
        }

        public static bool operator >(Fraction a, Fraction b)
        {
            return a.Numerator * b.Denominator > b.Numerator * a.Denominator;
        }

        public static bool operator <(Fraction a, Fraction b)
        {
            return a.Numerator * b.Denominator < b.Numerator * a.Denominator;
        }

        public static bool operator ==(Fraction a, Fraction b)
        {
            return Fraction.Equals(a, b);
        }

        public static bool operator !=(Fraction a, Fraction b)
        {
            return !(a == b);
        }

        public static Fraction CalculateExpression(Fraction a, Fraction b, Fraction c, Fraction d)
        {
            // ((a4/b4)+(a2/b2))
            Fraction sum1 = Fraction.Add(Fraction.Divide(a, b), Fraction.Divide(c, d));

            // ((a1/b1) - (a3/b3))
            Fraction diff = Fraction.Subtract(Fraction.Divide(a, b), Fraction.Divide(c, d));

            if (diff.Numerator == 0)
            {
                throw new DivideByZeroException("Деление на ноль в выражении");
            }

            // ((a4/b4)+(a2/b2)) / ((a1/b1) - (a3/b3))
            return Fraction.Divide(sum1, diff);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Fraction a = new Fraction(23, 13);
            Fraction b = new Fraction(13, 3);
            Fraction c = new Fraction(5, 7);
            Fraction d = new Fraction(2, 5);

            
            Fraction sum = Fraction.Add(a, b);
            Console.WriteLine("Сложение дробей");
            Console.WriteLine("a + b = " + sum);

            
            Fraction difference = Fraction.Subtract(a, b);
            Console.WriteLine("Вычитание дробей");
            Console.WriteLine("a - b = " + difference);

            
            Fraction product = Fraction.Multiply(a, b);
            Console.WriteLine("Умножение дробей");
            Console.WriteLine("a * b = " + product);

            
            Fraction quotient = Fraction.Divide(a, b);
            Console.WriteLine("Деление дробей");
            Console.WriteLine("a / b = " + quotient);

            
            Console.WriteLine("a = " + a);
            Console.WriteLine("a в виде десятичной дроби = " + a.ToDecimal());
            Console.WriteLine("b = " + b);
            Console.WriteLine("b в виде десятичной дроби = " + b.ToDecimal());
            Console.WriteLine("\n\n===================================");
            try
            {
                
                Fraction result = Fraction.CalculateExpression(a, b, c, d);
                Console.WriteLine("((a4/b4)+(a2/b2)) / ((a1/b1)-(a3/b3)) = " + result);

                
                Fraction additionalResult = Fraction.Multiply(Fraction.Add(Fraction.Divide(a, b), Fraction.Divide(c, d)),
                                                              Fraction.Subtract(Fraction.Divide(c, d), Fraction.Divide(a, b)));
                Console.WriteLine("((a1/b1)+(a2/b2)) * ((a3/b3) - (a1/b1)) = " + additionalResult);
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }

            Fraction result1 = Fraction.CalculateExpression(a, b, c, d); 
            Fraction result2 = Fraction.Multiply(Fraction.Add(Fraction.Divide(a, b), Fraction.Divide(c, d)),
                                                Fraction.Subtract(Fraction.Divide(c, d), Fraction.Divide(a, b))); 

          
            if (result1 > result2)
            {
                Console.WriteLine("Первая дробь больше второй дроби.");
            }
            else if (result1 < result2)
            {
                Console.WriteLine("Первая дробь меньше второй дроби.");
            }
            else
            {
                Console.WriteLine("Дроби равны.");
            }
        }
    }
}
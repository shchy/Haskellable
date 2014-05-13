using Haskellable.Code.Monads.HeadTailList;
using Haskellable.Code.Monads.Either;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Haskellable.SandBox
{
    class Program
    {
        static void Main(string[] args)
        {
            var ano = new
            {
                suji = 9,
                moji = 'q'
            };

            var a1 =
                from a in ano.ToMaybe()
                where a.suji < 10
                select a;

            var a2 =
                ano.suji
                .ToCaseOf()
                .Match((int x) => x)
                .Match((string x) => 0)
                .Return(_ => int.MaxValue);

            var a3 =
                ano
                .ToGuards()
                .Where(a => new { a.suji, a.moji, other = a.suji + a.moji })
                .When(
                    a => char.IsWhiteSpace(a.moji)
                    , a => '*')
                .When(
                    a => a.suji == 0
                    , a => '8')
                .Return(a => (char)a.other);

            var a4 =
                ano
                .ToLeft()
                .ToEither<int>()
                .OnLeft(a => Console.WriteLine(a))
                .OnRight(right => Console.WriteLine(right));

            var a5 =
                ano.suji
                .ToLeft().ToEither<double>()
                .ToCaseOf()
                .Match((IRight<double> right) => right.ToString())
                .Match((ILeft<int> left) => left.ToString())
                .Return(_ => _.ToString());

            var a6 =
                Fn.New(() => { throw new Exception(); return 1; })
                .ToExceptional()
                .OnRight(Console.WriteLine)
                .OnLeft(ex =>
                    ex.ToCaseOf()
                    .Match((ArgumentException e) => "arg")
                    .Match((SystemException e) => "sys")
                    .Return("err"));

            var a7 =
                Fn.New(() => { throw new ArgumentException(); return 1; })
                .ToExceptional()
                .ToCaseOf((ArgumentException e) => "arg")
                .Match((SystemException e) => "sys")
                .Match((int right) => "right")
                .Return("err");

            var a8 =
                from ei in ano.suji.ToLeft().ToEither<string>()
                select ei + "unko";

            var a9 =
                from ei in ano.moji.ToRight().ToEither<object>()
                select ei + "unko";


            var a10 =
                from m in 1.ToMaybe()
                where false
                select m;

            var a11 =
                from m in 2.ToMaybe()
                where false
                select m;

            var a12 =
                a10.Concat(a11).FirstOrDefault();




            var obj = new MyClass{ Int = 9, Moji = "unko" };

            var intMaybe =
                from m in 9.ToMaybe()
                from m2 in 8.ToMaybe()
                select m + m2;

            var query =
                from a in obj.ToMaybe()
                where a.Int > 0
                select a;

            var query2 =
                query.Select(x => x.Int);

            var query3 =
                from i in intMaybe
                let a = i * 2
                from q1 in query
                from q2 in query2
                select i + q1.Int + q2;

            query
                .On(Console.WriteLine)
                .Or(() => Console.WriteLine("none"));
            Console.WriteLine(query3);


            var query4 =
                10.ToGuards()
                .Where(a=> new{ moji = a.ToString(), suji = a })
                .When(a =>  a.suji < 5, _ => "unko")
                .When(a => a.suji < 6, a => (a.suji * 2).ToString())
                .Return(_ => "otherwise");

            var query5 =
                "".ToCaseOf()
                .Match((MyClass a) => a.Int)
                .Match((int a) => a)
                .Return(_ => 99);

            var array = new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            var aaa = array.ToHeadTailList()
                        .Recursion(
                            0
                            , x => x
                            , (a, b) => a + b);


            var scanResult =
                array.Scan(0, (acc, value) => acc + value, acc => acc);

            var query6 =
                Fn.New((int a) => a * 2)
                .ToJoinable()
                .Next(a => a.ToString())
                .Next(int.Parse)
                .Return(5);

            var either = GetLeft();

            either
                .OnLeft(Console.WriteLine)
                .OnRight(Console.WriteLine);

            either = GetRight();

            either
                .OnLeft(Console.WriteLine)
                .OnRight(Console.WriteLine);

            var query7 =
                either
                .ToCaseOf()
                .Match((ILeft<int> l) => Fn.New(() => Console.WriteLine(l)))
                .Match((IRight<string> r) => Fn.New(() => Console.WriteLine(r)))
                .Return(() => Console.WriteLine("error"));
            query7();

            Console.WriteLine(query4);

            Console.ReadLine();
        }

        private static IEither<int, string> GetRight()
        {
            return "right".ToRight<int,string>();
        }

        private static IEither<int, string> GetLeft()
        {
            var a = 9.ToLeft();
            return a.ToEither<string>();
        }
    }

    class MyClass
    {
        public int Int { get; set; }
        public string Moji { get; set; }
    }
}

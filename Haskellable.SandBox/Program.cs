using Haskellable.Code.HeadTailList;
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
                .Return(() => 99);

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
            return 9.ToLeft<int, string>();
        }

    }

    class MyClass
    {
        public int Int { get; set; }
        public string Moji { get; set; }
    }


}

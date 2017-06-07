using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeWars
{
    using System;
    using System.Collections.Generic;

    //https://www.codewars.com/kata/functional-streams/train/csharp
    /*
        A Stream is an infinite sequence of items. It is defined recursively
        as a head item followed by the tail, which is another stream.
        Consequently, the tail has to be wrapped with Lazy to prevent
        evaluation.
    */
    public class Stream<T>
    {
        public readonly T Head;
        public readonly Lazy<Stream<T>> Tail;

        public Stream(T head, Lazy<Stream<T>> tail)
        {
            Head = head;
            Tail = tail;
        }
    }

    public static class Stream
    {
        /*
            Your first task is to define a utility function which constructs a
            Stream given a head and a function returning a tail.
        */

        public static Stream<T> Cons<T>(T h, Func<Stream<T>> t)
        {
            return new Stream<T>(h, new Lazy<Stream<T>>(t));
        }

        // .------------------------------.
        // | Static constructor functions |
        // '------------------------------'

            // Construct a stream by repeating a value.
        public static Stream<T> Repeat<T>(T x)
        {
            return Cons(x, () => Repeat(x) );
        }

        // Construct a stream by repeatedly applying a function.
        public static Stream<T> Iterate<T>(Func<T, T> f, T x)
        {
            return Cons(x, () => Iterate(f, f.Invoke(x)));
        }

        // Construct a stream by repeating an enumeration forever.
        public static Stream<T> Cycle<T>(IEnumerable<T> a)
        {
            var q = new Queue<T>(a);
            var t = q.Dequeue();
            q.Enqueue(t);
            return Cons(t, () => Cycle(q));
        }

        // Construct a stream by counting numbers starting from a given one.
        public static Stream<int> From(int x)
        {
            return FromThen(x, 1);
        }

        // Same as From but count with a given step width.
        public static Stream<int> FromThen(int x, int d)
        {
            return Iterate(a => a + d, x);
        }

        // .------------------------------------------.
        // | Stream reduction and modification (pure) |
        // '------------------------------------------'

        /*
            Being applied to a stream (x1, x2, x3, ...), Foldr shall return
            f(x1, f(x2, f(x3, ...))). Foldr is a right-associative fold.
            Thus applications of f are nested to the right.
        */
        public static U Foldr<T, U>(this Stream<T> s, Func<T, Func<U>, U> f)
        {
            return f.Invoke(s.Head, () => s.Tail.Value.Foldr(f));
        }

        // Filter stream with a predicate function.
        public static Stream<T> Filter<T>(this Stream<T> s, Predicate<T> p)
        {
            if (p.Invoke(s.Head)) return Cons(s.Head, () => s.Tail.Value.Filter(p));
            return s.Tail.Value.Filter(p);
        }

        // Returns a given amount of elements from the stream.
        public static IEnumerable<T> Take<T>(this Stream<T> s, int n)
        {
            if (n < 0 ) return new List<T>();
            var list = new List<T>(n);
            var str = s;
            for (var i = 0; i < n; i++)
            {
                list.Add(s.Head);
                str = str.Tail.Value;
            }
            return list;
        }

        // Drop a given amount of elements from the stream.
        public static Stream<T> Drop<T>(this Stream<T> s, int n)
        {
            var str = s;
            for (var i = 0; i < n; i++)
            {
                str = str.Tail.Value;
            }
            return str;
        }

        // Combine 2 streams with a function.
        public static Stream<R> ZipWith<T, U, R>(this Stream<T> s, Func<T, U, R> f, Stream<U> other)
        {
            return Cons(f.Invoke(s.Head, other.Head), () => s.Tail.Value.ZipWith<T, U, R>(f, other.Tail.Value));
        }

        // Map every value of the stream with a function, returning a new stream.
        public static Stream<U> FMap<T, U>(this Stream<T> s, Func<T, U> f)
        {

            return Cons(f.Invoke(s.Head), () => FMap(s.Tail.Value, f));
        }

        // Return the stream of all fibonacci numbers.
        public static Stream<int> Fib()
        {
            var phi = (Math.Sqrt(5) + 1) / 2;
            return From(0).FMap(i => (int)Math.Round( Math.Pow(phi, i) / Math.Sqrt(5) - Math.Pow(- phi + 1, i) / Math.Sqrt(5)));
        }

        // Return the stream of all prime numbers.
        public static Stream<int> Primes()
        {
            return From(2).Filter(IsPrime); 
        }

        private static bool IsPrime(int n)
        {
            int k = 2;
            while (k * k <= n) {
                if (n % k == 0) return false;
                k++;
            }
            return true;
        }

        [Fact]
        public static void Repeat()
        {
            var v = new Random().Next();
            var s = Stream.Repeat(v);
            for (var i = 0; i < 100; i++)
            {
                Assert.Equal(v, s.Head);
                s = s.Tail.Value;
            }
        }

        [Fact]
        public static void Iterate()
        {
            long multiplier = new Random().Next(9);
            Func<long, long> multiply = x => x * multiplier;
            long multiplied = multiplier;
            var expStream = Stream.Iterate(multiply, multiplied);

            Func<string, string> concatenate = x => x + " ";
            var concatenated = "";
            var addWSStream = Stream.Iterate(concatenate, concatenated);

            for (var i = 0; i < 10; i++)
            {
                Assert.Equal(multiplied, expStream.Head);
                expStream = expStream.Tail.Value;
                multiplied = multiply(multiplied);

                Assert.Equal(concatenated, addWSStream.Head);
                addWSStream = addWSStream.Tail.Value;
                concatenated = concatenate(concatenated);
            }
        }

        [Fact]
        public static void Cycle()
        {
            var r = new Random();
            var a = Enumerable.Range(0, 20).Select(i => r.Next()).ToArray();
            var s = Stream.Cycle(a);
            for (var i = 0; i < 100; i++)
            {
                Assert.Equal(a[i % a.Length], s.Head);
                s = s.Tail.Value;
            }
        }

        [Fact]
        public static void From()
        {
            var v = new Random().Next();
            var s = Stream.From(v);
            for (var i = v; i < v + 100; i++)
            {
                Assert.Equal(i, s.Head);
                s = s.Tail.Value;
            }
        }

        [Fact]
        public static void FromThen()
        {
            var r = new Random();
            var v = r.Next();
            var d = r.Next(200);
            var s = Stream.FromThen(v, d);
            for (var i = v; i < v + 100 * d; i += d)
            {
                Assert.Equal(i, s.Head);
                s = s.Tail.Value;
            }
        }

        [Fact]
        public static void Foldr()
        {
            var v = new Random().Next();
            var a = Stream.Repeat(v).Foldr<int, Stream<int>>((x, r) => Stream.Cons(x + 1, () => r())).Take(10).ToArray();
            for (var i = 0; i < 10; i++)
            {
                Assert.Equal(a[i], v + 1);
            }
        }

        [Fact]
        public static void Filter()
        {
            var v = new Random().Next();
            var a = Stream.From(v).Filter(x => x % 2 == 0).Take(10).ToArray();
            foreach (var i in a)
            {
                Assert.Equal(0, i % 2);
            }
        }

        [Fact]
        public static void Take()
        {
            var s = Stream.From(0);
            for (var i = -2; i <= 2; i++)
            {
                Assert.Equal(Math.Max(0, i), s.Take(i).Count());
            }
        }

        [Fact]
        public static void Drop()
        {
            var s = Stream.From(0);
            for (var i = -2; i <= 2; i++)
            {
                Assert.Equal(Math.Max(0, i), s.Drop(i).Head);
            }
        }

        [Fact]
        public static void ZipWith()
        {
            var s = Stream.From(0).ZipWith((x, y) => x * 2 + y, Stream.Repeat(42));
            var t = Stream.FromThen(42, 2);

            for (var i = 0; i < 20; i++)
            {
                Assert.Equal(t.Head, s.Head);
                s = s.Tail.Value;
                t = t.Tail.Value;
            }
        }

        [Fact]
        public static void FMap()
        {
            var s = Stream.FromThen(42, 2).FMap(x => (x - 42) / 2);
            var t = Stream.From(0);

            for (var i = 0; i < 20; i++)
            {
                Assert.Equal(t.Head, s.Head);
                s = s.Tail.Value;
                t = t.Tail.Value;
            }
        }
    }
}

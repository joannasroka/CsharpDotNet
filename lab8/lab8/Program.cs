using System;
using System.Collections;
using System.Collections.Generic;

namespace lab8
{
    class ListOfArrayList<T> : IEnumerable<T>, IList<T>
    {
        private int size;
        private List<ArrayList> result = new List<ArrayList>();
        public ListOfArrayList(int size = 4)
        {
            this.size = size <= 0 ? 4 : size;
        }

        public T this[int index]
        {
            get
            {
                if (index >= Count || index < 0) throw new ArgumentOutOfRangeException();
                int arrayIndex = index / size;
                int elemIndex = index % size;
                return (T)result[arrayIndex][elemIndex];
            }
            set
            {
                if (index >= Count || index < 0) throw new ArgumentOutOfRangeException();
                int arrayIndex = index / size;
                int elemIndex = index % size;
                result[arrayIndex][elemIndex] = value;
            }
        }

        public int Count
        {
            get;
            private set;
        } = 0;

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(T item)
        {
            if (result.Count == 0)
            {
                result.Add(new ArrayList(size));
                result[0].Add(item);
            }
            else
            {
                if (result[result.Count - 1].Count == size)
                {
                    result.Add(new ArrayList(size));

                }
                result[result.Count - 1].Add(item);
            }
            Count++;
        }
        public bool Remove(T item)
        {
            if (result.Count == 0) return false;
            bool removed = false;

            foreach (ArrayList array in result)
            {
                for (int i = 0; i < array.Count; i++)
                {
                    if (array[i].Equals(item) && !removed)
                    {
                        array.Remove(array[i]);
                        removed = true;
                    }
                }
            }
            FixAfterRemove();
            Count--;
            return removed;
        }

        public void RemoveAt(int index)
        {
            if (index >= Count || index < 0) throw new ArgumentOutOfRangeException();
            int arrayIndex = index / size;
            int elemIndex = index % size;
            result[arrayIndex].RemoveAt(elemIndex);
            FixAfterRemove();
            Count--;
        }

        private void FixAfterRemove()
        {
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].Count < size)
                {
                    if (i + 1 < result.Count && result[i + 1].Count != 0)
                    {
                        result[i].Add(result[i + 1][0]);
                        result[i + 1].RemoveAt(0);
                    }
                }
            }
        }

        public void Show()
        {
            foreach (ArrayList array in result)
            {
                foreach (T elem in array)
                {
                    Console.Write($"{elem} ");
                }
                if (result.IndexOf(array) != result.Count - 1) Console.Write("---> ");
                else Console.WriteLine();
            }
        }

        public void Clear()
        {
            foreach (ArrayList array in result)
            {
                array.Clear();
            }
            Count = 0;
        }

        public bool Contains(T item)
        {
            return (IndexOf(item) != -1);
        }

        public int IndexOf(T item)
        {
            if (result.Count == 0) return -1;

            int index = 0;

            for (int i = 0; i < result.Count; i++)
            {
                for (int j = 0; j < result[i].Count; j++)
                {
                    if (result[i][j].Equals(item)) return index;
                    index++;
                }
            }
            return -1;
        }

        public void Trim()
        {
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].Count == 0) result.RemoveAt(i);
            }
        }

        public static ListOfArrayList<T> operator +(ListOfArrayList<T> first, IEnumerable<T> second)
        {
            ListOfArrayList<T> addingResult = new ListOfArrayList<T>(first.size);

            foreach (T elem in first)
            {
                addingResult.Add(elem);
            }

            foreach (T elem in second)
            {
                addingResult.Add(elem);
            }

            return addingResult;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public class ListOfArrayListEnumerator : IEnumerator<T>
        {
            private ListOfArrayList<T> list;
            private int curIndex;
            private T currElem;

            public ListOfArrayListEnumerator(ListOfArrayList<T> list)
            {
                this.list = list;
                curIndex = -1;
                currElem = default(T);
            }

            public T Current
            {
                get { return currElem; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public void Dispose()
            {

            }

            public bool MoveNext()
            {
                if (++curIndex >= this.list.Count)
                {
                    return false;
                }
                else
                {
                    currElem = this.list[curIndex];
                }
                return true;
            }

            public void Reset()
            {
                curIndex = -1;
            }
        }


        public IEnumerator<T> GetEnumerator()
        {
            return new ListOfArrayListEnumerator(this);
        }



        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class Program
    {
        static void Task1()
        {
            ListOfArrayList<int> list = new ListOfArrayList<int>(2);
            list.Add(0);
            list.Add(1);
            list.Add(2);
            list.Add(1);
            list.Add(4);
            //list.Add("napis");
            Console.WriteLine($"Liczba elementow w liscie: {list.Count}");
            Console.WriteLine("Przed: ");
            list.Show();

            list.RemoveAt(0);
            Console.WriteLine("Po usunieciu zerowego elementu: ");
            list.Show();

            list.Remove(1);
            Console.WriteLine("Po usunieciu pierwszej jedynki: ");
            list.Show();

            list.Trim();
            Console.WriteLine("Po uzyciu Trim(): ");
            list.Show();
            Console.WriteLine($"Liczba elementow w liscie: {list.Count}");

            Console.WriteLine($"Czy zawiera jedynke? {list.Contains(1)}");
            Console.WriteLine($"Czy zawiera zero? {list.Contains(0)}");
            Console.WriteLine($"Indeks jedynki to: {list.IndexOf(1)}");

            Console.WriteLine("Uzycie foreach: ");
            foreach (int elem in list)
            {
                Console.Write($"{elem}, ");
            }
            Console.WriteLine();

            list.Clear();
            Console.WriteLine($"Po uzyciu Clear(): ");
            Console.WriteLine($"Liczba elementow w liscie: {list.Count}");
            list.Show();
        }

        static void Task2()
        {
            ListOfArrayList<String> list1 = new ListOfArrayList<string>(3)
            {
            "Alicja",
            "Mateusz",
            "Jan",
            "Weronika",
            "Aleksandara",
            "Piotr",
            "Pawel"
            };
            list1.Show();

            List<String> list2 = new List<string>()
            {
            "Pierogi",
            "Nalesniki",
            "Racuchy",
            "Pomidorowa"
            };

            ListOfArrayList<String> result = list1 + list2;
            result.Show();
            Console.WriteLine($"Liczba elementow po dodaniu: {result.Count}");
        }

        static void Main(string[] args)
        {
            //Task1();
            Task2();
        }
    }
}
